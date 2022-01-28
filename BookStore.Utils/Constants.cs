using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utils
{
    public static class Constants
    {
        public const string Role_User_Private_Customer = "PrivateCustomer";
        public const string Role_User_Company_Customer = "Company";
        public const string Role_User_Admin = "Admin";
        public const string Role_User_Employee = "Employee";

        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_InProcess = "Processing";
        public const string Status_Shipped = "Shipped";
        public const string Status_Cancelled = "Cancelled";
        public const string Status_Refunded = "Refunded";

        public const string Payment_Status_Pending = "Pending";
        public const string Payment_Status_Approved = "Approved";
        public const string Payment_Status_Delayed_Payment = "ApprovedForDelayedPayment";
        public const string Payment_Status_Rejected = "Rejected";

        public const string SessionCart = "SessionShoppingCartItem";
    }
}
