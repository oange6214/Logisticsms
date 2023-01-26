using CommunityToolkit.Mvvm.ComponentModel;
using Logisticsm.Repository.Entities;

namespace Logisticsm
{
	public class AppData : ObservableObject
	{

		public static AppData Instance { get; set; } = new Lazy<AppData>().Value;

		/// <summary>
		/// 當前用戶
		/// </summary>
		public Member CurrentUser { get; set; } = null!;



	}
}
