The contents of the `Material {}` node can very greatly and depend on the value assigned to `materialType`

## Diffuse {#Diffuse}
This shader only renders a color texture. It does not offer a normal map or detail textures. Due to its simplicity, this shader lends itself very well to small opaque detail objects for which per-pixel normal vectors don't contribute too much to the overall appearance. The color parameter is applied multiplicatively to the output of the texture sample.

### Pseudocode {#Diffuse_Pseudocode}
```csharp
color render_pixel(vector2 texture_coordinate, vector3 surface_normal)
{
    // Load the color set in the Material node.
    color output_color = GET_PARAMETER_VALUE("color");

    // Transform the texture coordinates in accordance with the mainTexScale/Offset parameters.
    vector2 main_texture_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("mainTexScale")) + GET_PARAMETER_VALUE("mainTexOffset");

    // Sample the texture and multiply with the base color from the Material.
    output_color *= SAMPLE_TEXTURE("mainTex", main_texture_coordinates);

    // Apply shading.
    APPLY_LIGHTING(output_color, surface_normal);

    // Return the shaded, colored pixel.
    return output_color;
}
```

### Example {#Diffuse_Example}
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
}
```

## BumpedDiffuse {#BumpedDiffuse}
A cousin of the above Diffuse shader that also offers a normal map for per-pixel surface normals. This offers a more detailed appearance. If you do not intend to use a normal map, consider using the above basic Diffuse shader for performance.

### Pseudocode {#BumpedDiffuse_Pseudocode}
```csharp
color render_pixel(vector2 texture_coordinate, vector3 surface_normal)
{
    // Load the color set in the Material node.
    color output_color = GET_PARAMETER_VALUE("color");

    // Transform the texture coordinates in accordance with the mainTexScale/Offset parameters.
    vector2 main_texture_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("mainTexScale")) + GET_PARAMETER_VALUE("mainTexOffset");

    // Sample the texture and multiply with the base color from the Material.
    output_color *= SAMPLE_TEXTURE("mainTex", main_texture_coordinates);

    // Sample the normal map.
    vector2 normalmap_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("bumpMapScale")) + GET_PARAMETER_VALUE("bumpMapOffset");
    vector3 normalmap = SAMPLE_NORMALMAP("bumpMap", normalmap_coordinates);

    // Adjust the surface normal using the normal map.
    surface_normal = ADJUST_SURFACE_NORMAL(surface_normal, normalmap);

    // Apply shading, now taking the normal map into account.
    APPLY_LIGHTING(output_color, surface_normal);

    // Return the shaded, colored pixel.
    return output_color;
}
```

### Example {#BumpedDiffuse_Example}
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	bumpMap = Texture, default is "bump"
	bumpMapScale = vector2
	bumpMapOffset = vector2
}
```

## DiffuseDetail {DiffuseDetail}
Another cousin of the base Diffuse shader. This variant uses a secondary color texture that is applied multiplicatively to the color output. This allows for some further detail using a secondary, tilable detail texture without having to increase the main texture resolution.

### Pseudocode {#DiffuseDetail_Pseudocode}
```csharp
color render_pixel(vector2 texture_coordinate, vector3 surface_normal)
{
    // Load the color set in the Material node.
    color output_color = GET_PARAMETER_VALUE("color");

    // Transform the texture coordinates in accordance with the mainTexScale/Offset parameters.
    vector2 main_texture_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("mainTexScale")) + GET_PARAMETER_VALUE("mainTexOffset");

    // Sample the texture and multiply with the base color from the Material.
    output_color *= SAMPLE_TEXTURE("mainTex", main_texture_coordinates);

    // Sample the detail map and multiply it with the respective color channels.
    vector2 detailmap_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("detailScale")) + GET_PARAMETER_VALUE("detailOffset");
    output_color *= SAMPLE_TEXTURE("detail", detailmap_coordinates);

    // Apply shading.
    APPLY_LIGHTING(output_color, surface_normal);

    // Return the shaded, colored pixel.
    return output_color;
}
```

### Example {#DiffuseDetail_Example}
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	detail = Texture, default is "gray"
	detailScale = vector2
	detailOffset = vector2
}
```

## DiffuseWrapped {#DiffuseWrapped}
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	diff = Float, default is 2
}
```

## CutoutDiffuse {#CutoutDiffuse}
Another variant of the base diffuse shader. This shader uses a feature called 'opacity clip' or 'alpha clip'. Put simply, the shader discards the pixel being rendered if the alpha channel of the pixel is below some threshold cutoff value. This does not allow for smooth, overlay transparency like with glass. It will be a binary transparency.

This does still let Unity render the scatter as an opaque object instead of a transparent one, and this results in better performance* and accurate depth sorting. An example use case is foliage, which can use a quad mesh and a cutout shader to achieve a smooth, detailed leaf shape without requiring a lot of mesh vertices.

