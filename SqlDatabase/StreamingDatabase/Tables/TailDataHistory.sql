CREATE TABLE [dbo].[TailDataHistory] (
    [TailNumber]          INT                NOT NULL,
    [LastUpdate]          DATETIME2 (7)      NOT NULL,
    [SchLocalDepDateTime] DATETIMEOFFSET (7) NOT NULL,
    [PlannedTimeTurn]     INT                NOT NULL,
    [ValidFrom]           DATETIME2 (7)      NOT NULL,
    [ValidTo]             DATETIME2 (7)      NOT NULL
);

GO
CREATE CLUSTERED INDEX [ixc_TailDataHistory]
    ON [dbo].[TailDataHistory]([ValidTo] ASC, [ValidFrom] ASC) WITH (DATA_COMPRESSION = PAGE);

