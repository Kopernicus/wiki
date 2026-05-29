# PQS terrain materials

The [`PQS { }`](/Syntax/PQS) node's `Material { }` paints a body's **near-surface terrain** — the real, subdivided quad-sphere you fly down to and land on, which fades in over the distant [scaled-space sphere](/Syntax/Material/Scaled/) as the camera descends. The shader is chosen with `materialType = …`.

- [Vacuum](/Syntax/Material/PQS/Vacuum) — the base sphere-projection terrain, for airless bodies.
- [Basic](/Syntax/Material/PQS/Basic) — the aerial-perspective (atmosphere) variant of Vacuum.
- [Main](/Syntax/Material/PQS/Main) — full world-space triplanar terrain with every elevation band normal-mapped.
- [Optimized](/Syntax/Material/PQS/Optimized) — like Main, but only the mid band is normal-mapped.
- [Extra](/Syntax/Material/PQS/Extra) — Main plus an ocean-fog distance control.
- [MainFastBlend](/Syntax/Material/PQS/MainFastBlend) — the 1.8 "fast blend" terrain used by stock Kerbin.
- [OptimizedFastBlend](/Syntax/Material/PQS/OptimizedFastBlend) — the cut-down fast-blend build (mid band only).
- [Triplanar](/Syntax/Material/PQS/Triplanar) — the 1.8 zoom-rotation triplanar terrain.
- [TriplanarAtlas](/Syntax/Material/PQS/TriplanarAtlas) — the 1.9 texture-array zoom-rotation terrain (Ultra detail).
- [MainTriplanarZoomRotation](/Syntax/Material/PQS/MainTriplanarZoomRotation) — the PBR (specular) zoom-rotation terrain.
