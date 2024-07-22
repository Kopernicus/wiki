Each `Body { }` may have a node called `Rings { }`, a wrapper node containing one or more `Ring { }` nodes, each of which defines a planetary ring. These may be flat discs as with Saturn, loops of ribbon as with the Ringworld, or voluminous cylinders.

Distance units for rings are milliradii, thousandths of the radius of the parent body. So 1000 is 1 radius, 2000 is 2 radii, etc. This way, rings automatically scale with the planet.

## Properties {#Properties}
For the sake of clarity, we will discuss the parameters of the ring in several categories.

### Shape Properties {#Shape_Properties}
The parameters `innerRadius` and `outerRadius` are self-explanatory, but the `InnerRadiusMultiplier` and `OuterRadiusMultiplier` less so. These FloatCurves exist **for non-circular rings**. They essentially map angle (degrees, x coordinate) to radius multiplier (y coordinate). This gives precise and total control over the shape of the ring.

|Property|Format|Description|
|--------|------|-----------|
|innerRadius|Decimal|The distance from center of parent to inner edge of ring in milliradii.|
|outerRadius|Decimal|The distance from center of parent to outer edge of ring in milliradii.|
|InnerRadiusMultiplier|FloatCurve|A curve that defines a multiplier for the inner radius using an angle. The first value is an angle in degrees, while the second is the multiplier. Allows for the deformation of rings.|
|OuterRadiusMultiplier|FloatCurve|Similar to `InnerRadiusMultiplier`, but for the outer radius rather than the inner radius.|
|thickness|Decimal|The distance between top and bottom faces of ring in milliradii.|
|steps|Integer|The amount of vertices around the ring.|

### Placement Properties {#Placement_Properties}

|Property|Format|Description|
|--------|------|-----------|
|angle|Decimal|The angle in degrees between the plane of the ring and the equatorial plane of the parent planet.|
|longitudeOfAscendingNode|Decimal|Angle in degrees between the absolute reference direction and the ascending node. Works just like the corresponding property on celestial bodies. Only effective if `lockRotation` is true.|
|lockRotation|Boolean|Whether to lock the rotation of the ring. If false, the ring's LAN rotates with the parent body (unnatural if the `angle` is not 0).|
|rotationPeriod|Decimal|The number of seconds for the ring to complete one rotation. If zero, it will default to the parent body's `rotationPeriod`. Only noticeable if `tiles` is not 0.|

### Appearance Properties {#Appearance_Properties}

These properties define how the ring is shaded.

|Property|Format|Description|
|--------|------|-----------|
|texture|File Path|The path to the ring texture.|
|color|Color|A tint applied to the ring.|
|unlit|Boolean|Whether to apply an Unlit/Transparent shader instead of a Transparent/Diffuse shader.|
|useNewShader|Boolean|Whether to use the new custom ring shader that includes a planet shadow instead of the built-in Unity shaders.|
|penumbraMultiplier|Decimal|A penumbra multiplier to the NewShader. Makes planet shadow softer (values larger than one) or less soft (smaller than one). Softness still depends on distance from sun, distance from planet and radius of sun and planet.|
|tiles|Integer|Number of times the texture should be tiled around the cylinder. If zero, use the old behavior of sampling a thin diagonal strip from (0,0) to (1,1). Look at the example above for more info.|
|innerShadeTexture|File Path|The path to the texture whose opaque pixels cast shadows on the ring's inner surface.|
|innerShadeTiles|Integer|The `innerShadeTexture` repeats this many times over the inner surface.|
|innerShadeRotationPeriod|Decimal|The number of seconds the `innerShadeTexture` takes to complete one rotation.|

### Fadeout Properties {#Fadeout_Properties}

_Note: the parameters in this sub-section are optional and apply only if_ `useNewShader = True`.

Rings typically occupy a sizable chunk of the host body's sphere of influence. As such, it is not unusual for vessels to pass through the rings. Unfortunately this by default introduces an immersion breaking experience when the screen the ring's mesh. To solve this, the custom ring shader (used when `useNewShader = True`) includes a fade-out that is applied if the camera is close to the rings.

By default, the rings start to fade out smoothly from 100 Unity units and closer, becoming fully transparent at or under 20 Unity units from the camera.

These parameters offer explicit control over this fade-out mechanic.

