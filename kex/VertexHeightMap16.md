<!-- TITLE: VertexHeightMap16 -->
<!-- SUBTITLE: A Heightmap PQSMod that lets you use 16-bpp encoded textures. -->

# Explanation
What is "bpp"? [This article should make it clearer.](https://en.wikipedia.org/wiki/Color_depth)

# Example

```text
PQS
{
  Mods
  {
    VertexHeightMap16
    {
      map = //filepath to heightmap
      deformity = //same as always
      scaleDeformityByRadius = //this is exactly the same as VertexColorMap, config-wise
      enabled = true
      order = //you decide
    }
  }
}
```
