using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Services.Account.Application.UseCases.User.Queries.GetAllUsers
{
    public class GetAllUserQuery : IRequest<APIResponse>
    {
        public int Page { get; set; }
        public int Pagesize { get; set; }

        public GetAllUserQuery(int page, int pagesize)
        {
            Page = page;
            Pagesize = pagesize;
        }

    }
}
