The `Scatters` node in the LandControl PQSMod defines one or more detail meshes that may be scattered across the planet's terrain as little props. The [LandClasses]( /Syntax/PQSMods/LandControl/LandClasses) provided by LandControl determines which scatters may spawn where. They can have various configurable features added via the `Components` subnode.

## Subnodes {#Subnodes}
* [Material { }]( /Syntax/PQSMods/LandControl/ScatterMaterial)
* [Components { }]( /Syntax/PQSMods/LandControl/ModularScatter) (Also known as ModularScatter)

## Example {#Example}
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
      allowedBiomes = Arid,Desert
      lethalRadius = 5
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

## Parameters {#Parameters}
In keeping with the format of other LandControl pages, we shall divide the parameters into sections and discuss them one at a time.
1. **Appearance Control**: these parameters control the appearance of the terrain scatter, regardless of _where_ they appear.
2. **Distribution Control**: these parameters control the distribution (IE placement) of terrain scatters.
3. **Caching Control**: these parameters control the behavior of the caching strategy that KSP uses to try and increase performance.
4. **Kopernicus Parameters**: these are parameters that are not in the game by default, but which were added by Kopernicus as part of the **ModularScatter** extension.

## Appearance Control {#Appearance_Control}

These are all the settings that control the appearance of terrain scatters. Most of these parameters are self-evident but the material is more complex. Just know that you can either target a built-in material through the `materialType` property by setting it to the name of the material you want, or you can set the `material` property to target a given built-in shader. The number of options does not change. For the sake of simplicity, using `materialType` is advisable. Avoid using both simultaneously; only use one of the two.

| Property | Format | Description |
|---|---|---|
| materialType | [ScatterMaterialType]( /Syntax/PQSMods/LandControl/ScatterMaterialType) | The type of the material of the scatter. Valid options can be found on the [ScatterMaterialType]( /Syntax/PQSMods/LandControl/ScatterMaterialType) page. |
| material | BUILTIN | Stock material to use instead of specifying a materialType and Material { }. Avoid using this! Will not work in conjunction with the materialType and Material { }. |
| mesh | File Path | The path to an .obj file that contains the scatter's mesh. This can also be a built-in mesh. |
| castShadows | Boolean | Whether the scatter should cast shadows. |
| receiveShadows | Boolean | Whether the scatter should receive shadows - i.e., have shadows casted upon it. |
| maxScale | Decimal | The scatter model(s) will be scaled by a random multipler choosen between `minScale` and `maxScale`. |
| minScale | Decimal | The scatter model(s) will be scaled by a random multipler choosen between `minScale` and `maxScale`. |
| verticalOffset | Decimal | Vertical offset added to placed scatters. Use this to move them up or down along the radial. |

## Distribution Control {#Distribution_Control}

These parameters offer some control over how and where terrain scatters are distributed. To gain a full understanding, it is necessary to discuss the apparent quirks of terrain scatters.

### Scatter density {#Scatter_Density}

The amount of individual scatters per quad is determined as follows :\
`baseAmount = maxScatter * densityFactor * (density defined in the LandClass)`

Then if `useBetterDensity` is enabled, the average amount will be :\
`averageAmount = baseAmount * spawnChance`

Furthermore, `maxScatter` seems to act like some kind of limiter to terrain scatter density. The exact behavior is unclear. The main takeaway is that increasing it, increases the number of scatters.

### Scatter Filtering {#scatter_Filtering}
There are two parameters that actually control where and when terrain scatters may spawn. The first is `maxSpeed`. This parameter is rather buggy but the intended behavior seems to be that it prevents a scatter from appearing unless the speed (in m/s) of the craft or camera is lower than the given value.

The other parameter is `maxLevelOffset` and this is a bit more complex. Recall that planets in KSP are just patchworks of 2D grids that have their vertices displaced to the surface of a sphere. The closer a tile is to the camera, the more detailed it is. This is an important optimization because it greatly reduces the render cost of distant tiles without significant loss of quality. After all, distant tiles typically take up less screen space and as such their features may be less pronounced. As an added benefit, the GPU strongly dislikes triangles that are approaching pixel scale. Keeping these triangles larger therefore has a secondary benefit to the rendering performance.

