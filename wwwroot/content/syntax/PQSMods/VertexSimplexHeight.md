The `VertexSimplexHeight` PQSMod generates monochrome [Perlin noise](/Prerequisites/datatypes.md) for use in terrain deformation.

## Example {#Example}
```
PQS
{
    Mods
    {
        VertexSimplexHeight
        {
            deformity = 10000
            frequency = 0.6
            octaves = 5
            persistence = 0.7
            seed = 78309

            enabled = true
            order = 2
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|deformity|Decimal|The deformity of the perlin noise.|
|frequency|Decimal|The size of the each feature of the perlin noise. As frequency gets bigger, size gets smaller.|
|octaves|Decimal|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Decimal|The complexity of or amount of detail in the noise.|
|seed|Integer|The random seed of the noise.|
