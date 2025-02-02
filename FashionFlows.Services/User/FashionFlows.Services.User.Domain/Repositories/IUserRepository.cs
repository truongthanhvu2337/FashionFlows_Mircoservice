using FashionFlows.BuildingBlock.Infrastructure.Repositories;
using FashionFlows.Services.Account.Domain.Entities;

namespace FashionFlows.Services.Account.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetUsersByKeywordAsync(string keyword);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> AddUser(User newUser);
    User? GetByEmail(string email);
    User? GetUserById(Guid userId);
    Task<IEnumerable<User>> FilterUsersAsync(Guid? userId = null, string? fullName = null, string? email = null, string? phone = null, string? status = null);


    //Task<IEnumerable<UserStatisticResponseDto>> GetAllUserWithStatistics(int page, int pageSize, string sortBy, bool isAscending = false);



}
