The `LightEmitter` ModularScatter component allows for scatters to provide their own light based on numerous arguments. Most arguments are optional.

## Performance warning {#Performance}

This feature can be quite performance heavy if used on a scatter type that has a large amount of objects per quad. Less than 5-20 scatters per quad is alright, higher numbers will start to have a quite noticeable impact.

```
LandControl
{
    ...
    Scatters
    {
        Value
        {
            ...
            Components
            {
                LightEmitter
                {
                    type = Point
                    color = 0.85,0.88,1,1
                    range = 3000
                    intensity = 6
                    offset = 0,0,0
                }
            }
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|type|LightType|The type of light to be emitted from the scatter. Values are `Spot`, `Directional` (more resource-intensive), `Point`, `Rectangle` (rectangle-shaped area light that affects only baked lightmaps and lightprobes), or `Disc` (disc-shaped area light that affects only baked lightmaps and lightprobes).|
|color|Color|The color of light to be emitted.|
|colorTemperature|Decimal|The color temperature of the light. Correlated Color Temperature (abbreviated as CCT) is multiplied with the color filter when calculating the final color of a light source. The color temperature of the electromagnetic radiation emitted from an ideal black body is defined as its surface temperature in Kelvin. White is 6500K according to the D65 standard. Candle light is 1800K.|
|intensity|Decimal|The intensity of the light.|
|bounceIntensity|Decimal|The intensity of the light after bouncing off a surface?|
|shadows|LightShadows|The shadow that the light should cast. Values are `None`, `Hard`, and `Soft`. Default is `None`.|
|shadowStrength|Decimal|The strength of the shadows formed by the light.|
|shadowResolution|LightShadowResolution|The resolution of the shadow map. Values are `FromQualitySettings`, `Low`, `Medium`, `High`, and `VeryHigh`.|
|shadowCustomResolution|Integer|The custom resolution of the shadow map. By default, shadow map resolution is computed from its importance on screen. Setting this property to a value greater than zero will override that behavior. Please note that the shadow map resolution will still be rounded to the nearest power of two and capped by memory and hardware limits.|
|shadowBias|Decimal|Shadow mapping constant bias. Shadow caster surfaces are pushed by this world-space amount away from the light, to help prevent self-shadowing ("shadow acne") artifacts.|
|shadowNormalBias|Decimal|Shadow mapping normal-based bias. Shadow caster surfaces are pushed inwards along their normals by this amount, to help prevent self-shadowing ("shadow acne") artifacts. Units of normal-based bias are expressed in terms of shadowmap texel size; typically values between 0.3-0.7 work well. Larger values prevent shadow acne better, at expense of making shadow shape smaller than the object actually is. Currently normal-based bias is only implemented for directional lights; it has no effect for other light types.|
|shadowNearPlane|Decimal|Near plane value to use for shadow frustums. This determines how close to the light shadows will stop being rendered from an object.|
|range|Decimal|The range of the light. (in meters?)|
|spotAngle|Decimal|The angle of the light's spotlight cone in degrees. This is used primarily for Spot lights and has no effect for Point lights.|
|innerSpotAngle|Decimal|The angle of the light's spotlight inner cone in degrees. This is only used for Spot lights.|
|cookieSize|Decimal|The size of a directional light's cookie.|
|cookie|File Path|The path to the cookie texture projected by the light. If the cookie is a cube map, the light will become a Point light. Note that cookies are only displayed for pixel lights.|
|flare|Flare|The flare asset to use for this light.|
|renderMode|LightRenderMode|How to render the light. Pixel lights render slower but look better, especially on not very highly tesselated geometry. Some effects (e.g. bumpmapping) are only displayed for pixel lights. Values are `Auto`, `ForcePixel`, and `ForceVertex`.|
|cullingMask|Integer|(Optional) This is used to light certain objects in the Scene selectively. An object will only be illuminated by a light if that light's `cullingMask` includes the layer chosen for the object (ie, the mask bit for the layer must be set to 1 for the object to receive any light).|
|useBoundingSphereOverride|Boolean|Whether to override the light bounding sphere for culling.|
|boundingSphereOverride|Vector4|Bounding sphere used to override the regular light bounding sphere during culling.|
|renderingLayerMask|Integer|Determines which rendering layer mask the light affects.|
|lightShadowCasterMode|LightShadowCasterMode|Allows you to override the global Shadowmask Mode per light. Values are `Default`, `NonLightmappedOnly`, and `Everything`.|
|layerShadowCullDistances|Float List|(Optional) A list of 32 per-light, per-layer shadow culling distances. Dynamic shadows can be cast into view from shadow casters that are very far away from the camera. At low incident light angles, this can lead to a lot of objects needing to cast dynamic shadows, which in turn can result in high rendering costs during shadow maps generation. Using `layerShadowCullDistances` lets you limit, on a per-layer basis, how far from the camera shadows casters are allowed to be before they get culled from shadow maps generation. The feature complements `Camera.layerCullDistances`, but only affects shadow casting, not regular object rendering. Just like `Camera.layerCullDistances`, `layerShadowCullDistances` requires that you assign a float array of exactly 32 values. A distance of 0 in a layer's index means keep current behaviour for that layer. Assigning null completely disables shadow distance culling, and is effectively the same as passing an array of 32 zeros.|
|offset|Vector3|(Optional) The offset of the light, relative to the center of the scatter object. Defaults to 0,0,0.|
