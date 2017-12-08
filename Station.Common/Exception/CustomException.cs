using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Station.Common.Exception
{
    public class CustomException : ApplicationException
    {
        public virtual string Identifier { get { return "Error"; } }

        protected virtual string DefaultComment { get { return "Exception accured!"; } }

        public virtual string Comment { get; protected set; }

        public virtual int StatusCode { get; set; } = (int)HttpStatusCode.InternalServerError;

        public override string Message
        {
            get
            {
                return String.IsNullOrWhiteSpace(this.Comment) ? this.DefaultComment : this.Comment;
            }
        }

        public CustomException() { }

        public CustomException(string comment)
        { this.Comment = comment; }
    }

    public class CustomOperationException : CustomException
    {
        public override string Identifier { get { return "Operation"; } }

        public override int StatusCode { get { return (int)HttpStatusCode.MethodNotAllowed; } }

        protected override string DefaultComment { get { return "Invalid operation!"; } }

        public CustomOperationException(string comment = "")
            : base(comment)
        { }
    }

}
