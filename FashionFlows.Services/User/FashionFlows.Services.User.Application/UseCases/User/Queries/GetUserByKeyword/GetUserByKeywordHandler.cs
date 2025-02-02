using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.Services.Account.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Services.Account.Application.UseCases.User.Queries.GetUserByKeyword
{
    public class GetUserByKeywordHandler : IRequestHandler<GetUserByKeyword, APIResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByKeywordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<APIResponse> Handle(GetUserByKeyword request, CancellationToken cancellationToken)
        {

            var users = await _userRepository.GetUsersByKeywordAsync(request.keyword);
            return new APIResponse
            {
                StatusResponse = HttpStatusCode.OK,
                Message = "MessageCommon.Complete",
                Data = users
            };
        }
    }
}
