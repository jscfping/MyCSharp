using Core31.Library.Response;
using Microsoft.AspNetCore.Mvc;
using Net6.Library.Models.MessageBoard;
using Net6.Library.Services.MessageBoard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebNet6.Controllers.Api.V2
{
    [ApiVersion("2.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PostgresMessageBoardController : ControllerBase
    {

        private readonly IMessageBoardService _messageBoardService;

        public PostgresMessageBoardController(IMessageBoardService messageBoardService)
        {
            _messageBoardService = messageBoardService;
        }


        [HttpGet("Messages")]
        public Task<AppResponse<IEnumerable<Message>>> CreateMessage()
        {
            return _messageBoardService.GetAllMessages();
        }

        [HttpPost("Messages")]
        public Task<AppResponse> CreateMessage(Message aMessage)
        {
            return _messageBoardService.CreateMessageAsync(aMessage);
        }


        [HttpPut("Messages/{MessagesId}")]
        public Task<AppResponse> CreateMessage([FromRoute]int MessagesId, Message aMessage)
        {
            aMessage.Id = MessagesId;
            return _messageBoardService.UpdateMessage(aMessage);
        }


        [HttpDelete("Messages/{MessagesId}")]
        public Task<AppResponse> CreateMessage([FromRoute] int MessagesId)
        {
            return _messageBoardService.DeleteMessage(MessagesId);
        }



    }
}
