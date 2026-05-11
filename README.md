# MovieSearcher

A small Blazor application for searching movies using the OMDb API.

## Tech stack
- .NET 8
- Blazor WebAssembly / Blazor Web App
- ASP.NET Core Web API
- HttpClient
- LocalStorage
- MSTest / Moq

## Features
- Search movies by title
- Display movie details
- Store last 5 searches in browser local storage
- API layer separated from UI
- Basic controller/service tests

## How to run
1. Get an OMDb API key.
2. Add it using user-secrets:
   dotnet user-secrets set "apikey" "YOUR_KEY"
3. Run:
   dotnet run --project MovieSearcher/MovieSearcher
