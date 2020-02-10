---
layout: default
title: PQS
---
The `PQS { }` node describes properties of the Procedural Quad Sphere that makes up the body and the material used for it (physics and redering-wise). It also contains the `Mods { }` subnode, which specifies which PQSMods to apply to customize the body.

### Subnodes
***
* [Mods]({{ site.baseurl }}{% link PQSMods/PQSMods.md%})

## Example
```md
Body
{
    PQS
    {
        // Surface physics material
        PhysicsMaterial
        {
            bounceCombine = // Enum, values are Average = 0, Multiply = 1, Minimum = 2, Maximum = 4
            bounceCombine = // same type as above
            bounciness = //Single
            staticFriction = // Single
            dynamicFriction = // Single
        }

        // PQS Detail Settings
        minLevel = // Int32
        maxLevel = // Int32
        minDetailDistance = // Double
        maxQuadlengthsPerFrame = // Single

        //PQS fades, should roughly line up with ScaledSpace fades
        fadeStart = // Single
        fadeEnd = // Single
        deactivateAltitude = // Double, whata ltutude is PQS disabled at

        mapMaxHeight = // Double, the max altitude that can be represented by the height map?

        // PQS Render material?
        materialType = // Enum, values are Vacuum, AtmosphericBasic, AtmosphericMain, AtmosphericOptimized, AtmosphericExtra, AtmosphericOptimizedFastBlend, AtmosphericTriplanarZoomRotation 
        Material
        {
            factor = // Int32, how many zoom levels there are (see video), fewer results in more discrete levels, 1 will softlock
            factorBlendWidth = // Single CHECK WITH GAMESLINX EXAMPLE
            factorRotation = // Single
            saturation = // single
            contrast = single
            tintColor = Color
            specularColor = color
            albedoBrightness = single
            steepPower = single
            steepTexStart = Single
            steepTexEnd = single
            steepTex = texture2d
            steepTexScale = vector2
            steepTexOffset = vector2
            steepBumpMap = texture2d
            steepBumpMapScale = vector2
            steepBumpMapOffset = vector2
            steepNearTiling = single
            steepTiling = single // far tiling
            lowTex = texture3d
            lowTexScale = vector2
            lowTexOffset = vector2
            lowTiling = single
            midTex = texture3d
            midTexScale = vector2
            midTexOffset = vector2
            midTiling = single
            
        }

    }
}
```
