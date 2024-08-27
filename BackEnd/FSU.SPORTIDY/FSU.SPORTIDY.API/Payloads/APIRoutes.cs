namespace FSU.SPORTIDY.API.Payloads
{
    public static class APIRoutes
    {
        public const string Base = "/api";

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
        public static class User
        {
            public const string GetAll = Base + "/users/";

            public const string GetByID = Base + "/users/{id}";

            public const string GetByEmail = Base + "/users/by-email/{email}";

            public const string Update = Base + "/users/";

            public const string SoftDelete = Base + "/users/soft-delete/{id}";

            public const string Delete = Base + "/users/delete-user/{id}";

            public const string Add = Base + "/users/";
        }

        public static class Club
        {
            public const string GetAll = Base + "/clubs/";

            public const string GetByID = Base + "/clubs/{id}";

            public const string GetClubJoinedByUserId = Base + "/clubs/joined-club/{userId}";

            public const string GetAllMeetingsOfClub = Base + "/clubs/meetings/{clubId}";

            public const string InsertToUserClub = Base + "/clubs/{id}";

            public const string JoinedClub = Base + "/clubs/joined-club";

            public const string Update = Base + "/clubs/";

            public const string Delete = Base + "/clubs/delete-club/{id}";

            public const string Add = Base + "/clubs/{currentUserId}";
        }

    }
}
