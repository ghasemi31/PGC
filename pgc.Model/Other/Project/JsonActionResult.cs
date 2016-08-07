using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class JsonActionResult
    {
        public JsonActionResult()
        {
            this.Parameters = new Dictionary<string, string>();
        }
        public JsonActionResult(bool res,string msg)
        {
            this.Parameters = new Dictionary<string, string>();
            this.Result = res;
            this.Message = msg;
        }
        public JsonActionResult(bool res, string msg,Dictionary<string, string> parameters)
        {
            this.Parameters = parameters;
            this.Result = res;
            this.Message = msg;
        }
        public bool Result { get; set; }

        public string Message   { get; set; }

        public Dictionary<string, string> Parameters;

    }
}
