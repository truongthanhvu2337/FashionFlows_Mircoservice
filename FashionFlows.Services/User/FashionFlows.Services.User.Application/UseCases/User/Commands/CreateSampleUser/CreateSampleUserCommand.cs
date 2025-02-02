using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Services.Account.Application.UseCases.User.Commands.CreateSampleUser;

public class CreateSampleUserCommand : IRequest<APIResponse>
{
    public string Fullname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

}
