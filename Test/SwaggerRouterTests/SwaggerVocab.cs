//using System;
//using System.Linq;
//using System.Collections.Generic;
//using SwaggerRouter;
//using System;
//using System.Collections.Generic;

//namespace SwaggerRouterTests
//{
//    public static class SwaggerVocab
//    {

//        public static Vocabulary Create()
//        {
//            var vocab = new Vocabulary();
//            vocab.AddTermHandler<SwaggerDocument, string>("swagger", (s, o) =>
//            {
//                s.Version = o;
//            });
//            vocab.AddTermHandler<SwaggerDocument, string>("schemes", (s, o) =>
//            {
//                if (s.Schemes == null)
//                {
//                    s.Schemes = new List<String>();
//                }
//                s.Schemes.Add(o);
//            });

//            vocab.AddTermHandler("info", (c, o) =>
//            {
//                var sdoc = (SwaggerDocument)c.Subject;
//                sdoc.Info = new Info();
//                return new Context() { Subject = sdoc.Info, Term = "info" };
//            });

//            vocab.AddTermHandler("paths", (c, o) =>
//            {
//                return new Context() { Subject = c.Subject, Term = "paths" };
//            });

//            vocab.AddDefaultTermHandler("paths", (s, o) =>
//            {
//                var sdoc = (SwaggerDocument)s.Subject;
//                var path = sdoc.AddPath((string)o);
//                return new Context() { Subject = path, Term = "_path" };
//            });

//            vocab.AddTermHandler("_path", "get", (c, o) =>
//            {
//                return new Context()
//                {
//                    Subject = (c.Subject as Path)?.AddOperation("get", ""),
//                    Term = "_operation"
//                };
//            });
//            vocab.AddTermHandler("_path", "post", (s, o) =>
//            {
//                return new Context()
//                {
//                    Subject = (s.Subject as Path)?.AddOperation("post", ""),
//                    Term = "_operation"
//                };
//            });
//            vocab.AddTermHandler("description", (c, o) =>
//            {
//                var descProp = c.Subject.GetType().GetProperty("Description");
//                if (descProp != null)
//                {
//                    descProp.SetValue(c.Subject, o);
//                }
//                return null;
//            });
//            vocab.AddSimpleStringTermHandler("title", "Title");
//            return vocab;
//        }
//    }
//}