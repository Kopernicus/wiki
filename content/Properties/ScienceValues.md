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
|landedDataValue|Single|Science multiplier for "landed" science.|
|splashedDataValue|Single|Science multiplier for "splashed down" science.|
|flyingLowDataValue|Single|Science multiplier for "flying low" science.|
|flyingHighDataValue|Single|Science multiplier for "flying high" science.|
|inSpaceLowDataValue|Single|Science multiplier for "in space low" science.|
|inSpaceHighDataValue|Single|Science multiplier for "in space high" science.|
|recoveryValue|Single|The recovery value for this body. It is a science multiplier as well as a multiplier for the recovery of a craft returning from this body.|
|flyingAltitudeThreshold|Single|The altitude when "flying low" becomes "flying high."|
|spaceAltitudeThreshold|Single|The altitude when "in space low" becomes "in space high."|
