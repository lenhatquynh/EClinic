using Project.PaymentService.Model;

namespace Project.PaymentService.Helper
{
    public class DataHelper
    {
        public static bool CheckTimeType(DateTime startTime, DateTime endTime, TimeType timeType)
        {
            bool result = false;
            if (endTime < startTime)
            {
                return result;
            }
            switch (timeType)
            {
                case TimeType.Day:
                    if (startTime.Year != endTime.Year) return false;
                    result = true;
                    break;
                case TimeType.Month:
                    if (startTime.Year != endTime.Year) return false;
                    result = true;
                    break;
                case TimeType.Year:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
    }
}
