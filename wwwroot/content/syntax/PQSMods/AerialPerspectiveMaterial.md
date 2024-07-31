**Internal mod name:** `PQSMod_AerialPerspectiveMaterial`

AerialPerspectiveMaterial is a peculiar PQSMod. It appears to have no effect on the vertex height or color of the planet. Instead, it seems to partially configure the material of the planet's surface. It is suspected that the intent of this mod is to update certain shader parameters on every frame so as to correct any atmospheric effects.

## Example {#Example}
```
PQS
{
    Mods
    {
        // Source: Dres (built-in)
        AerialPerspectiveMaterial
        {
            atmosphereDepth = 150000
            globalDensity = -1E-05
            heightFalloff = 6.75
            oceanDepth = 0
            DEBUG_SetEveryFrame = True
            order = 100
            enabled = True
        }
    }
}
```

## Properties {#Properties}

|Property|Format|Description|
|--------|------|-----------|
|atmosphereDepth|Decimal|The altitude of the atmosphere in m.|
|globalDensity|Decimal|Unknown, presumed to be an atmosphere density multiplier.|
|heightFalloff|Decimal|The PQSMod appears to calculate atmospheric density at the altitude of the camera. This parameter seems to control the density falloff with altitude. The suspected behavior is `localDensity = exp(-heightFalloff * observerAltitude)`.|
|oceanDepth|Decimal|The depth of the ocean in m.|
|DEBUG_SetEveryFrame|Boolean|The parameters of this PQSMod are not expected to change dynamically. As such, it ususally suffices to set them only once when the planet gets loaded. Setting this parameter to true forces an update of `globalDensity`, `heightFalloff` and `atmosphereDepth` on every frame.