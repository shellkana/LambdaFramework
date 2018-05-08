using Amazon.DynamoDBv2.DataModel;
using KFrameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogger
{
    /// <summary>
    /// コメント追加タスク
    /// </summary>
    class AddCommentTask : BaseTask
    {
        public override async Task Start(string _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await base.Start(_postJson, _dynamoDBContext);
            JsPath = "blogger/add.comment.js";
            var comment = JsonConvert.DeserializeObject<Comment>(_postJson);
            comment.Id = Guid.NewGuid().ToString();
            comment.TimeStamp = DateTime.Now;
            await _dynamoDBContext.SaveAsync<Comment>(comment);
            var search = _dynamoDBContext.ScanAsync<Comment>(null);
            var page = await search.GetNextSetAsync();
            string json = JsonConvert.SerializeObject(page);
            await loadJs();
            ExecJs = ExecJs.Replace("JSON", json.Replace("\\\"", "”").Replace("\"", "\\\""));
        }
    }
}
