using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Threading.Tasks;
using MvcOnlineShopping.Models.Entity;
namespace MvcOnlineShopping.Models
{
    public class SendMail
    {
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
       public void Google(string GondericiMail, string GondericiPass, string AliciMail)
        {
            TBLMember m = db.TBLMember.FirstOrDefault(x => x.Email == GondericiMail);
            Random rnd = new Random();
            m.Password = rnd.Next(1000, 10000).ToString();
            db.SaveChanges();
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential(GondericiMail, GondericiPass);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(GondericiMail, "Online Alışveriş Sitesi");
            mail.To.Add(AliciMail);
            mail.Subject = "Şifre sıfırlama talebinde bulundunuz.";
            mail.IsBodyHtml = true;
            mail.Body = $@"{DateTime.Now.ToString()} tarihinde şifre sıfırlama talebinde bulundunuz. Yeni Şifreniz: {m.Password}";
            sc.Send(mail);

        }
    }
}