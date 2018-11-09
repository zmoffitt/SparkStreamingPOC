
CREATE PROCEDURE dbo.GetTailData
@TailNumber INT
AS
SELECT 
	TailNumber,
	SFL.* 
FROM 
	dbo.TailJsonData J
CROSS APPLY
	OPENJSON(J.JsonData, '$.Flight.ScheduledFlightLeg') A
CROSS APPLY
	OPENJSON(A.value) WITH
		(
			FlightLegSeqNumber INT,
			SchLocalDepDateTime DATETIMEOFFSET
		) AS SFL
WHERE 
	TailNumber = @TailNumber
