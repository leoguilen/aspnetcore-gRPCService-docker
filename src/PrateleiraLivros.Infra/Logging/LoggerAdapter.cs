using Microsoft.Extensions.Logging;
using PrateleiraLivros.Dominio.Interfaces;
using System;

namespace PrateleiraLivros.Infra.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogError(Exception ex, params object[] args)
        {
            _logger.LogError(ex, ex.Message, args);
        }
    }
}
