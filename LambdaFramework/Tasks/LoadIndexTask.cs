using Amazon.DynamoDBv2.DataModel;
using KFrameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KFrameServer
{
    /// <summary>
    /// ロードインデックスタスク
    /// </summary>
    class LoadIndexTask : BaseTask
    {
        public override async Task Start(string _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await base.Start(_postJson, _dynamoDBContext);
            JsPath = "index.view.js";
            List<string> projects = new List<string>()
            {
                "sample"
            };
            var json = JsonConvert.SerializeObject(projects);
            await loadJs();
            ExecJs = ExecJs.Replace("JSON", json.Replace("\"", "\\\""));
        }
    }
}