_Asterisk: this is because Unity renders opaque objects front-to-back. Nearby objects have a relatively low probability of being occluded by other objects, so rendering in this order means that Unity can skip per-pixel rendering for a lot of objects in the scene, because it can easily verify that the pixel it is looking to render is behind a pixel that it has already drawn. This is called the Z-test, and it avoids having to perform potentially costly shading and lighting calculations. If we instead render back-to-front then we will be needlessly evaluating the pixel shader: we'll render the entire surface of object A, only to draw over the output with the surface of object B, only to render over that with object C, and so on. Doing a front-to-back render greatly reduces the issue of overdraw, IE where we waste work by drawing pixels that we end up replacing._

_For transparent objects, we have no choice but to do a back-to-front render since there is no predicting which pixels will be fully opaque, and which pixels will blend with what was already drawn. In this case we have to fo a back-to-front rendering order because then we would be rendering objects in the order that a ray of light would encounter them on its trajectory toward the viewer. But if we do not need such precise blending and can get away with binary opacity, then a cutout shader lets us still use the opaque render queue, and lets us avoid a lot of overdraw and thus a lot of pointless shader evaluations._

### Pseudocode {#CutoutDiffuse_Pseudocode}
```csharp
color render_pixel(vector2 texture_coordinate, vector3 surface_normal)
{
    // Load the color set in the Material node.
    color output_color = GET_PARAMETER_VALUE("color");

    // Transform the texture coordinates in accordance with the mainTexScale/Offset parameters.
    vector2 main_texture_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("mainTexScale")) + GET_PARAMETER_VALUE("mainTexOffset");

    // Sample the texture and multiply with the base color from the Material.
    output_color *= SAMPLE_TEXTURE("mainTex", main_texture_coordinates);

    if (color.alpha < GET_PARAMETER_VALUE("cutoff"))
    {
        // The pixel is not rendered onto the screen, into shadow maps or into the depth buffer.
        // It will be as though the mesh does not exist at this point in space.
        DISCARD_PIXEL();
    }

    // Apply shading.
    APPLY_LIGHTING(output_color, surface_normal);

    // Return the shaded, colored pixel.
    return output_color;
}
```

### Example {#CutoutDiffuse_Example}
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	cutoff = Float, default is 0.5
}
```

## AerialCutout {#AerialCutout}
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	texCutoff = Float, default is 0.5
	fogColor = Color, default is 0,0,1,1
	heightFallOff = Float, default is 1
	globalDensity = Float, default is 1
	atmosphereDepth = Float, default is 1
}
```

## Standard {#Standard}

This is the slowest and most accurate shader available, because it can produce a physically accurate shading output. Its full list of features are:
- Per-pixel surface normals.
- Opacity/Alpha clipping (requires `mode = Cutout`)
- True transparency with blending control.
- Color and normal detail textures, with an optional `detailMask` parameter to control their prominence on a per-pixel level.
- The ability to choose between texture coordinate channels 0 (default) and 1 (typically used for baked lighting).
- Per-pixel ambient occlusion through a texture.
- Parallax mapping. This offsets texture samples based on a heightmap to create the illusion of per-pixel surface displacement.
- Precise (optionally per-pixel) control over the surface material properties (smoothness and metallicity).
- Emission, to create the illusion of the surface giving off light.

