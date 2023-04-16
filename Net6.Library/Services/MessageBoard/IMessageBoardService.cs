using Core31.Library.Response;
using Net6.Library.Models.MessageBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6.Library.Services.MessageBoard
{
    public interface IMessageBoardService
    {
        Task<AppResponse> CreateMessageAsync(Message aMessage);
        Task<AppResponse<IEnumerable<Message>>> GetAllMessages();
        Task<AppResponse> UpdateMessage(Message aMessage);
        Task<AppResponse> DeleteMessage(int id);
    }
}
