## Example {#Example}
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
|landedDataValue|Decimal|Science multiplier for "landed" science.|
|splashedDataValue|Decimal|Science multiplier for "splashed down" science.|
|flyingLowDataValue|Decimal|Science multiplier for "flying low" science.|
|flyingHighDataValue|Decimal|Science multiplier for "flying high" science.|
|inSpaceLowDataValue|Decimal|Science multiplier for "in space low" science.|
|inSpaceHighDataValue|Decimal|Science multiplier for "in space high" science.|
|recoveryValue|Decimal|The recovery value for this body. It is a science multiplier as well as a multiplier for the recovery of a craft returning from this body.|
|flyingAltitudeThreshold|Decimal|The altitude when "flying low" becomes "flying high."|
|spaceAltitudeThreshold|Decimal|The altitude when "in space low" becomes "in space high."|
