CREATE TABLE [dbo].[TailJsonData] (
    [TailNumber] INT                                                NOT NULL,
    [LastUpdate] DATETIME2 (7)                                      DEFAULT (sysdatetime()) NOT NULL,
    [JsonData]   NVARCHAR (MAX)                                     NOT NULL,
    [ValidFrom]  DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ValidTo]    DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   NOT NULL,
    PRIMARY KEY CLUSTERED ([TailNumber] ASC),
    PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE=[dbo].[TailJsonDataHistory], DATA_CONSISTENCY_CHECK=ON));

