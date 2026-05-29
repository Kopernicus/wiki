# Ocean materials

A body's ocean is its own Procedural Quad-Sphere sitting at sea level, and its [`Ocean { }`](/Syntax/Ocean) node's `Material { }` renders the water surface. Unlike the [PQS terrain](/Syntax/Material/PQS/) and [scaled-space](/Syntax/Material/Scaled/) materials there is no `materialType` selector — the ocean always uses the one shader.

- [Ocean Surface Quad](/Syntax/Material/Ocean/OceanSurfaceQuad) — the animated water surface for a body's ocean.
