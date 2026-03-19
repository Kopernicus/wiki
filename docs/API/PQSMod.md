---
outline: deep
---

# PQSMod

This page documents methods and fields available on the `PQSMod` class.

## Fields
### `PQS sphere`
A reference to the `PQS` instance that this `PQSMod` belongs to.

### `int order`
Used to determine the position of this `PQSMod` the overall list. This
decides what order each of the callbacks below are made at runtime.

You _can_ modify this at runtime. However, it will not do anything until
`PQS.RebuildSphere()` is called since it is only checked when the PQS
sphere is created.

### `bool modEnabled`
Controls whether this PQSMod is enabled at all. If `false` then none of the
callback methods below will be called for this `PQSMod`.

You _can_ modify this at runtime. However, it will not do anything until
`PQS.RebuildSphere()` is called since it is only checked when the PQS
sphere is created.

### `PQS.ModifierRequirements requirements`
A flags enum that declares what additional mesh or UV channels this mod wants
to see added to the PQS quad meshes. This is the canonical way to make sure
that the mesh data you need is prepared for you.

The PQS sphere will combine together all the individual `modRequirements`
fields to get the final set of requirements.

The available options are:
| Flag | Effect |
|------|--------|
| `Default` | No flags are set. |
| `UniqueMaterialInstances` | Each quad gets its own material instance instead of a shared one |
| `VertexMapCoords` | `latitude`, `longitude`, `u`, and `v` will be computed in `VertexBuildData` |
| `VertexGnomonicMapCoords` | `gnomonicUVs` will now be computed in `VertexBuildData` |
| `UVSphereCoords` | UV0 will be initialized with sphere-space UV coordinates. |
| `UVQuadCoords` | UV0 will be initialized with quad-space UV coordinates. |
| `MeshColorChannel` | Requests that the mesh is assigned a color channel from `VertexBuildData.vertColor` |
| `MeshCustomNormals` | Indicates that KSP should manually compute normals instead of using Unity's `Mesh.RecalculateNormals` |
| `MeshBuildTangents` | Requests that tangents get computed as part of the quad build. Implies `MeshAssignTangents` |
| `MeshAssignTangents` | Requests that `PQS.cacheTangents` gets assigned to the mesh. |
| `MeshUV2` | Requests that the mesh is assigned a uv channels from `VertexBuildData.u2/v2` |
| `MeshUV3` | Requests that the mesh is assigned a uv channels from `VertexBuildData.u3/v3` |
| `MeshUV4` | Requests that the mesh is assigned a uv channels from `VertexBuildData.u4/v4` |

You _can_ modify this at runtime. However, it will not do anything until
`PQS.RebuildSphere()` is called since it is only checked after `OnSetup` is
called.

## Methods
### Setup & Lifecycle
#### `void OnSetup()`
Called when the PQS sphere initializes its `PQSMod` list.

Use this to set `requirements` for what your mod needs or to perform other initialization
work that needs to happen during PQS sphere setup.

The default implementation just sets `requirements` to `Default`.

::: details Existing Uses
`OnSetup` is used by nearly every stock mod. The most common patterns are:

* **Setting `requirements`** — color-writing mods request `MeshColorChannel`; height
  mods typically request `MeshCustomNormals` so KSP computes normals manually;
  map-based mods request `VertexMapCoords` to get `latitude`/`longitude`/`u`/`v`;
  UV-coordinate mods request `UVQuadCoords` or `UVSphereCoords`;
  `PQSMod_MaterialQuadRelative` requests `UniqueMaterialInstances`.
* **Initializing noise generators** — all simplex, LibNoise (Perlin, Billow,
  RidgedMultifractal), and Voronoi-based mods construct and seed their noise
  modules here.
* **Pre-computing derived values** — mods like `PQSMod_FlattenArea`,
  `PQSMod_MapDecal`, and `PQSMod_HeightColorMap2` calculate angles, height deltas,
  or other constants needed during vertex building.
