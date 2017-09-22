using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Fazaa
{
public class CartItemList : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;
	public ObservableCollection<CartModel.UserFavoriteProduct> _items;

		public ObservableCollection<CartModel.UserFavoriteProduct> Items
	{
		get { return _items; }
		set { _items = value; OnPropertyChanged("Items"); }
	}

	protected virtual void OnPropertyChanged(string propertyName)
	{
		if (PropertyChanged == null)
			return;
		PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
	}

public CartItemList(List<CartModel.UserFavoriteProduct> itemList)
	{
		Items = new ObservableCollection<CartModel.UserFavoriteProduct>();
		foreach (CartModel.UserFavoriteProduct itm in itemList)
			{
				Items.Add(itm);
			}
		}
	}
}
