using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Fazaa
{
	public static class WebService
	{
		static HttpClient client = new HttpClient();
		public static UserModel Login(UserModel usermodel)
		{
string deviceType = string.Empty;
			UserModel um = new UserModel();
			Userdetail _userModel = null;
			try
			{
				string url = Constants.BaseUrl + "user_login.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("useremail", usermodel.email);
				j.Add("password", usermodel.password);
				j.Add("gcm_id", StaticDataModel.DeviceToken);
				if (Device.OS == TargetPlatform.iOS)
					deviceType = "ios";
				else
					deviceType = "android";
				j.Add("U_Device", deviceType);  

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						um.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						um.responseMessage = jObj["responseMessage"].ToString();
						if (um.responseCode == 200)
						{
							um.userdetail = jObj["userdetail"].ToObject<UserModel.Userdetail>();

						}

					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static UserModel SignUp(UserModel usermodel)
		{

			UserModel um = new UserModel();
			Userdetail _userModel = null;
			try
			{
				string url = Constants.BaseUrl + "regisration.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_firstname", usermodel.user_firstname);
				j.Add("user_lastname", usermodel.user_lastname);
				j.Add("user_email", usermodel.user_email);
				j.Add("user_password", usermodel.user_password);
				j.Add("user_dob", usermodel.user_dob);
				j.Add("user_city", usermodel.user_city);
				j.Add("user_phonenuber", usermodel.user_phonenuber);
				j.Add("facebook_id", usermodel.facebook_id);
				j.Add("twitter_id", usermodel.twitter_id);
				j.Add("gcm_id", usermodel.gcm_id);
				j.Add("registration_type", usermodel.registration_type);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						um.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						um.responseMessage = jObj["responseMessage"].ToString();
						if (um.responseCode == 200)
						{
							um.userdetail = jObj["userdetail"].ToObject<UserModel.Userdetail>();

						}

					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static StoreModel GetAllStores(int store_type_id)
		{

			StoreModel sm = new StoreModel();
			Userdetail _userModel = null;
			try
			{
				string url = Constants.BaseUrl + "get_all_store.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("store_type_id", store_type_id);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.stordata = jObj["stordata"].ToObject<List<StoreModel.Stordata>>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static CategoryModel GetCategoriesByStoreId(int store_id)
		{

			CategoryModel sm = new CategoryModel();

			try
			{
				string url = Constants.BaseUrl + "get_all_store_category.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("store_id", store_id);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.stor_category_data = jObj["stor_category_data"].ToObject<List<CategoryModel.StorCategoryData>>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static ProductModel GetProductsByCategory(int store_id, int category_id)
		{

			ProductModel sm = new ProductModel();

			try
			{
				string url = Constants.BaseUrl + "get_all_store_category_product.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("store_id", store_id);
				j.Add("category_id", category_id);
				j.Add("user_id", StaticDataModel.userId);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.stor_category_product_data = jObj["stor_category_product_data"].ToObject<List<ProductModel.StorCategoryProductData>>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static ProductDetailsModel GetProductsDetails(int product_id)
		{

			ProductDetailsModel sm = new ProductDetailsModel();

			try
			{
				string url = Constants.BaseUrl + "get_product_detail.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("product_id", product_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.product_detail = jObj["product_detail"].ToObject<ProductDetailsModel.ProductDetail>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static FavouriteModel GetFavourites()
		{

			FavouriteModel sm = new FavouriteModel();

			try
			{
				string url = Constants.BaseUrl + "get_all_favorite_product.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.user_favorite_product = jObj["user_favorite_product"].ToObject<List<FavouriteModel.UserFavoriteProduct>>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static int AddToWishList(int product_id)
		{

			int ret = 0;

			try
			{
				string url = Constants.BaseUrl + "add_wishlist.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_id", StaticDataModel.userId);
				j.Add("product_id", product_id);



				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						ret = Convert.ToInt32(jObj["responseCode"].ToString());


					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return ret;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static int DeleteFromWishList(int favourite_id)
		{

			int ret = 0;

			try
			{
				string url = Constants.BaseUrl + "delete_favorite.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("Favorite_id", favourite_id);



				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						ret = Convert.ToInt32(jObj["responseCode"].ToString());


					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return ret;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static CartModel GetCartItems()
		{

			CartModel sm = new CartModel();

			try
			{
				string url = Constants.BaseUrl + "get_all_cart_product.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.user_favorite_product = jObj["user_favorite_product"].ToObject<List<CartModel.UserFavoriteProduct>>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
public static CartModel UpdateCartItem(int cart_id,int quantity)
{

	CartModel sm = new CartModel();

	try
	{
		string url = Constants.BaseUrl + "update_cart.php";
		HttpResponseMessage response = null;
		JObject j = new JObject();
		j.Add("user_id", StaticDataModel.userId);
				j.Add("cart_id", cart_id);
				j.Add("quantity", quantity); 

		var json = JsonConvert.SerializeObject(j);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		response = client.PostAsync(url, content).Result;
		if (response.IsSuccessStatusCode)
		{

			using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
			{
				var contents = reader.ReadToEnd();
				JObject jObj = JObject.Parse(contents);

				sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
				sm.responseMessage = jObj["responseMessage"].ToString();
				if (sm.responseCode == 200)
				{
					sm.user_favorite_product = jObj["user_cart_product"].ToObject<List<CartModel.UserFavoriteProduct>>();

				}

			}

		}
		return sm;
	}
	catch (Exception ex)
	{
		return null;
		Debug.WriteLine(@"ERROR {0}", ex.Message);
	}
	finally
	{
		StaticMethods.DismissLoader();

	}
		}
		public static CartModel AddToCart(int product_id, string quantity)
		{

			string  ret = string.Empty;
			CartModel cm= new CartModel();

			try
			{
				string url = Constants.BaseUrl + "addto_cart.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_id", StaticDataModel.userId);
				j.Add("product_id", product_id);
				j.Add("quantity", quantity);



				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						cm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						cm.responseMessage = jObj["responseMessage"].ToString();
						if(cm.responseCode==200)
							cm.responseMessage = jObj["quantity"].ToString();

					}

				}
				return cm;
			}
			catch (Exception ex)
			{
				return cm;
				Debug.WriteLine(@"ERROR {0}", ex.Message); 
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static int DeleteFromCart(int cart_id)
		{

			int ret = 0;

			try
			{
				string url = Constants.BaseUrl + "delete_cart.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_id", StaticDataModel.userId);
				j.Add("product_id", cart_id);



				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						ret = Convert.ToInt32(jObj["responseCode"].ToString());


					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return ret;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static CheckoutModel GetCheckOutDetails(int store_id)
		{

			CheckoutModel sm = new CheckoutModel();

			try
			{
				string url = Constants.BaseUrl + "get_checkout_address.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_id", StaticDataModel.userId);
				j.Add("store_id", store_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.storeaddress = jObj["storeaddress"].ToObject<CheckoutModel.Storeaddress>();
							sm.useraddress = jObj["useraddress"].ToObject<CheckoutModel.Useraddress>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
public static int AddOrder(PaymentModel usermodel)
{

			int ret = 0;
	Userdetail _userModel = null; 
	try
	{ 
		string url = Constants.BaseUrl + "add_order.php";  
		HttpResponseMessage response = null; 
		JObject j = new JObject(); 
		j.Add("user_id", StaticDataModel.userId); 
		j.Add("order_address", usermodel.order_address); 
		j.Add("Payment_By", usermodel.Payment_By);
		j.Add("card_holder_name", "");
		j.Add("card_number", 0);
		j.Add("Expiration_date", "");
		j.Add("Security_code", "");
		j.Add("Store_Id", usermodel.Store_Id);
		j.Add("order_store_address", usermodel.order_store_address);
		j.Add("order_store_lat", usermodel.order_store_lat);
		j.Add("order_store_long", usermodel.order_store_long);
		j.Add("Product_Id", usermodel.Product_Id);
		j.Add("Product_quantity", usermodel.Product_quantity);
		j.Add("Total_Price", usermodel.Total_Price);
		j.Add("delivery_method", usermodel.delivery_method);

		var json = JsonConvert.SerializeObject(j);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		response = client.PostAsync(url, content).Result;
		if (response.IsSuccessStatusCode)
		{

			using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
			{
				var contents = reader.ReadToEnd();
				JObject jObj = JObject.Parse(contents);

				ret = Convert.ToInt32(jObj["responseCode"].ToString());
			
			}

		}
		return ret;
	}
	catch (Exception ex)
	{
		return ret;
		Debug.WriteLine(@"ERROR {0}", ex.Message);
	}
	finally
	{
		StaticMethods.DismissLoader();

	}
		}
		public static ProductModel SearchProduct(string product_name)
		{

			ProductModel sm = new ProductModel();

			try
			{
				string url = Constants.BaseUrl + "search_product.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("product_name", product_name); 
			


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						sm.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						sm.responseMessage = jObj["responseMessage"].ToString();
						if (sm.responseCode == 200)
						{
							sm.stor_category_product_data = jObj["all_product"].ToObject<List<ProductModel.StorCategoryProductData>>();

						}

					}

				}
				return sm;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static int UpdateProfile(UserModel usermodel)
		{

			UserModel um = new UserModel();
			int ret = 0;
			Userdetail _userModel = null;
			try
			{
				
				string url = Constants.BaseUrl + "edit_profile.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("user_id", StaticDataModel.userId);
				j.Add("First_Name", usermodel.user_firstname);
				j.Add("Last_Name", usermodel.user_lastname);
				j.Add("Date_Of_Birth", usermodel.user_dob);
				j.Add("City", usermodel.user_city);
				j.Add("Phone_Number", usermodel.user_phonenuber);
				j.Add("ZipCode", usermodel.ZipCode);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						um.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						um.responseMessage = jObj["responseMessage"].ToString();
						if (um.responseCode == 200)
						{

							ret = um.responseCode;
						}

					}

				}

			}
			catch (Exception ex)
			{
				
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		public static UserModel GetProfile()
		{

			UserModel um = new UserModel();
			Userdetail _userModel = null;
			try
			{
				string url = Constants.BaseUrl + "get_profile.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();

				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						um.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						um.responseMessage = jObj["responseMessage"].ToString();
						if (um.responseCode == 200)
						{
							um.userdetail = jObj["user_profiledata"].ToObject<UserModel.Userdetail>();

						}

					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static List<MyOrderModel.Stordata> GetMyAllOrder()
		{

			List<MyOrderModel.Stordata> um = new List<MyOrderModel.Stordata>();

			try
			{
				string url = Constants.BaseUrl + "get_all_user_order.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();

				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						var responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						var responseMessage = jObj["responseMessage"].ToString();
						if (responseCode == 200)
						{
							um = jObj["stordata"].ToObject< List< MyOrderModel.Stordata>>();

						}

					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static string VarifyEmail(string email)
		{

			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl + "verify_email.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();

				j.Add("email", email);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						var responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						var responseMessage = jObj["responseMessage"].ToString();
						ret = responseMessage;
						if (responseCode == 200)
						{
							//Success

						}

					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static CardDetailsModel.UserCarddata GetCardDetails()
		{

			CardDetailsModel.UserCarddata um = new CardDetailsModel.UserCarddata();

			try
			{
				string url = Constants.BaseUrl + "get_carddetail.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();

				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						var responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						var responseMessage = jObj["responseMessage"].ToString();
						if (responseCode == 200)
						{
							um = jObj["user_carddata"].ToObject<CardDetailsModel.UserCarddata>();

						}

					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static string UpdateCardDetails(CardDetailsModel.UserCarddata UserCardData)
		{

			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl + "edit_carddetail.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();

				j.Add("user_id", StaticDataModel.userId);
				j.Add("Card_Holder_Name", UserCardData.Card_Holder_Name);
				j.Add("Card_Number", UserCardData.Card_Number);
				j.Add("Card_Expairy", UserCardData.Card_Expairy);
				j.Add("Security_Code", UserCardData.Security_Code);
				j.Add("Card_Type", UserCardData.Card_Type);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						var responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						var responseMessage = jObj["responseMessage"].ToString();
						if (responseCode == 200)
						{
							ret = "success";

						}

					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static string UpdateAddress(string address)
		{

			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl + "edit_useraddress.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();

				j.Add("user_id", StaticDataModel.userId);
				j.Add("Address", address);
			


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						var responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						var responseMessage = jObj["responseMessage"].ToString();
						if (responseCode == 200)
						{
							ret = "success";

						}

					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static string ForgotPassword(string email)
		{

			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl + "forgotpass_mail.php";
				HttpResponseMessage response = null;
				JObject j = new JObject();

			
				j.Add("email", email);



				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						var responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						var responseMessage = jObj["responseMessage"].ToString();
						if (responseCode == 200)
						{
							ret = "success";

						}

					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
	}
}
