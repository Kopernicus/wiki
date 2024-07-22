`LandClasses` are regions specified by the LandControl PQSMod that can locally customize several features of the local terrain. To put it very, very simply: LandControl's LandClasses are what you get when you take the concept of [HeightColorMap]( /Syntax/PQSMods/HeightColorMap) and empower it with greater control over local colors, the ability to place detail objects called terrain scatters (examples are grass, trees and boulders) and even an ability to adjust the local elevation of the terrain.

Similarly to HeightColorMap, the LandClasses are limited by altitude, but unlike HeightColorMap, LandControl also lets you limit the latitude and longitude range(s) wherein a LandClass should act.

## Subnodes {#Subnodes} - Both are defined under the main table.
* LerpRange { } = Defines the range along the given coordinate axes wherein a LandClass should act.
* Scatters { } = Defines the terrain scatters that may spawn in this LandClass, and the prominence of each.

## Example {#Example}
```
LandControl
{
  LandClasses
  {
    Value
    {
      alterApparentHeight = 100
      alterRealHeight = 10
      color = 1,1,1,0
      coverageBlend = 1
      coverageFrequency = 12	
      coverageOctaves = 6
      coveragePersistance = 0.5
      coverageSeed = 234124
      name = IceCaps
      latDelta = 0
      latitudeDouble = True
      lonDelta = 1
      minimumRealHeight = 20
      noiseBlend = 0.25
      noiseColor = 0.552238822,0.519182861,0.480795324,0
      noiseFrequency = 24
      noiseOctaves = 8
      noisePersistance = 0.5
      noiseSeed = 5646345
      altitudeRange
      {
        endEnd = 2
        endStart = 2
        startEnd = -0.5
        startStart = -0.5
      }
      latitudeRange
      {
        endEnd = 0.0414999984204769
        endStart = 0.0399999991059303
        startEnd = -10
        startStart = -10
      }
      latitudeDoubleRange
      {
        endEnd = 11
        endStart = 11
        startEnd = 0.96000000089407
        startStart = 0.958500001579523
      }
      longitudeRange
      {
        endEnd = 10
        endStart = 10
        startEnd = -10
        startStart = -10
      }
      Scatters
      {
        Value
        {
          density = -1
          scatterName = BrownRock
          delete = False
        }
      }
    }
  }
}
```

## Parameters {#Parameters}
Similar to the article on the base mod, we'll separate the parameters for a LandClass into categories:
1. **Coverage Control**: parameters that apply further control to the coverage of a LandClass.
2. **Color Control**: parameters that specify the colors that a LandClass may apply to the terrain.
3. **Elevation Control**: parameters that control the local elevation of terrain that falls under the given LandClass.
4. **Range Control**: parameters that specify the range of altitudes, latitudes and longitudes that the LandClass should cover.

One parameter does not fall into any of these categories: `name`, which is useful in distinguishing LandClasses. Naming LandClasses is up to user preference, pick any name that seems applicable and intuitive.

We shall now discuss each category in turn.

(Note: the parameters `latDelta` and `lonDelta` also appear in exports from Kittopia. These are used internally by KSP and should not actually be set from config files. **Ignore these.**)

## Coverage Control {#Coverage_Control}
Similar to how the base mod applies a Simplex noise to the altitude, latitude and longitude, each LandClass may apply a Simplex noise to the computed coverage.

To be specific, LandControl first obtains a normalized value for altitude, latitude and longitude from the input vertex. It then applies a simplex noise to each of these coordinates just to shake things up a little and avoid obvious 'zones'.

The resulting shifted coordinates are then compared to the altitude, latitude and longitude ranges specified in each LandClass. The per-coordinate-axis contributions are multiplied together to get the overall **coverage** of the LandClass. A simplex noise can be applied to this coverage **multiplicatively** for further detail. The below parameters control this noise.

