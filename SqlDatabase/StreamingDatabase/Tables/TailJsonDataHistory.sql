CREATE TABLE [dbo].[TailJsonDataHistory] (
    [TailNumber] INT            NOT NULL,
    [LastUpdate] DATETIME2 (7)  NOT NULL,
    [JsonData]   NVARCHAR (MAX) NOT NULL,
    [ValidFrom]  DATETIME2 (7)  NOT NULL,
    [ValidTo]    DATETIME2 (7)  NOT NULL
);


GO
CREATE CLUSTERED INDEX [ixc_TailJsonDataHistory]
    ON [dbo].[TailJsonDataHistory]([ValidTo] ASC, [ValidFrom] ASC) WITH (DATA_COMPRESSION = PAGE);

