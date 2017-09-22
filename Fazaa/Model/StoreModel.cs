using System;
using System.Collections.Generic;

namespace Fazaa
{
	public class StoreModel
	{
		public class Stordata
		{
			public string store_id { get; set; }
			public string store_type_id { get; set; }
			public string cover_photo { get; set; }
			public string store_logo { get; set; }
			public string store_name { get; set; }
			public double height { get; set; }
		}
		public string responseMessage { get; set; }
		public List<Stordata> stordata { get; set; }
		public int responseCode { get; set; }

	}
}
