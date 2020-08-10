---
layout: default
title: ScatterColliders
---

The `ScatterColliders` ModularScatter component allows for a scatter to be collidable via a set mesh.

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
|collider|File Path|The path to the collider mesh to use for the scatter.|
