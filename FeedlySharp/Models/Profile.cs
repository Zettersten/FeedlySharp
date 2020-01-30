using System;
using System.Collections.Generic;

namespace FeedlySharp.Models
{
    public class Profile : Resource
    {
        public string Client { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public List<Login> Logins { get; set; }

        public string Wave { get; set; }

        public string Product { get; set; }

        public DateTimeOffset EnterpriseJoinDate { get; set; }

        public string Picture { get; set; }

        public CardDetails CardDetails { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string Google { get; set; }

        public string Gender { get; set; }

        public string SubscriptionPaymentProvider { get; set; }

        public long ProductExpiration { get; set; }

        public DateTimeOffset SubscriptionRenewalDate { get; set; }

        public string SubscriptionStatus { get; set; }

        public DateTimeOffset UpgradeDate { get; set; }

        public DateTimeOffset LastPaymentDate { get; set; }

        public bool DropboxConnected { get; set; }

        public bool TwitterConnected { get; set; }

        public bool FacebookConnected { get; set; }

        public int ProductRenewalAmount { get; set; }

        public bool EvernoteConnected { get; set; }

        public bool PocketConnected { get; set; }

        public bool WordPressConnected { get; set; }

        public bool WindowsLiveConnected { get; set; }

        public bool InstapaperConnected { get; set; }

        public string Locale { get; set; }

        public string FullName { get; set; }
    }
}
