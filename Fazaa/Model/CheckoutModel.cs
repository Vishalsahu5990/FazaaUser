using System;
namespace Fazaa
{
	public class CheckoutModel
	{
		public string responseMessage { get; set; }
		public Useraddress useraddress { get; set; }
		public Storeaddress storeaddress { get; set; }
		public int responseCode { get; set; }
		public class Useraddress
		{
			public string user_address { get; set; }
			public string user_city { get; set; }
			public string user_zipcode { get; set; }
		}

		public class Storeaddress
		{
			public string store_address { get; set; }
			public string store_latitude { get; set; }
			public string store_longitude { get; set; }
		}
	}
}
