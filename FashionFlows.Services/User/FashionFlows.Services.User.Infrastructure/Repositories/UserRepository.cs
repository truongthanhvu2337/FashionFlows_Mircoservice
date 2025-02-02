using FashionFlows.BuildingBlock.Infrastructure.Repositories.Common;
using FashionFlows.Services.Account.Domain.Entities;
using FashionFlows.Services.Account.Domain.Repositories;
using FashionFlows.Services.Account.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FashionFlows.Services.Account.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User, ApplicationDbContext>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public User? GetUserById(Guid userId)
        {
            return _context.Users.Find(userId);
        }

        public async Task<IEnumerable<User>> GetUsersByKeywordAsync(string keyword)
        {
            return await _context.Users.Where(a => a.Email!.StartsWith(keyword) || a.Email.Contains(keyword)).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId!.Equals(userId));
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email!.ToLower().Equals(email.ToLower()));
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email!.ToLower().Equals(email.ToLower()));
        }

        public async Task<bool> AddUser(User newUser)
        {
            if (_context.Users.Any(x => x.Email!.Equals(newUser.Email)))
            {
                return false;
            }
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return true;
        }





        public async Task<IEnumerable<User>> FilterUsersAsync(Guid? userId = null, string? fullName = null, string? email = null, string? phone = null, string? status = null)
        {
            var query = _context.Users.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(u => u.UserId == userId.Value);
            }

            if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(u => u.FullName != null && u.FullName.Contains(fullName));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email != null && u.Email.ToLower().Contains(email.ToLower()));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(u => u.Phone != null && u.Phone.Contains(phone));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(u => u.Status.Equals(status));
            }

            return await query.ToListAsync();
        }





    }
}
