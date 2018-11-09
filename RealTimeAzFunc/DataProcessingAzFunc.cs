using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;

namespace RealTimeAzFunc
{
    public static class DataProcessingAzFunc
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("DataProcessingAzFunc")]
        public static async System.Threading.Tasks.Task RunAsync([EventHubTrigger("%EventHubName%", Connection = "EventHubConnectionAppSetting", ConsumerGroup = "%ConsumerGroup%")]string myEventHubMessage, ILogger log)
        { 
            if (myEventHubMessage.Contains("TailNumber"))
            {
                //log.LogInformation($"C# Event Hub trigger function processed a message: {myEventHubMessage}");

                var str = Environment.GetEnvironmentVariable("sqldb_connection");
                var powerBiUrl = Environment.GetEnvironmentVariable("PowerBiEndPoint");

                try
                {
                    var message = JsonConvert.DeserializeObject<FlightData>(myEventHubMessage);
                    int tailNumber = Convert.ToInt32(message.Flight.ScheduledFlightLeg[0].CurrentFlightLeg[0].TailNumber);
                    DateTimeOffset schLocalDepDateTime = message.Flight.ScheduledFlightLeg[0].SchLocalDepDateTime;

                    using (SqlConnection conn = new SqlConnection(str))
                    {
                        // Get existing value of schLocalDepDateTime if tailNumber already exists in DB
                        var oldData = conn.QueryFirstOrDefault<TailData>(
                            "dbo.GetTailData",
                            new
                            {
                                @TailNumber = tailNumber,

                            },
                            commandType: CommandType.StoredProcedure
                        );

                        // Proceed to compare LocalDepDateTime if tailNumber exists in DB
                        if(oldData != null)
                        {
                            int variable = (schLocalDepDateTime - oldData.SchLocalDepDateTime).Minutes;

                            // Call SP to update the difference in SchLocalDepDateTime
                            conn.Query(
                                "dbo.AddOrUpdateTailData",
                                new
                                {
                                    @TailNumber = tailNumber,
                                    @SchLocalDepDateTime = schLocalDepDateTime,
                                    @PlannedTimeTurn = variable
                                },
                                commandType: CommandType.StoredProcedure
                            );
                            
                            // Create data to send to PowerBI Stream
                            var powerBiObj = new List<StreamingTimeTurn>();
                            powerBiObj.Add(new StreamingTimeTurn
                            {
                                TailNumber = tailNumber,
                                SchLocalDepDateTime = schLocalDepDateTime,
                                LastUpdate = new DateTimeOffset(DateTime.UtcNow),
                                PlannedTimeTurn = variable
                            });

                            var payload = JsonConvert.SerializeObject(powerBiObj);
                            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

                            // Send data to PowerBI Stream
                            var result = await httpClient.PostAsync(powerBiUrl, httpContent);
                            log.LogInformation(result.StatusCode.ToString());
                            var resultContent = await result.Content.ReadAsStringAsync();                            
                            log.LogInformation(resultContent);

                        }
                        
                        // Update existing TailNumber info or Creat if not found
                        conn.Query(
                            "dbo.AddOrUpdateTailJsonData",
                            new
                            {
                                @TailNumber = tailNumber,
                                @JsonData = myEventHubMessage
                            },
                            commandType: CommandType.StoredProcedure
                        );
                    }
                }
                catch(Exception ex)
                {
                    log.LogError("Exception = " + ex.Message);
                }
                
            }
        }
    }
}