* **Getting component references** — `PQSMod_OceanFX` and `PQSMod_MaterialFadeAltitude`
  look up or create material references; `PQSROCControl` and `PQSMod_SurfaceObjectQuads`
  retrieve their `CelestialBody` reference.
:::

#### `bool OnSphereStart()`
Called each frame until the sphere becomes alive. Returning `true` from this will
cause the build of the PQS sphere to be delayed until the next frame.

::: warning
KSP adds a `PQSMod_CelestialBodyTransform` to each PQS sphere. This
will always return `true` from `OnSphereStart` and manually set
`sphere.isAlive = true` when the main vessel starts orbiting the planet.

This means that in practice you can't use the return value from this function
to control when the sphere starts in any way. It is better to just always return
`false`.
:::

::: details Existing Uses
* `PQSMod_CelestialBodyTransform` always returns `true` here and manually sets
  `sphere.isAlive = true` once the main vessel is orbiting the body, which means no
  other mod can actually gate sphere startup via the return value.
* `PQSCity` and `PQSCity2` return `false` but use this callback to deactivate their
  child game objects when the sphere is not yet alive.
:::

#### `void OnPostSetup()`
Called after the inital batch of PQS quads are created, but before `OnSphereStarted` is
called.

Note that at the point that this is called no quads have been built, the only thing
that has happened is that the actual quadtree structured used by the sphere has been
constructed.

::: details Existing Uses
* `PQSCity` and `PQSCity2` use this to align the game object they spawn relative to
  the surface.
:::

#### `void OnSphereStarted()`
Called immediately after `OnPostSetup`.

There doesn't really seem to be much of a difference in how KSP uses this as compared
to `OnPostSetup`. The only distinction seems to be that `PQSCity` stuff is put in
`OnPostSetup` and scatter-related stuff is put in `OnSphereStarted`.

::: details Existing Uses
In stock, this is used by `PQSLandControl` and `PQSROCControl` to start child objects
that manage ongoing work (scatters, etc.)
:::

#### `void OnSphereReset()`
This is called during sphere teardown, before `sphere.isAlive` and `sphere.isStarted`
get set to false.

Use this to destroy any child game objects associated with this PQS sphere
or to otherwise perform any custom teardown you need.

::: details Existing Uses
* `PQSCity` and `PQSCity2` deactivate all their child game objects (city meshes and
  LOD objects).
* `PQSLandControl` calls `SphereInactive()` on every scatter controller, disabling
  all terrain scatter objects.
* `PQSROCControl` similarly calls `SphereInactive()` on every ROC controller.
* `PQSMod_CelestialBodyTransform` triggers a fade-out animation on the planet's
  visual transform.
:::

### Active/Inactive
#### `void OnSphereActive()`
Called when the sphere gets activated, after quads are initialized. This is different
from `OnSphereStarted`/`OnPostSetup` because it can happen multiple times in a scene
as the player moves around.

::: details Existing Uses
In stock, this callback is used to
* toggle the visibility of spawned meshes (`PQSCity` and `PQSCity2`)
* update global shader variables related to the current body
  (`PQSMod_AerialPerspectiveMaterial`).

Kopernicus uses this to start loading OnDemand textures in preparation for
actually building terrain quads.
:::

#### `void OnSphereInactive()`
Called when the sphere is deactivated or permanently disabled. This is the
counterpart to `OnSphereActive` and can happen multiple times in a scene.

::: details Existing Uses
In stock, this mirrors `OnSphereActive`: spawned meshes are hidden and the
aerial-perspective shader globals are cleared.
:::

### Per-Frame Updates
#### `void OnSphereTransformUpdate()`
Called every `FixedUpdate`. Use this to track changes in the sphere's world-space
transform (position/rotation) that need to be reflected in your mod's state.

::: details Existing Uses
In stock, this is used by `PQSMod_CelestialBodyTransform` to sync the planet
transform with the celestial body's orbit position.
:::