|Property|Format|Description|
|--------|------|-----------|
|fadeoutStartDistance|Decimal|The distance to the camera, in Unity units, whereat the rings should start fading out. Default is 100.|
|fadeoutStoptDistance|Decimal|The distance to the camera, in Unity units, whereat the rings should be fully faded out. Default is 20.|
|fadeoutMinAlpha|Decimal|The minimum opacity of the rings. Zero by default. Can be set to 1 to disable fadeout entirely.|

### Detail Properties {#Detail_Properties}

_Note: the parameters in this sub-section are optional and apply only if_ `useNewShader = True`.

By default, a single texture is used for a ring that spans thousands of miles or km. Using a single texture to obtain fine details would require a huge amount of pixels and this is not acceptable for obvious reasons. To solve this, the custom ring shader allows for two detail passes.

All of the detail parameters must be defined under a Detail node which itself is nested in the Ring node.

The Detail node itseld contains the following parameters.

|Property|Format|Description|
|--------|------|-----------|
|detailRegionsTexture|File Path|A path to a secondary ring texture wherein the RGBA color channels regulate the prominence of the respective channels of the two detail textures at the given mask pixel's position in the ring. Use this to control which detail textures are shown at which position.|
|detailRegionsMask|4D Vector|Multiplied with the sample of the regions texture. Use this to add or reduce the strength of color channels. Creative users may even use this feature to share textures between rings.

### Detail Passes {#Detail_Passes}
These properties exist within two uniquely named subnodes of `Detail`, namely `Coarse` and `Fine`. These names are arbitrary, the only true difference is that the `Coarse` pass is applied first.

Each of the two detail passes references a texture. Normally you would expect a texture to store color (and optionally opacity) information. However, we can also use textures to store other kinds of information, by not actually interpreting the texture sample as color data. An example that is used in Unity is to pack the per-pixel metallicity and smoothness values into a single texture, or for opaque non-metallic objects, to store the smoothness in the opacity channel of the color texture.

The big benefit of this is that shaders may have a limit to the number of textures they can sample. Instead of referencing and sampling several grayscale textures, it is more efficient to reduce the number of textures by packing three or four grayscale textures into a single false color texture. In the case of the ring shader, it is expected that each of the four color channels of the detail mask describes a different grayscale noise texture. This way, up to four different detail patterns can be displayed per-detail-pass without any additional texture samples being required.

Additionally, it may allow a single texture to be shared between rings or detail passes. Reducing the number of total and/or loaded textures improves the memory usage of KSP.

To further empower the re-use of textures, each detail pass lets one adjust the minimum and maximum opacity multiplier of each of the four noise texture channels. This is done through the `alphaMin` and `alphaMax` parameters. This can be used to adjust the blending strength on a per-texture-channel basis. Additionally, the `strength` parameter acts as an altogether multiplier for the change in opacity made by this detail pass. That is to say, the change in opacity as a consequence of the detail pass is scaled by `strength`.

Ideally the detail effect should start fading in on close proximity. It may be desirable to also allow the detail pass to fade out on closer proximity, to retain total control. This is what the parameters `fadeInStart`, `fadeInEnd`, `fadeOutStart` and `fadeOutEnd` are for. Some special use cases:
- If you do not want a fade-in, set `fadeInEnd = -1` and `fadeInStart = -2`. If doing this, the detail will only fade-out on close proximity.
- If you do not want a fade-out, set `fadeOutStart = -1` and `fadeOutEnd = -2`. If doing this, the detail will only fade-in as the camera approaches, but not fade out on close proximity.
- If using both of the above, there is no fading and the `strength` parameter has total control over the prominence of the detail pass.

|Property|Format|Description|
|--------|------|-----------|
|detailMask|4D Vector|A per-detail-pass multiplier to the prominence of each of the color channels of the texture. Use this to adjust the intensity of each of the detail color channels.|
|texture|File Path|The detail texture to use at this position. This should be a channel packed detail texture, IE this is not a color texture, but instead each of the four color channels encodes a separate noise texture.|
|alphaMin|4D Vector|The minimum ring opacity multiplier for each detail texture channel.|
|alphaMax|4D Vector|The maximum ring opacity multiplier for each detail texture channel.|
|tiling|2D Vector|The tiling of the detail texture along the ring.|
|strength|Decimal|The strength of this detail pass.|
|fadeInStart|Decimal|The distance from the camera at which this detail pass should start contributing.|
|fadeInEnd|Decimal|The distance from the camera at which this detail pass should be at full strength.|
|fadeOutStart|Decimal|The distance from the camera at which this detail pass should start fading out again.|
|fadeOutEnd|Decimal|The distance from the camera at which this detail pass should no longer be visible.|

