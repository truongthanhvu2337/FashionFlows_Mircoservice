using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Services.Account.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Services.Account.Application.UseCases.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, APIResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }


    public async Task<APIResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existUsers = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existUsers == null)
        {
            return new APIResponse
            {
                StatusResponse = HttpStatusCode.NotFound,
                Message = "Not Found",
                Data = null,
            };
        }

        existUsers.FullName = request.FullName;
        existUsers.Email = request.Email;
        existUsers.Phone = request.Phone;

        await _userRepository.Update(existUsers);
        await _unitOfWork.SaveChangesAsync();


        return new APIResponse
        {
            StatusResponse = HttpStatusCode.OK,
            Message = "Updated successfully",
            Data = null,
        };
    }
}
