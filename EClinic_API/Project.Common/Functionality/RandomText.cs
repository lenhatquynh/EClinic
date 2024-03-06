using Project.Common.Enum;
using System.Security.Cryptography;
using System.Text;

namespace Project.Common.Functionality
{
    public static class RandomText
    {
        public static string RandomCharacter()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public static string RandomByNumberOfCharacters(int length, RandomType randomType)
        {
            string valid = "";
            switch (randomType)
            {
                case RandomType.Number:
                    valid = "1234567890";
                    break;
                case RandomType.String:
                    valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    break;
            };
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
