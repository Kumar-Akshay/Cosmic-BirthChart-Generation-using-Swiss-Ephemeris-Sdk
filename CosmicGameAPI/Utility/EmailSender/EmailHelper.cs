using CosmicGameAPI.Utility.Constant;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Web;
namespace CosmicGameAPI.Utility.EmailSender
{
    public static class EmailHelper
    {
        
        public static DateTime GetDateTime()
        {
            return DateTime.UtcNow.AddHours(5.5);
        }
        public static string GetTemplateBody(string Path)
        {
            string Strbody = "";
            //Strbody = readFile(Server.MapPath("~/EmailTemplates/" + template));
            Strbody = readFile(Path);

            Strbody = Strbody.Replace("`facebook`", "");
            Strbody = Strbody.Replace("`twitter`", "");
            Strbody = Strbody.Replace("`linkedin`", "");
            Strbody = Strbody.Replace("`google`", "");
            Strbody = Strbody.Replace("`instagram`", "");

            Strbody = Strbody.Replace("`SiteName`", "Cosmic Game");
            Strbody = Strbody.Replace("`SiteUrl`", GlobalVars.SiteUrl);
            Strbody = Strbody.Replace("`SitePhone`", "");
            Strbody = Strbody.Replace("`SiteEmail`", "");
            Strbody = Strbody.Replace("`logo`", "");
            Strbody = Strbody.Replace("`ahome`", "");
            Strbody = Strbody.Replace("`lnkContact`", "");
            return Strbody;
        }
        public static string readFile(string strFileName)
        {
            System.IO.StreamReader streamReader = new System.IO.StreamReader(strFileName);
            string s = "";
            try
            {
                do
                {
                    s += streamReader.ReadLine();
                } while (streamReader.Peek() != -1);
            }
            catch
            {
                s = "File is empty";
            }
            finally
            {
                streamReader.Close();
            }
            return s;
        }
        public static string SendEmail(string strTO, string strCC, string strSubject, string strBody, string[] attachment = null)
        {
            try
            {
                MailMessage email = new();
                SmtpClient smtp = new();
                email.From = new MailAddress(GlobalVars.SenderEmail);
                email.To.Add(strTO);
                smtp.Port = GlobalVars.SenderEmailPort;
                System.Net.NetworkCredential creadintial = new(GlobalVars.SenderEmail, GlobalVars.SenderEmailPassword);
                smtp.Host = GlobalVars.SenderEmailSMTP;
                smtp.Credentials = creadintial;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = GlobalVars.SenderEmailSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (strCC != null && strCC.Trim() != "")
                    email.CC.Add(strCC);
                try
                {
                    if (attachment != null && attachment.Length > 0)
                    {
                        for (int i = 0; i < attachment.Length; i++)
                        {
                            email.Attachments.Add(new Attachment(attachment[i]));
                        }
                    }
                }
                catch { }

                email.Subject = strSubject;
                email.IsBodyHtml = true;
                email.Body = strBody;
                smtp.Send(email);
                email.Dispose();
                return "true";
            }
            catch (Exception)
            {
                return "false:Email Sending Fail.!\nConfigure EMail Settings";// +e.ToString();
            }
        }

    }
}
