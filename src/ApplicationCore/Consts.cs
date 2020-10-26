using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Consts
{
    public static class Common
    {
        public static string Google => "Google";
        public static string Notifications => "Notifications";
        public static string MAILCONTENT => "MAILCONTENT";
    }

    public static class RoleNames
    {
        public static string Boss => "Boss";
        public static string Dev => "Dev";
    }

    public static class CacheKeys
    {
        public static string Cities => "Cities";
        public static string Categories => "Categories";
    }

    public enum PaymentTypes
    {
        CREDIT,
        ATM
    }

    public enum ThirdPartyPayment
    { 
        EcPay
    }

    

}
