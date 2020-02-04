---
layout: default
title: HazardousBody
---

The `HazardousBody { }` subnode of the `Body { }` node indicates that the body is hazardous, and thus, the body will gradually apply heat to the vessel as the vessel nears the body. It replaces `HazardousOceans { }` as the `HazardousBody { }` has more fine control and detail.

**Example**
```
Body
{
  HazardousBody
  {
    Instance
    {
      ambientTemp = 270
      biomeName = Sargasso Seas
      sumTemp = true
      HeatMap = MyMod/PluginData/MyPlanet/heatmap.dds

      AltitudeCurve
      {
        key = 0 1
        key = 20000 0.9
        key = 40000 0.75
        key = 75000 0.5
        key = 100000 0.35
        key = 150000 0.1
        key = 200000 0
      }
      LatitudeCurve
      {
        key = -75 0.1
        key = -30 0.7
        key = 0 1
        key = 30 0.7
        key = 75 0.1
      }
      LongitudeCurve
      {
        key = -160 1
        key = -125 0.2
        key = -75 0.8
        key = -35 0.3
        key = 0 1
        key = 35 0.5
        key = 75 0.9
        key = 125 0.1
        key = 165 0.6
      }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|sumTemp|Boolean|Whether or not to add the ambientTemp to the calculated temperature.|
|biomeName|String|Optional. This limits this instance of HazardousBody to the specified biome.|
|ambientTemp|Double|Optional, defaults to 0. This is the base temperature before applying modifiers such as the Alt/Lon/Lat Curves and the HeatMap. After all such modifiers are applied, if the new temperature is higher than KSP's default ambient temperature, then the new one will be applied. If KSP's is higher, KSP's shall be used instead.|
|HeatMap|File Path|Optional. A greyscale map for fine control of the ambient temperature. It acts as a multiplier map. Black = 0, White = 1.|
|AltitudeCurve|FloatCurve|Optional, defaults to 1 at all altitudes. A multiplier of the average heat that gets applied at a certain altitude.|
|LatitudeCurve|FloatCurve|Optional, defaults to 1 at all latitudes. A multiplier of the average heat that gets applied at a certain latitude.|
|LongitudeCurve|FloatCurve|Optional, defaults to 1 at all longitudes. A multiplier of the average heat that gets applied at a certain longitude.|
