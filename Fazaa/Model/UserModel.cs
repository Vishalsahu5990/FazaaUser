using System;
namespace Fazaa
{
	public class UserModel
	{
public Userdetail userdetail { get; set; }
		public string responseMessage { get; set; }
		public int responseCode { get; set; }
		public int user_id { get; set; }
		public string email { get; set; }
		public string name { get; set; }
		public string password { get; set; }
		public string gcm_id { get; set; }
		public string created_date { get; set; }
		public int is_social_signup { get; set; }
        public int registration_type { get; set; } 
public string user_firstname { get; set; }
public string user_lastname { get; set; }
public string user_email { get; set; }
public string user_password { get; set; }
public string user_dob { get; set; }
public string user_city { get; set; }
public string user_phonenuber { get; set; }
public string facebook_id { get; set; }
public string twitter_id { get; set; }
		public string ZipCode { get; set; }
		public string Address { get; set; }


public class Userdetail
{
//Login Model
	public string User_Id { get; set; }
	public string First_Name { get; set; }
	public string Last_Name { get; set; }
	public string Email_Address { get; set; }
	public string Date_Of_Birth { get; set; }
	public string City { get; set; }
	public string Phone_Number { get; set; }
	public string Address { get; set; }
	public string ZipCode { get; set; }

			//signup Model
public string user_id { get; set; }
public string user_firstname { get; set; }
public string user_lastname { get; set; }
public string user_email { get; set; }
public string user_dob { get; set; }
public string user_city { get; set; }
public string user_phone { get; set; }
public string registration_type { get; set; }
public string email_varify { get; set; }

}
	}
}
