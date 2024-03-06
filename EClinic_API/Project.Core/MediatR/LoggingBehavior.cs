using MediatR;
using Microsoft.Extensions.Logging;
using Project.Core.Logger;

namespace Project.Core.MediatR
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<IRequest<TResponse>, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;
        public async Task<TResponse> Handle(IRequest<TResponse> request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.WriteLogInformation($"Handling {typeof(TRequest).Name}");

            var response = await next();

            _logger.WriteLogInformation($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }

}
