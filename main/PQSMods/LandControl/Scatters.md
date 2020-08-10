---
layout: default
title: Scatters
---

The `Scatters` node in the LandControl PQSMod defines the scatters to be used within the [LandClasses]({{ site.baseurl }}{% link main/PQSMods/LandControl/LandClasses.md %}) provided by LandControl.

**Subnodes**
* [Material { }]({{ site.baseurl }}{% link main/PQSMods/LandControl/ScatterMaterialType.md %})
* [Components { }]({{ site.baseurl }}{% link main/PQSMods/LandControl/ModularScatter/ModularScatter.md %}) (Also known as ModularScatter)

**Example**
```
LandControl
{
  Scatters
  {
    Value
    {
      materialType = DiffuseWrapped
      mesh = BUILTIN/boulder
      castShadows = True
      densityFactor = 1
      maxCache = 512
      maxCacheDelta = 32
      maxLevelOffset = 0
      maxScale = 1.5
      minScale = 0.25
      maxScatter = 30
      maxSpeed = 1000
      recieveShadows = True
      name = BrownRock
      seed = 345234534
      verticalOffset = 0
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
|Meshes|List of File Paths|A list of file paths to be used as meshes. Inside this node, there can be keys named anything, and the value would be a File Path.|
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
