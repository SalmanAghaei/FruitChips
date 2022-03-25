using Core.Application.Commands;

namespace I6.Utilities.Services.Commands
{
    public class CommandDispatcherDomainExceptionHandlerDecorator : CommandDispatcherDecorator
    {
        #region Fields
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region Constructors
        public CommandDispatcherDomainExceptionHandlerDecorator(CommandDispatcher commandDispatcher, IServiceProvider serviceProvider) : base(commandDispatcher)
        {
            _serviceProvider = serviceProvider;

        }

        #endregion

        #region Send Commands
        public override Task<CommandResult> Send<TCommand>(TCommand command)
        {
            try
            {
                var result = _commandDispatcher.Send(command);
                var ex = result.Exception;
                return result;
            }
            catch (Exception ex)
            {
                return ExceptionHandlingWithoutReturnValue<TCommand>(ex);
            }

        }

        public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            try
            {
                var result = _commandDispatcher.Send<TCommand, TData>(command);
                var ex = result.Exception;
                if (ex != null && ex is AggregateException)
                {
                    if (ex.InnerException != null && ex.InnerException is Exception)
                    {
                        throw ex.InnerException;
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                return ExceptionHandlingWithReturnValue<TCommand, TData>(ex);
            }
        }
        #endregion

        #region Privaite Methods
        private Task<CommandResult> ExceptionHandlingWithoutReturnValue<TCommand>(Exception ex)
        {
            var commandResult = new CommandResult
            {
                Status = System.Net.HttpStatusCode.InternalServerError
            };

            commandResult.Message=(GetExceptionText(ex));

            return Task.FromResult(commandResult);
        }

        private Task<CommandResult<TData>> ExceptionHandlingWithReturnValue<TCommand, TData>(Exception ex)
        {
            var commandResult = new CommandResult<TData>()
            {
                Status = System.Net.HttpStatusCode.InternalServerError
            };

            commandResult.Message=GetExceptionText(ex);

            return Task.FromResult(commandResult);
        }

        private string GetExceptionText(Exception exception)
        {
            //var translator = _serviceProvider.GetService<ITranslator>();
            //if (translator == null)
            //    return exception.ToString();

            //var result = (exception?.Parameters.Any() == true) ?
            //     translator[exception.Message, exception?.Parameters] :
            //       translator[exception.Message];

            

            return exception.Message;
        }
        #endregion
    }
}
