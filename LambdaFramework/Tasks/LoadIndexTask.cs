using Amazon.DynamoDBv2.DataModel;
using KFrameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blogger
{
    /// <summary>
    /// ロードインデックスタスク
    /// </summary>
    class LoadIndexTask : BaseTask
    {
        public override async Task Start(string _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await base.Start(_postJson, _dynamoDBContext);
            JsPath = "blogger/helloworld.view.js";
            var search = _dynamoDBContext.ScanAsync<Comment>(null);
            var page = await search.GetNextSetAsync();
            page.Sort((a, b) => { return b.TimeStamp.CompareTo(a.TimeStamp); });
            string json = JsonConvert.SerializeObject(page);
            await loadJs();
            ExecJs = ExecJs.Replace("JSON", json.Replace("\"", "\\\""));
        }
    }
}
