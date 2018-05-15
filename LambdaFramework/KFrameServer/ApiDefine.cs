using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using System;
using System.Collections.Generic;

namespace KFrameServer
{
    /// <summary>
    /// Api定義クラス
    /// </summary>
    public partial class ApiDefine
    {
        public static Dictionary<string, Type> ClassDictionary { get { return classDictionary; } }
        private static Dictionary<string, Type> classDictionary = new Dictionary<string, Type>();
        private static Dictionary<string, Type> tableDictionary = new Dictionary<string, Type>();
        public static AWSCredentials Credentials = null;
        public static string CognitoClientId = "";
        public static string CognitoPoolId = "";



        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            InitializeImpl();
            CreateCredential();
            foreach (var kv in tableDictionary)
            {
                AWSConfigsDynamoDB.Context.TypeMappings[kv.Value] = new Amazon.Util.TypeMapping(kv.Value, kv.Key);
            }
        }

        /// <summary>
        /// 初期化パーシャル宣言
        /// </summary>
        static partial void InitializeImpl();

        /// <summary>
        /// 認証の追加
        /// </summary>
        static partial void CreateCredential();


    }
}
