The `ScatterColliders` ModularScatter component allows for a scatter to be collidable via a set mesh.

## Performance warning {#Performance}

This feature can be quite performance heavy if used on a scatter type that has a large amount of objects per quad. Less than 5-20 scatters per quad is fine, higher numbers will start to have a quite noticeable impact.

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
                ScatterColliders
                {
                    collider = Fruits/PluginData/meshes/pineapple_leaves.mu
                }
            }
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|collider|File Path|The path to the collider mesh to use for the scatter. If not defined, the rendering mesh will be used|
