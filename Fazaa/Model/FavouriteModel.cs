using System;
using System.Collections.Generic;
namespace Fazaa
{
	public class FavouriteModel
	{
		public string responseMessage { get; set; }
		public List<UserFavoriteProduct> user_favorite_product { get; set; }
		public int responseCode { get; set; }
		public class UserFavoriteProduct
		{
			public string Favorite_id { get; set; }
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
