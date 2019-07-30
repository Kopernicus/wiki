---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

# This is the base Jekyll theme. You can find out more info about customizing your Jekyll theme, as well as basic Jekyll usage documentation at [jekyllrb.com](https://jekyllrb.com/)

# You can find the source code for Minima at GitHub:
# [jekyll][jekyll-organization] /
# [minima](https://github.com/jekyll/minima)

layout: default
title: Kopernicus Wiki
subtitle: A mod to modify the planetary system used by KSP
---

# Prerequisites
* [What are ConfigNodes?]({{ site.baseurl }}{% link main/ConfigNodes.md %})
* [A Beginner's Guide to Kopernicus: The Basics](https://forum.kerbalspaceprogram.com/index.php?/topic/129540-a-beginners-guide-to-kopernicus-the-basics/)
* [Data Types]({{ site.baseurl }}{% link main/datatypes.md %})

## Syntax for planets
* [Body node]({{ site.baseurl }}{% link main/Body.md %})
  + [Template subnode]({{{ site.baseurl }}{% link main/Template.md %})
  + [Properties subnode]({{ site.baseurl }}{% link main/Properties.md %})
    - [ScienceValues subnode]({{ site.baseurl }}{% link main/Properties/ScienceValues.md %})
    - [Biomes subnode]({{ site.baseurl }}{% link main/Properties/Biome.md %})
  + [Orbit subnode]({{ site.baseurl }}{% link main/Orbit.md %})
  + [ScaledVersion subnode]({{ site.baseurl }}{% link main/ScaledVersion.md %})
    - Material subnode
  + [Rings subnode]({{ site.baseurl }}{% link main/Rings.md %})
  + Atmosphere subnode
    - AtmosphereFromGround subnode
  + PQS subnode
    - [PQSMod subnodes]({{ site.baseurl }}{% link PQSMods/PQSMods.md %})
  + Ocean subnode
  + [HazardousBody subnode]({{ site.baseurl }}{% link main/HazardousBody.md %})
  + [Particles subnode]({{ site.baseurl }}{% link main/Particle.md %})
  + [Debug subnode]({{ site.baseurl }}{% link main/Debug.md %})

## Syntax for stars
* [Body node]({{ site.baseurl }}{% link main/Body.md %})
  + [Template subnode]({{{ site.baseurl }}{% link main/Template.md %})
  + [Properties subnode]({{ site.baseurl }}{% link main/Properties.md %})
    - [ScienceValues subnode]({{ site.baseurl }}{% link main/Properties/ScienceValues.md %})
  + [Orbit subnode]({{ site.baseurl }}{% link main/Orbit.md %})
  + [ScaledVersion subnode]({{ site.baseurl }}{% link main/ScaledVersion.md %})
    - Material subnode
    - [Light subnode]({{ site.baseurl }}{% link main/ScaledVersion/Light.md %})
    - [Coronas subnode]({{ site.baseurl }}{% link main/ScaledVersion/Corona.md %})
  + [Rings subnode]({{ site.baseurl }}{% link main/Rings.md %})
  + Atmosphere subnode
  + Ocean subnode
  + HazardousBody subnode
  + Particles subnode
  + [Debug subnode]({{ site.baseurl }}{% link main/Debug.md %})

# KopernicusExpansion
### WARNING: These pages are not intended for beginners, and a basic level of experience is assumed.
*   [Comet Tails]({{ site.baseurl }}{% link kex/CometTails.md %})
*   [Emissive FX]({{ site.baseurl }}{% link kex/EmissiveFX.md %})
*   [EVA Footprints]({{ site.baseurl }}{% link kex/EVAFootprints.md %})
*   Modular Noise
*   [Procedural Gas Giants]({{ site.baseurl }}{% link kex/ProceduralGasGiants.md %})
*   [Reentry Effects]({{ site.baseurl }}{% link kex/ReentryEffects.md %})
*   Regional PQS Mods
*   [VertexHeightDeformity]({{ site.baseurl }}{% link kex/VertexHeightDeformity.md %})
*   [VertexHeightMap16]({{ site.baseurl }}{% link kex/VertexHeightMap16.md %})
*   [Wormholes]({{ site.baseurl }}{% link kex/Wormholes.md %})
