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
            this.TYPE = null;
            this.CONTENT = null;
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