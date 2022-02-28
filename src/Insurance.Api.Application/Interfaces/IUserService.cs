﻿using Insurance.Api.Domain.Entities;
using System;
using System.Threading.Tasks;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.User;
using Insurance.Api.Application.Filters;


namespace Insurance.Api.Application.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<User> Authenticate(string email, string password);

        Task<GetUserDto> CreateUser(CreateUserDto dto);
        Task<bool> DeleteUser(Guid id);
        Task<GetUserDto> UpdatePassword(Guid id, UpdatePasswordDto dto);
        Task<PaginatedList<GetUserDto>> GetAllUsers(GetUsersFilter filter);
        Task<GetUserDto> GetUserById(Guid id);
    }
}
