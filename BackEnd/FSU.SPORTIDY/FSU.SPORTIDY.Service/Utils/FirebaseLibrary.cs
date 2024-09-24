using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Utils
{
    public class FirebaseLibrary
    {
        public static async Task<string> SendMessageFireBase(string title, string body, string deviceToken)
        {
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = body,
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/sportidy-447fd.appspot.com/o/SPORTYDI.png?alt=media&token=e5dbca1b-32c2-463b-8c2d-c1c914dccf17"
                },
                Token = deviceToken
            };

            var reponse = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return reponse;

        }

        public static async Task<bool> SendRangeMessageFireBase(string title, string body, List<string> deviceTokens)
        {
            var message = new MulticastMessage()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = body,
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/sportidy-447fd.appspot.com/o/SPORTYDI.png?alt=media&token=e5dbca1b-32c2-463b-8c2d-c1c914dccf17"
                },
                Tokens = deviceTokens
            };

            var reponse = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message);
            return true;

        }
    }
}
