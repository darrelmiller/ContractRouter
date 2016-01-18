using System;
using System.Linq;
using System.Collections.Generic;

namespace ContractRouter
{
    public class Vocabulary : Dictionary<string, Func<Context, object, Context>>
    {
        public void AddSimpleStringTermHandler(string term, string propertyName)
        {
            AddTermHandler(term, (c, o) =>
            {
                var descProp = c.Subject.GetType().GetProperty(propertyName);
                if (descProp != null)
                {
                    descProp.SetValue(c.Subject, o);
                }
                return null;
            });
        }
        public void AddTermHandler<TS, TO>(string term, Action<TS, TO> handler)
        {
            AddTermHandler(term, (s, o) => { handler((TS)s.Subject, (TO)Convert.ChangeType(o, typeof(TO))); return null; });
        }

        public void AddTermHandler(string term, Func<Context, object, Context> handler)
        {
            this.Add("*|" + term, handler);
        }

        public void AddTermHandler(string contextTerm, string term, Func<Context, object, Context> handler)
        {
            this.Add(contextTerm + "|" + term, handler);
        }

        public void AddDefaultTermHandler(string contextTerm, Func<Context, object, Context> handler)
        {
            this.Add(contextTerm + "|*", handler);
        }

        public Func<Context, object, Context> FindHandler(string contextTerm, string term)
        {
            var handler = GetHandler(contextTerm, term);
            if (handler == null)
            {
                handler = GetHandler(term);
            }
            if (handler == null)
            {
                handler = GetDefaultHandler(contextTerm);
            }
            return handler;
        }


        public Func<Context, object, Context> GetHandler(string contextTerm, string Term)
        {
            Func<Context, object, Context> value = null;
            if (this.TryGetValue(contextTerm + "|" + Term, out value))
            {
                return value;
            }
            else {
                return null;
            }
        }

        public Func<Context, object, Context> GetHandler(string Term)
        {
            Func<Context, object, Context> value = null;
            if (this.TryGetValue("*|" + Term, out value))
            {
                return value;
            }
            else {
                return null;
            }
        }

        public Func<Context, object, Context> GetDefaultHandler(string contextTerm)
        {
            Func<Context, object, Context> value = null;
            if (this.TryGetValue(contextTerm + "|*", out value))
            {
                return value;
            }
            else {
                return null;
            }

        }
    }
}