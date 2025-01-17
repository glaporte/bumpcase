﻿using calendar.Context;
using calendar.Entites;
using calendar.Utilities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using static calendar.Controllers.MeetingController;

namespace calendar.Repository
{
    public class MeetingRepository
    {
        public SlotRepository SlotRepository { get; set; }
        public MeetingRepository(SlotRepository slotRepository)
        {
            SlotRepository = slotRepository;
        }
        public Meeting AddMeeting(Meeting meeting)
        {
            using (var context = new MeetingContext())
            {
                var slot = SlotRepository.GetSlot(meeting.SlotId);
                if (slot == null)
                {
                    throw new ArgumentException("Cannot find slot");
                }
                else if (slot.State != Slot.SlotState.Available)
                {
                    throw new ArgumentException($"Slot is not available.");
                }

                SlotRepository.UpdateSlotState(meeting.SlotId, Slot.SlotState.Booked);

                context.Meetings.Add(meeting);
                context.SaveChanges();
            }
            return meeting;
        }

        public Meeting AddMeetingWithCustomDate(MeetingParameter meetingParameter)
        {
            // clamp sec and min
            meetingParameter.Start = DateUtility.CleanMeetingDate(meetingParameter.Start);
            meetingParameter.End = DateUtility.CleanMeetingDate(meetingParameter.End);

            using (var context = new MeetingContext())
            {
                var initialSlot = SlotRepository.FindSlot(meetingParameter.Start, meetingParameter.Meeting.VeterinarianId);
                if (initialSlot == null)
                {
                    throw new ArgumentException("Cannot find slot");
                }
                if (initialSlot.State != Slot.SlotState.Available)
                {
                    throw new ArgumentException("Slot is not available");
                }

                SlotRepository.SplitSlot(initialSlot!, meetingParameter.Start, meetingParameter.End);
                var targetSlot = SlotRepository.FindSlot(meetingParameter.Start, meetingParameter.Meeting.VeterinarianId);
                if (targetSlot == null)
                {
                    throw new ArgumentException("Something went wront on re-arranging slot");
                }
                SlotRepository.UpdateSlotState(targetSlot.Id, Slot.SlotState.Booked);
                meetingParameter.Meeting.SlotId = targetSlot.Id;
                context.Meetings.Add(meetingParameter.Meeting);
                context.SaveChanges();
            }

            return meetingParameter.Meeting;
        }

        public void CancelMeeting(int meetingId)
        {
            using (var context = new MeetingContext())
            {
                Meeting? meeting = context.Meetings.Where(x => x.Id == meetingId).FirstOrDefault();
                if (meeting == null)
                    throw new ArgumentException("Meeting not found");

                SlotRepository.ResetAndMergeSlot(meeting.SlotId);

                context.Meetings.Remove(meeting);
                context.SaveChanges();
            }
        }

        public Meeting RescheduleMeeting(MeetingUpdateParameter meetingUpdate)
        {
            // clamp sec and min
            meetingUpdate.Start = DateUtility.CleanMeetingDate(meetingUpdate.Start);
            meetingUpdate.End = DateUtility.CleanMeetingDate(meetingUpdate.End);

            using (var context = new MeetingContext())
            {
                Meeting? meeting = context.Meetings.Where(x => x.Id == meetingUpdate.MeetingId).FirstOrDefault();
                if (meeting == null)
                    throw new ArgumentException("Meeting not found");

                var previousSlot = SlotRepository.GetSlot(meeting.SlotId)!;
                var targetSlot = SlotRepository.FindSlot(meetingUpdate.Start, meeting.VeterinarianId);
                if (targetSlot == null)
                {
                    throw new ArgumentException("Cannot find slot");
                }
                if (targetSlot.State != Slot.SlotState.Available)
                {
                    throw new ArgumentException("Slot is not available");
                }

                SlotRepository.ResetAndMergeSlot(meeting.SlotId);

                try
                {
                    SlotRepository.SplitSlot(targetSlot!, meetingUpdate.Start, meetingUpdate.End);
                }
                catch (Exception) // rollback initial slot due to bad rescheduling date
                {
                    var restoredSlot = SlotRepository.AddSlot(new Slot(previousSlot.Start, previousSlot.End, previousSlot.VeterinarianId, Slot.SlotState.Booked));
                    meeting.SlotId = restoredSlot.Id;
                    context.SaveChanges();

                    throw;
                }
                targetSlot = SlotRepository.FindSlot(meetingUpdate.Start, meeting.VeterinarianId);
                if (targetSlot == null)
                {
                    throw new ArgumentException("Something went wront on re-arranging slot");
                }
                SlotRepository.UpdateSlotState(targetSlot.Id, Slot.SlotState.Booked);
                meeting.SlotId = targetSlot.Id;
                context.SaveChanges();

                return meeting;
            }
        }

        public List<Meeting> GetMeetings(int veneId)
        {
            using (var context = new MeetingContext())
            {
                return context.Meetings.Where(x => x.VeterinarianId == veneId).ToList();
            }
        }

        public Meeting? GetMeeting(int meetingId)
        {
            using (var context = new MeetingContext())
            {
                return context.Meetings.Where(x => x.Id == meetingId).FirstOrDefault();
            }
        }
    }
}
