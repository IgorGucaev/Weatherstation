using Station.Common.Enums;
using System;

namespace Station.Common.Classes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DBAttribute : Attribute
    {
        public DBType Type;

        public DBAttribute(DBType type)
        {
            this.Type = type;
        }
    }
}