using Amazon.DynamoDBv2.DataModel;
using KFrameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cognito
{
    /// <summary>
    /// ロードインデックスタスク
    /// </summary>
    class LoadIndexTask : BaseTask
    {
        public override async Task Start(string _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await base.Start(_postJson, _dynamoDBContext);
            JsPath = "cognito/index.view.js";
            await loadJs();
        }
    }
}
