using Core.Application.Commands;
using Core.Application.Queries;
using I6.Utilities.Services.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Api
{

    [ApiController]
    [Route("api/v01/[Controller]")]
    public class BaseContoller: ControllerBase
    {
        protected ICommandDispatcher CommandDispatcher => HttpContext.CommandDispatcher();
        protected IQueryDispatcher QueryDispatcher => HttpContext.QueryDispatcher();

        protected async Task<IActionResult> Create<TCommand, TCommandResult>(TCommand command) where TCommand : class, ICommand<TCommandResult>
        {
            var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        protected async Task<IActionResult> Create<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var result = await CommandDispatcher.Send(command);
            if (result.Status == HttpStatusCode.OK)
            {
                return StatusCode((int)HttpStatusCode.Created,result);
            }
            return BadRequest(result);
        }

        protected async Task<IActionResult> Edit<TCommand, TCommandResult>(TCommand command) where TCommand : class, ICommand<TCommandResult>
        {
            var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else if (result.Status == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        protected async Task<IActionResult> Edit<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var result = await CommandDispatcher.Send(command);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else if (result.Status == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }


        protected async Task<IActionResult> Delete<TCommand, TCommandResult>(TCommand command) where TCommand : class, ICommand<TCommandResult>
        {
            var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else if (result.Status == HttpStatusCode.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound, result);
            }
            return BadRequest(result);
        }

        protected async Task<IActionResult> Delete<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var result = await CommandDispatcher.Send(command);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else if (result.Status == HttpStatusCode.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound, result);
            }
            return BadRequest(result);
        }
        protected async Task<IActionResult> Query<TQuery, TQueryResult>(TQuery query) where TQuery : class, IQuery<TQueryResult>
        {
            var result = await QueryDispatcher.Execute<TQuery, TQueryResult>(query);
            if (result.Status == HttpStatusCode.NotFound || result.Data == null)
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }
            else if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
