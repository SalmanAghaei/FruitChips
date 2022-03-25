using Core.Application.Commands;
using Core.Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Utilities;
using Utilities.Extensions;

namespace I6.Utilities.Services.Commands
{
    public class CommandDispatcherValidationDecorator : CommandDispatcherDecorator
    {
        #region Fields
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CommandDispatcherValidationDecorator> _logger;
        #endregion

        #region Constructors
        public CommandDispatcherValidationDecorator(CommandDispatcherDomainExceptionHandlerDecorator commandDispatcher,
                                                    IServiceProvider serviceProvider, ILogger<CommandDispatcherValidationDecorator> logger)
                                                    : base(commandDispatcher)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        #endregion

        #region Send Commands
        public override Task<CommandResult> Send<TCommand>(TCommand command)
        {
            _logger.LogDebug("Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);
            var validationResult = Validate<TCommand, CommandResult>(command);

            if (validationResult != null)
            {
                _logger.LogInformation("Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Message);
                return Task.FromResult(validationResult);
            }
            _logger.LogDebug("Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
            return _commandDispatcher.Send(command);
        }

        public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            _logger.LogDebug("Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);

            var validationResult = Validate<TCommand, CommandResult<TData>>(command);

            if (validationResult != null)
            {
                _logger.LogInformation("Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Message);
                return Task.FromResult(validationResult);
            }
            _logger.LogDebug("Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
            return _commandDispatcher.Send<TCommand, TData>(command);
        }
        #endregion

        #region Privaite Methods
        private TValidationResult Validate<TCommand, TValidationResult>(TCommand command) where TValidationResult : ApiResult, new()
        {
            var validator = _serviceProvider.GetService<IValidator<TCommand>>();
            TValidationResult res = null;

            if (validator != null)
            {
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    res = new()
                    {
                        Status = System.Net.HttpStatusCode.InternalServerError
                    };
                    res.Message = validationResult.Errors.Select(x => x.ErrorMessage).JoinErrors();
                }
            }
            else
            {
                _logger.LogInformation("There is not any validator for {CommandType}", command.GetType());
            }
            return res;
        }
        #endregion
    }


  
}
