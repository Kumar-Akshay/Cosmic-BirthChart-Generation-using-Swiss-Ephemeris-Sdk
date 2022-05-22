using System;
using System.Collections.Generic;
using System.Text;

namespace CosmicGameAPI.Utility.Constant
{
  public  class GlobalVars
    {
        public const string JwtKey = "Cosmic@Secretkey_210224";
        public static string appDateFormatFull = "dd-MM-yyyy HH:mm:ss tt";
        public static string cookieUserNameKey;
        public static string cookiePasswordKey;
        public static string cookieUserName;
        public static string cookiePassword;
        public static string siteVersion;
        public static string APIUrl;
        public static string SiteUrl;
        public static string LoginInfoId;
        public static string SenderEmail => "astrologlogic@gmail.com";
        public static string SenderEmailPassword => "#cosmicControl&5432";
        public static bool SenderEmailSSL => true;
        public static string SenderEmailSMTP => "smtp.gmail.com";
        public static int SenderEmailPort => 587;

    }

}
