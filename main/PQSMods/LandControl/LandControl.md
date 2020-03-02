---
layout: default
title: LandControl
---

The `LandControl` PQSMod allows defining regions known as LandClasses, within which you can change terrain height, change terrain color, and add ground scatters. LandClasses are defined via LatitudeRange, LongitudeRange, and AltitudeRange

**Example**
```
PQS
{
  Mods
  {
    LandControl
    {	// Descriptions are only provided for the altitude input paramaters but they apply to Latitude and Longitude as well.
        altitudeBlend = 0.01	// Blend: Determines how it blends with above or below terrain - SINGLE
        altitudeFrequency = 2	// Frequency: Frequency, or how big/small the noise is - SINGLE
        altitudeOctaves = 2	// In some nodes they may slightly alter the appearance from blurry to less blurry, but that's about it - INT32
        altitudePersistance = 0.5	// How much it resists being overwritten? - SINGLE
        altitudeSeed = 53453		// It's a seed.  Fairly obvious. - INT32
        createColors = True	// True/False  Whether or not to use or affect colors - BOOLEAN
        createScatter = True	// True/false Whether or not it bothered with creating scatters - BOOLEAN
        heightMap = BUILTIN/oceanmoon_height	// Use for height map as of yet unknown.  Using it as a mask? - FILE PATH

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

        useHeightMap = False	//Whether or not to use the height map...  For whatever it's for. - BOOLEAN
        vHeightMax = 6000 - SINGLE
        order = 100
        enabled = True
        name = _LandClass	//LC name - string

        scatters	//Adds a scatter to be used.  In this case, a brown rock.
        {
            Value
            {
                materialType = DiffuseWrapped // Type of material to use - ScatterMaterialType { Diffuse, BumpedDiffuse, DiffuseDetail, DiffuseWrapped, CutoutDiffuse, AerialCutout, Standard}
                mesh = BUILTIN/boulder		//Filepath to mesh. Must be .obj!!! - maybe list BUILTINs
                castShadows = True	//Obvious - BOOLEAN
                densityFactor = 1 - DOUBLE
                material = BUILTIN/scatter_rock_laythe	//Avoid using this.  Delete it.  Can take the place of the Material subside below.  The two are not compatible together.
                maxCache = 512		//How many scatters this has loaded? Not sure. - INT32
                maxCacheDelta = 32	//No idea what delta means - INT32
                maxLevelOffset = 0 // INT32
                maxScale = 1.5	//Max size - SINGLE
                minScale = 0.25	//Minimun size - SINGLE
                maxScatter = 30	//Max # of scatters loaded?? - INT32
                maxSpeed = 1000		//Mostly unknown. It affects the frequency of scatter spawns. Posssibly a misspelling of maxSpread.
                recieveShadows = True		//If it receives shadows or not duh - BOOLEAN
                name = BrownRock	//Name - STRING
                seed = 345234534	//Seed for scatter distribution
                verticalOffset = 0		//How far it is offset, so it can be floating or offset underground
                delete = False
                Material
                {
					// COntents depend on materialType chosen above, see ScatterMaterialType page
                }
            }
        }
        landClasses	//Not unlike HCM, land classes are assigned, allowing for color change, what scatters are created in which area, etc.
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
                delete = False
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
                scatters	//List what scatters you want to appear in this land class, if any.
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
  }
}
```


|Property|Format|Description|
|--------|------|-----------|
|deformity|Single|The deformity of the simplex terrain noise.|
|frequency|Single|The size of the each feature of the simplex terrain noise. As frequency gets bigger, size gets smaller.|
|octaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Single|The complexity of or amount of detail in the noise.|
|seed|Integer|The random seed of the noise.|
