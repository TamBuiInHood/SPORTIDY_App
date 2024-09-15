namespace FSU.SPORTIDY.API.Payloads
{
    public static class APIRoutes
    {
        public const string Base = "/sportidy";

        public static class Sportidy
        {
            public const string GetAll = Base + "/weatherforecast/";
        }

        public static class Payment
        {
            public const string createPaymentLink = Base + "/payment/create-payment-link";
            public const string getPaymentInformation = Base + "/payment/get-payment-information";
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

            public const string GetByID = Base + "/Sports/{sport-id}";

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

            public const string UpdateAvatar = Base + "/users/update-avatar/{id}";

            public const string SoftDelete = Base + "/users/soft-delete/{id}";

            public const string Delete = Base + "/users/delete-user/{id}";

            public const string Add = Base + "/users/";
        }

        public static class Club
        {
            public const string GetAll = Base + "/clubs/";

            public const string GetByID = Base + "/clubs/{clubId}";

            public const string GetClubJoinedByUserId = Base + "/clubs/joined-club/{userId}";

            public const string GetAllMeetingsOfClub = Base + "/clubs/meetings/{clubId}";

            public const string InsertToUserClub = Base + "/clubs/{id}";

            public const string JoinedClub = Base + "/clubs/joined-club";

            public const string Update = Base + "/clubs/";

            public const string Delete = Base + "/clubs/delete-club/{id}";

            public const string Add = Base + "/clubs/{currentUserId}";

            public const string UploadAvatarClub = Base + "/clubs/upload-avatar-club/";

            public const string UploadCoverImageClub = Base + "/clubs/upload-cover-image-club/";
        }

        public static class PlayFieldFeedback
        {
            public const string GetAll = Base + "/playfield-feedback/";

            public const string GetByID = Base + "/playfield-feedback/{feedbackId}";

            public const string GetByOwnerId = Base + "/playfield-feedback/owner/{ownerId}";

            public const string Delete = Base + "/playfield-feedback/{playfieldFeedbackId}";

            public const string Create = Base + "/playfield-feedback/";

            public const string Update = Base + "/playfield-feedback/";

            public const string UploadImage = Base + "/playfield-feedback/upload-image/";

            public const string UploadVideo = Base + "/playfield-feedback/upload-video/";

        }

        public static class SystemFeedback
        {
            public const string GetAll = Base + "/system-feedback/";

            public const string GetAllNoPaging = Base + "/system-feedback/get-all-no-paging";

            public const string GetByID = Base + "/system-feedback/{systemFeedbackId}";

            public const string GetByUserId = Base + "/system-feedback/user/{userId}";

            public const string Dashboard = Base + "/system-feedback/dashboard";

            public const string Delete = Base + "/system-feedback/{systemFeedbackId}";

            public const string Create = Base + "/system-feedback/";

            public const string Update = Base + "/system-feedback/";

            public const string UploadImage = Base + "/system-feedback/upload-image/";

            public const string UploadVideo = Base + "/system-feedback/upload-video/";

        }

        public static class Playfields
        {
            public const string GetAll = Base + "/Playfields/";

            public const string GetByID = Base + "/Playfields/{playfield-id}";

            public const string GetByUserID = Base + "/Playfields/{user-id}";

            public const string Update = Base + "/Playfields/";

            public const string UpdateAvatar = Base + "/Playfields/update-avatar";

            public const string UpdateStatus = Base + "/Playfields/update-status";

            public const string Delete = Base + "/Playfields/";

            public const string Add = Base + "/Playfields/";

            public const string GetAllNotPaging = Base + "/Playfields/get-all-not-paging";
        }

        public static class Friendship
        {
            public const string GetAll = Base + "/friendships/";

            public const string GetByID = Base + "/friendships/{friendship-id}";

            public const string GetBy2UserId = Base + "/friendships/{user-id-1}/{user-id-2}";

            public const string Update = Base + "/friendships/";

            public const string Delete = Base + "/friendships/";

            public const string Add = Base + "/friendships/";

            public const string GetAllNotPaging = Base + "/friendships/get-all-not-paging";
        }
    }
}
