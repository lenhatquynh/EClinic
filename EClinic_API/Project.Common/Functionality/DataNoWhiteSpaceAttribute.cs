using System.ComponentModel.DataAnnotations;

namespace Project.Common.Functionality
{

    [AttributeUsage(AttributeTargets.Property)]
    public class DataNoWhiteSpaceAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is string strValue)
                return !string.IsNullOrWhiteSpace(strValue);
            return true;
        }
    }
}