Given the complexity of this shader and the vagueness of the parameters to the uninitiated, we'll discuss the 'new' features in greater detail.
Note: you may also wish to consult the [Unity documentation]( https://docs.unity3d.com/2019.4/Documentation/Manual/shader-StandardShader.html) on this shader.

### Parallax Mapping {#Standard_ParallaxMapping}

See also: the [Unity Documentation]( https://docs.unity3d.com/2019.4/Documentation/Manual/StandardShaderMaterialParameterHeightMap.html) on parallax mapping.

Parallax Mapping gives the illusion of per pixel depth and displacement without generating additional geometry. It uses a simple screen-space ray-trace and a heightmap to achieve this. In essence, the logic behind it is this: we are rendering a flat surface (namely the surface of a mesh triangle), but what if, though a heightmap, we had a measure of the surface's actual shape? Through the heightmap we can tell how 'inset' the surface ought to be. So through a simple per-pixel ray-trace, we can try to step forward 'into' the surface, until we intersect where the 'true' surface would have been, using the heightmap as a guide. If we adjust the texture coordinates accordingly then this creates an illusion of surface detail that only falls apart at very shallow viewing angles.

The relevant parameters are:
| Property | Format | Description |
|---|---|---|
| parallax | Decimal | If white pixels in the heightmap are at the surface, how 'deep' would a black pixel be inset into the surface? That's what you define with this parameter. |
| parallaxMap | Texture | The heightmap to use for parallax mapping. If not set, then this feature is not used. |
| parallaxMapScale | Vector2 | Per-coordinate scaling of the texture coordinates when sampling the parallax map. |
| parallaxMapOffset | Vector2 | Per-coordinate offset of the texture coordinates when sampling the parallax map. |


### Pseudocode {#Standard_Pseudocode}
```csharp
color render_pixel(vector2 uv_channel_0, vector2 uv_channel_1, vector3 surface_normal, color current_pixel_value)
{
    // Determine the texture coordinate to use.
    vector2 texture_coordinates;
    if (GET_PARAMETER_VALUE("UVSec") == 1)
        texture_coordinates = uv_channel_1;
    else
        texture_coordinates = uv_channel_0;

    // Sample the heightmap.
    vector2 heightmap_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("parallaxMapScale")) + GET_PARAMETER_VALUE("parallaxMapOffset");
    value height = SAMPLE_TEXTURE("parallaxMap", heightmap_coordinates).red_channel;
    // Shift the texture coordinates using parallax mapping.
    texture_coordinates = APPLY_PARALLAX_MAPPING(texture_coordinates, height);


    // Load the color set in the Material node.
    color output_color = GET_PARAMETER_VALUE("color");

    // Transform the texture coordinates in accordance with the mainTexScale/Offset parameters.
    vector2 main_texture_coordinates = (texture_coordinates * GET_PARAMETER_VALUE("mainTexScale")) + GET_PARAMETER_VALUE("mainTexOffset");

    // Sample the texture and multiply with the base color from the Material.
    output_color *= SAMPLE_TEXTURE("mainTex", main_texture_coordinates);

    BlendMode mode = GET_PARAMETER_VALUE("mode");
    if (mode == Cutout)
    {
        if (color.alpha < GET_PARAMETER_VALUE("cutoff"))
        {
            // The pixel is not rendered onto the screen, into shadow maps or into the depth buffer.
            // It will be as though the mesh does not exist at this point in space.
            DISCARD_PIXEL();
        }
    }

    // Apply shading.
    APPLY_LIGHTING(output_color, surface_normal);

    // Return the shaded, colored pixel.
    return output_color;
}
```

Material
{
	color = Color, default is 1,1,1,1

    // Main texture.
    // If smoothnessTextureChannel = AlbedoAlpha then the alpha channel of this texture is sampled for per-pixel smoothness.
    mainTex = Texture, default is white
	mainTexScale = vector2
	mainTexOffset = vector2

    // Opacity clip
	cutoff = Float, default is 0.5, Alpha Cutoff

    // Smoothness source control
    smoothnessTextureChannel = TextureChannel, default is MetallicAlpha (0), can be either MetallicAlpha (0) or AlbedoAlpha (1)

    // Smoothness scale
    glossiness = Float, default is 0.5, smoothness

    // Standalone smoothness control (when using AlbedoAlpha?)
    glossMapScale = Float, default is 1

    // Metallicity control
    metallic = Float, default is 0

    // Per-pixel metallicity & smoothness texture. If smoothnessTextureChannel = MetallicAlpha then the alpha channel of this texture is sampled for per-pixel smoothness.
    metallicGlossMap = Texture, default is "white"
    metallicGlossMapScale = Vector2
    metallicGlossMapOffset = Vector2

    // If True, lights shining upon this surface create a highlight where the surface normal causes most light to reflect toward the camera.
    // Imagine the glare of the sun reflecting off a car.
    specularHighlights = Boolean, default is true

    // If true, then smooth surfaces will use reflection probe data to reflect the environment light.
    // If false, you will not see the environment reflected in smooth surfaces.
    glossyReflections = Boolean, default is true

    // Main normal map settings.
    bumpScale = Float, default is 1
    bumpMap = Texture, default is "bump"
	bumpMapScale = vector2
	bumpMapOffset = vector2

    // Parallax mapping settings.
    parallax = Float, default is 0.02, height scale
    parallaxMap = Texture, default is black
    parallaxMapScale = Vector2
    parallaxMapOffset = Vector2

    // Ambient occlusion settings.
    occlusionStrength = Float, default is 1
    occlusionMap = Texture, default is white
    occlusionMapScale = Vector2
    occlusionMapOffset = Vector2

    // Emissivity settings.
    emissionColor = Color, default is 0,0,0,1
    emissionMap = Texture, default is white
    emissionMapScale = Vector2
    emissionMapOffset = Vector2

    // Detail mask: a black/white texture providing per-pixel prominence of detail textures.
    detailMask = Texture, default is white
    detailMaskOffset = Vector2
    detailMaskScale = Vector2

    // Detail color map that gets blended with the color data.
    detailAlbedoMap = Texture, default is grey
    detailAlbedoMapOffset = Vector2
    detailAlbedoMapScale = Vector2

    // Detail normal map that gets applied on top of the existing bump map to add additional details.
    detailNormalMap = Texture, default is bump
    detailNormalMapScale = Vector2
    detailNormalMapOffset = Vector2

    // Control over which UV channel is used.
    UVSec = UvSet, either 0 or 1, default is 0

    // Which blending mode should be used. Options are Opaque, Cutout, Fade and Transparent with numeric values 0, 1, 2 and 3 respectively.
    mode = BlendMode, default is 0

    // The source and destination blending mode.
    // Full articles by Unity: https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Rendering.BlendMode.html
    // https://docs.unity3d.com/2019.4/Documentation/Manual/SL-Blend.html
    srcBlend = Float, default is 1
    dstBlend = Float, default is 0

    // Whether or not the pixel shader should store its distance from the camera in the depth buffer. 0 = no, 1 = yes.
    ZWrite = Float, default is 0
}
```
