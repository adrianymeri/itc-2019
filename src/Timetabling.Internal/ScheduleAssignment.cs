namespace Timetabling.Internal
{
    public class ScheduleAssignment : Schedule
    {
        public ScheduleAssignment(
            uint weeks,
            uint days,
            int start,
            int length,
            int penalty)
            : base(weeks, days, start, length)
        {
            Penalty = penalty;
        }

        public readonly int Penalty;
    }
}
