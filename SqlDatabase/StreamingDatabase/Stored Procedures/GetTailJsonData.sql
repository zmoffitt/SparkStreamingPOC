CREATE PROCEDURE dbo.GetTailJsonData
@TailNumber INT
AS
SELECT JsonData FROM dbo.TailJsonData WHERE TailNumber = @TailNumber 
