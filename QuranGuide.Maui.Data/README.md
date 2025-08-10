# QuranGuide.Maui.Infrastructure (Data & External Services)

## Overview
- Infrastructure layer for data access and external API integrations (target: .NET 8)

## Services
- `ExternalServices/QuranService`: Implements `IQuranService`; wraps Quran API endpoints
- `ExternalServices/AudioService`: Implements `IAudioService`; audio editions and audio retrieval
- `ExternalServices/HadithService`: Implements `IHadithService`; books/chapters/hadiths
- `ExternalServices/QuranSearchService`: Implements `ISearchService`; search endpoints
- `ExternalAPI/*`: Low-level clients

## Data
- `QuranDbContext`: EF Core DbContext (models from Core)
- `Repositories/*`: Generic repository + `ISurahRepository`

## DI Extension
- `ServiceCollectionExtensions.AddQuranGuideInfrastructure(...)` registers:
  - HttpClient for `IQuranService`, `IAudioService`, `IHadithService`, `ISearchService`
  - Repositories (e.g., `ISurahRepository`)

## Configuration
- Consumed keys:
  - `QuranApi:BaseUrl`
  - `HadithApi:BaseUrl`, `HadithApi:ApiKey`
