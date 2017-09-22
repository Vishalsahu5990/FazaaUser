using System;
namespace Fazaa
{
	public class CardDetailsModel
	{
		public string responseMessage { get; set; }
		public UserCarddata user_carddata { get; set; }
		public int responseCode { get; set; }

		public class UserCarddata
		{
			public string Card_Holder_Name { get; set; }
			public string Card_Number { get; set; }
			public string Card_Expairy { get; set; }
			public string Security_Code { get; set; }
			public string Address { get; set; }
			public string Card_Type { get; set; }
		}
	}
}
