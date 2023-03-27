---
layout: default
title: ScienceValues
---

**Example**
```
ScienceValues
{
  landedDataValue = 15
  splashedDataValue = 10
  flyingLowDataValue = 7.5
  flyingHighDataValue = 6.25
  inSpaceLowDataValue = 3.5
  inSpaceHighDataValue = 2
  recoveryValue = 1.5
  flyingAltitudeThreshold = 45000
  spaceAltitudeThreshold = 600000
}
```

|Property|Value|Description|
|--------|-----|-----------|
|landedDataValue|Float|Science multiplier for "landed" science.|
|splashedDataValue|Float|Science multiplier for "splashed down" science.|
|flyingLowDataValue|Float|Science multiplier for "flying low" science.|
|flyingHighDataValue|Float|Science multiplier for "flying high" science.|
|inSpaceLowDataValue|Float|Science multiplier for "in space low" science.|
|inSpaceHighDataValue|Float|Science multiplier for "in space high" science.|
|recoveryValue|Float|The recovery value for this body. It is a science multiplier as well as a multiplier for the recovery of a craft returning from this body.|
|flyingAltitudeThreshold|Float|The altitude when "flying low" becomes "flying high."|
|spaceAltitudeThreshold|Float|The altitude when "in space low" becomes "in space high."|
