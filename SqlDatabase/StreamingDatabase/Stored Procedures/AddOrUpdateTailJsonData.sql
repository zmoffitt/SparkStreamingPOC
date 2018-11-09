
CREATE PROCEDURE [dbo].[AddOrUpdateTailJsonData]
@TailNumber INT,
@JsonData NVARCHAR(MAX)
AS
MERGE INTO
	dbo.TailJsonData T
USING
	(VALUES (@TailNumber, @JsonData)) S(TailNumber,JsonData)
ON
	T.TailNumber = S.TailNumber
WHEN MATCHED THEN
	UPDATE SET T.[JsonData] = S.JsonData
WHEN NOT MATCHED THEN
	INSERT (TailNumber, JsonData) VALUES (S.TailNumber, S.JsonData)
;