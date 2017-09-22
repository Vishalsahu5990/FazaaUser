using System;
namespace Fazaa
{
	public static class StaticDataModel
	{
		public static RootPage currentContext=null;
		public static int userId = 0;
		public static bool IsFromPopup = false;
		public static int CartItemsCount = 0;
        public static string DeviceToken = string.Empty;
	}
}
