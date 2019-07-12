The `Body { }` node is a wrapper node for the aspects of a body and contains a lot of the essential components for making a body. `Body { }` is a subnode of the `@Kopernicus` node.

**Subnodes**
- `Debug { }`
- `Template { }`
- `Properties { }`
- `Orbit { }`
- `ScaledVersion { }`
- `PQS { }`
- `Atmosphere { }`
- `Ocean { }`
- `Particles { }`

**Example**
```
Body
{
  name = Pear
  cacheFile = Fruits/Cache/Pear.bin
  
  Debug
  {
  ...
  }
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
}
