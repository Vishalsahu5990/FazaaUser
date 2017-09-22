using System;
namespace Fazaa
{
	public class ProductDetailsModel
	{
		public string responseMessage { get; set; }
		public ProductDetail product_detail { get; set; }
		public int responseCode { get; set; }
		public class ProductDetail
		{
			public string Product_Id { get; set; }
			public string Store_Id { get; set; }
			public string Product_Name { get; set; }
			public string Product_description { get; set; }
			public string Product_Category { get; set; }
			public string Product_Store_Type { get; set; }
			public string Product_Price { get; set; }
			public string Product_Image { get; set; }
		}



	}
}
