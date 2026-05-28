# What's new in Kopernicus 245?

We're definitely not going to do this every release, but 245 has some big changes
that we figure people want to know about.

## Custom Shader Support
With 245, any time you have a `Material` block in your config you can now
add a `shader` key to explicitly set which shader you want it to use. This
works for any shader, not just stock ones, though you will need to use
something like Shabby to load the shaders into KSP.

As an example, this makes Minmus' PQS terrain use Unity's internal error shader
```
@Kopernicus:AFTER[Kopernicus]
{
    @Body[Minmus]
    {
        @PQS
        {
            !Material { }

            Material
            {
                shader = Hidden/InternalErrorShader
            }
        }
    }
}
```

## Arbitrary Material Properties
Sometimes you want to set a shader property that is not covered by the existing
material loader. This is especially true if you are using a custom shader, since
most of the time those will not have a custom material loader.

With 245 you can now set any shader property whose name starts with `_` directly
in the `Material` node. This will automatically parse it as the right type 
(e.g. float, color, texture2d, etc) and also transparently handles loading
textures on-demand.

Here's an example
```
Material
{
    shader = MyMod/CustomShader

    _MainTex = MyMod/PluginData/MyPlanet/MyPlanetColor.dds
    _Color   = RGBA(255, 0, 0, 255)
    _Scale   = 7.5
    _Offset  = 1, 2, 3, 4
}
```

For textures, you can also set the offset and scale by using `_TexScale` and
`_TexOffset` properties, like this:
```
Material
{
    _MainTex = MyMod/PluginData/MyPlanet/MyPlanetColor.dds
    _MainTexScale = 2, 2
    _MainTexOffset = 0.1, 0.5
}
```

For custom shaders without their own loader (i.e. not stock materials) you can
also directly set shader keywords by using the `keyword` property. You can
pass multiple to set multiple keywords. Example:

```
Material
{
    shader = MyMod/CustomShader

    keyword = ENABLE_LIGHTING
    keyword = ENABLE_BLORPS

    _MainTex = MyMod/PluginData/MyPlanet/MyPlanetColor.dds
}
```

## Custom Material Loaders
Sometimes you need custom code to drive your shader. Sometimes you want to
provide nice names for shader properties rather than using the defaults.
Now you can!

To register one, create a class that inherits from `MaterialLoader`, then
stick a `[MaterialLoader("YourMod/Shader")]` attribute on it. Kopernicus
will automatically use it when someone creates a material with
`shader = YourMod/Shader`.

Here's how implementing one would look
```cs
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.Configuration.Attributes;
using Kopernicus.Configuration.MaterialLoader;
using Kopernicus.Configuration.MaterialLoader.Parsing;
using UnityEngine;

[RequireConfigType(ConfigType.Node)]
[MaterialLoader("YourMod/Shader")]
public class YourModShaderLoader : CustomMaterialLoader
{
    // Here's a texture target
    [ParserTarget("mainTex")]
    public MaterialTextureParser MainTex
    {
        get => GetTextureName("_MainTex");
        set => SetTexture("_MainTex", value);
    }

    // Or a color
    [ParserTarget("color")]
    public ColorParser Color
    {
        get => GetColor("_Color");
        set => SetColor("_Color", value);
    }

    // or a float
    [ParserTarget("scale")]
    public NumericParser<float> Scale
    {
        get => GetFloat("_Scale");
        set => SetFloat("_Scale", value);
    }

    // There are multiple constructors available, the ones that take a string
    // will get passed the name of the shader. This is useful if you want to
    // use the same loader for multiple shaders.
    //
    // CustomMaterialLoader will also take care of this for you.

    public YourModShaderLoader() : base() { }
    public YourModShaderLoader(Material material) : base(material) { }
    // public YourModShaderLoader(Material material, string shaderName) { }
}
```

If your shader needs a custom component then there are callbacks you can override
to make that work too. Make sure to call the base class ones, though, because
they set up on-demand texture handling.

```cs
// Called when the material is used as a PQS terrain material.
public virtual void OnParentApply(PQS pqs, PQSMod_OnDemandHandler handler);

// Called when the material is used within a PQSMod.
public virtual void OnParentApply(IPQSModWithMaterial mod, PQSMod_OnDemandHandler handler);

// Called when the material is used on a scaled space object.
public virtual void OnParentApply(GameObject scaledBody);
```

If you want your shader to only be usable in certain conditions, then you can
throw a descriptive error message from one of these methods and that will cause
kopernicus to fail to load the system.

## Animated Gas Giant Shaders
KSP 1.10 introduced a new animated texture for gas giant shaders. You can use
it by setting `type = GasGiant` in your `ScaledVersion` node.

See the [1.10 modders notes post][modders-notes] for documentation on how what
the various parameters and textures do, and see [GasGiantLoader.cs] for how
Kopernicus exposes them. More documentation will be coming for this soon.

[modders-notes]: https://forum.kerbalspaceprogram.com/topic/195178-modders-notes-1100/
[GasGiantLoader.cs]: https://github.com/Kopernicus/Kopernicus/blob/master/src/Kopernicus/Configuration/MaterialLoader/GasGiantLoader.cs
