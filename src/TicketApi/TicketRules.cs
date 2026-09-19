namespace TicketApi;

/// <summary>
/// The only piece of real logic in this demo.
/// It lives in its own class so that a unit test can call it directly,
/// without starting a web server.
/// </summary>
public static class TicketRules
{
    /// <summary>
    /// Works out which status a ticket moves to next.
    /// New -> Open -> Resolved -> Closed, and Closed stays Closed.
    /// </summary>
    public static string NextStatus(string current) => current switch
    {
        "New"      => "Open",
        "Open"     => "Resolved",
        "Resolved" => "Closed",
        "Closed"   => "Closed",
        _ => throw new ArgumentException($"Unknown status: {current}", nameof(current))
    };
}
