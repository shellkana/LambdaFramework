using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFrameServer
{
    public class BaseTask
    {
        public string ExecJs { get; set; }
        protected string JsPath { get; set; }

        /// <summary>
        /// 開始処理
        /// </summary>
        public virtual async Task Start(String _postJson, IDynamoDBContext _dynamoDBContext)
        {
            await Task.Run(() => { });
            if (validate())
            {
                onError();
            }
        }

        /// <summary>
        /// バリデード処理
        /// </summary>
        protected virtual bool validate()
        {
            return false;
        }

        /// <summary>
        /// エラー時処理
        /// </summary>
        protected virtual void onError()
        {

        }

        /// <summary>
        /// jsパスのjsを読み込む
        /// </summary>
        protected virtual async Task loadJs()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            HttpResponseMessage response = await client.GetAsync("https://shellkana.github.io/static_web_site/application/" + JsPath + "?" + Guid.NewGuid().ToString("N").Substring(0, 10));
            string result = await response.Content.ReadAsStringAsync();
            ExecJs = result.Replace(Environment.NewLine, "").Replace("\"", "\\\"");

        }
    }
}

