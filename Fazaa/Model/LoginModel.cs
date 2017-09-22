using System;
namespace Fazaa
{
	public class LoginModel
	{
public string responseMessage { get; set; }
public Userdetail userdetail { get; set; }
public int responseCode { get; set; }
	}
public class Userdetail
{
	public string User_Id { get; set; }
	public string First_Name { get; set; }
	public string Last_Name { get; set; }
	public string Email_Address { get; set; }
	public string Date_Of_Birth { get; set; }
	public string City { get; set; }
	public string Phone_Number { get; set; }
	public string Address { get; set; }
	public string ZipCode { get; set; }
	public string email_varify { get; set; }

}


}

