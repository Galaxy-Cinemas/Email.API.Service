using Galaxi.Bus.Message;
using Galaxi.Email.API.Service.Models;

namespace Galaxi.Email.API.Service.Service
{
    public interface IEmailService
    {
        void SendEmail(TickedCreated Ticket);
    }
}