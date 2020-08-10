---
# Feel free to add content and custom Front Matter to this file
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

# This is the base Jekyll theme. You can find out more info about customizing your Jekyll theme, as well as basic Jekyll usage documentation at [jekyllrb.com](https://jekyllrb.com/)

# You can find the source code for Minima at GitHub
# [jekyll][jekyll-organization] /
# [minima](https://github.com/jekyll/minima)

layout: default
title: Kopernicus Wiki
subtitle: A mod to modify the planetary system used by KSP
---

## Prerequisites
* [What are ConfigNodes?]({{ site.baseurl }}{% link main/ConfigNodes.md %})
* [A Beginner's Guide to Kopernicus: The Basics](https://forum.kerbalspaceprogram.com/index.php?/topic/129540-a-beginners-guide-to-kopernicus-the-basics/)
* [Data Types]({{ site.baseurl }}{% link main/DataTypes.md %})

## External Resources
* [Kopernicus Asteroids - OhioBob](https://www.dropbox.com/s/lag8opde3zimjqc/KopernicusAsteroids.pdf?dl=0)
* [VoronoiCraters PQSMod - OhioBob](https://www.dropbox.com/s/fnd0bblv5otqlhc/KSP_VoronoiCraters.pdf?dl=0)
* [Planetary Texturing Guide Repository](https://forum.kerbalspaceprogram.com/index.php?/topic/165285-planetary-texturing-guide-repository/)
* [List of BUILTIN Textures for 1.8](https://github.com/GER-Space/Kerbal-Konstructs/wiki/Builtin-Textures-for-KSP-1.8)

## Syntax for Celestial Bodies
* [Body node]({{ site.baseurl }}{% link main/Body.md %})
  + [Template subnode]({{ site.baseurl }}{% link main/Template.md %})
  + [Properties subnode]({{ site.baseurl }}{% link main/Properties.md %})
    - [ScienceValues subnode]({{ site.baseurl }}{% link main/Properties/ScienceValues.md %})
    - [Biomes subnode]({{ site.baseurl }}{% link main/Properties/Biome.md %})
  + [Orbit subnode]({{ site.baseurl }}{% link main/Orbit.md %})
  + [ScaledVersion subnode]({{ site.baseurl }}{% link main/ScaledVersion.md %})
    - [Material subnode]({{ site.baseurl }}{% link main/ScaledVersion/Material.md %})
    - [OnDemand subnode]({{ site.baseurl }}{% link main/ScaledVersion/OnDemand.md %})
    - [Light subnode]({{ site.baseurl }}{% link main/ScaledVersion/Light.md %}) **STARS ONLY**
    - [Coronas subnode]({{ site.baseurl }}{% link main/ScaledVersion/Corona.md %}) **STARS ONLY**
  + [Rings subnode]({{ site.baseurl }}{% link main/Rings.md %})
  + [Atmosphere subnode]({{ site.baseurl }}{% link main/Atmosphere.md %})
    - [AtmosphereFromGround subnode]({{ site.baseurl }}{% link main/Atmosphere/AtmosphereFromGround.md %}) **NON-STARS ONLY**
  + [PQS subnode]({{site.baseurl}}{% link main/PQS.md %}) **NON-STARS ONLY**
    - [PQSMod subnodes]({{ site.baseurl }}{% link main/PQSMods/PQSMods.md %}) **NON-STARS ONLY**
  + [Ocean subnode]({{ site.baseurl }}{% link main/Ocean.md%}) **NON-STARS ONLY**
    - Fog subnode **NON-STARS ONLY**
  + [SpaceCenter subnode]({{site.baseurl}}{% link main/SpaceCenter.md %}) **HOMEWORLD ONLY**
  + [HazardousBody subnode]({{ site.baseurl }}{% link main/HazardousBody.md %})
  + [Debug subnode]({{ site.baseurl }}{% link main/Debug.md %})
  
  ***WARNING - The Particles subnode has been removed in Kopernicus 1.8.1-1, with no replacement. Only use this node if your pack is restricted to KSP 1.7.3 or earlier.***
  + [~~Particles subnode~~]({{ site.baseurl }}{% link main/Particles.md %})

## KopernicusExpansion
**WARNING: These pages are not intended for beginners, and a basic level of experience is assumed.**
* [Comet Tails]({{ site.baseurl }}{% link kex/CometTails.md %})
* [Emissive FX]({{ site.baseurl }}{% link kex/EmissiveFX.md %})
* [EVA Footprints]({{ site.baseurl }}{% link kex/EVAFootprints.md %})
* Modular Noise
* [Procedural Gas Giants]({{ site.baseurl }}{% link kex/ProceduralGasGiants.md %})
* [Reentry Effects]({{ site.baseurl }}{% link kex/ReentryEffects.md %})
* Regional PQS Mods
* [VertexHeightDeformity]({{ site.baseurl }}{% link kex/VertexHeightDeformity.md %})
* [VertexHeightMap16]({{ site.baseurl }}{% link kex/VertexHeightMap16.md %})
* [Wormholes]({{ site.baseurl }}{% link kex/Wormholes.md %})
