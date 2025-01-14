using calendar.Context;
using calendar.Entites;

namespace calendar.Repository
{
    public class MeetingRepository
    {
        public Meeting AddMeeting(Meeting meeting)
        {
            using (var context = new MeetingContext())
            {
                context.Meetings.Add(meeting);
                context.SaveChanges();
            }
            return meeting;
        }
    }
}
