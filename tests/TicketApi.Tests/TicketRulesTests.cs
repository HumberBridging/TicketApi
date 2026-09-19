using TicketApi;

namespace TicketApi.Tests;

public class TicketRulesTests
{
    [Theory]
    [InlineData("New", "New")]
    [InlineData("Open", "Resolved")]
    [InlineData("Resolved", "Closed")]
    public void NextStatus_moves_a_ticket_forward(string current, string expected)
    {
        Assert.Equal(expected, TicketRules.NextStatus(current));
    }

    [Fact]
    public void A_closed_ticket_stays_closed()
    {
        Assert.Equal("Closed", TicketRules.NextStatus("Closed"));
    }

    [Fact]
    public void An_unknown_status_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => TicketRules.NextStatus("Banana"));
    }
}