For a more detailed explanation of simplex noise parameters, see [the main mod article]( /Syntax/PQSMods/LandControl/LandControl#Noise_Control).

|Property|Format|Description|
|--------|------|-----------|
|coverageBlend|Decimal|The strength of the simplex noise. This linearly scales the strength of the simplex noise multiplication.|
|coverageFrequency|Decimal|The initial size of the simplex noise.|
|coverageOctaves|Integer|The number of simplex noise iterations.|
|coveragePersistance|Decimal|The multiplicative falloff of noise strength for each successive octave.|
|coverageSeed|Integer|A seed for the simplex noise generator, to produce different patterns.|

## Color Control {#Color_Control}

Unlike HeightColorMap, which only allows for a single color value per region, LandControl allows for two color values,
between which it interpolates with yet another Simplex noise.

|Property|Format|Description|
|--------|------|-----------|
|color|Color|The main color of the region.|
|noiseColor|Color|A secondary color for the region.|
|noiseBlend|Decimal|A multiplicative strength value for the Simplex noise.|
|noiseFrequency|Decimal|The size of the color noise. As frequency gets bigger, size gets smaller.|
|noiseOctaves|Integer|The number of color noise octaves.|
|noisePersistance|Decimal|The falloff of successive noise iterations.|
|noiseSeed|Integer|The seed of the simplex noise.|

## Elevation Control {#Elevation_Control}

As an extra luxury, LandControl also allows for limited adjustments to local elevation.

|Property|Format|Description|
|--------|------|-----------|
|alterApparentHeight|Decimal|LandControl writes the normalized altitude to the alpha channel of the vertex color. Should you want to, then you can use this parameter to adjust the normalized altitude within this LandClass, but only insofar as the vertex color is concerned.|
|alterRealHeight|Decimal|An absolute offset (IE it is applied additively) to the terrain elevation within this LandClass, scaled by the coverage of the LandClass.|
|minimumRealHeight|Decimal|**If not set to zero**: if the altitude of the input vertex relative to sea level is less than this value (yes, this value describes a sea level relative elevation) then the vertex' elevation is raised to this value, depending on the LandClass coverage.|

## Range Control {#Range_Control}

At first glance the parameters for a **LerpRange** may seem a bit confusing. In essence, Kerbal Space Program performs a double linear interpolation.

Think of each LerpRange one at a time. For example, picture in your head a 2D grid, like a map. The horizontal axis of that map will be the planet's longitude, progressing from zero on the left, to one on the right. Similarly, the vertical axis represents latitude, from zero at the bottom to one at the top. As with a heightmap, you can imagine the brightness of points in the grid as the elevation.

Each LerpRange essentially defines a region along one of these axes. Let's take the horizontal or 'longitude' axis. The parameters supplied to `longitudeRange` will create a vertical 'band' in the 2D grid where intensity increases from zero to one, and then from one to zero again.

The same will apply to the latitude and the altitude, and the product of these three 'bands' is the coverage - changes from the coverage noise notwithstanding.

Note that the latitude actually specifies two ranges: `latitudeRange` and `latitudeDoubleRange`. This second range goes unused unless the `latitudeDouble` parameter is set to True. It is used to create a second latitude range for this LandClass. KSP will take the largest value of the two latitude ranges as the coverage for that coordinate axis. A notable use of this feature is the polar ice caps of Kerbin.

|Property|Format|Description|
|--------|------|-----------|
|latitudeDouble|Boolean|Whether to use a second latitude range. Example use case: mirroring over the equator.|
|altitudeRange|LerpRange|Determines the heights at which the LandClass has dominion.|
|latitudeRange|LerpRange|Determines the latitudes at which the LandClass has dominion.|
|latitudeDoubleRange|LerpRange|Optionally, a second latitude range over which the LandClass has dominion. Only used if `latitudeDouble` is true.|
|longitudeRange|LerpRange|Determines the longitudes at which the LandClass has dominion.|

## LerpRange {#Lerp_Range}
Each `LerpRange { }` node describes a range of numbers to encompass, or lerp over. They contain four values: `startStart`, `startEnd`, `endStart` and `endEnd`. These names a perhaps a bit confusing so let's discuss them in detail.

|Property|Format|Description|
|--------|------|-----------|
|startStart|Decimal|The true start of the LandClass coverage. Coverage before this point is non-existent, while coverage after this point is sparse.|
|startEnd|Decimal|The end of the starting area of the LandClass coverage. Coverage before this point is sparse, while coverage after this point is complete.|
|endStart|Decimal|The start of the ending area of the LandClass coverage. Coverage before this point is complete, while coverage after this point is sparse.|
|endEnd|Decimal|The true end of the LandClass coverage. Coverage before this point is sparse, while coverage after this point is non-existent.|

## Scatters {#Scatters}
LandClasses may specify which terrain scatters may spawn in the area that they have dominion over, as well as a relative density for these whitelisted terrain scatters. This takes the form of **one** `Scatters { }` node containing `Value { }` nodes.

Each `Value { }` node references a terrain scatter from the main `Scatters { }` (as defined under in the LandControl node) by name. Example:

```
LandControl
{
    // The 'main' scatters node. This DEFINES the terrain scatters.
    Scatters
    {
        Value
        {
            name = Example_Scatter
        }
    }
    LandClasses
    {
        Value
        {
            name = Example_LandClass
            // This REFERENCES the defined terrain scatters.
            Scatters
            {
                Value
                {
                    // Reference defined scatters by name.
                    scatterName = Example_Scatter
                    density = 0.5
                }
            }
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|density|Decimal|The relative density for this terrain scatter.|
|scatterName|Text|The name of the scatter to spawn in this LandClass.|

### How precisely does density work? {#How_Does_Density_Work}
LandControl first checks which LandClasses are relevant to a given vertex. It keeps track of these and their contribution values, which it turns into a weighted sum.

Later, if spawning scatters is requested, it tracks density per terrain scatter. For some, this will be zero. The density value of a LandClass' terrain scatter reference is added to a terrain scatter's density tally, multiplied by the weighted contribution of the LandClass, as well as the terrain scatter density as set by the player in the game's settings.

Finally, terrain scatters are created using the sum density for each scatter, which gets multiplied with their `densityFactor` and `maxScatter` parameters. This seems to form the basis for whatever scatter distribution system KSP uses. More info on the parameters for terrain scatters themselves can be found in the [main article on terrain scatters]( /Syntax/PQSMods/LandControl/Scatters).
