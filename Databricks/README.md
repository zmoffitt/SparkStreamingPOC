# Databricks Notes

Here's a description of the files available this folder, along with their purpose. In order to run the samples the following libraries needs to be imported into the Databricks cluster from Maven:

- com.microsoft.azure:azure-eventhubs-spark_2.11:2.3.6
- org.scalaj:scalaj-http_2.11:2.4.1
- com.lihaoyi:ujson_2.11:0.6.7

### stream-processing.dbc

An example that shows how to setup ingestion from EventHub and how data can be aggregated on the fly using Structured Streaming

[Structured Streaming with Azure Event Hubs](
https://docs.azuredatabricks.net/spark/latest/structured-streaming/streaming-event-hubs.html)


### powerbi-steraming.dbc

Show how to send data to Power BI Streaming API so that a dashboard that gets updated in real time can be created.

[Real-time streaming in Power BI: Pushing data to datasets
](https://docs.microsoft.com/en-us/power-bi/service-real-time-streaming#pushing-data-to-datasets)

### stateful-straming.dbc

In Databricks Structured Streaming there is no support for the LAG function and so, in order to compare current data with previous values, the "Stateful API" needs to be used:

[Arbitrary Stateful Processing in Apache Spark’s Structured Streaming](https://databricks.com/blog/2017/10/17/arbitrary-stateful-processing-in-apache-sparks-structured-streaming.html)
