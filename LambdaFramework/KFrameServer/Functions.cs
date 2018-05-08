using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace KFrameServer
{
    public class Functions
    {
        IDynamoDBContext DDBContext { get; set; }

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
            ApiDefine.Initialize();
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            this.DDBContext = new DynamoDBContext(new AmazonDynamoDBClient(), config);
        }

        /// <summary>
        /// タスクの起動
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> StartTask(APIGatewayProxyRequest _request, ILambdaContext context)
        {
            var request = JsonConvert.DeserializeObject<Request>(_request?.Body);
            BaseTask task = Activator.CreateInstance(ApiDefine.ClassDictionary[request.TaskName]) as BaseTask;
            await task.Start(request.JsonData, DDBContext);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "{\"exec_js\":\"" + task.ExecJs + "\"}",
                Headers = new Dictionary<string, string> {
                    { "Content-Type", "application/json"},
                    { "Access-Control-Allow-Origin" , "*"},
                    { "Access-Control-Allow-Credentials" , "true" }
                }
            };
            return response;
        }

    }
}
