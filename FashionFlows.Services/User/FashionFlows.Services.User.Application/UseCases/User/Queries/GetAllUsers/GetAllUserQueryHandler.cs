using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.Services.Account.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Services.Account.Application.UseCases.User.Queries.GetAllUsers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, APIResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task<APIResponse> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {

            var users = await _userRepository.GetAll(request.Page, request.Pagesize);

            return new APIResponse
            {
                StatusResponse = HttpStatusCode.OK,
                Message = "Get successfully",
                Data = users
            };
        }
    }
}
