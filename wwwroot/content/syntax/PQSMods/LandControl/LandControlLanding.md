**Internal class name:** `PQSLandControl`

The `LandControl` PQSMod allows very detailed control over the coloration of terrain, creation of ground scatters, local adjustments in terrain height, and much more.

It is both verbose and complex with multiple key parts, so in the interest of ease of use and readability (as well as the sanity of the maintainers), LandControl will be split up into a series of pages. All you need to know for now is that LandControl defines a series of regions called LandClasses. These regions are limited in terms of altitude, latitude and longitude, and they define the terrain scatters found therein.

A notable example of LandControl in action is [Kerbin](https://github.com/Kopernicus/kittopia-dumps/blob/8a4e0737f18ee2b9755e4f7f2451e9de56f2a82f/Configs/Kerbin.cfg#L438); LandControl is wholly responsible for Kerbin's terrain color. As such, Kerbin's use of LandControl will be used as an example in this explanation.

To illustrate the point made previously: Kerbin uses LandControl to specify the colors **and location** of its desert and polar regions. Because these LandClasses are responsible for the terrain scatters of cacti and pine trees respectively, cacti and pine trees will only appear in the areas affected by the aforementioned LandClasses (or any other LandClass that allows them to spawn).

LandControl as a PQSMod firstly specifies a series of parameters that govern the **global** behavior of the PQSMod. That is to say, they affect the entire planet. It also specifies an array of named terrain scatter objects, and an array of LandClasses which specify the **local** behavior. These LandClasses may in turn reference elements in the Scatters array by name. Each of these three areas of configuration (global settings, local settings (LandClasses) and terrain scatters) are explained extensively in their own sub-pages:

* [LandControl PQSMod]( /Syntax/PQSMods/LandControl/LandControl)
* [LandClasses]( /Syntax/PQSMods/LandControl/LandClasses)
* [Scatters]( /Syntax/PQSMods/LandControl/Scatters)
