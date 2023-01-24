using Logisticsm.DAL.Models;

namespace Logisticsm
{
	public class AppData
	{
		public static AppData Instance { get; set; } = new Lazy<AppData>().Value;
		public Member? CurrentUser { get; set; } = null;
	}
}
