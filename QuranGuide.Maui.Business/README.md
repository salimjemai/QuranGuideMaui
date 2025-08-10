# QuranGuide.Maui.Core (Business/Core)

## Overview
- Core contracts and business models used by both API and MAUI app (target: .NET 8)

## Contents
- `Services/*` interfaces:
  - `IQuranService`, `IAudioService`, `ISearchService`, `IHadithService`, `ITafseerService`, `IOfflineService`, `IUserPreferencesService`
- `Models/*`: Domain entities (`Surah`, `Ayah`, `Edition`, `Hadith`, etc.)
- `DataTransferObjects/*`: DTOs for cross-layer data
- `Business/*`: Example orchestrating services (`QuranBusinessService`, `AudioBusinessService`)

## Notes
- Implementations of service interfaces reside in the Infrastructure project
- Add validation/mapping as needed when wiring to the UI or API
