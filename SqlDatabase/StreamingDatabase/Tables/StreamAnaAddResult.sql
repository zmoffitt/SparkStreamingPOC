CREATE TABLE [dbo].[StreamAnaAddResult] (
    [EventId]                         NVARCHAR (100)  NULL,
    [FlightCarrierCode]               NVARCHAR (100)  NULL,
    [FlightNumber]                    NVARCHAR (100)  NULL,
    [EventTimeStamp]                  NVARCHAR (100)  NULL,
    [EventSubType]                    NVARCHAR (100)  NULL,
    [ActFlightType]                   NVARCHAR (100)  NULL,
    [FlightOriginStnCode]             NVARCHAR (10)   NULL,
    [OriginCode_IsInternational]      BIT             NULL,
    [FlightDestStnCode]               NVARCHAR (10)   NULL,
    [DestinationCode_IsInternational] BIT             NULL,
    [CancelledIndicator]              BIT             NULL,
    [DiversionsIndicator]             BIT             NULL,
    [SchFlightIndicator]              BIT             NULL,
    [BlockVariance]                   DECIMAL (12, 3) NULL
);

