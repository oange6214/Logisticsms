using System.Security.Cryptography;
using System.Text;

namespace Logisticsm.Helper
{
	public class MD5Helper
	{
		public static string GetMD5(string input)
		{
            using var md5 = MD5.Create();
            byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(output).Replace("-", "");
        }
	}
}