Similarly, there isn't much need for terrain scatters on tiles that are far away from the camera. The render cost is typically not worth the insignificant change in visual quality. To this end, LandControl only places terrain scatters on planet tiles that have a high enough detail level. Specifically, LandControl looks for the **smallest** value of `maxLevelOffset` and adds it to the `maxLevel` parameter of the base PQS settings. This, then, forms the **minimum level of subdivision** for scatters to spawn.

Leaving this at 0 means that scatters only appear on the highest level of subdivision, meaning the tiles that are closest to the camera. Increasing it may cause a terrain scatter to never be applicable for display, whilst decreasing it below zero may cause scatters to appear from a greater distance but at a cost to performance. Remember that LandControl will look for the smallest value assigned to `maxLevelOffset` across all terrain scatters that are defined in the mod. Setting the value lower will always result in a bit of additional processing overhead for planet tiles that suddenly become applicable for terrain scatters.

| Property | Format | Description |
|---|---|---|
| name | Text | The name of the scatter. This is the name you refer to from LandClasses to allow this scatter to spawn within that LandClass. |
| seed | Integer | The random seed for scatter distribution. |
| densityFactor | Decimal | A [0,1] base factor applied to `maxScatter`. Usually you want this set to 1 and just change `maxScatter`. |
| maxLevelOffset | Integer | By default, LandControl will only place scatters on tiles that are at the maximum level(s) of subdivision. This value is an offset to that level of subdivision: -1 means that the scatters should start appearing exactly one subdivision level sooner. |
| maxScatter | Integer | The base amount of scatter objects per quad. Actual amount depends on `densityFactor`, the `density` defined in the `LandClasses` node and `spawnChance` if `useBetterDensity` is true. |
| maxSpeed | Decimal | Scatter quads won't be created/rendered if the active vessel speed (in m/s) is higher than this value. Due to a stock bug, this is unreliable and quads might still appear anyway. |

## Caching Control {#Caching_Control}

By default, KSP tries to optimize the terrain scatters. It does so by trying to cache and merge terrain scatters. These parameters let you control this caching behavior, although the default settings should work fine in most cases.

| Property | Format | Description |
|---|---|---|
| maxCache | Integer | Maximum amount of active scatter quads. Leaving this to the default value (512) should be always fine. |
| maxCacheDelta | Integer | How many quads are added to the cache when it isn't large enough to hold all active scatter quads. `maxCache` must be a multiple of this value. Default value (64) should be fine. |

## Kopernicus Parameters {#Kopernicus_Parameters}
The built-in terrain scatters are rather limited in their scope. As such, Kopernicus replaces the built-in terrain scatters with an extended framework that offers various new features. Some of these are built into these new terrain scatters without having to use components.

| Property | Format | Description |
|---|---|---|
| allowedBiomes | Text | A comma delimited string of permitted scatter biome names. No spaces between entries. If set, then this terrain scatter can only spawn on terrain that belongs to the given biome(s). |
| lethalRadius | Decimal | The closest a Kerbal on EVA can get to this scatter without being killed, in meters. Set to 0 (the default) to disable. |
| lethalRadiusMsg | Text | A message to be displayed in a dialog box when a Kerbal is killed by `lethalRadius`. Leave empty to disable. |
| lethalRadiusWarnMsg | Text | A message to be displayed in a dialog box when a Kerbal comes within 2x `lethalRadius` to alert the player. Leave empty to disable. |
| Meshes | List of File Paths | Optionally, a list of meshes from which Kopernicus will randomly pick. Inside this node, there can be keys named anything, and the value should be the file path to the .obj file. This serves as a replacement for the regular `mesh` property, in case you have multiple variants of the same terrain scatter. Note that these will use the same material. |
| useBetterDensity | Boolean | Set this to true to enable randomization of the amount of scatter objects per quad. |
| spawnChance | Decimal | Requires `useBetterDensity` to be true. [0, 1] probability of each scatter object spawning. |
| ignoreDensityGameSetting | Boolean | If set to true, the KSP main menu settings scatter density % will be ignored. |
| instancing | Boolean | Enables or disables object instancing support on the scatter's generated material. This lets Unity render identical meshes that use identical material instances within a single draw call. |
| rotation | 2 Decimals | To add some visual variety, terrain scatters are randomly rotated along their local up axis. This parameter lets you control the minimum and maximum angle (in degrees), if you so wish. By default, full 360 degree rotation is set. |
