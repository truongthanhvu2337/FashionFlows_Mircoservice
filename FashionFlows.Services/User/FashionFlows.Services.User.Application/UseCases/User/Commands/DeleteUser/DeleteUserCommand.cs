using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Services.Account.Application.UseCases.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<APIResponse>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
