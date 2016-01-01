using System;
using System.Collections.Generic;

namespace OpenApiRouter
{
    public static class OpenApiVocab
    {

        public static Vocabulary Create()
        {
            var vocab = new Vocabulary();
            vocab.AddTermHandler<OpenApiDocument, string>("swagger", (s, o) =>
            {
                s.Version = o;
            });
            vocab.AddTermHandler<OpenApiDocument, string>("schemes", (s, o) =>
            {
                if (s.Schemes == null)
                {
                    s.Schemes = new List<String>();
                }
                s.Schemes.Add(o);
            });

            vocab.AddTermHandler("info", (c, o) =>
            {
                var sdoc = (OpenApiDocument)c.Subject;
                sdoc.Info = new Info();
                return new Context() { Subject = sdoc.Info, Term = "info" };
            });

            vocab.AddTermHandler("paths", (c, o) =>
            {
                return new Context() { Subject = c.Subject, Term = "paths" };
            });

            vocab.AddDefaultTermHandler("paths", (s, o) =>
            {
                var sdoc = (OpenApiDocument)s.Subject;
                var path = sdoc.AddPath((string)o);
                return new Context() { Subject = path, Term = "_path" };
            });

            vocab.AddTermHandler("_path", "get", (c, o) =>
            {
                return new Context()
                {
                    Subject = (c.Subject as Path)?.AddOperation("get", ""),
                    Term = "_operation"
                };
            });
            vocab.AddTermHandler("_path", "post", (s, o) =>
            {
                return new Context()
                {
                    Subject = (s.Subject as Path)?.AddOperation("post", ""),
                    Term = "_operation"
                };
            });

            vocab.AddSimpleStringTermHandler("description", "Description");
            vocab.AddSimpleStringTermHandler("title", "Title");
            vocab.AddSimpleStringTermHandler("operationId", "Id");
            vocab.AddSimpleStringTermHandler("x-controller", "XController");
            return vocab;
        }
    }
}