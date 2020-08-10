---
layout: default
title: Body
subtitle: Bodies lie dead on the floor...
---

The `Body { }` node contains all the aspects of a body and describes the essential components for making a body. `Body { }` is a subnode of the `@Kopernicus` node.

**Subnodes**
- [Template { }]({{ site.baseurl }}{% link main/Template.md %})
- [Properties { }]({{ site.baseurl }}{% link main/Properties.md %})
- [Orbit { }]({{ site.baseurl }}{% link main/Orbit.md %})
- [ScaledVersion { }]({{ site.baseurl }}{% link main/ScaledVersion.md %})
- [Atmosphere { }]({{ site.baseurl }}{% link main/Atmosphere.md %})
- [PQS { }]({{ site.baseurl }}{% link main/PQS.md %})
- [Ocean { }]({{ site.baseurl }}{% link main/Ocean.md %})
- [Rings { }]({{ site.baseurl }}{% link main/Rings.md %})
- [~~Particles { }~~]({{ site.baseurl }}{% link main/Particles.md %}) *(Removed in 1.8.1-1!)*
- [HazardousBody { }]({{ site.baseurl }}{% link main/HazardousBody.md %})
- [SpaceCenter { }]({{ site.baseurl }}{% link main/SpaceCenter.md %})
- [Debug { }]({{ site.baseurl }}{% link main/Debug.md %})
- PostSpawnOrbit { }

**Example**
```
@Kopernicus:AFTER[Kopernicus]
{
    Body
    {
        name = Pear
        cacheFile = Fruits/Cache/Pear.bin
        barycenter = false
        identifier = Fruits/Pear
        randomMainMenuBody = true
        contractWeight = 15

        Debug
        {
            ...
        }
        Template
        {
            ...
        }
        Properties
        {
            ...
        }
        Orbit
        {
            ...
        }
        ScaledVersion
        {
            ...
        }
        PQS
        {
            ...
        }
        Ocean
        {
            ...
        }
    }
```

|Property|Format|Description|
|--------|------|-----------|
|name|String|The name of the body.|
|cacheFile|File Path|The path to the cache file for the body.|
|barycenter|Boolean|Whether the body should act as a barycenter. Also makes the body unselectable.|
|cbNameLater|String|(Deprecated, use `Properties/displayName` to change the name instead.) Applies a name change after loading the body.|
|identifier|String|The Unique Body Identifier (UBI) for the body. Used in the [Interstellar Consortium](https://forum.kerbalspaceprogram.com/index.php?/topic/177439-kopernicus-interstellar-consortium/) and follows the format `System/Body`.|
|self|List of String[]|(Optional) The UBIs that the body implements. Each line in this node should have the key "implements" and have a String[] as a value.|
|finalizeOrbit|Boolean|Whether the orbit of the body should be finalized.|
|randomMainMenuBody|Boolean|Whether the body should have a chance at being displayed on the Main Menu.|
|contractWeight|Integer|How often contracts should be generated for a body. Default is 30.|
