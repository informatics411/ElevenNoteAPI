using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly ElevenNoteDbContext _context;
    public UserService(ElevenNoteDbContext context)
    {
        _context = context;
    }
    public async Task<bool> RegisterUserAsync(UserRegister model)
    {
        if (await GetUserByEmailAsync(model.Email) != null || await GetUserByUsernameAsync(model.Username) != null)
            return false;
        var entity = new UserEntity
        {
            Email = model.Email,
            Username = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateCreated = DateTime.Now
        };
        var passwordHasher = new PasswordHasher<UserEntity>();
        entity.Password = passwordHasher.HashPassword(entity, model.Password);

        _context.Users.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();
        return numberOfChanges == 1;
    }

    private async Task<UserEntity> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
    }
    private async Task<UserEntity> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
    }

    private async Task<UserDetail> GetUserByIdAsync(int userId)
    {
        var entity = await _context.Users.FindAsync(userId);
        if (entity is null)
            return null;

        var userDetail = new UserDetail
        {
            Id = entity.Id,
            Email = entity.Email,
            Username = entity.Username,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            DateCreated = entity.DateCreated
        };
        return userDetail;
    }

    Task<UserDetail> IUserService.GetUserByIdAsync(int userID)
    {
        throw new NotImplementedException();
    }
}
