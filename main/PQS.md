---
layout: default
title: PQS
---
The `PQS { }` node describes properties of the Procedural Quad Sphere that makes up the body and the material used for it (physics and redering-wise). It also contains the `Mods { }` subnode, which specifies which PQSMods to apply to customize the body.

### Subnodes
***
* [Mods]({{ site.baseurl }}{% link main/PQSMods/PQSMods.md%})

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
            frictionCombine = // same type as above
            bounciness = //Float
            staticFriction = // Float
            dynamicFriction = // Float
        }

        // PQS Detail Settings
        minLevel = // Integer, the minimum subdivision level allowed on the planet
        maxLevel = // Integer, the maximum subdivision level allowed on the planet
        minDetailDistance = // Float, the distance at which scatters become allowed to render at
        maxQuadlengthsPerFrame = // Float

        //PQS fades, should roughly line up with ScaledSpace fades
        fadeStart = // Float
        fadeEnd = // Float
        deactivateAltitude = // Float, what altitude is PQS disabled at in meters above sea level

        mapMaxHeight = // Float, the max altitude that can be represented by the height map?

        materialType = // Enum, values are Vacuum, AtmosphericBasic, AtmosphericMain, AtmosphericOptimized, AtmosphericExtra, AtmosphericOptimizedFastBlend, AtmosphericTriplanarZoomRotation, but only 4 of these are commonly used and the rest will be deferred or not added.
        Material
        {
            // Each materialType has different options, so choose one from the list at the bottom of the page to see the associated Material block.
        }

    }
}
```


* [AtmosphericTriplanarZoomRotation]({{ site.baseurl }}{% link main/Material/PQSAtmosphericTriplanarZoomRotation.md %})