#### `void OnPreUpdate()`
Called at the start of every `Update`, before quad LOD decisions are made and
before any quads are built or rebuilt for that frame.

Use this for per-frame work that needs to happen before terrain geometry changes,
such as updating parameters that will be read during quad building.

::: details Existing Uses
In stock, this is used by `PQSMod_AerialPerspectiveMaterial` to update
atmosphere shader uniforms each frame.
:::

#### `void OnUpdateFinished()`
Called at the end of every `Update`, after all quad LOD updates have completed
for the frame. Only called when the sphere is active.

Use this for any per-frame work that should happen after the terrain has settled
for the frame.

::: details Existing Uses
* `PQSCity` and `PQSCity2` check each frame whether the camera target is within LOD
  range, activating or deactivating child transforms accordingly, and fire
  `OnPQSCityLoaded`/`OnPQSCityUnloaded` and POI range events at the boundaries.
* `PQSMod_CelestialBodyTransform` uses this to actually fade controll when the
   terrain fades in and out.
* `PQSMod_OceanFX` animates water texture cycling and computes sun-angle specular
  colour, applying both to the ocean material each frame.
* `PQSMod_MaterialSetDirection` pushes the current sun direction into a global shader
  vector property.
:::

### Vertex Building
#### `double GetVertexMaxHeight()`
Called once during `SetupMods()`. Return the maximum height offset that this mod
can ever add to a vertex. The values from all mods are added up to compute
`sphere.radiusMax`.

Getting this wrong will cause culling and collider artefacts, since the sphere's
bounding radius will be incorrect.

::: details Existing Uses
The vast majority of height mods simply return their `deformity` field directly
(e.g. `PQSMod_VertexHeightNoise`, all `PQSMod_VertexSimplex*` variants,
`PQSMod_VertexVoronoi`, etc.). A few notable exceptions:

* `PQSMod_VertexHeightMap` and `PQSMod_VertexHeightMapStep` return
  `heightMapOffset + heightDeformity` to account for the baseline height offset.
* `PQSMod_VertexPlanet` returns `deformity * 1.2` — a conservative over-estimate to
  account for compound terrain noise.
* `PQSMod_FlattenOcean` and `PQSMod_VertexDefineCoastLine` return `0.0` since they
  only push vertices downward, never up.
:::


#### `double GetVertexMinHeight()`
Called once during `SetupMods()`. Return the minimum height offset (in metres,
typically zero or negative) that this mod can ever produce. Added up across all
mods to compute `sphere.radiusMin`.

::: details Existing Uses
Most mods return `0.0` since noise-based height mods typically only add height above
the base radius. The exceptions are:

* Signed simplex mods (`PQSMod_VertexSimplexHeight`, `PQSMod_VertexSimplexHeightFlatten`,
  `PQSMod_VertexSimplexHeightMap`, `PQSMod_VertexSimplexHeightAbsolute`) return
  `-deformity`, since they can produce both upward and downward offsets.
* `PQSMod_VertexDefineCoastLine` returns a negative `depthOffset` (typically `-2.0`)
  to account for the ocean-floor depth it carves out.
* `PQSMod_VertexHeightMap` and `PQSMod_VertexHeightMapStep` return `heightMapOffset`.
:::

#### `void OnVertexBuildHeight(PQS.VertexBuildData data)`
If you're looking to modify the terrain height then this is the callback for
you. It gets called for each and every vertex of every quad that gets built.
It also gets called at unrelated times when some other part of KSP needs to
know the terrain height (i.e. somebody calls `PQS.GetSurfaceHeight()`).

The main job of this callback is to modify `data.vertHeight`. It starts out at
`sphere.radius` so most of the time you should be adding or subtracting from it,
but you can really do anything you want here.

::: details Existing Uses
This is the most heavily used callback — nearly every mod that contributes to terrain
shape overrides it. Implementations fall into clear categories:

