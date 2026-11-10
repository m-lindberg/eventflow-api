var builder = WebApplication.CreateBuilder(args);

// Tillåt frontend att hämta offentlig testdata.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().WithMethods("GET").AllowAnyHeader()));

var app = builder.Build();
app.UseCors();

// Fiktiva evenemang. Listan återställs när API:t startas om.
var events = new[]
{
    new { id = 1, title = "Kodkväll för nybörjare", description = "En kväll med enkla programmeringsövningar.", startsAt = "2026-12-02T18:00:00+01:00", category = "Teknik", organizer = "Kodföreningen", venue = "Kulturhuset, Stockholm" },
    new { id = 2, title = "Vintermarknad", description = "Lokala utställare, mat och aktiviteter.", startsAt = "2026-12-05T11:00:00+01:00", category = "Kultur", organizer = "Kulturföreningen", venue = "Stadsparken, Uppsala" },
    new { id = 3, title = "Löpargrupp i parken", description = "Ett lugnt löppass för alla nivåer.", startsAt = "2026-12-09T17:30:00+01:00", category = "Hälsa", organizer = "Aktiv tillsammans", venue = "Slottsparken, Malmö" },
    new { id = 4, title = "Sagostund för familjer", description = "Sagoläsning och skapande för barn och vuxna.", startsAt = "2026-12-12T10:30:00+01:00", category = "Kultur", organizer = "Stadsbiblioteket", venue = "Stadsbiblioteket, Göteborg" }
};

// Alla evenemang.
app.MapGet("/api/events", () => Results.Ok(events));

// Ett evenemang; ett okänt id ger 404.
app.MapGet("/api/events/{id:int}", (int id) =>
{
    var eventItem = events.FirstOrDefault(e => e.id == id);
    return eventItem is null
        ? Results.NotFound(new { message = "Evenemanget finns inte." })
        : Results.Ok(eventItem);
});

// Kontrollerade testfall för tom lista och serverfel.
app.MapGet("/api/test/empty", () => Results.Ok(Array.Empty<object>()));
app.MapGet("/api/test/error", () => Results.Json(
    new { message = "Ett simulerat serverfel." }, statusCode: 500));

app.Run();
