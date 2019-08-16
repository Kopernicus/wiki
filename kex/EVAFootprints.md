---
layout: default
title: EVA Footprints
subtitle: Lets kerbals leave a mark! Warranty void if used extravehicular.
---

This handy, or should I say, "footy," part of Kopernicus Expansion allows kerbals on EVA on a body to make footprints on that body. However, footprints aren't permanent and disappear between game sessions and quicksaves/loads.

**Example**
```
Body
{
  PQS
  {
    allowFootprints = true
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|allowFootprints|Boolean|Whether to allow footprints on the specified body. Defaults to `false`.|