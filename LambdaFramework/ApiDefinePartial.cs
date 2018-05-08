using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Blogger;
using System;
using System.Collections.Generic;

namespace KFrameServer
{
    public partial class ApiDefine
    {
        public const string COMMENT_TABLE_NAME = "CommentTable";
        public const string BLOG_TABLE_NAME = "BlogTable";


        /// <summary>
        /// 初期化パーシャル
        /// </summary>
        static partial void InitializeImpl()
        {
            tableDictionary.Add(COMMENT_TABLE_NAME, typeof(Comment));

            classDictionary.Add("LoadIndex", typeof(LoadIndexTask));
            classDictionary.Add("AddComment", typeof(AddCommentTask));
        }
    }
}

