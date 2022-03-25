using I6.Utilities.Services.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Application.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        #region Fields
        private readonly IServiceProvider _serviceProvider;
        private readonly Stopwatch _stopwatch;
  
        #endregion

        #region Constructors
        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _stopwatch = new Stopwatch();
         
        }
        #endregion

        #region Send Commands
        public async Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            
            _stopwatch.Start();
            try
            {
                //_logger.Sendlog(Serilog.Events.LogEventLevel.Debug,$"Routing command of type {command.GetType()} With value {command}  Start at {DateTime.Now}");
                var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
                return await handler.Handle(command);

            }
            catch (InvalidOperationException ex)
            {
                //_logger.Sendlog(Serilog.Events.LogEventLevel.Error, $"There is not suitable handler for {command.GetType()} Routing failed at {DateTime.Now}.");
                throw;
            }
            finally
            {
                _stopwatch.Stop();
            }

        }

        public  Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>
        {
            _stopwatch.Start();
            try
            {
                string msgStr = $"Routing command of type {command.GetType()} With value {command}  Start at {DateTime.Now}";
                var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
                var result = handler.Handle(command);
                return result;

            }
            catch (Exception ex)
            {
               
                throw;
            }
            finally
            {
                _stopwatch.Stop();
                
               
            }
        }
        #endregion

    }
}
