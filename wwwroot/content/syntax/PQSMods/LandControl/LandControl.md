The `LandControl` PQSMod allows defining regions known as LandClasses, within which you can customize several features of the particular region.

## Subnodes {#Subnodes}
* [Scatters { }](/Syntax/PQSMods/LandControl/Scatters) Defines used scatters
* [LandClasses { }](/Syntax/PQSMods/LandControl/LandClasses) = Specifies land regions and their customizations

## Example {#Example}
```
PQS
{
  Mods
  {
    LandControl
    {
      createColors = True
      createScatter = True
      heightMap = BUILTIN/oceanmoon_height
      useHeightMap = False
      vHeightMax = 6000
      
      altitudeBlend = 0.01
      altitudeFrequency = 2
      altitudeOctaves = 2
      altitudePersistance = 0.5
      altitudeSeed = 53453

      latitudeBlend = 0.05
      latitudeFrequency = 12
      latitudeOctaves = 6
      latitudePersistance = 0.5
      latitudeSeed = 53456345

      longitudeBlend = 0.05
      longitudeFrequency = 12
      longitudeOctaves = 4
      longitudePersistance = 0.5
      longitudeSeed = 98888

      order = 100
      enabled = True
      name = LCExample

      Scatters
      {
        ...
      }
      LandClasses
      {
        ...
      }
    }
  }
}
```

## How it works {#How_it_works}
When it is LandControl's turn to affect the color and elevation of a planet's surface, it obtains three coordinates from the data of the vertex being built: the altitude, the latitude and the longitude. Each of these is **normalized**: that is to say, it is zero at least and one at most.

LandControl then loops over all defined [LandClasses]( /Syntax/PQSMods/LandControl/LandClasses) and, using the values for altitude, latitude and longitude, evaluates several range intervals. In essence, since land classes are defined along a certain altitude, latitude and longitude range, LandControl finds the extent to which each LandClass has dominion over the vertex that is being processed at that moment. This is the '**coverage**' of a LandClass, though perhaps a more apt term is 'contribution', as it determines the extent to which a given LandClass contributes to the final result for a given vertex. **The sum of contributions of LandClasses for a vertex is always one.**

Depending on their relative contribution, LandControl may then add [terrain scatters]( /Syntax/PQSMods/LandControl/Scatters), adjust the terrain elevation, and color the terrain. **Each of these changes is scaled by the LandClass' relative contribution to that vertex.**

## Parameters {#Parameters}

The base parameters for LandControl can be distinguished into three categories:
1. **Noise Control**: parameters for simplex noise that is applied to each of the three input coordinates: normalized altitude, latitude and longitude.
2. **Altitude Control**: parameters that specify how LandControl should obtain the normalized altitude coordinate (latitude and longitude are calculated from the position on the sphere).
3. **Permission Control**: parameters that specify what exactly you do or don't allow LandControl to modify.
We shall discuss each category in sequence.

### Noise Control {#Noise_Control}
It would look a bit plain if the LandClasses were purely linear (see HeightColorMap for example). To this end, LandControl __adds__ a simplex noise to the three coordinates (altitude, latitude, longitdue) We shall discuss each noise parameter in general, not specifically for each coordinate, because the per-coordinate behavior is mostly identical.

In other words, the explanation for 'Blend' applies to 'altitudeBlend', 'latitudeBlend' and 'longitudeBlend'. Furthermore, each of the properties below exists for all three coordinate types, prefixed similarly by the coordinate's name, so 'Frequency' is the explanation for the properties 'altitudeFrequency', 'latitudeFrequency' and 'longitudeFrequency'.

|Property|Format|Description|
|--------|------|-----------|
|Blend|Decimal|The strength of the noise. Is applied multiplicative to the noise output before being added to the respective coordinate.|
|Frequency|Decimal|The higher the frequency, the smaller the period of the noise, resulting in more densely packed 'peaks'.|
|Octaves|Integer|The simplex being evaluated is a 'gradient noise'. This means that multiple iterations or '**octaves**' of the noise are applied on top of each other to add more detail.|
|Persistance|Decimal|Controls the strength of each successive octave. Another way to look at it is that it controls the falloff, IE if persistance equals 0.5 then the first octave has strength 1, the second octave has strength 0.5, the third has strength 0.25, the fourth has 0.125, etc.|
|Seed|Integer|Each seed results in a (typically unique) noise pattern, in the same way that each integer value is similar yet distinct.|

### Altitude Control {#Altitude_Control}
These offer control over the way the altitude coordinate is obtained: is it sampled from a specified heightmap, or calculated from the current (taking PQSMod order into account) altitude of the given terrain vertex?

You can provide LandControl with a heightmap to sample for each vertex as a source for the normalized height. In this case, the brightness of the heightmap pixel at a given vertex' position is taken as the normalized height. If no heightmap is specified, or if the use thereof is disabled through the use of `useHeightMap`, then LandControl must calculate the normalized height from the given vertex, by looking at the displacement applied by previous PQSMods. Initially, the created terrain vertex will be at the surface of a sphere, and preceding PQSMods may have shifted it upward or downward to create mountains, valleys, canyons, craters, etc. But LandControl does not know in advance by how much the terrain will at most be moved.

Though theoretically this can be calculated through analyzing the possible minimum and maximum output of each PQSMod, a far simpler option - and one that gives more control - is to specify a maximum altitude. This is the function of `vHeightMax`: it specifies the altitude (relative to sea level) that LandControl should regard as having a normalized altitude of 1.

The math is then: `normalized_altitude = (distance_from_planet_center - planet_radius) / vHeightMax`

|Property|Format|Description|
|--------|------|-----------|
|heightMap|File Path|Optional parameter. If set, and if `useHeightMap` is **True**, then LandControl samples the specified heightmap for each vertex to obtain the normalized altitude coordinate.|
|useHeightMap|Boolean|If **True**, the specified height map is used as the source for the normalized altitude coordinate.|
|vHeightMax|Decimal|If the heightmap is not set, or if `useHeightMap` is **False**, then LandControl calculates the normalized altitude from the given vertex' elevation relative to `vHeightMax`. In other words, this parameter specifies the elevation relative to sea level at which the normalized altitude is 1.|

### Permission Control {#Permission_Control}

|Property|Format|Description|
|--------|------|-----------|
|createColors|Boolean|If true, then LandControl will **replace** the terrain colors using the weighted local contribution of the LandClasses.|
|createScatters|Boolean|If true, then LandControl will (attempt to) place terrain scatters. The scatters placed in an area depend on the applicable LandClasses and their densities are scaled by the relative contribution of each LandClass.|
|order|Integer|The order in the mod stack to apply LandControl at.|
|enabled|Boolean|If set to **False** then LandControl is skipped and does not contribute to the appearance of the terrain.|
