using System;
using Acr.UserDialogs;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Fazaa
{
	public static class StaticMethods
	{
public static bool IsIpad()
{
	if (Device.Idiom == TargetIdiom.Phone)
		return false;
	else
		return true;
}
		public static string DeviceType()
{
			if (Device.OS == TargetPlatform.Android)
 		return "android";
	else
		return "ios";
}
public static void ShowLoader()
{

	try
	{
		if (Device.OS == TargetPlatform.iOS)
		{
			UserDialogs.Instance.ShowLoading();

		}
		else
		{
					UserDialogs.Instance.ShowLoading();

		}
	}
	catch (Exception ex)
	{

	}
}
public static void DismissLoader()
{

	try
	{
		if (Device.OS == TargetPlatform.iOS)
		{
			UserDialogs.Instance.HideLoading();

		}
		else
		{

					UserDialogs.Instance.HideLoading();
		}
	}
	catch (Exception ex)
	{

	}
}
public static void ShowToast(string msg)
{

	try
	{
		UserDialogs.Instance.Toast(msg);
	}
	catch (Exception ex)
	{

	}	}
public static Userdetail GetLocalSavedData()
{
	Userdetail ud = null;
	try
	{
		var exists = CrossSecureStorage.Current.HasKey("userid");
		if (exists)
		{
			ud = new Userdetail();
			ud.First_Name = CrossSecureStorage.Current.GetValue("first_name").ToString();
			ud.Last_Name = CrossSecureStorage.Current.GetValue("last_name").ToString();
			ud.Email_Address = CrossSecureStorage.Current.GetValue("email").ToString();
			ud.email_varify = CrossSecureStorage.Current.GetValue("email_varify").ToString();

		}
	}
	catch (Exception ex)
	{

	}
	return ud;
		}
	}
}
