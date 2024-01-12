using System.Collections.Concurrent;
using System.Data.SqlTypes;

namespace apibronco.bronco.com.br
{
    public class CustomLoggerProvider : ILoggerProvider 
    {
        private readonly CustomLoggerProviderConfiguration _loggerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> _loggers = new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration cf) 
        {
            this._loggerConfig = cf;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, nome => new CustomLogger(nome, _loggerConfig));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        
    }
}
