using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfMailDal : IMailDal
    {
        public void SendMail(SendMailDto sendMailDto)
        {
            using (MailMessage mail = new MailMessage()) 
            {
                mail.From = new MailAddress(sendMailDto.mailParemeter.Email);
                mail.To.Add(sendMailDto.Mail);
                mail.Subject = sendMailDto.Subject;
                mail.Body = sendMailDto.Body;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(sendMailDto.mailParemeter.SMTP, sendMailDto.mailParemeter.Port))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(sendMailDto.mailParemeter.Email, sendMailDto.mailParemeter.Password);
                    smtp.EnableSsl = sendMailDto.mailParemeter.SSL;
                    smtp.Send(mail);
                }

            }
        }
    }
}
