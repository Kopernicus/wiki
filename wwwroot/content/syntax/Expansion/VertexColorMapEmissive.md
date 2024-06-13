VertexColorMapEmissive is a custom PQS Mod intended for use by planet modders to give more control over planetary emissives than can be achieved with EVE CityLights or KopernicusExpansion's EmissiveFX.

Users may implement emissives in ScaledVersion, PQS, and/or Ocean in any combination, and each can have different maps if desired.

## Example

The node structure is the same in all use cases except for PQS and Ocean, in which case add the normal `order` and `enabled` values.
```
VertexColorMapEmissive
{
    map = QuackPack/Textures/PluginData/CindHeatMap.dds
    brightness = 1
    transparency = 0.5
}
```
| Property     | Data Type | Description   |
|--------------|-----------|---------------|
| map          | File Path | The RGBA color map to use for the emissive overlay|
| brightness   | Decimal   | A global multiplier to apply to the RGB channels of the map. The default value is 1, smaller values will dim the emissive and larger values will brighten it.|
| opacity | Decimal   | A global multiplier to apply to the Alpha channel of the map. Default is 0.5, larger values will make the emissive more opaque (less transparent) and smaller values will make it less opaque (or more transparent).|

### ScaledVersion
```
ScaledVersion
{

    VertexColorMapEmissive
    {
        ...
    }
}
```
![A view of a planet from far away showing the emissive across the terminator.](/content/images/VCME_ScaledSpaceEmissive.png)

### PQS
```
PQS
{
    Mods
    {
        VertexColorMapEmissive
        {
            ...
        }
    }
}
```
![An up close view of the night side of a planet showing the oceans and terrain glowing.](/content/images/VCME_PQSEmissive.png)

### Ocean
```
Ocean
{
    Mods
    {
        VertexColorMapEmissive
        {
            ...
        }
    }
}
```
