using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Sample;
using System;
using System.Collections.Generic;

namespace KFrameServer
{
    public partial class ApiDefine
    {
        public const string COMMENT_TABLE_NAME = "CommentTable";
        


        /// <summary>
        /// 初期化パーシャル
        /// </summary>
        static partial void InitializeImpl()
        {
            tableDictionary.Add(COMMENT_TABLE_NAME, typeof(Comment));

            classDictionary.Add("LoadIndex", typeof(LoadIndexTask));
            classDictionary.Add("sample/LoadIndex", typeof(Sample.LoadIndexTask));
            classDictionary.Add("sample/AddComment", typeof(AddCommentTask));
            classDictionary.Add("cognito/LoadIndex", typeof(Cognito.LoadIndexTask));
            classDictionary.Add("cognito/SignUp", typeof(Cognito.SignUpTask));
            classDictionary.Add("cognito/Confirm", typeof(Cognito.ConfirmTask));
            classDictionary.Add("cognito/SignIn", typeof(Cognito.SignInTask));
        }
    }
}

