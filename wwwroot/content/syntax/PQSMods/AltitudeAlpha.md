**Internal mod name:** `PQSMod_AltitudeAlpha`

AltitudeAlpha, much like [AerialPerspectiveMaterial](/Syntax/PQSMods/AerialPerspectiveMaterial), is a PQSMod that operates on mesh data for the sake of the terrain shader. While AerialPerspectiveMaterial seems to directly modify material properties, AltitudeAlpha (as its name implies) passes altitude data to the alpha channel of the color data of the mesh vertices. How this is used in the terrain shader is unknown.

## Example {#Example}
```
PQS
{
    Mods
    {
        // Source: Dres (built-in)
        AltitudeAlpha
        {
            atmosphereDepth = 4000
            invert = False
            order = 999999999
            enabled = False
        }
    }
}
```

## Properties {#Properties}

|Property|Format|Description|
|--------|------|-----------|
|atmosphereDepth|Decimal|The sea-level-relative altitude of the vertex gets divided by this value to obtain the alpha value.|
|invert|Boolean|If true, the alpha data is inverted. By default, it has alpha=0 at sea level and alpha=1 at `atmosphereDepth`. This parameter inverts this gradient if need be.|