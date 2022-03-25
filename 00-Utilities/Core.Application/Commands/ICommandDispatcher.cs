using Core.Application.Commands;
using System.Threading.Tasks;

namespace I6.Utilities.Services.Commands
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>;
    }
}
