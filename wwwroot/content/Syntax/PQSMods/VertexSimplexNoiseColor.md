The `VertexSimplexNoiseColor` PQSMod generates RGB [Perlin noise](/Prerequisites/DataTypes) for use in terrain coloration.

## Example {#Example}
```
PQS
{
  Mods
  {
    VertexSimplexNoiseColor
    {
      blend = 0.1
      colorStart = 0.2,0.2,0.3,1
      colorEnd = 0.7,0.9,0.4,1
      frequency = 0.2
      octaves = 2
      persistence = 0.3
      seed = 223476
      
      enabled = true
      order = 80
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|blend|Decimal|The amount to blend the noise by.|
|colorStart|Color|The starting color to use for the perlin noise. Noise will generate between this color and `colorEnd`.|
|colorEnd|Color|The ending color to use for the perlin noise. Noise will generate between this color and `colorStart`.|
|frequency|Decimal|The size of the each feature of the perlin noise. As frequency gets bigger, size gets smaller.|
|persistence|Decimal|The complexity of or amount of detail in the noise.|
|seed|Integer|The random seed of the noise.|
