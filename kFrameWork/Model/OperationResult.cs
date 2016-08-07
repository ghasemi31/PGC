using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Enums;
using pgc.Model.Enums;

namespace kFrameWork.Model
{
    public class OperationResult
    {
        public OperationResult()
        {
            this.Messages = new List<UserMessageKey>();
            this.Result = ActionResult.Unknown;
            this.Data = new Dictionary<object, object>();
        }



        public void AddMessage(UserMessageKey Key)
        {
            this.Messages.Add(Key);
        }

        public ActionResult Result { get; set; }

        public List<UserMessageKey> Messages { get; set; }

        public Dictionary<object,object> Data { get; set; }
    }

    public enum ActionResult
    {
        Unknown = 0,
        Done = 1,
        DonWithFailure = 2,
        Failed = 3 ,
        Error = 3
    }
}
