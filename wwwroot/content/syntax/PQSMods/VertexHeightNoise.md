The `VertexHeightNoise` PQSMod is a mod that adds height noise to the terrain. This makes the terrain bumpier, though the "style" of bumps/features change with the noise type.  
The noise is also additive, meaning that instead of overwriting the terrain altitude, it simply adds or subtracts from it.

<button data-toggle="collapse" data-target="#collapse-vhn-example">Show Example</button>

{: #collapse-vhn-example .collapse}  
## Example {#Example}
```
PQS
{
  Mods
  {
    VertexHeightNoise
    {
      deformity = 1200
      frequency = 0.5
      octaves = 3
      persistence = 0.2
      seed = 134256
      noiseType = Perlin
      mode = High
      lacunarity = 0.7

      enabled = true
      order = 25
    }
  }
}
```

<button data-toggle="collapse" data-target="#collapse-vhn-table">Show VertexHeightNoise Table</button>

{: #collapse-vhn-table .collapse}  
|Property|Format|Description|
|--------|------|-----------|
|deformity|Decimal|The deformity of the simplex terrain noise.|
|frequency|Decimal|The size of the each feature of the simplex terrain noise. As frequency gets bigger, size gets smaller.|
|octaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Decimal|The complexity of or amount of detail in the noise.|
|lacunarity|Decimal|The size of the gaps that are in the noise.|
|seed|Integer|The random seed of the noise.|
|noiseType|[NoiseType]( /Prerequisites/DataTypes)|The type of the specified noise.|
|mode|[NoiseQuality]( /Prerequisites/DataTypes)|The quality mode of the noise.|


This may seem nice, but there are several issues with VertexHeightNoise. These include:

* Scarped terrain at the planet's poles
* A black line which appears around sea level
* Terrain disappears after going below sea level

It is recommended that you instead use one of the VertexHeightNoiseVertHeightCurve PQSMods instead.

* VertexHeightNoiseVertHeightCurve
* [VertexHeightNoiseVertHeightCurve2]( /Syntax/PQSMods/VertexHeightNoiseVertHeightCurve2)
* VertexHeightNoiseVertHeightCurve3

If you want to replicate the effect of VHN with one of the above mods, it is relatively simple to do so.

```
PQS
{
    Mods
    {
        VertexHeightNoiseVertHeightCurve2
        {
            // All blank fields can be whatever you want, see the VHNVHC2 page for more information on them.
            deformity =
            ridgedMode =

            ridgedAddSeed =
            ridgedAddFrequency =
            ridgedAddLacunarity =
            ridgedAddOctaves =

            ridgedSubSeed =
            ridgedSubFrequency = 0
            ridgedSubLacunarity =
            ridgedSubOctaves =

            simplexCurve
            {
                key = 0 1
                key = 1 0
            }

            simplexHeightStart =
            simplexHeightEnd =
            simplexSeed =
            simplexOctaves =
            simplexPersistence =
            simplexFrequency = 0

            enabled = true
            order =
        }
    }
}
```
