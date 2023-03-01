---
# Feel free to add content and custom Front Matter to this file
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

# This is the base Jekyll theme. You can find out more info about customizing your Jekyll theme, as well as basic Jekyll usage documentation at [jekyllrb.com](https://jekyllrb.com/)

# You can find the source code for Minima at GitHub
# [jekyll][jekyll-organization] /
# [minima](https://github.com/jekyll/minima)

layout: default
title: Kopernicus Wiki
---

## Prerequisites
* [What are ConfigNodes?]({{ site.baseurl }}{% link content/ConfigNodes.md %})
* [A Beginner's Guide to Kopernicus: The Basics](https://forum.kerbalspaceprogram.com/index.php?/topic/129540-a-beginners-guide-to-kopernicus-the-basics/)
* [Data Types]({{ site.baseurl }}{% link content/datatypes.md %})

## External Resources
* [Kopernicus Asteroids - OhioBob](https://www.dropbox.com/s/lag8opde3zimjqc/KopernicusAsteroids.pdf?dl=0)
* [VoronoiCraters PQSMod - OhioBob](https://www.dropbox.com/s/fnd0bblv5otqlhc/KSP_VoronoiCraters.pdf?dl=0)
* [Planetary Texturing Guide Repository](https://forum.kerbalspaceprogram.com/index.php?/topic/165285-planetary-texturing-guide-repository/)
* [List of BUILTIN Textures for 1.8](https://github.com/GER-Space/Kerbal-Konstructs/wiki/Builtin-Textures-for-KSP-1.8)

## Syntax for Celestial Bodies
* [Body node]({{ site.baseurl }}{% link content/Body.md %})
  + [Template subnode]({{ site.baseurl }}{% link content/Template.md %})
  + [Properties subnode]({{ site.baseurl }}{% link content/Properties/Properties.md %})
    - [ScienceValues subnode]({{ site.baseurl }}{% link content/Properties/ScienceValues.md %})
    - [Biomes subnode]({{ site.baseurl }}{% link content/Properties/Biome.md %})
  + [Orbit subnode]({{ site.baseurl }}{% link content/Orbit.md %})
  + [ScaledVersion subnode]({{ site.baseurl }}{% link content/ScaledVersion/ScaledVersion.md %})
    - [Material subnode]({{ site.baseurl }}{% link content/ScaledVersion/Material.md %})
    - [OnDemand subnode]({{ site.baseurl }}{% link content/ScaledVersion/OnDemand.md %})
    - [Light subnode]({{ site.baseurl }}{% link content/ScaledVersion/Light.md %}) **STARS ONLY**
    - [Coronas subnode]({{ site.baseurl }}{% link content/ScaledVersion/Corona.md %}) **STARS ONLY**
  + [Rings subnode]({{ site.baseurl }}{% link content/Rings.md %})
  + [Atmosphere subnode]({{ site.baseurl }}{% link content/Atmosphere/Atmosphere.md %})
    - [AtmosphereFromGround subnode]({{ site.baseurl }}{% link content/Atmosphere/AtmosphereFromGround.md %}) **NON-STARS ONLY**
  + [PQS subnode]({{site.baseurl}}{% link content/PQSMods/PQS.md %}) **NON-STARS ONLY**
    - [PQSMod subnodes]({{ site.baseurl }}{% link content/PQSMods/PQSMods.md %}) **NON-STARS ONLY**
  + [Ocean subnode]({{ site.baseurl }}{% link content/Ocean.md%}) **NON-STARS ONLY**
    - Fog subnode **NON-STARS ONLY**
  + [SpaceCenter subnode]({{site.baseurl}}{% link content/SpaceCenter.md %}) **HOMEWORLD ONLY**
  + [HazardousBody subnode]({{ site.baseurl }}{% link content/HazardousBody.md %})
  + [Debug subnode]({{ site.baseurl }}{% link content/Debug.md %})
  
  ***WARNING - The Particles subnode has been removed for KSP 1.8.1 and later versions. Only use this node if your mod does not support versions after KSP 1.7.3.***
  + [Particles subnode (Pre-1.8)]({{ site.baseurl }}{% link content/Particles-old.md %})

## KopernicusExpansion
> **WARNING: These pages are not intended for beginners, and a basic level of experience is assumed.**
* [Comet Tails]({{ site.baseurl }}{% link content/kex/CometTails.md %})
* [Emissive FX]({{ site.baseurl }}{% link content/kex/EmissiveFX.md %})
* [EVA Footprints]({{ site.baseurl }}{% link content/kex/EVAFootprints.md %})
* Modular Noise
* [Procedural Gas Giants]({{ site.baseurl }}{% link content/kex/ProceduralGasGiants.md %})
* [Reentry Effects]({{ site.baseurl }}{% link content/kex/ReentryEffects.md %})
* Regional PQS Mods
* [VertexHeightDeformity]({{ site.baseurl }}{% link content/kex/VertexHeightDeformity.md %})
* [VertexHeightMap16]({{ site.baseurl }}{% link content/kex/VertexHeightMap16.md %})
* [Wormholes]({{ site.baseurl }}{% link content/kex/Wormholes.md %})
