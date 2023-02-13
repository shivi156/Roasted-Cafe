using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;
using System.Net;



namespace roasted1.Pages.Contact;

public class Contact : PageModel
{
    
    public string isSend { get; set; }

    public void OnPost()
    {
        var name = Request.Form["name"];
        var email = Request.Form["emailaddress"];
        var message = Request.Form["message"];

        try
        {
            SendMail(name, email, message);
            isSend = "send";
        }
        catch (Exception)
        {
            isSend = "failed";
            throw;
        }
        
    }
    public bool SendMail(string name, string email, string message1)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        message.From = new MailAddress(email);
        message.To.Add("2123818@chester.ac.uk");
        message.Subject = "Contact";
        message.IsBodyHtml = true;
        message.Body = "<p>Name: " + name + "</p>" + "<p>Email: " + email + "</p>" + "<p>Message: " + message1 + "</p>";

        smtpClient.Port = 587;
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("roastedchester@gmail.com", "jwfjiledlljtatfc");
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Send(message);
        return true;
    }

}