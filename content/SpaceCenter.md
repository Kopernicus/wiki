---
layout: default
title: SpaceCenter
---

The `SpaceCenter { }` node describes the location and altitude of the KSC and the MapDecal that creates the flat region it is located on. It is a subnode of `Body { }`. It should only be used for the home world(?).

**Subnodes**
```
* `Material { }` link to grassamaterial
```

**Example**
```md
@Kopernicus
{
    Body
    {
        SpaceCenter
        {
            latitude = // Float, latitude of the KSC building collection
            longitude = // Float, longitude of the KSC building collection
            repositionRadial = // Vector3
            decalLatitude = // Float, Latitude of the KSC MapDecal
            decalLongitude = // Float, Longitude of the KSC MapDecal
            lodvisibleRangeMultiplier = // Float
            reorientFinalAngle = // Float
            reorientInitialUp = // Vector3
            reorientToSphere = // Boolean
            repositionRadiusOffset = // Float
            repositionToSphere = // Boolean
            repositionToSphereSurface = // Boolean
            repositionToSphereSurfaceAddHeight = // Boolean
            position = // Vector3
            radius = // Float, height of KSC
            heightMapDeformity = // Float
            absoluteOffset = // Float
            absolute = // Boolean
            groundColor = // Color, the color of the KSC grass
            groundTexture = // Texture, KSC grass texture when up close
            editorGroundColor = // Color, color of the ksc grass as seen from the editor
            Material
            {
                nearGrassTexture = Texture
                nearGrassTiling = Float
                farGrassTexture = Texture
                farGrassTiling = Float
                farGrassBlendDistance = Float
                grassColor = Color
                tarmacTexture = Texture
                tarmacTextureOffset = Vector2
                tarmacTextureScale = Vector2
                opacity = Float
                rimColor = Color
                rimFalloff = Float
                underwaterFogFactor = Float
            }

            editorGroundTex = // Texture, grass surface texture as seen from the editor
            editorGroundTexScale = // Vector2, scale of the grass surface texture as seen from inside the editor
            editorGroundTexOffset = // Vector2, offset of the ksc grass surface texture as seen from inside the editor
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|latitude|Decimal|Latitude of the KSC building collection.|
|longitude|Decimal|Longitude of the KSC building collection.|
|repositionRadial|Vector3|?|
|decalLatitude|Decimal|Latitude of the KSC MapDecal.|
|decalLongitude|Decimal|Longitude of the KSC MapDecal.|
|lodvisibleRangeMultiplier|Decimal|Multiplier of the visible range from the Space Center view(?).|
|reorientFinalAngle|Decimal|Final angle of the KSC(?).|
|reorientInitialUp|Vector3|"Up" direction of the KSC(?).|
|reorientToSphere|Boolean|Whether to reorient to a sphere(?).|
|reorientToSphereSurface|Boolean|Whether to reorient to the sphere's surface(?).|
|reorientToSphereSurfaceAddHeight|Boolean|Whether to add height to the reoriented sphere's surface(?).|
|position|Vector3|The position of the KSC. NOT LAT/LONG!|
|radius|Decimal|Height and scale of the KSC.|
|heightMapDeformity|Decimal|External height map deformity(?).|
|absoluteOffset|Decimal|Offset from the ground.|
|absolute|Boolean|?|
|groundColor|Color|Color of the KSC grass.|
|groundTexture|File Path|The texture containing the KSC's ground texture when viewed up close.|
|editorGroundColor|Color|Color of the KSC grass found in the editor.|
|editorGroundTex|File Path|The texture containing the KSC's ground texture when viewed up close from the editor.|
|editorGroundTexScale|Vector2|Scale of the grass texture found in the editor.|
|editorGroundTexOffset|Vector2|Offset of the grass texture found in the editor.|
