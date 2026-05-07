# QuadEnhanceCoast

The `QuadEnhanceCoast` PQSMod allows you to tune terrain detail around sea level
and under the ocean. It can be used to make coastlines more detailed, so that they
look better from a distance. Similarly, it can also be used to reduce the detail
on the seafloor, 

## How it works
On each quad build, the mod compares the quad's vertex altitudes to a "coast" altitude, defined as `sphere.radius + coastLessThan`:

* If every vertex of the quad is below this altitude then the distance that the PQS
  quad gets subdivided at is multiplied by `oceanFactor`.
* If the quad straddles the threshold then the distance it gets subdivided at is
  multiplied by `coastFactor`.
* Otherwise, the subdivision distance threshold stays the same.

A factor greater than 1 means that the quad will be subdivided further away from the
main vessel (higher detail); a factor less than 1 does the opposite (lower detail).

By default, `QuadEnhanceCoast` will decrease the detail on ocean quads by 50%, and
increase the detail on coastline quads by 50%.

## Example
```
PQS
{
    Mods
    {
        QuadEnhanceCoast
        {
            coastLessThan = 1
            oceanFactor = 0.5
            coastFactor = 1.5

            enabled = true
        }
    }
}
```

|  Property   |Format |Default|Description|
|-------------|-------|-------|-----------|
|coastLessThan|Decimal|1.0|Altiture of the top of the "ocean" region, in meters. Vertices with an altitude less than `coastLessThan` are considered to be "ocean" vertices.
| oceanFactor |Decimal|0.5|Multiplier applied to the subdivision distance for quads that are entirely below the coast threshold.
| coastFactor |Decimal|1.5|Multiplier applied to the subdivision distance for quads that straddle the coast threshold.

## Notes
* `coastFactor` is only exposed in kopernicus 245 or later.
