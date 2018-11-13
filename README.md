# Stream Processing POC

[Kappa](https://docs.microsoft.com/en-us/azure/architecture/data-guide/big-data/#kappa-architecture
)/[Lambda](https://docs.microsoft.com/en-us/azure/architecture/data-guide/big-data/#lambda-architecture) Architecture POC Sample

Here's a description of folder contents:

## Databricks

Samples of Stream Processing using Databricks Spark Structured Streaming

- [Spark Structured Streaming](https://spark.apache.org/docs/latest/structured-streaming-programming-guide.html)
- [Databricks Structured Streaming](
https://docs.azuredatabricks.net/spark/latest/structured-streaming/index.html)

## RealTimeAzFunc

Stateful Streaming sample using Azure Functions.

Two functions are deployed to Azure:

- `RealTimeAzFunc_EH`: Get called by EventGrid on newly created Blob in the monitored Azure Blob Storage. Reads Blob content and the send it ito the EventHub used to support Stream Processig
- `DataProcessingAzFunc`: Implement a stateful processing on the Stream (EventHubs) to calculate the `PlannedTimeTurn` KPI for a specific Tail Number.

## SqlDatabase

Azure SQL Database used to
- Store Azure Function state
- Be the Serving Layer in Azure Function and Stream Analytics examples

Feature used that may be useful to JetBlue:

- [Temporal Tables](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-temporal-tables) (to automatically store all changes made to data)
- [JSON Support](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-json-features)
- [Column-Store](https://azure.microsoft.com/en-us/blog/clustered-columnstore-index-in-azure-sql-database/) [In-Memory Tables](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-in-memory) (not used in the POC is stream volume was very low, useful for [Near-Real Time Operational Analytics](https://docs.microsoft.com/en-us/sql/relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics?view=sql-server-2017))


## StreamAnalyticsQueries

This example show how complex JSON can be manipulated using Stream Analytics and how data can be aggregate using [Temporal Windowing Functions](https://docs.microsoft.com/en-us/azure/stream-analytics/stream-analytics-window-functions). The output is send to PowerBI and Azure SQL at the same time.