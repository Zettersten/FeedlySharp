namespace FeedlySharp.Models
{
    public class Login : Resource
    {
        public bool Verified { get; set; }

        public string Picture { get; set; }

        public string Provider { get; set; }

        public string ProviderId { get; set; }

        public string FullName { get; set; }
    }
}
