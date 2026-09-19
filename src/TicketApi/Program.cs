using TicketApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// A few tickets held in memory. No database - this demo is about the pipeline,
// not about data access.
var tickets = new[]
{
    new { Id = 1, Title = "Cannot log in",        Status = "New"  },
    new { Id = 2, Title = "Invoice PDF is blank", Status = "Open" },
    new { Id = 3, Title = "Password reset loop",  Status = "Open" }
};

// THE LINE WE CHANGE IN THE DEMO.
// Edit the message, push, and watch it appear on the live site a minute later.
app.MapGet("/", () => "SupportHub Ticket API - deployed by GitHub Actions");

// A health endpoint. Azure and the pipeline both use this to ask "are you alive?"
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapGet("/api/tickets", () => tickets);

app.MapGet("/api/tickets/{id:int}/next-status", (int id) =>
{
    var ticket = tickets.FirstOrDefault(t => t.Id == id);
    return ticket is null
        ? Results.NotFound()
        : Results.Ok(new { ticket.Id, ticket.Status, next = TicketRules.NextStatus(ticket.Status) });
});

app.Run();
