using System;
using System.Collections.Generic;

namespace Fazaa
{
	public class CategoryModel
	{
		public string responseMessage { get; set; }
		public List<StorCategoryData> stor_category_data { get; set; }
		public int responseCode { get; set; }
		public class StorCategoryData
		{
			public string Category_Id { get; set; }
			public string Store_Id { get; set; }
			public string Store_Type { get; set; }
			public string Category_Name { get; set; }
			public string cover_photo { get; set; }
			public double height { get; set; }

		}
	}
}