* **Noise** — `PQSMod_VertexHeightNoise`, all `PQSMod_VertexSimplex*` variants,
  `PQSMod_VertexNoise`, `PQSMod_VertexRidgedAltitudeCurve`, and the
  `PQSMod_VertexHeightNoise*` family all add noise-sampled values to
  `data.vertHeight`.
* **Height maps** — `PQSMod_VertexHeightMap` and `PQSMod_VertexHeightMapStep`
  sample a texture map and add a scaled, offset value.
* **Craters** — `PQSMod_VoronoiCraters` and `PQSMod_VoronoiCraters2` apply
  Voronoi-based crater deformations shaped by animation curves.
* **Geometric** — `PQSMod_VertexHeightOblate` flattens the sphere toward the poles
  using a latitude formula; `PQSMod_VertexPlanet` layers multiple fractal generators
  to produce continents and ocean floors.
* **Flattening** — `PQSMod_FlattenOcean` clamps vertices up to ocean radius;
  `PQSMod_FlattenArea` and `PQSMod_FlattenAreaTangential` smooth a bounded region
  toward a target altitude.
* **Decals** — `PQSMod_MapDecal` and `PQSMod_MapDecalTangent` blend in a height-map
  texture over a circular patch on the surface.
* **Other** — `PQSMod_SmoothLatitudeRange` transitions height within a latitude band;
  `PQSMod_VertexDefineCoastLine` pushes vertices below the waterline;
  `PQSMod_VertexHeightOffset` adds a uniform offset to every vertex.

Almost all implementations use `+=` so that multiple mods can stack their height
contributions.
:::

#### `void OnVertexBuild(PQS.VertexBuildData data)`
Called for every vertex immediately after `OnVertexBuildHeight`. By this point
`data.vertHeight` has its final value. Use this to write to color, UV, or other
per-vertex channels rather than modifying height.

::: info
Unlike `OnVertexBuildHeight`, this is **not** called during the fake-build
pass, so it is safe to do work here that depends on a real quad existing.
:::

::: details Existing Uses
`OnVertexBuild` is the primary place for non-height per-vertex work. Stock usages fall
into a few patterns:

* **Vertex color** — the majority of mods write to `data.vertColor` using texture maps
  (`PQSMod_VertexColorMap`, `PQSMod_HeightColorMap`, `PQSMod_HeightColorMap2`), noise
  generators (`PQSMod_VertexColorNoise`, `PQSMod_VertexSimplexColorRGB`,
  `PQSMod_VertexSimplexNoiseColor`), solid colors (`PQSMod_VertexColorSolid`), or
  height-based land-class lookups (`PQSMod_VertexPlanet`, `PQSMod_VoronoiCraters`).
* **Alpha only** — `PQSMod_AltitudeAlpha` writes only the alpha channel of
  `data.vertColor` based on the vertex's altitude above the sphere radius.
* **UV channels** — `PQSMod_AltitudeUV` writes altitude-scaled values into UV3;
  `PQSMod_TangentTextureRanges` writes height-quantized ranges into the tangent X
  component.
* **Decal blending** — `PQSMod_MapDecal` and `PQSMod_MapDecalTangent` blend a
  color-map texture into vertex color within a circular region on the surface.
:::

### Quad Lifecycle
#### `void OnQuadCreate(PQ quad)`
Called when a new `PQ` quad object is instantiated. Use this to attach any
per-quad state your mod needs to track (e.g. data arrays sized to the quad's
vertex count).

::: details Existing Uses
No stock `PQSMod` overrides this method. Per-quad state in stock KSP is typically
allocated in `OnQuadBuilt` or set up via helper components registered during
`OnSphereStarted`.
:::

#### `void OnQuadDestroy(PQ quad)`
Called just before a quad is destroyed and its mesh returned to the pool. Use
this to clean up any per-quad state created in `OnQuadCreate`.

::: details Existing Uses
* `PQSMod_QuadMeshColliders` disables the quad's `MeshCollider` and clears its mesh
  and material references.
