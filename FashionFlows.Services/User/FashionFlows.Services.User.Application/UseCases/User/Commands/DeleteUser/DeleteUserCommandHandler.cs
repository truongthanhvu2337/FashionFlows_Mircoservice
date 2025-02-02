using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Services.Account.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Services.Account.Application.UseCases.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, APIResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }


        public async Task<APIResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existUsers = await _userRepository.GetUserByIdAsync(request.Id);
            if (existUsers == null)
            {
                return new APIResponse
                {
                    StatusResponse = HttpStatusCode.NotFound,
                    Message = "Not Found",
                    Data = null,
                };
            }

            if (existUsers.Status == "Active")
            {
                existUsers.Status = "Blocked";
            }
            else if (existUsers.Status == "Blocked")
            {
                existUsers.Status = "Active";
            }
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
}
