using System;
using System.Linq;
using System.Collections.Generic;
namespace SwaggerRouter
{
    public class Context
    {
        public object Subject { get; set; }
        public string Term { get; set; }

        public override string ToString()
        {
            return $"{Subject?.GetType().Name} : {Term}";
        }
    }
}