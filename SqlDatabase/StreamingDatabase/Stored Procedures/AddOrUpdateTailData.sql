CREATE PROCEDURE dbo.AddOrUpdateTailData
@TailNumber INT,
@SchLocalDepDateTime DATETIMEOFFSET,
@PlannedTimeTurn INT
AS
MERGE INTO
	dbo.TailData T
USING
	(VALUES (@TailNumber, @SchLocalDepDateTime, @PlannedTimeTurn)) S(TailNumber,SchLocalDepDateTime,PlannedTimeTurn)
ON
	T.TailNumber = S.TailNumber
WHEN MATCHED THEN
	UPDATE SET 
		T.SchLocalDepDateTime = S.SchLocalDepDateTime,
		T.PlannedTimeTurn = S.PlannedTimeTurn
WHEN NOT MATCHED THEN
	INSERT (TailNumber, SchLocalDepDateTime, PlannedTimeTurn) 
	VALUES (S.TailNumber, S.SchLocalDepDateTime, S.[PlannedTimeTurn])
;
