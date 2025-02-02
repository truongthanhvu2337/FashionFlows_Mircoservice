using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Services.Account.Application.UseCases.User.Queries.GetUserByKeyword
{
    public class GetUserByKeyword : IRequest<APIResponse>
    {
        public string? keyword { get; set; }

        public GetUserByKeyword(string? keyword)
        {
            this.keyword = keyword;
        }
    }
}
