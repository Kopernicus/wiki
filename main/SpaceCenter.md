---
layout: default
title: SpaceCenter
---

The `SpaceCenter { }` node describes the location and altitude of the KSC and the MapDecal that creates the flat region it is located on. It is a subnode of `Body { }`.

## Example
```md
@Kopernicus
{
    Body
    {
        SpaceCenter
        {
            latitude = // Double, latitude of the KSC building collection
            longitude = // Double, longitude of the KSC building collection
            repositionRadial = // Vector3
            decalLatitude = // Double, Latitude of the KSC MapDecal
            decalLongitude = // Double, Longitude of the KSC MapDecal
            lodvisibleRangeMultiplier = // Double
            reorientFinalAngle = // Single
            reorientInitialUp = // Vector3
            reorientToSphere = // Boolean
            repositionRadiusOffset = // Double
            repositionToSphere = // Boolean
            repositionToSphereSurface = // Boolean
            repositionToSphereSurfaceAddHeight = // Boolean
            position = // Vector3
            radius = // Double, height of KSC
            heightMapDeformity = // Double
            absoluteOffset = // Double
            absolute = // Boolean
            groundColor = // Color, the color of the KSC grass
            groundTexture = // Texture2D, KSC grass texture when up close
            editorGroundColor = // Color, color of the ksc grass as seen from the editor
            Material
            {
                nearGrassTexture = Texture2D
                nearGrassTiling = Single
                farGrassTexture = Texture2D
                farGrassTiling = Single
                farGrassBlendDistance = Single
                grassColor = Color
                tarmacTexture = Texture2D
                tarmacTextureOffset = Vector2
                tarmacTextureScale = Vector2
                opacity = Single
                rimColor = Color
                rimFalloff = Single
                underwaterFogFactor = Single
            }

            editorGroundTex = // Texture2D, grass surface texture as seen from the editor
            editorGroundTexScale = // Vector2, scale of the grass surface texture as seen from inside the editor
            editorGroundTexOffset = // Vector2, offset of the ksc grass surface texture as seen from inside the editor
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
