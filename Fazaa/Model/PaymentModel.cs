using System;
using System.Collections.Generic;

namespace Fazaa
{
	public class PaymentModel
	{
		public string user_id { get; set; }
		public string order_address { get; set; }
		public string Payment_By { get; set; }
		public string card_holder_name { get; set; }
		public string card_number { get; set; }
		public string Expiration_date { get; set; }
		public string Security_code { get; set; }
		public string Store_Id { get; set; }
		public string order_store_address { get; set; }
		public string order_store_lat { get; set; }
		public string order_store_long { get; set; }
		public string Product_Id { get; set; }
		public string Product_quantity { get; set; }
		public string Total_Price { get; set; }
		public string delivery_method { get; set; }
		public List<CartModel.UserFavoriteProduct> cartitems { get; set; }
	}
}
