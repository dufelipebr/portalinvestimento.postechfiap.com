
namespace portalinvestimento.virtualtilab.com
{
    public class CustomLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly CustomLoggerProviderConfiguration _cfg;

        public CustomLogger(string nome, CustomLoggerProviderConfiguration cfg) 
        {
            this._cfg = cfg;
            this._loggerName = nome;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
                LogLevel logLevel, 
                EventId eventId, 
                TState state, 
                Exception? exception, 
                Func<TState, Exception?, string> formatter)
        {
            var mensagem = string.Format($"{DateTime.Now}-{logLevel}:{eventId} - {formatter(state, exception)}");
            EscreverArquivo(mensagem);
        }

        private void EscreverArquivo(string mensagem)
        {
            var caminhoArquivo = @$"C:\temp\Log-{DateTime.Now:yyyy-MM-dd}.txt";
            if (!File.Exists(caminhoArquivo))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoArquivo));
                File.Create(caminhoArquivo).Dispose();
            }

            using StreamWriter streamWriter = new StreamWriter(caminhoArquivo, true);
            streamWriter.WriteLine(mensagem);
            streamWriter.Close();
        }
    }
}
