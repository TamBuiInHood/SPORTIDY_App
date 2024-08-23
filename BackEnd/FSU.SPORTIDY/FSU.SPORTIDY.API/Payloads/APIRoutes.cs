namespace FSU.SPORTIDY.API.Payloads
{
    public static class APIRoutes
    {
        public const string Base = "api";

        public static class Sportidy
        {
            public const string GetAll = Base + "/weatherforecast/";
        }

        public static class Authentication
        {
            public const string Login = Base + "/authentication/login";
            public const string LoginMobile = Base + "/authentication/login-mobile";

            public const string RefreshToken = Base + "/authentication/refresh-token";

        }
        public static class Account
        {
            public const string ChangePassword = Base + "/account/change-password";

            public const string BanAccount = Base + "/account/ban-account";
        }
        public static class Meeting
        {
            public const string GetAll = Base + "/meetings/";

            public const string GetByID = Base + "/meetings/{meeting-id}";

            public const string Update = Base + "/meetings/";

            public const string Delete = Base + "/meetings/";

            public const string Add = Base + "/meetings/";
        }
        public static class Sport
        {
            public const string GetAll = Base + "/Sports/";

            public const string GetByID = Base + "/Sports/{meeting-id}";

            public const string Update = Base + "/Sports/";

            public const string Delete = Base + "/Sports/";

            public const string Add = Base + "/Sports/";

            public const string GetAllNotPaging = Base + "/Sports/get-all-not-paging";
        }
    }
}
