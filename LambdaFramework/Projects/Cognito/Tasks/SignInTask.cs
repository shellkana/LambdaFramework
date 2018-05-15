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
    class SignInTask : BaseTask
    {
        public override async Task Start(string _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await base.Start(_postJson, _dynamoDBContext);
            User user = JsonConvert.DeserializeObject<User>(_postJson);


            var authReq = new AdminInitiateAuthRequest()
            {
                UserPoolId = ApiDefine.CognitoPoolId,
                ClientId = ApiDefine.CognitoClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
            };
            authReq.AuthParameters.Add("USERNAME", user.Email);
            authReq.AuthParameters.Add("EMAIL", user.Email);
            authReq.AuthParameters.Add("PASSWORD", user.Password);

            var client = new AmazonCognitoIdentityProviderClient(ApiDefine.Credentials, RegionEndpoint.USWest2);
            AdminInitiateAuthResponse authResp = await client.AdminInitiateAuthAsync(authReq);
            // AccessTokenを元にUser名を取得
            var getUserReq = new GetUserRequest()
            {
                AccessToken = authResp.AuthenticationResult.AccessToken
            };

            var getUserResp = await client.GetUserAsync(getUserReq);
            /*var req = new AdminRespondToAuthChallengeRequest()
            {
                ChallengeName = ChallengeNameType.ADMIN_NO_SRP_AUTH,
                ClientId = ApiDefine.CognitoClientId,
                UserPoolId = ApiDefine.CognitoPoolId,
                Session = authResp.Session,
                ChallengeResponses = new Dictionary<string, string>() {
                    { "USERNAME", user.Email }, { "PASSWORD", user.Password }
                },
            };
            var resp =  await client.AdminRespondToAuthChallengeAsync(req);*/
            JsPath = "cognito/my.page.js";
            string json = JsonConvert.SerializeObject(getUserResp);
            await loadJs();
            ExecJs = ExecJs.Replace("_JSON", json.Replace("\"", "\\\""));
        }
    }
}
