---
layout: default
title: LandClasses
---

`LandClasses` are regions specified by the LandControl PQSMod that can locally customize several features of the PQS, including features from other PQSMods like HeightColorMap or noise PQSMods. LandClasses can change terrain height, change terrain color, add color noise and height noise, and add ground scatters. LandClasses are defined via ranges of altitude, latitude, and longitude.

**Subnodes** - Both are defined under the main table.
* LerpRange { } = Defines range values
* Scatters { } = Defines used scatter amounts

**Example**
```md
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
      latitudeFloat = True
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
      latitudeFloatRange
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

|Property|Format|Description|
|--------|------|-----------|
|name|Text|The name of the LandClass.|
|alterApparentHeight|Float|Supposedly adjusts the terrain's appearance. Only ever observed in the practice of forming icecaps.|
|alterRealHeight|Float|Supposedly adjusts the terrain's actual height.|
|minimumRealHeight|Float|The minimum height of the LandClass's terrain.|
|color|Color|The color of the region.|
|coverageBlend|Float|The blend of the coverage with surrounding LandClasses.|
|coverageFrequency|Float|The size of the each feature of the LandClass coverage. As frequency gets bigger, size gets smaller.|
|coverageOctaves|Integer|The amount of blanketing over the LandClass coverage. Higher octaves mean rougher coverage.|
|coveragePersistance|Float|The complexity of or amount of detail in the LandClass coverage.|
|coverageSeed|Integer|The random seed of the LandClass coverage.|
|noiseBlend|Float|The blend of the LandClass noise with adjacent terrain.|
|noiseFrequency|Float|The size of the each feature of the LandClass noise. As frequency gets bigger, size gets smaller.|
|noiseOctaves|Integer|The amount of blanketing over the LandClass noise. Higher octaves mean rougher noise.|
|noisePersistance|Float|The complexity of or amount of detail in the LandClass noise.|
|noiseSeed|Integer|The random seed of the LandClass noise.|
|noiseColor|Color|The main color of the noise to be added to the LandClass.|
|latDelta|Float|The change between min and max of ~~the latitude specified.~~ 0 latitude?|
|latitudeFloat|Boolean|Whether to use a second latitude range - could be used for mirroring over the equator.|
|lonDelta|Float|The change between min and max of ~~the longitude specified.~~ 0 longitude?|
|altitudeRange|LerpRange|Determines the heights at which the LandClass encompasses.|
|latitudeRange|LerpRange|Determines the latitudes at which the LandClass encompasses.|
|latitudeFloatRange|LerpRange|Determines the second latitudes at which the LandClass encompasses - only used if `latitudeFloat` is true.|
|longitudeRange|LerpRange|Determines the longitudes at which the LandClass encompasses.|

## LerpRange
Each `LerpRange { }` node describes a range of numbers to encompass, or lerp over. These ranges are applied to each dimension. The image below describes the valid ranges for latitude and longitude, with a handy diagram at the bottom for a visual description of the coverage of the LandClass over a single "dimension." Areas where coverage is not complete are determined by the `coverage___` properties.

![alttext](https://media.discordapp.net/attachments/717082915565076491/717506199100194876/LANDCONTROL.png)

|Property|Format|Description|
|--------|------|-----------|
|startStart|Float|The true start of the LandClass coverage. Coverage before this point is non-existent, while coverage after this point is sparse.|
|startEnd|Float|The end of the starting area of the LandClass coverage. Coverage before this point is sparse, while coverage after this point is complete.|
|endStart|Float|The start of the ending area of the LandClass coverage. Coverage before this point is complete, while coverage after this point is sparse.|
|endEnd|Float|The true end of the LandClass coverage. Coverage before this point is sparse, while coverage after this point is non-existent.|

## Scatters
Although not a true scatters node, the `Scatters { }` node in a LandClass node has a list of values in which each modifies the density of the scatter's use in the LandClass.

|Property|Format|Description|
|--------|------|-----------|
|density|Float|The amount to modify the scatter's density with. Seems to be multiplied with the scatter's `maxScatter`?|
|scatterName|Text|The name of the scatter to modify the density of.|
