# QuranGuide.Maui (Client App)

- .NET MAUI multi-target: net8.0-android, net8.0-ios, net8.0-maccatalyst, net8.0-windows10.0.19041.0
- UI shell: `AppShell.xaml` → page: `MainPage.xaml`
- Logging: `Microsoft.Extensions.Logging.Debug`

## Structure
- `App.xaml` / `App.xaml.cs`: App bootstrap
- `AppShell.xaml`: Navigation shell
- `MainPage.xaml`: Sample page (placeholder)
- `MauiProgram.cs`: Host and DI root (UI-only for now)
- `Platforms/*`: Per-platform bootstrap/config

## Dependencies
- `QuranGuide.Maui.Core` (business contracts + models/DTOs)
- `QuranGuide.Maui.Shared` (shared types, e.g., `ApiResponse<T>`)

## Notes
- The MAUI app does not yet call the backend. Add typed HttpClients and view models in `MauiProgram.cs` to integrate with the API.
- iOS codesigning properties are present in the csproj; adjust keys/profiles as needed.

## Next Steps
- Add pages: Surah list/detail, Search, Audio player, Hadith browser
- Register typed clients to the API base URL and bind to view models
- Consider `IOptions<>` for client configuration
