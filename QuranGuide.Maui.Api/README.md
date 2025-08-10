# QuranGuide.Maui.Api (Web API)

- ASP.NET Core Web API (.NET 8)
- Swagger via Swashbuckle (see `Program.cs`)
- CORS allows `http://localhost:4200` and `https://localhost:4200`

## Structure
- `Program.cs`: DI, CORS, Swagger, `AddQuranGuideInfrastructure`
- `Controllers/*`:
  - `QuranController`: Surah list/detail; delegates search to `ISearchService`
  - `AudioController`: Audio editions and surah audio via `IAudioService`
  - `HadithController`: Books and chapters via `IHadithService`
  - `EditionController`, `SearchController`, `TafseerController`: placeholders

## Configuration
- `appsettings.json` keys used by Infrastructure:
  - `QuranApi:BaseUrl`
  - `HadithApi:BaseUrl`
  - `HadithApi:ApiKey`

## Dependency Injection
- From Infrastructure extension:
  - `IQuranService`, `IAudioService`, `IHadithService`, `ISearchService`
  - `ISurahRepository`

## Run
- `dotnet run` in this project directory
- Swagger UI: `/swagger` in Development
