namespace FeedlySharp.Models;

/// <summary>
/// User profile model
/// </summary>
public record Profile : Resource
{
    public string? Client { get; init; }
    public string? Email { get; init; }
    public string? Login { get; init; }
    public List<Login>? Logins { get; init; }
    public string? Wave { get; init; }
    public string? Product { get; init; }
    public DateTimeOffset EnterpriseJoinDate { get; init; }
    public string? Picture { get; init; }
    public CardDetails? CardDetails { get; init; }
    public string? GivenName { get; init; }
    public string? FamilyName { get; init; }
    public string? Google { get; init; }
    public string? Gender { get; init; }
    public string? SubscriptionPaymentProvider { get; init; }
    public long ProductExpiration { get; init; }
    public DateTimeOffset SubscriptionRenewalDate { get; init; }
    public string? SubscriptionStatus { get; init; }
    public DateTimeOffset UpgradeDate { get; init; }
    public DateTimeOffset LastPaymentDate { get; init; }
    public bool DropboxConnected { get; init; }
    public bool TwitterConnected { get; init; }
    public bool FacebookConnected { get; init; }
    public int ProductRenewalAmount { get; init; }
    public bool EvernoteConnected { get; init; }
    public bool PocketConnected { get; init; }
    public bool WordPressConnected { get; init; }
    public bool WindowsLiveConnected { get; init; }
    public bool InstapaperConnected { get; init; }
    public string? Locale { get; init; }
    public string? FullName { get; init; }
}
