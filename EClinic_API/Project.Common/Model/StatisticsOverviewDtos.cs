using Project.Common.Enum;

namespace Project.Common.Model
{
    public class StatisticsOverviewDtos
    {
        public int Total { get; set; }
        public StatisticsStatus Status { get; set; }
        public double Percent { get; set; }
    }
}
