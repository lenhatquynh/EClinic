using System.Reflection;

namespace Project.Common.Functionality
{
    public static class ConvertObject
    {
        public static List<Dictionary<string, object>> ConvertObjectToString(object obj)
        {
            List<Dictionary<string, object>> keyValues= new List<Dictionary<string, object>>();
            Type objType = obj.GetType();
            FieldInfo[] fields = objType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo field in fields)
            {
                string fieldName = field.Name.Replace("k__BackingField", "").Replace("<", "").Replace(">", "");
                string fieldValue = field.GetValue(obj).ToString();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(fieldName, fieldValue);
                keyValues.Add(keyValue);

            }
            return keyValues;
        }
    }
}
