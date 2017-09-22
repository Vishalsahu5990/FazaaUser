using System;
using System.Collections.Generic;

namespace Fazaa
{
	public class MyOrderModel
	{
		public class ProductDetail
		{
			public string Product_Name { get; set; }
			public string Product_description { get; set; }
			public string Product_Image { get; set; }
			public double Product_Price  { get; set; }
			public string quantity { get; set; } 
		}

		public class Stordata
		{
			public string Order_Id { get; set; }
			public string Order_Id_Show { get; set; }
			public string Total_Price { get; set; }
			public string order_staus { get; set; }
			public string Order_DateTime { get; set; }
			public List<ProductDetail> product_detail { get; set;}
			//Extra fields
			public string Product_Name { get; set; }
			public string Product_description { get; set; }
			public string Product_Image { get; set; }
			public string Product_Price { get; set; }
			public string quantity { get; set; }
		}
		public string responseMessage { get; set; }
		public List<Stordata> stordata { get; set; }
		public int responseCode { get; set; }


	}
}