* `PQSMod_SurfaceObjectQuads` terminates the `SurfaceObject` component that was
  attached to the quad during `OnQuadBuilt`.
* `PQSROCControl` removes its cached ROC-type entries for the destroyed quad.
:::

#### `void OnQuadPreBuild(PQ quad)`
Called at the start of a quad's build pass, before any vertex callbacks are
invoked. Use this to prepare per-quad data that will be consumed in
`OnVertexBuildHeight` / `OnVertexBuild`.

::: details Existing Uses
The dominant pattern is computing a per-quad inclusion flag by checking the angular
distance between the quad and a target position, then skipping all per-vertex work if
the quad falls outside the relevant region:

* `PQSMod_FlattenArea`, `PQSMod_FlattenAreaTangential`, `PQSMod_MapDecal`, and
  `PQSMod_MapDecalTangent` all set a `quadActive` flag this way.
* `PQSLandControl` checks the quad's subdivision level against the minimum required for
  scatter rendering and sets a `scatterActive` flag accordingly.
* `PQSROCControl` similarly gates ROC placement by subdivision level.
* `PQSMod_RemoveQuadMap` initialises a `quadVisible` flag to `false` before per-vertex
  map sampling in `OnVertexBuild`.
:::

#### `void OnMeshBuild()`
Called once all the vertices for the current quad have been built but before
that vertex data is actually assigned to the mesh. You can modify the mesh
data stored either directly in current quad (at `PQS.buildQuad`) or in various
static arrays directly on `PQS`.

::: details Existing Uses
The only stock mode that uses this is `PQSMod_TangentTextureRanges`. It stores
the height offsets in a temporary in `OnVertexBuild` and then copies those values
to the X component of `PQS.cacheTangents` in `OnMeshBuild`.
:::

#### `void OnQuadBuilt(PQ quad)`
Called after `OnMeshBuild`, once the quad is fully constructed. This is the
standard place to apply a material or do any final per-quad setup that should
only happen once after the geometry is ready.

::: details Existing Uses
Stock usages fall into several patterns:

* **Scatter spawning** — `PQSLandControl` and `PQSROCControl` assign pre-pooled
  scatter mesh controllers to qualifying quads; `PQSMod_MeshScatter` creates a
  per-quad controller for procedural mesh placement.
* **Material assignment** — `PQSMod_MaterialQuadRelative` sets up UV projection
  matrices on the quad's unique material instance; `PQSMod_TextureAtlas` analyses
  vertex positions to choose an atlas blend variant and packs blend data into UV3.
* **Collision** — `PQSMod_QuadMeshColliders` creates and attaches a `MeshCollider`
  at the configured subdivision level.
* **Surface objects** — `PQSMod_SurfaceObjectQuads` creates a `SurfaceObject`
  component for vessel-surface interaction tracking.
* **LOD adjustment** — `PQSMod_QuadEnhanceCoast` tweaks the quad's
  `subdivideThresholdFactor` to push extra detail near coastlines.
* **Visibility** — `PQSMod_RemoveQuadMap` sets `quad.isForcedInvisible` based on a
  height-map sample.
* **State reset** — `PQSMod_FlattenArea`, `PQSMod_MapDecal`, and their tangential
  variants reset their `quadActive`/`buildHeight` flags so the next build pass starts
  clean.
:::

#### `void OnQuadUpdateNormals(PQ quad)`
Called after normals have been computed for a quad (either by Unity's
`RecalculateNormals` or by the custom normal pass when `MeshCustomNormals` is
requested). Use this to read or further adjust the normals on `quad.vertNormals`
before they are assigned to the mesh.

::: details Existing Uses
* `PQSMod_UVPlanetRelativePosition` recomputes UVs based on the updated normal vector.
:::

#### `void OnQuadUpdate(PQ quad)`
KSP never calls this method. If you want to register something to be called every
frame for a quad either add a new component to it with an `Update` method or register
a callback with `quad.onUpdate`.
