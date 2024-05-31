The `Scatters` node in the LandControl PQSMod defines the scatters to be used within the [LandClasses]( /Syntax/PQSMods/LandControl/LandClasses) provided by LandControl. Scatters are 3D meshed objects that are generated on the surface and can have various configurable features added via the `Components` subnode.

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

| Property | Format | Description |
|---|---|---|
| name | Text | The name of the scatter. |
| allowedBiomes | Text | A comma delimitted string of permitted scatter biome names.  No spaces between entries.  If this list is not present, all biomes are spawned in. |
| lethalRadius | Decimal | The closest a kerbal can get to this scatter without being killed, in meters. Set to 0 (the default) to disable. |
| lethalRadiusMsg | Text | A message to be displayed in a dialog box when a kerbal is killed by `lethalRadius`. Leave empty to disable. |
| lethalRadiusWarnMsg | Text | A message to be displayed in a dialog box when a kerbal comes within 2x `lethalRadius` to alert the player. Leave empty to disable. |
| seed | Integer | The random seed for scatter distribution. |
| materialType | [ScatterMaterialType]( /Syntax/PQSMods/LandControl/ScatterMaterialType) | The type of the material of the scatter. Valid options can be found on the [ScatterMaterialType]( /Syntax/PQSMods/LandControl/ScatterMaterialType) page. |
| material | BUILTIN | Stock material to use instead of specifying a materialType and Material { }. Avoid using this! Will not work in conjunction with the materialType and Material { }. |
| mesh | File Path | The path to an .obj file that contains the scatter's mesh. |
| Meshes | List of File Paths | A list of meshes that will be picked randomly. Inside this node, there can be keys named anything, and the value should be the file path to the .obj file. |
| castShadows | Boolean | Whether the scatter should cast shadows. |
| receiveShadows | Boolean | Whether the scatter should receive shadows - i.e., have shadows casted upon it. |
| densityFactor | Float | A [0,1] base factor applied to `maxScatter`. Usually you want this set to 1 and just change `maxScatter`. |
| maxCache | Integer | Maximum amount of active scatter quads. Leaving this to the default value (512) should be always fine. |
| maxCacheDelta | Integer | How many quads are added to the cache when it isn't large enough to hold all active scatter quads. `maxCache` must be a multiple of this value. Default value (64) should be fine. |
| maxLevelOffset | Integer | The max offset from the PQS level? (the ones controlled by `minLevel` and `maxLevel`) |
| maxScale | Float | The scatter model(s) will be scaled by a random multipler choosen between `minScale` and `maxScale`. |
| minScale | Float | The scatter model(s) will be scaled by a random multipler choosen between `minScale` and `maxScale`. |
| maxScatter | Integer | The base amount of scatter objects per quad. Actual amount depends on `densityFactor`, the `density` defined in the `LandClasses` node and `spawnChance` if `useBetterDensity` is true. |
| maxSpeed | Float | Scatter quads won't be created/rendered if the active vessel speed (in m/s) is higher than this value. Due to a stock bug, this is unreliable and quads might still appear anyway. |
| verticalOffset | Float | Vertical offset from the ground in meters for scatter objects placement. |
| instancing | Boolean | Whether to instance the material, presumably to create better performance? |
| useBetterDensity | Boolean | Set this to true to enable randomization of the amount of scatter objects per quad. |
| spawnChance | Float | Requires `useBetterDensity` to be true. [0, 1] probability of each scatter object spawning. |
| ignoreDensityGameSetting | Boolean | If set to true, the KSP main menu settings scatter density % will be ignored. |

## Scatter density {#Density}

The amount of individual scatters per quad is determined as follows :\
`baseAmount = maxScatter * densityFactor * (density defined in the LandClass)`

Then if `useBetterDensity` is enabled, the average amount will be :\
`averageAmount = baseAmount * spawnChance`
