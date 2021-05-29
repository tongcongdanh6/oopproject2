using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Entities
{
    public class Message
    {
        // Constructor
        public Message()
        {
            this.TYPE = "";
            this.CONTENT = "";
        }
        public Message(string type, string content)
        {
            this.TYPE = type;
            this.CONTENT = content;
        }
        public string TYPE { set; get; }

        private string _content;
        public string CONTENT {
            get
            {
                return this._content.ToUpper();
            }
            set
            {
                this._content = value;
            }
        }
    }
}