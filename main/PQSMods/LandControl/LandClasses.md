---
layout: default
title: LandClasses
---

`LandClasses` are regions defined by the LandControl PQSMod, in which several features can be customized including features which other PQSMods provide, such as HeightColorMap or noise PQSMods. LandControl regions can change terrain height, change terrain color, add color noise and height noise, and add ground scatters. LandClasses are defined via ranges of altitude, latitude, and longitude.

**Subnodes** - Both are defined under the main table.
* LerpRange { } = Defines range values
* Scatters { } = Defines used scatter amounts

**Example**
```
LandControl
{
  LandClasses	//Not unlike HCM, land classes are assigned, allowing for color change, what scatters are created in which area, etc.
  {
    Value
    {
      alterApparentHeight = 100
      alterRealHeight = 10	//These "alter" inputs allegedly adjust the terrain, but I've only witnessed true PQS adjustment in the creation of icecaps.
      color = 1,1,1,0		//What color you want the land to be
      coverageBlend = 1	//How much it blends with terrain under (and/or over?) it
      coverageFrequency = 12	
      coverageOctaves = 6	//Honestly not sure this actually does anything
      coveragePersistance = 0.5
      coverageSeed = 234124
      name = IceCaps	//Self explanatory
      latDelta = 0
      latitudeDouble = True	//Whether it should use latitudeDouble as well as normal latitude
      lonDelta = 1	//Not a clue what these Delta options do
      minimumRealHeight = 20
      noiseBlend = 0.25	//How much colored noise blends with standard Value color
      noiseColor = 0.552238822,0.519182861,0.480795324,0	//Color of colored noise added
      noiseFrequency = 24	//Frequency of the color noise
      noiseOctaves = 8	//Octaves of noise, duh
      noisePersistance = 0.5	//?
      noiseSeed = 5646345	//Seed for color noise
      altitudeRange	//Like HCM, determines the heights at which this land class affects color and/or creates scatters
      {
        endEnd = 2	//In all of these endEnd & endStart/startEnd & startStart inputs, the density/intensity of whatever it is you're doing will fade from the first input to the second.  They will not if you just make them the same number.
        endStart = 2
        startEnd = -0.5
        startStart = -0.5
      }
      latitudeRange	//Determines the start point for latitude range
      {
        endEnd = 0.0414999984204769
        endStart = 0.0399999991059303
        startEnd = -10
        startStart = -10
      }
      latitudeDoubleRange	//Determines the end point for latitude range
      {
        endEnd = 11
        endStart = 11
        startEnd = 0.96000000089407
        startStart = 0.958500001579523
      }
      longitudeRange	//Determines the longitude of the land class
      {
        endEnd = 10
        endStart = 10
        startEnd = -10
        startStart = -10
      }
      Scatters	//List what scatters you want to appear in this land class, if any.
      {
        Value
        {
          density = -1	//Unknown?  At first I suspected it increased the density of scatter spawns up to 20.
          scatterName = BrownRock		//Must match the name set under the previous scatters node
          delete = False
        }
      }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|name|String|The name of the LandClass.|
|alterApparentHeight|Single|Supposedly adjusts the terrain's appearance. Only ever observed in the practice of forming icecaps.|
|alterRealHeight|Double|Supposedly adjusts the terrain's actual height.|
|minimumRealHeight|Double|The minimum height of the LandClass' terrain.|
|color|Color|The color of the region.|
|coverageBlend|Single|The blend of the coverage with surrounding LandClasses.|
|coverageFrequency|Single|The size of the each feature of the LandClass coverage. As frequency gets bigger, size gets smaller.|
|coverageOctaves|Integer|The amount of blanketing over the LandClass coverage. Higher octaves mean rougher coverage.|
|coveragePersistance|Single|The complexity of or amount of detail in the LandClass coverage.|
|coverageSeed|Integer|The random seed of the LandClass coverage.|
|noiseBlend|Single|The blend of the LandClass noise with adjacent terrain.|
|noiseFrequency|Single|The size of the each feature of the LandClass noise. As frequency gets bigger, size gets smaller.|
|noiseOctaves|Integer|The amount of blanketing over the LandClass noise. Higher octaves mean rougher noise.|
|noisePersistance|Single|The complexity of or amount of detail in the LandClass noise.|
|noiseSeed|Integer|The random seed of the LandClass noise.|
|noiseColor|Color|The main color of the noise to be added to the LandClass.|
|latDelta|Double|The change between min and max of ~~the latitude specified.~~ 0 latitude?|
|latitudeDouble|Boolean|Whether to use a second latitude range - could be used for mirroring over the equator.|
|lonDelta|Double|The change between min and max of ~~the longitude specified.~~ 0 longitude?|
|altitudeRange|LerpRange|Determines the heights at which the LandClass encompasses.|
|latitudeRange|LerpRange|Determines the latitudes at which the LandClass encompasses.|
|latitudeDoubleRange|LerpRange|Determines the second latitudes at which the LandClass encompasses - only used if `latitudeDouble` is true.|
|longitudeRange|LerpRange|Determines the longitudes at which the LandClass encompasses.|

##LerpRange
Each `LerpRange { }` node describes a range of numbers to encompass, or lerp over. These ranges are applied to each dimension. The image below describes the valid ranges for latitude and longitude, with a handy diagram at the bottom for a visual description of the coverage of the LandClass over a single "dimension." Areas where coverage is not complete are determined by the `coverage___` properties.

<Insert image pinned in #landcontrol here>

|Property|Format|Description|
|--------|------|-----------|
|startStart|Double|The true start of the LandClass coverage. Coverage before this point is non-existent, while coverage after this point is sparse.|
|startEnd|Double|The end of the starting area of the LandClass coverage. Coverage before this point is sparse, while coverage after this point is complete.|
|endStart|Double|The start of the ending area of the LandClass coverage. Coverage before this point is complete, while coverage after this point is sparse.|
|endEnd|Double|The true end of the LandClass coverage. Coverage before this point is sparse, while coverage after this point is non-existent.|

##Scatters
Although not a true scatters node, the `Scatters { }` node in a LandClass node has a list of values in which each modifies the density of the scatter's use in the LandClass.

|Property|Format|Description|
|--------|------|-----------|
|density|Double|The amount to modify the scatter's density with. Seems to be multiplied with the scatter's `maxScatter`?|
|scatterName|String|The name of the scatter to modify the density of.|
