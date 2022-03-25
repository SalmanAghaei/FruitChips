using Core.Application.Commands;
using System.Threading.Tasks;

namespace I6.Utilities.Services.Commands
{
    public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
    {
        Task<CommandResult<TData>> Handle(TCommand request);
    }
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<CommandResult> Handle(TCommand request);
    }
}
