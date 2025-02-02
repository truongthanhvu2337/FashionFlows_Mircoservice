using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Services.Account.Application.UseCases.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<APIResponse>
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }

        public UpdateUserCommand(string? email, string? fullName, string? phone)
        {
            Email = email;
            FullName = fullName;
            Phone = phone;
        }
    }
}
