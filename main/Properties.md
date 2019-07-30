---
layout: default
title: Properties
subtitle: The basic building blocks of every world
---

The `Properties { }` node describes the body itself, and is a subnode of `Body { }`. Basic parameters like description, radius, gravity and biomes are specified here. 

### Subnodes
***
* [Biomes/Biome { }]({{ site.baseurl }}{% link Properties/Biome.md %})
* [ScienceValues { }]({{ site.baseurl }}{% link Properties/ScienceValues.md %})

## Example:
```
Properties
{
  description = A big ol' round blueberry. I wonder if it's sweet and juicy?
  radius = 500000
  geeASL = 1.2
  rotates = true
  rotationPeriod = 18600
  initialRotation = 12
  tidallyLocked = false
  isHomeWorld = false
  timewarpAltitudeLimits = 0 10000 20000 40000 75000 150000 300000 400000
  sphereOfInfluence = 52500000
  maxZoom = 100000
  ScienceValues
  {
  ...
  }
  biomeMap = Fruits/PluginData/Blueberry_biomemap.dds
  Biomes
  {
  ...
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|description|String|Here goes the description for the info box of the body that you can access in map view. *You can insert line breaks using the syntax `\\nn`*|
|useTheInName|Boolean|If the body name should be prefixed with "the" in some situations, such as "the sun."|
|radius|Double|The radius (half of the body's diameter) of the body in meters.|
|mass|Double|The mass of the body in kilograms. You can use scientific notation here, like `1.234567+e20`|
|[gravParameter](https://en.wikipedia.org/wiki/Standard_gravitational_parameter)|Double|Standard gravitational parameter, calcualted as the [gravitation constant](https://en.wikipedia.org/wiki/Gravitational_constant) (G) times the mass (M) of the body: G\*M. This is the parameter that is used in the actual simulation. The gravitational acceleration in any point would be calculated as gravParameter/r<sup>2</sup> where r is a distance from that point to the body center.|
|geeASL|Double|The Gravitational parameter At Sea Level in Gs. For Earth/Kerbin this would simply be 1. If the reference body is Kerbin, which has an acceleration of gravity of 9.8m/s<sup>2</sup>, geeASL= gravParameter/9.8 (equal to 1).|
|rotates|Boolean|Statement that determines if the body rotates or not. In reality no celestial body doesn't rotate, but in KSP the Sun, aka Kerbol, doesn't rotate.|
|[rotationPeriod](https://en.wikipedia.org/wiki/Rotation_period)|Double|The period in seconds that the body needs to rotate around its axis one time.|
|initialRotation|Double|The rotation in degrees (0-359) that the body starts at (clockwise)|
|inverseRotThresholdAltitude|Single|Altitude where the Game switches the reference frame from Surface to Orbit in meters.|
|[albedo](https://en.wikipedia.org/wiki/Albedo)|Double|How reflective the body is. scale from 0 to 1|
|[emissivity](https://en.wikipedia.org/wiki/Emissivity)|Double|?scale from 0 to 1?|
|coreTemperatureOffset|?|?|
|[tidallyLocked](https://en.wikipedia.org/wiki/Tidal_locking)|Boolean|Statement that determines if the body is tidally locked to its parent. This means that it takes as long to rotate around its own axis as it does to make a full orbit around its parent. In real-life and KSP most (large) moons are tidally locked.|
|isHomeWorld|Boolean|Statement that determines if this is the body that houses KSC. For stability's sake It's recommended to keep this at false for any bodies you add.|
|timewarpAltitudeLimits|Integer[]|Determines at which altitude above sea level certain timewarp altitudes become available. 0 30000 30000 60000 100000 300000 600000 800000 means that 1x timewarp is available at 0 meters, 5x timewarp at 30000 meters all the way up to the max timewarp starting at 800000 meters.|
|sphereOfInfluence|Double|In meters. The sphere of influence of the body. This is generally calculated as described [here](http://wiki.kerbalspaceprogram.com/wiki/Sphere_of_influence). In case you need it to be unrealistically big or small you can change it here.|
|solarRotationPeriod|Boolean|Whether the body should use the solar day instead of the sidereal day.|
|navballSwitchRadiusMult|Double|?In meters?|
|navballSwitchRadiusMultLow|Double|?In meters?|
|selectable|Boolean|Whether the body should be selectable. Partially controlled by `Body/barycenter`.|
|RnDVisibility|RnDVisibility|(Also RDVisibility) The visibility state of the body in the RnD archives. Possible values are `Visible`, `Noicon`, `Hidden`, or `Skip`.|
|RnDRotation|Boolean|Whether the body should rotate in the RnD archives.|
|maxZoom|Single|The max zoom limit for the tracking station and the map view. Sets the number of meters that can fit in the full height of the screen.|
|biomeMap|File Path|The path to the biome map texture. See the [Biome subnode]({{ site.baseurl }}{% link /main/Properties/Biome.md %}) for more information|
