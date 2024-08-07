The `PQS { }` node describes properties of the Procedural Quad Sphere that makes up the body and the material used for it (physics and redering-wise). It also contains the `Mods { }` subnode, which specifies which PQSMods to apply to customize the body.

## Subnodes {#Subnodes}
* PhysicsMaterial { } (below)
* Material { }
  + [AtmosphericTriplanarZoomRotation]( /Syntax/Material/AtmosphericTriplanarZoomRotation)
  + [AtmosphericTriplanarZoomRotationTextureArray]( /Syntax/Material/AtmosphericTriplanarZoomRotationTextureArray)
* FallbackMaterial { }
* [Mods { }]( /Syntax/PQSMods)

## Example {#Example}
```
Body
{
    PQS
    {
        // PQS Detail Settings
        minLevel = 2
        maxLevel = 10
        minDetailDistance = 8
        maxQuadLengthsPerFrame = 0.03

        // Surface physics material
        PhysicsMaterial
        {
            bounceCombine = Multiply
            frictionCombine = Maximum
            bounciness = 0
            staticFriction = 0.9
            dynamicFriction = 0.9
        }
        
        //PQS fades, should roughly line up with ScaledSpace fades
        fadeStart = 60000
        fadeEnd = 120000
        deactivateAltitude = 160000

        mapMaxHeight = 8000
        
        materialType = AtmosphericOptimized
        Material
        {
            ...
        }

        Mods
        {
            ...
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|minLevel|Integer|The minimum level of triangles needed to render the PQS (subdivision level). Advised not to alter.|
|maxLevel|Integer|The maximum level of triangles needed to render the PQS (subdivision level). Higher levels can lead to more detailed, yet much more noisy and sharp, terrain.|
|minDetailDistance|Decimal|The minimum subdivision level for scatters to spawn.|
|maxQuadLengthsPerFrame|Decimal|Unknown use. Advised not to alter.|
|fadeStart|Decimal|The altitude, in meters, in which the PQS begins to fade out. Should line up with ScaledVersion's `fadeStart`.|
|fadeEnd|Decimal|The altitude, in meters, in which the PQS is fully faded. Should line up with ScaledVersion's `fadeEnd`.|
|deactivateAltitude|Decimal|The altitude, in meters, in which the PQS is deactivated.|
|mapMaxHeight|Decimal|The maximum altitude, in meters, that can be represented in a height map exported from Kittopia. Omit to use the full height of the current PQS.|
|materialType|PQSMaterial|The name of the material type to use in the `Material { }` subnode. Possible values: Vacuum, AtmosphericBasic, AtmosphericMain, AtmosphericOptimized, AtmosphericExtra, AtmosphericOptimizedFastBlend, AtmosphericTriplanarZoomRotation, AtmosphericTriplanarZoomRotationTextureArray (1.9).|

## PhysicsMaterial
The `PhysicsMaterial { }` subnode describes how the PQS's terrain acts physically.

|Property|Format|Description|
|--------|------|-----------|
|bounceCombine|PhysicsCombineMode|Determines how the bounciness combines. Values are Average = 0, Multiply = 1, Minimum = 2, Maximum = 4. Default is Average.|
|frictionCombine|PhysicsCombineMode|Determines how the friction combines. Values are Average = 0, Multiply = 1, Minimum = 2, Maximum = 4. Default is Maximum.|
|bounciness|Decimal|The bounciness of the terrain. Default is 0.0.|
|staticFriction|Decimal|The friction of the terrain when unmoving. Default is 0.8.|
|dynamicFriction|Decimal|The friction of the terrain when moving. Default is 0.6.|
