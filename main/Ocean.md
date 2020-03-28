---
layout: default
title: Ocean
---

The `Ocean { }` subnode contains all of the information needed to produce an ocean for the specified body. Note that the ocean is one of two Procedural Quad Spheres on the body it is applied to. This means that some PQS Settings can be applied to Oceans.

**Subnodes**
* Material { }
* FallbackMaterial { }
* Fog { }

**Example**
```
Ocean
{
    ocean = True/False
    oceanColor = Color, color of the ocean in the map view
    oceanHeight = Float
    density = Float
    minLevel = Integer
    maxLevel = Integer
    minDetailDistance = Float
    maxQuadLengthsPerFrame = Float
  Material
  {
    color = Color as seen from up close, default is 1,1,1,1
    colorFromSpace = Color as seen from far away (how do color maps affect this?), default is 1,1,1,1
    specColor = Color, default = 1,1,1,1
    shininess = Float, default = 0.078125
    gloss = Float, default = 0.078125
    tiling = Float, default = 1
    waterTex = Texture, default is "white"
    waterTexScale = X,Y
    waterTexOffset = X,Y
    waterTex1 = Texture, default is "white"
    waterTex1Scale = X,Y
    waterTex1Offset = X,Y
    bTiling = Float, default = 1 // Bump Map Tiling
    bumpMap = Texture, default is "bump"
    bumpMapScale = X,Y
    bumpMapOffset = X,Y
    displacement = Float, default is 1 // Water Movement
    texDisplacement = Float, default is 1 // Texture Displacement (what does this mean)
    dispFreq = Float, default is 1, the frequency with which the water moves.
    mix = Float, default is one, ??????
    oceanOpacity = Float, default is 1, opacity of the ocean surface
    falloffPower = Float, default is 1, ???
    falloffExp = Float, default is 2
    fogColor = Color, default is 0,0,1,1, AP Fog Color (what is that)
    heightFallOff = Float, default = 1, AP Height Fall Off
    globalDensity = Float, default is 1, AP Global Density
    atmosphereDepth = Float, default is 1, AP Atmosphere Depth
    fogColorRamp = Gradient
    fogColorRampScale = X,Y
    fogColorRampOffset = X,Y
    fadeStart = Float, default is 1
    fadeEnd = Float, default is 1
    planetOpacity = Float, default = 1
    normalXYFudge = Float, default = 0.1
    normalZFudge = Float, default = 1.1
  }
  FallbackMaterial
  {
    color = Color as seen from up close, default is 1,1,1,1
    colorFromSpace = Color as seen from far away (how do color maps affect this?), default is 1,1,1,1
    specColor = Color, default = 1,1,1,1
    shininess = Float, default = 0.078125
    gloss = Float, default = 0.078125
    tiling = Float, default = 1
    waterTex = Texture, default is "white"
    waterTexScale = X,Y
    waterTexOffset = X,Y
    waterTex1 = Texture, default is "white"
    waterTex1Scale = X,Y
    waterTex1Offset = X,Y
    fadeStart = Float, default is 1
    fadeEnd = Float, default is 1
    planetOpacity = Float, default = 1
  }
  Mods
  {
    // just put PQSMods here. WHether they work is a different story
  }
  Fog
  {
    afgAltMult = Float
    afgBase = Float
    afgLerp = True/False
    afgMin = Float
    fogColorEnd = Color
    fogColorStart = Color
    fogDensityAltScalar = Float
    fogDensityExponent = Float
    fogDensityPQSMult = Float
    fogDensityStart = Float
    skyColorMult = Float
    skyColorOpacityAltMult = Float
    skyColorOpacityBase = Float
    sunAltMult = Float
    sunBase = Float
    sunMin = Float
    useFog = True/False
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|ocean|Boolean|Whether the ocean is enabled.|
|oceanColor|Color|The color of the ocean on the map.|
|oceanHeight|Double|The height of the ocean in meters.|
|density|Double|The density of the ocean in g/m3. 1 is the density of actual water.|
|minLevel|Integer|The PQS minimum level of detail.|
|maxLevel|Integer|The PQS maximum level of detail.|
|minDetailDistance|Double|The minimum detail distance of ???.|
|maxQuadLengthsPerFrame|Single|?|