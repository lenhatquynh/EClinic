namespace Project.Common.Functionality
{
    public static class TimeCalculation
    {
        public static DateTime GetOldTime(DateTime currentTime)
        {
            int monthOffset = 1;
            int yearOffset = 0;
            if (currentTime.Month <= monthOffset)
            {
                yearOffset = 1;
                monthOffset = 12 - monthOffset;
            }

            return currentTime.AddMonths(-monthOffset).AddYears(-yearOffset);
        }
    }
}
