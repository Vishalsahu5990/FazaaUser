using System;
using System.Collections.Generic;
namespace Fazaa
{
	public class ProductModel
	{
		public string responseMessage { get; set; }
		public List<StorCategoryProductData> stor_category_product_data { get; set; }
		public int responseCode { get; set; }
		public class StorCategoryProductData
		{
			public string Product_Id { get; set; }
			public string Store_Id { get; set; }
			public string Product_Name { get; set; }
			public string Product_description { get; set; }
			public string Product_Category { get; set; }
			public string Product_Store_Type { get; set; }
			public string Product_Price { get; set; }
			public string Product_Image { get; set; }
			public string favorite { get; set; }
			public double height { get; set; }
		}
	}
}
