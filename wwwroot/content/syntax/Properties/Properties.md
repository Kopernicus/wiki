The `Properties { }` node describes the body itself, and is a subnode of `Body { }`. Basic parameters like description, radius, gravity and biomes are specified here.

## Subnodes {#Subnodes}
* [Biomes](/Syntax/Properties/Biomes)
* [ScienceValues](/Syntax/Properties/ScienceValues)

## Example {#Example}
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
|description|Text|Here goes the description for the info box of the body that you can access in map view. *You can insert line breaks using the syntax `\\nn`*|
|useTheInName|Boolean|If the body name should be prefixed with "the" in some situations, such as "the sun."|
|radius|Decimal|The radius (half of the body's diameter) of the body in meters.|
|mass|Decimal|The mass of the body in kilograms. You can use scientific notation here, like `1.234567+e20`|
|[gravParameter](https://en.wikipedia.org/wiki/Standard_gravitational_parameter)|Decimal|Standard gravitational parameter, calculated as the gravitational constant (G) times the mass (M) of the body: G\*M. This is the parameter that is used in the actual simulation. The gravitational acceleration in any point would be calculated as gravParameter/r<sup>2</sup> where r is a distance from that point to the body center. NOTE: For historical reasons, in KSP G = 6.67408E-11.|
|geeASL|Decimal|The gravitational acceleration at sea level measured in standard gravities, where 1 g = 9.80665 m/s<sup>2</sup>. For Earth/Kerbin, geeASL = 1. The gravParameter and geeASL are related as follows, geeASL*9.80665 = gravParameter/radius<sup>2</sup>.|
|rotates|Boolean|Does the body rotate or not?|
|[rotationPeriod](https://en.wikipedia.org/wiki/Rotation_period)|Decimal|The period in seconds that the body needs to rotate around its axis one time.|
|initialRotation|Decimal|The rotation in degrees (0-359) that the body starts at (clockwise)|
|inverseRotThresholdAltitude|Decimal|Altitude where the Game switches the reference frame from Surface to Orbit in meters.|
|[albedo](https://en.wikipedia.org/wiki/Albedo)|Decimal|How reflective the body is. scale from 0 to 1|
|[emissivity](https://en.wikipedia.org/wiki/Emissivity)|Decimal|How emissive the body is, scale from 0 to 1.|
|coreTemperatureOffset|?|?|
|[tidallyLocked](https://en.wikipedia.org/wiki/Tidal_locking)|Boolean|Statement that determines if the body is tidally locked to its parent. This means that it takes as long to rotate around its own axis as it does to make a full orbit around its parent. In real-life and KSP most (large) moons are tidally locked.|
|isHomeWorld|Boolean|Statement that determines if this is the body that houses KSC. For stability's sake It's recommended to keep this at false for any bodies you add.|
|timewarpAltitudeLimits|Integer List|Determines at which altitude above sea level certain timewarp altitudes become available. 0 30000 30000 60000 100000 300000 600000 800000 means that 1x timewarp is available at 0 meters, 5x timewarp at 30000 meters all the way up to the max timewarp starting at 800000 meters.|
|sphereOfInfluence|Decimal|In meters. The sphere of influence of the body. This is generally calculated as described [here](http://wiki.kerbalspaceprogram.com/wiki/Sphere_of_influence). In case you need it to be unrealistically big or small you can change it here.|
|solarRotationPeriod|Boolean|Whether the body should use the solar day instead of the sidereal day.|
|navballSwitchRadiusMult|Decimal|Altitude, in meters, where the NavBall switches from surface velocity to orbital velocity while ascending.|
|navballSwitchRadiusMultLow|Decimal|Altitude, in meters, where the NavBall switches from orbital velocity to surface velocity while descending.|
|selectable|Boolean|Whether the body should be selectable. Partially controlled by `Body/barycenter`.|
|RnDVisibility|RnDVisibility|(Also RDVisibility) The visibility state of the body in the RnD archives. Possible values are `Visible`, `Noicon`, `Hidden`, or `Skip`.|
|RnDRotation|Boolean|Whether the body should rotate in the RnD archives.|
|maxZoom|Decimal|The max zoom limit for the tracking station and the map view. Sets the number of meters that can fit in the full height of the screen.|
|biomeMap|File Path|The path to the biome map texture. See the [Biomes subnode]( /Syntax/Properties/Biomes) for more information|
