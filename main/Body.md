The `Body { }` node is a wrapper node for the aspects of a body and contains a lot of the essential components for making a body. `Body { }` is a subnode of the `@Kopernicus` node.

**Subnodes**
- `Template { }`
- `Properties { }`
- `Orbit { }`
- `ScaledVersion { }`
- `Atmosphere { }`
- `PQS { }`
- `Ocean { }`
- `Rings { }`
- `Particles { }`
- `HazardousBody { }`
- `SpaceCenter { }`
- `Debug { }`
- `PostSpawnOrbit { }`

**Example**
```
Body
{
  name = Pear
  cacheFile = Fruits/Cache/Pear.bin
  barycenter = false
  identifier = Grapefruit/Pear
  randomMainMenuBody = true
  contractWeight = 15
  
  Template
  {
  ...
  }
  Properties
  {
  ...
  }
  Orbit
  {
  ...
  }
  ScaledVersion
  {
  ...
  }
  PQS
  {
  ...
  }
  Ocean
  {
  ...
  }
  Debug
  {
  ...
  }
}
