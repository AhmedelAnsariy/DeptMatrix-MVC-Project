using MVCRev.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVCRev.PL.Helper
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("ahmedansarei77@gmail.com", "esvkgfeectuwrsof");
			client.Send("ahmedansarei77@gmail.com", email.Receipts, email.Subject, email.Body);
		}



	}
}
 