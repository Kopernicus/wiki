# Scaled-space materials

The [`ScaledVersion { }`](/Syntax/ScaledVersion/) node's [`Material { }`](/Syntax/ScaledVersion/Material) paints a body's **distant, low-LOD sphere** — the version of the planet you see from far away, before the near-surface [PQS terrain](/Syntax/Material/PQS/) fades in as the camera descends. The shader is chosen with `type = …`.

- [Vacuum](/Syntax/Material/Scaled/Vacuum) — airless bodies; a painted colour map and normals, with no atmospheric rim.
- [Atmospheric](/Syntax/Material/Scaled/Atmospheric) — bodies with an atmosphere; adds a view-angle limb glow (Blinn-Phong lit).
- [AtmosphericStandard](/Syntax/Material/Scaled/AtmosphericStandard) — the same, lit with Unity's Standard PBR model (KSP 1.9+).
- [Gas Giant](/Syntax/Material/Scaled/GasGiant) — animated, procedurally-banded gas giants (Jool).
- [Star](/Syntax/Material/Scaled/Star) — the emissive, animated star surface (the Sun).
