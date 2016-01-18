using System;
using System.Linq;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace OpenApiRouterTests
{
    public class ConsoleLogger : IObserver<KeyValuePair<string, object>>
    {
        readonly ITestOutputHelper output;

        public ConsoleLogger(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(KeyValuePair<string, object> value)
        {

            this.output.WriteLine($"{value.Key} {value.Value}");

        }
    }
}