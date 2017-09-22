using System;
namespace Fazaa
{
	public class CartViewModel
	{
		public CartViewModel()
		{
			if (Items != null)
			{
				var it = Items.Items;
			}
		}
public CartItemList Items { get; set; }
public string Total_Price { get; set; }
public string Store_Id { get; set; }
public string Amount { get { return "Amount:"+Total_Price+"SR"; } }
public string Summary { get { return " Total " + Items.Items.Count + " items"; }}
	}
}
