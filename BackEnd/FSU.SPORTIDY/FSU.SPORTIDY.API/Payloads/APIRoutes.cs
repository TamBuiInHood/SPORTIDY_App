namespace FSU.SPORTIDY.API.Payloads
{
    public static class APIRoutes
    {
        public const string Base = "/sportidy";

        public static class WebSocket
        {
            public const string ws = Base + "websocket";
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

            public const string UpdateRoleInMeeting = Base + "/meetings/update-role-in-meeting";

            public const string KickUser = Base + "/meetings/kick-user";

            public const string EngageToMeeting = Base + "/meetings/engage-to-meeting";

            public const string getAllUserInMeeting  = Base + "/meetings/get-all-user-in-meeting";



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

            public const string GetAllByRoleName = Base + "/users/{roleName}";

            public const string GetByID = Base + "/users/{id}";

            public const string GetByEmail = Base + "/users/by-email/{email}";

            public const string Update = Base + "/users/";

            public const string UpdateAvatar = Base + "/users/update-avatar/{id}";

            public const string SoftDelete = Base + "/users/soft-delete/{id}";

            public const string Delete = Base + "/users/delete-user/{id}";

            public const string Add = Base + "/users/";

            public const string UpdateDeviceCode = Base + "/users/device-code/{id}";
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

            public const string GetByUserID = Base + "/Playfields/get-by-user-id/{user-id}";

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

        public static class Booking
        {
            public const string GetAll = Base + "/bookings/";

            public const string GetByID = Base + "/bookings/{booking-id}";

            public const string GetByUserID = Base + "/bookings/get-by-user-id";

            public const string Update = Base + "/bookings/";

            public const string UpdateStatus = Base + "/bookings/update-status";

            public const string Delete = Base + "/bookings/{booking-id}";

            public const string Add = Base + "/bookings/";

            public const string RevenuePlayField = Base + "/bookings/{playField-id}/{year}";

            public const string RevenuePlayFieldForAdmin = Base + "/bookings/statistic/{year}";

            public const string StatisticPlayFieldTypePercentage = Base + "/bookings/statistic/{month}/{year}";
        }

        public static class Notifcation
        {
            public const string GetByEmail = Base + "/notifications/get-by-email/{email}";

            public const string GetByID = Base + "/notifications/get-by-id/{notification-id}";

            public const string GetByCustomerID = Base + "/notifications/get-by-customer-id/{customer-id}";

            public const string Update = Base + "/notifications/update";

            public const string Delete = Base + "/notifications/{notification-id}";

            public const string AddByCustomerId = Base + "/notifications/{customer-id}";

            public const string AddByRole = Base + "/notifications/add-by-role/{role-name}";

            public const string AddByListUserId = Base + "/notifications/list-user-id";

            public const string MarkAllCustomerNotificationIsReadByCustomerId = Base + "/notifications/mark-is-read-customer-id/{customer-id}";

            public const string MarkNotificationIsReadByNotificationId = Base + "/notifications/mark-is-read/{notification-id}";
        }
        public static class Comment
        {
            public const string GetAll = Base + "{meeting-id}/comments/";

            public const string GetByID = Base + "/comments/{comment-id}";

            public const string Update = Base + "/comments/";

            public const string Delete = Base + "/comments/{comment-id}";

            public const string Add = Base + "/comments/";
        }

    }
}
