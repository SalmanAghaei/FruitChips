using Core.Application.Commands;
using System.Net;

namespace I6.Utilities.Services.Commands
{
    public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
    where TCommand : ICommand<TData>
    {


        protected readonly CommandResult<TData> result = new();
        [Obsolete(message: "Services Dependency removed from Constructor in the next release")]
        public CommandHandler()
        {

        }

        public abstract Task<CommandResult<TData>> Handle(TCommand request);
        protected virtual Task<CommandResult<TData>> OkAsync(TData data)
        {
            result.Data = data;
            result.Status = HttpStatusCode.OK;
            result.Success = true;
            return Task.FromResult(result);
        }
        protected virtual CommandResult<TData> Ok(TData data)
        {
            result.Data = data;
            result.Status = HttpStatusCode.OK;
            result.Success = true;
            return result;
        }
        protected virtual Task<CommandResult<TData>> ResultAsync(TData data, HttpStatusCode status)
        {
            result.Data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual CommandResult<TData> Result(TData data, HttpStatusCode status)
        {
            result.Data = data;
            result.Status = status;
            return result;
        }


    }

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
    {

        protected readonly CommandResult result = new();

        [Obsolete(message: "Services Dependency removed from Constructor in the next release")]
        public CommandHandler()
        {

        }
        public abstract Task<CommandResult> Handle(TCommand request);

        protected virtual Task<CommandResult> OkAsync()
        {
            result.Status = HttpStatusCode.OK;
            return Task.FromResult(result);
        }


        
        protected virtual CommandResult Ok()
        {
            result.Status = HttpStatusCode.OK;
            return result;
        }
        protected virtual CommandResult Failed(string message)
        {
            result.Status = HttpStatusCode.BadRequest;
            result.Message = message;
            return result;
        }
        protected virtual Task<CommandResult> FailedAsync(string message)
        {
            result.Status = HttpStatusCode.BadRequest;
            result.Message = message;
            return Task.FromResult(result);
        }

        protected virtual Task<CommandResult> ResultAsync(HttpStatusCode status)
        {
            result.Status = status;
            return Task.FromResult(result);
        }
        protected virtual CommandResult Result(HttpStatusCode status)
        {
            result.Status = status;
            return result;
        }

    }
}
