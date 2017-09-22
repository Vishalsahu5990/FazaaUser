using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Fazaa
{
	public class FavouritesItemList : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<FavouriteModel.UserFavoriteProduct> _items;

		public ObservableCollection<FavouriteModel.UserFavoriteProduct> Items
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

		public FavouritesItemList(List<FavouriteModel.UserFavoriteProduct> itemList)
		{
			Items = new ObservableCollection<FavouriteModel.UserFavoriteProduct>();
			foreach (FavouriteModel.UserFavoriteProduct itm in itemList)
			{
				Items.Add(itm);
			}
		}
	}
}