### How Detail Works {#How_Detail_Works}
This section aims to clarify the exact operations performed by the shader, in plain English.

1. Generate coordinates for the detail textures. (This happens on a per-vertex basis.)
2. Sample `detailRegionsTexture` using the same texture coordinates as the main texture. Multiply the result by `detailRegionsMask`. Let's call this the 'global detail mask'.
3. Sample both detail textures using the generated coordinates and the per-detail-pass tiling parameters. This results in two 4D values.
4. Use these 4D values to lerp between the per-detail-pass `alphaMin` and `alphaMax` parameters.
5. Take the dot product of these values and the global detail mask, multiplied by the per-detail-pass `detailMask` parameters. This is the detail noise value for this channel.
6. Calculate the prominence of each detail pass by interpolating between the `fadeIn` and `fadeOut` parameters. A smoothstep interpolation is used.
7. Multiply the per-pass prominence with the `strength` parameter of that pass.
8. Multiply the opacity of the rings with the detail noise value of the `Coarse` pass. Blend this result with the original opacity value using the prominence of the `Coarse` pass.
9. Multiply the (new) opacity of the rings with the detail noise value of the `Fine` pass. Blend this result with the original opacity value using the prominence of the `Fine` pass.

Or in C# code:

```csharp
struct DetailFeature
{
    Texture detailRegionsTexture;
    Vector_4D detailRegionsMask;
    DetailPass coarse;
    DetailPass fine;

    // Apply the detail feature to the ring opacity.
    float Apply(float ringOpacity, float cameraDistance, Vector_2D ringTextureCoords, Vector_2D ringDetailCoords)
    {
        // Get the global detail channel mask.
        Vector_4D globalDetailMask = SampleTexture(detailRegionsTexture, ringTextureCoords)
                                   * detailRegionsMask;
        // Apply both detail channels in sequence.
        ringOpacity = coarse.Apply(ringOpacity, cameraDistance, ringDetailCoords, globalDetailMask);
        ringOpacity =   fine.Apply(ringOpacity, cameraDistance, ringDetailCoords, globalDetailMask);

        return ringOpacity;
    }
}

struct DetailPass
{
    Vector_4D detailMask;
    Texture texture;
    Vector_4D alphaMin;
    Vector_4D alphaMax;
    Vector_2D tiling;
    float strength;
    float fadeInStart;
    float fadeInEnd;
    float fadeOutStart;
    float fadeOutEnd;

    // Apply this detail pass to the ring opacity.
    float Apply(float ringOpacity, float cameraDistance, Vector_2D ringDetailCoords, Vector_4D globalDetailMask)
    {
        // First we calculate the prominence of the detail pass.
        // That is to say, how intense should the effect be here.
        float fade_in = smoothstep(fadeInStart, fadeInEnd, cameraDistance);
        float fade_out = smoothstep(fadeOutEnd, fadeOutStart, cameraDistance);
        float prominence = fade_in * fade_out * strength;

        // Get the detail value.
        Vector_4D detailChannels = SampleTexture(texture, ringDetailCoords * tiling);
        // Linearly interpolate between the per-channel min and max alpha multiplier.
        detailChannels = alphaMin + (detailChannels * (alphaMax - alphaMin));

        // Get the channel filter.
        Vector_4D channelFilter = globalDetailMask * detailMask;

        // Compute the final detail noise value by adding the detail channels together, using the channel filter as a per-channel prominence value.
        float detailValue = (detailChannels.x * channelFilter.x)
                          + (detailChannels.y * channelFilter.y)
                          + (detailChannels.z * channelFilter.z)
                          + (detailChannels.w * channelFilter.w);

        // What would the effect of applying the detail be?
        float appliedOpacity = ringOpacity * detailValue;

        // Scale the intensity of the effect by the prominence.
        float changeInOpacity = appliedOpacity - ringOpacity;
        return ringOpacity + (prominence * changeInOpacity);
    }
}
```
Notice: the above shader code is not optimized and is written to enhance readability. In the actual shader the effect is implemented so as to take advantage of SIMD/vectorized arithmetic.

