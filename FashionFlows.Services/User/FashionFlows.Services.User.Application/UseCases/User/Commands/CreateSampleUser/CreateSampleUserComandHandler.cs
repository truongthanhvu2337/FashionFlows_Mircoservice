using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Services.Account.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Services.Account.Application.UseCases.User.Commands.CreateSampleUser;

public class CreateSampleUserComandHandler : IRequestHandler<CreateSampleUserCommand, APIResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateSampleUserComandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<APIResponse> Handle(CreateSampleUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new Domain.Entities.User
        {
            UserId = Guid.NewGuid(),
            Email = request.Email,
            FullName = request.Fullname,
            Phone = request.Phone,
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
        };

        await _userRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync();

        return new APIResponse
        {
            StatusResponse = HttpStatusCode.OK,
            Message = "Create successfully",
            Data = null
        };

    }
}
