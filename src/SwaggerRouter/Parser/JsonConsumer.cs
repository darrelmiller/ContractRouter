using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerRouter
{
    public interface IJsonConsumer
    {
        ParseMap GetParseMap(ParseContextObject contextObject, string objectName);

        object CreateObject(ParseContextObject contextObject, string objectName);
    }

    public class ParseMap
    {
        public IDictionary<string, Action<object, object>> Parsers { get; set; }
        public Action<string, object, object> DefaultParse { get; set; }

        public ParseMap(IDictionary<string, Action<object, object>> map)
        {
            Parsers = map ?? new Dictionary<string, Action<object, object>>();
        }

        internal Action<object, object> GetParser(string currentProperty)
        {
            if (Parsers.ContainsKey(currentProperty))
            {
                return Parsers[currentProperty];
            }
            else
            {
                return null;
            }
        }
    }

    public class ParseContextObject
    {
        public object ContextObject { get; set; }
        public string PropertyName { get; set; }
        public ParseMap ParseMap { get; set; }

        public ParseContextObject()
        {
            PropertyName = "";
        }
    }

}
