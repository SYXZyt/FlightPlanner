# Flight Planner
## Introduction
This tool will take a bunch of preloaded airports and aircraft, and generate a plan for you to fly. Current it only gives you a departure airport, and an arrival, but it could be updated to have fuel stops.
It has systems in place to not generate a plan if your plane does not have the range for it. It can also make sure you don't land at an airport with too short of a runway
## Loading custom airports
By default the program only includes a small selection of airports, but can editied to contain more. `Airport.txt` will store all of the airports used by the tool. In order to add an airport, this format must be followed. An example is provided
| ICAO | Name | Latitude | Longitude | Shortest Runway (ft) | Location |
| - | - | - | - | - | - |
| KLAX | Los Angeles Intl | 33.9425 | -118.408056 | 8914 | California US |

Note in `Airport.txt`, each airport data bit, must be terminated with a semi-colon.
Using the example, this would be inputted into `Airport.txt`. `KLAX;Los Angeles Intl;33.9425;-118.408056;8914;California US`
## Loading custom planes
Much like airports, custom aircraft can be loaded into `Plane.txt`.
This time the format is:
| Name | Model | Range (nm) | Endurance (hours) | Minimum Runway Length (ft) | Cruise Speed (kts) |
|-|-|-|-|-|-|
F-35 | b | 900 | 4 | 1000 | 451

In the custom format, this would be `F-35;b;900;4;1000;451`.
