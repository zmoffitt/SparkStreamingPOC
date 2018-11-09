CREATE TABLE [dbo].[TailData] (
    [TailNumber]          INT                                                NOT NULL,
    [LastUpdate]          DATETIME2 (7)                                      DEFAULT (sysdatetime()) NOT NULL,
    [SchLocalDepDateTime] DATETIMEOFFSET (7)                                 NOT NULL,
    [PlannedTimeTurn]     INT                                                NOT NULL,
    [ValidFrom]           DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ValidTo]             DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   NOT NULL,
    PRIMARY KEY CLUSTERED ([TailNumber] ASC),
    PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE=[dbo].[TailDataHistory], DATA_CONSISTENCY_CHECK=ON));

