---
layout: default
title: Scatters
---

The `Scatters` node in the LandControl PQSMod defines the scatters to be used within the [LandClasses]({{ site.baseurl }}{% link main/PQSMods/LandControl/LandClasses.md %}) provided by LandControl.

**Subnodes**
* [Material { }]({{ site.baseurl }}{% link main/PQSMods/LandControl/ScatterMaterialType.md %})
* [Components { }]({{ site.baseurl }}{% link main/PQSMods/LandControl/ModularScatter/ModularScatter.md %})

**Example**
```
LandControl
{
  Scatters
  {
    Value
    {
      materialType = DiffuseWrapped // Type of material to use - ScatterMaterialType { Diffuse, BumpedDiffuse, DiffuseDetail, DiffuseWrapped, CutoutDiffuse, AerialCutout, Standard}
      mesh = BUILTIN/boulder		//Filepath to mesh. Must be .obj!!! - maybe list BUILTINs
      castShadows = True	//Obvious - BOOLEAN
      densityFactor = 1 - DOUBLE
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
        ...
      }
      Components
      {
        ...
      }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|name|String|The name of the scatter|
|seed|Integer|The random seed for scatter distribution.|
|materialType|[ScatterMaterialType]({{ site.baseurl }}{% link main/PQSMods/LandControl/ScatterMaterialType.md %})|The type of the material of the scatter. Valid options can be found on the [ScatterMaterialType]({{ site.baseurl }}{% link main/PQSMods/LandControl/ScatterMaterialType.md %}) page.
|material|BUILTIN|Stock material to use instead of specifying a materialType and Material { }. Avoid using this! Will not work in conjunction with the materialType and Material { }.|
|mesh|File Path|The path to an .obj file that contains the scatter's mesh.|
|Meshes|File Path list|A list of file paths to be used as meshes. Discouraged because format is unknown for now.|
|castShadows|Boolean|Whether the scatter should cast shadows.|
|receiveShadows|Boolean|Whether the scatter should receive shadows - i.e., have shadows casted upon it.|
|densityFactor|Double|Should be set to 1, unless working with extremely small bodies? Use is unknown.|
|maxCache|Integer|Maximum amount of scatters to cache at any time?|
|maxCacheDelta|Integer|Change in amount of scatters cached?|
|maxLevelOffset|Integer|The max offset from the PQS level? (the ones controlled by `minLevel` and `maxLevel`)|
|maxScale|Single|The maximum scale multiplier for the size of the scatter.|
|minScale|Single|The minimum scale multiplier for the size of the scatter.|
|maxScatter|Integer|Maximum number of scatters rendered?|
|maxSpeed|Double|Affects frequency of scatter spawns. Main use unknown, could be a misspelling of `maxSpread`. Possibly the max speed a craft can travel at to render the scatter.|
|verticalOffset|Single|Offset from the ground to allow floating and underground scatters.|
|instancing|Boolean|Whether to instance the material, presumably to create better performance?|
|useBetterDensity|Boolean|Whether to use an alternate method of density calculation?|
|spawnChance|Single|The chance of spawning each scatter. Scale of units unknown.|
|ignoreDensityGameSetting|Boolean|Whether to ignore the game setting that controls scatter density.|
|densityVariance|Single|The variance between allowed densities. Can be used for uneven distribution of scatters.|
