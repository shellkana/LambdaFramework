using Amazon.DynamoDBv2.DataModel;
using KFrameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Newtonsoft;

namespace Cognito
{
    /// <summary>
    /// タスク
    /// </summary>
    class ConfirmTask : BaseTask
    {
        public override async Task Start(string _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await base.Start(_postJson, _dynamoDBContext);
            User user = JsonConvert.DeserializeObject<User>(_postJson);
            var signUpRequest = new ConfirmSignUpRequest
            {
                ClientId = ApiDefine.CognitoClientId,
                ConfirmationCode = user.Password,
                Username = user.Name
            };
            

            var client = new AmazonCognitoIdentityProviderClient(ApiDefine.Credentials, RegionEndpoint.USWest2);
            var result = await client.ConfirmSignUpAsync(signUpRequest);
            JsPath = "cognito/confirm.js";
            string json = JsonConvert.SerializeObject(result);
            await loadJs();
            ExecJs = ExecJs.Replace("JSON", json.Replace("\"", "\\\""));
        }
    }
}
