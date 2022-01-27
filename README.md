# Tangents
A 2D Geometry game built upon the .NET framework using C#, MonoGame, and the Microsoft XNA Framework.  
Created by Dan Estrin.

## Media/Gallery
<image src="https://user-images.githubusercontent.com/5817401/151265917-8c646b75-5223-4cdd-974b-a73b7e9c3dbe.png" width="481" height="293">
<image src="https://user-images.githubusercontent.com/5817401/151265156-7b79992a-8faa-41dc-aaf4-7a90d0862be5.gif" width="480" height="272">
<image src="https://user-images.githubusercontent.com/5817401/151265948-1038753d-d8f2-46bb-9aa7-b5a7810e8b8d.png" width="481" height="293">
  
## Gameplay
The player is the small blue circle and begins by rotating around one of the larger red circles. Upon pressing `SPACE`, the player is launched along the tangent line to that circle in an attempt to land from circle to circle, avoiding the boundaries of the screen. The player gets a point for landing on another circle, and gets additional points for any circles skipped along the way.

## Platforms
The game is built upon a cross-platform framework, but has so far only been developed for desktop (Windows, MacOS, Linux). The binaries and relevant files will be available under the `Releases` tab of this repo.

Further platforms in mind: MonoGame does not have official web support, but there are unofficial tools and frameworks to run .NET games on web, and I personally think this game would be well suited for a web environment. Porting to mobile (Android/iOS) would require additional work but is still possible for the future.
  
## Code
The visual studio `.sln` file is also provided in the repo. The entire project can be opened in Visual Studio (2022 Community Edition was used to develop this game).
