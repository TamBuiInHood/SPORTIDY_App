using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Common.Status
{
    public class BookingStatusID
    {
        #region pending
        public const string BOOKING_PENDING_DES = "Pending";
        public const int BOOKING_PENDING_ID = 1;
        #endregion

        #region Cancel
        public const string BOOKING_CANCEL_DES = "Cancel";
        public const int BOOKING_CANCEL_ID = 2;
        #endregion

        #region paid
        public const string BOOKING_PAID_DES = "Paid";
        public const int BOOKING_PAID_ID = 3;
        #endregion

        #region Deleted
        public const string BOOKING_DELETED_DES = "Deleted";
        public const int BOOKING_DELETED_ID = 4;
        #endregion
    }
}