## Example {#Example}
```
Rings
{
	Ring
	{
		innerRadius = 2000
		outerRadius = 4000
		
		InnerRadiusMultiplier
		{
			key = 0 1 0 0
		}
		OuterRadiusMultiplier
		{
			key = 0 1 0 0
		}
		
		thickness = 100
		steps = 120

		texture = Fruits/PluginData/Grapefruit_rings.dds
		// Number of times to tile the texture around the ring
		// Texture coordinates depend on this!
		// If 0, then a thin strip from (0,0) to (1,1) is applied to the whole ring.
		// Otherwise the following rectangles are tiled top-to-bottom:
		//   |      |            |            |
		//   | side |   inner    |   outer    |
		//   |      |            |            |
		//   (0.0,0)-(0.2,1): Top/bottom edges
		//   (0.2,0)-(0.6,1): Inner edge
		//   (0.6,0)-(1.0,1): Outer edge
		tiles = 10
		color = 1,1,1,1
		unlit = false
		useNewShader = false
		penumbraMultiplier = 200

		angle = 30
		lockRotation = true
		longitudeOfAscendingNode = 30
		rotationPeriod = 600

        // Ring proximity fadeout parameters
        fadeoutStartDistance = 200
        fadeoutStopDistance = 20
        fadeoutMinAlpha = 0

        // Ring detail pass. Omit this node to disable.
        Detail
        {
            detailRegionsMask = 1, 1, 1, 1
            detailRegionsTexture = ...
            Coarse
            {
                detailMask = 1, 1, 1, 1
                texture = ...
                alphaMin = 0, 0, 0, 0
                alphaMax = 1, 1, 1, 1
                tiling = 30, 400
                strength = 0.3
                fadeInStart = 8000
                fadeInEnd = 3000
                fadeOutStart = 1000
                fadeOutEnd = 500
            }
            Fine
            {
                detailMask = 1, 1, 1, 1
                texture = ...
                alphaMin = 0, 0, 0, 0
                alphaMax = 1, 1, 1, 1
                tiling = 60, 800
                strength = 0.7
                fadeInStart = 1200
                fadeInEnd = 450
                fadeOutStart = 400
                fadeOutEnd = 40
            }
        }
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|innerRadius|Decimal|The distance from center of parent to inner edge of ring in milliradii.|
|outerRadius|Decimal|The distance from center of parent to outer edge of ring in milliradii.|
|InnerRadiusMultiplier|FloatCurve|A curve that defines a multiplier for the inner radius using an angle. The first value is an angle in degrees, while the second is the multiplier. Allows for the deformation of rings.|
|OuterRadiusMultiplier|FloatCurve|Similar to `InnerRadiusMultiplier`, but for the outer radius rather than the inner radius.|
|thickness|Decimal|The distance between top and bottom faces of ring in milliradii.|
|angle|Decimal|The axis angle in degrees (inclination) of the ring.|
|longitudeOfAscendingNode|Decimal|Angle in degrees between the absolute reference direction and the ascending node. Works just like the corresponding property on celestial bodies. Only effective if `lockRotation` is true.|
|texture|File Path|The path to the ring texture.|
|color|Color|A tint applied to the ring.|
|lockRotation|Boolean|Whether to lock the rotation of the ring. If false, the ring's LAN rotates with the parent body (unnatural if the `angle` is not 0).|
|rotationPeriod|Decimal|The number of seconds for the ring to complete one rotation. If zero, it will default to the parent body's `rotationPeriod`. Only noticeable if `tiles` is not 0.|
|unlit|Boolean|Whether to apply an Unlit/Transparent shader instead of a Transparent/Diffuse shader.|
|useNewShader|Boolean|Whether to use the new custom ring shader that includes a planet shadow instead of the built-in Unity shaders.|
|penumbraMultiplier|Decimal|A penumbra multiplier to the NewShader. Makes planet shadow softer (values larger than one) or less soft (smaller than one). Softness still depends on distance from sun, distance from planet and radius of sun and planet.|
|steps|Integer|The amount of vertices around the ring.|
|tiles|Integer|Number of times the texture should be tiled around the cylinder. If zero, use the old behavior of sampling a thin diagonal strip from (0,0) to (1,1). Look at the example above for more info.|
|innerShadeTexture|File Path|The path to the texture whose opaque pixels cast shadows on the ring's inner surface.|
|innerShadeTiles|Integer|The `innerShadeTexture` repeats this many times over the inner surface.|
|innerShadeRotationPeriod|Decimal|The number of seconds the `innerShadeTexture` takes to complete one rotation.|
