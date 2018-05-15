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
    /// サインアップタスク
    /// </summary>
    class SignUpTask : BaseTask
    {
        public override async Task Start(string _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await base.Start(_postJson, _dynamoDBContext);
            User user = JsonConvert.DeserializeObject<User>(_postJson);
            var signUpRequest = new SignUpRequest
            {
                ClientId = ApiDefine.CognitoClientId,
                Password = user.Password,
                Username = user.Email
                
            };
            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = user.Email
            };
            var nickNameAttribute = new AttributeType
            {
                Name = "nickname",
                Value = user.Name
            };
            signUpRequest.UserAttributes.Add(emailAttribute);
            signUpRequest.UserAttributes.Add(nickNameAttribute);

            var client = new AmazonCognitoIdentityProviderClient(ApiDefine.Credentials, RegionEndpoint.USWest2);
            var result = await client.SignUpAsync(signUpRequest);
            JsPath = "cognito/sign.up.js";
            string json = JsonConvert.SerializeObject(result);
            await loadJs();
            ExecJs = ExecJs.Replace("JSON", json.Replace("\"", "\\\""));
        }
    }
}
