# EventFlow – enkelt API

Ett litet API med fiktiva evenemang för K2. Frontend hämtar data med fetch. Anmälan demonstreras i frontend och SQL Server-databasen görs separat.

## Starta

Installera .NET 10 SDK. Packa upp projektet, öppna en terminal i projektmappen och kör:

```bash
dotnet run --urls http://localhost:5043
```

Öppna http://localhost:5043/api/events. Lokal HTTP används för utveckling. 

## Endpoints

Alla anrop använder GET. Svaren är JSON och ingen API-nyckel behövs.

- `/api/events` – 200 och en lista med evenemang.
- `/api/events/1` – 200 och ett evenemang. Testa `/api/events/9999` för 404.
- `/api/test/empty` – 200 och en tom lista (`[]`).
- `/api/test/error` – 500 och ett simulerat fel.

Varje evenemang innehåller `id`, `title`, `description`, `startsAt`, `category`, `organizer` och `venue`. Sökning och filtrering görs i frontend. Listan returnerar alla fiktiva evenemang, även när deras datum har passerat. Datumen är fasta i december 2026.

## Testa

Öppna länkarna ovan i webbläsaren eller kör:

```bash
curl -i http://localhost:5043/api/events
curl -i http://localhost:5043/api/events/1
curl -i http://localhost:5043/api/events/9999
curl -i http://localhost:5043/api/test/empty
curl -i http://localhost:5043/api/test/error
```

Kontrollera svarskod och JSON för varje anrop. Simulera nätverksfel genom att stoppa API:t eller aktivera offline i webbläsarens utvecklarverktyg. Ett 500-svar är ett HTTP-fel, inte ett nätverksfel.

CORS tillåter GET från alla origin, utan inloggning, eftersom innehållet är offentlig fiktiv testdata. Det finns inga skrivfunktioner eller bokningar. 
