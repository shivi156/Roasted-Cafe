using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;

namespace roasted1.Pages.Contact;

public class Contact : PageModel
{
    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        var Name = Request.Form["Name"];
        var Email = Request.Form["Email"];
        var Message = Request.Form["Message"];
        // SendMail(Name, Email, Message);
    }

    // public object Name { get; set; }
    // public object Email { get; set; }
    // public object Message { get; set; }

    // public void SendMail(string Name, string Email, string Message)
    // {
    //     MailMessage message = new MailMessage();
    //     SmtpClient smtpClient = new SmtpClient();
    //     message.From = new MailAddress("ney@psg.com");
    //     message.To.Add("swarnimasingh156@gmail.com");
    //     message.Subject = "test email";
    //     message.IsBodyHtml = true;
    //     message.Body = "<p>Name: " + Name + "</p>" + "<p>Email: " + Email + "</p>" + "<p>Message: " + Message + "</p>";
    //
    //     smtpClient.Port = 7126;
    //     
    // }
 
}