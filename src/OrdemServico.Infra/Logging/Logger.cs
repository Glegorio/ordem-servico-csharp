using System;
using System.Configuration;
using System.IO;

namespace OrdemServico.Infra.Logging
{
    /// <summary>
    /// Logger simples thread-safe que escreve em arquivo
    /// </summary>
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logPath;

        static Logger()
        {
            var configured = ConfigurationManager.AppSettings["LogPath"] ?? "logs";
            _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configured);

            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);
        }
        public static void Info(string mensagem, string origem = null)
        {
            Write("INFO", mensagem, origem, null);
        }

        public static void Warn(string mensagem, string origem = null)
        {
            Write("WARN", mensagem, origem, null);
        }

        public static void Error(string mensagem, Exception ex = null, string origem = null)
        {
            Write("ERROR", mensagem, origem, ex);
        }

        private static void Write(string nivel, string mensagem, string origem, Exception ex)
        {
            try
            {
                var arquivo = Path.Combine(_logPath, "app-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
                var linha = string.Format("[{0:yyyy-MM-dd HH:mm:ss}] [{1}] [{2}] {3}",
                    DateTime.Now, nivel, origem ?? "-", mensagem);

                if (ex != null)
                    linha += Environment.NewLine + "  Exception: " + ex.GetType().Name + ": " + ex.Message
                          + Environment.NewLine + "  StackTrace: " + ex.StackTrace;

                lock (_lock)
                {
                    File.AppendAllText(arquivo, linha + Environment.NewLine);
                }
            }
            catch
            {
                // Logger nunca deve quebrar a aplicacao.
            }
        }
    }
}