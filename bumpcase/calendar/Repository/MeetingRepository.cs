using calendar.Context;
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
                    throw new ArgumentException ($"Slot is not available.");
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

        public List<Meeting> GetMeetings(int veneId)
        {
            using (var context = new MeetingContext())
            {
                return context.Meetings.Where(x => x.VeterinarianId == veneId).ToList();
            }
        }
    }
}
