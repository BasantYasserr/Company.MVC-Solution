using Company.MVC.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.MVC.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail (Email email)
		{
			//Mail Server : smtp.gmail.com
			//Smtp (simple mail transfer protocol)

			var client = new SmtpClient("smtp.gmail.com", 587);

			client.Credentials = new NetworkCredential("basantyaserr@gmail.com", "uiwfgcbchdjqjimf");
			
			client.Send("basantyaserr@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
