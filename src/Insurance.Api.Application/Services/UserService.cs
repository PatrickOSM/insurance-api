using Insurance.Api.Application.Interfaces;
using Insurance.Api.Domain.Entities;
using Insurance.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;


namespace Insurance.Api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userRepository.Dispose();
            }
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (user == null || !BC.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}
