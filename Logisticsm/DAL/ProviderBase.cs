using Logisticsm.DAL.Data;

namespace Logisticsm.DAL
{
	public abstract class ProviderBase
	{
		protected OrderDbContext db = null!;

		public ProviderBase()
		{
			db = new OrderDbContext();
		}
	}
}
