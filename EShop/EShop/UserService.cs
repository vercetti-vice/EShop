using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.Entities;
using EShop.Helpers;
using EShop.Infrastructure.Data;

namespace EShop.Services
{
  public interface IUserService
  {
    AppUser Authenticate(string username, string password);
    IEnumerable<AppUser> GetAll();
    AppUser GetById(string id);
    AppUser Create(AppUser user, string password);
    void Update(AppUser user, string password = null);
    void Delete(string id);
  }

  public class UserService : IUserService
  {
    private ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
      _context = context;
    }

    public AppUser Authenticate(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        return null;

      var user = _context.Users.SingleOrDefault(x => x.UserName == username);

      // check if username exists
      if (user == null)
        return null;

      // check if password is correct
      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        return null;

      // authentication successful
      return user;
    }

    public IEnumerable<AppUser> GetAll()
    {
      return _context.Users;
    }

    public AppUser GetById(string id)
    {
      return _context.Users.Find(id);
    }

    public AppUser Create(AppUser user, string password)
    {
      // validation
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("Password is required");

      if (_context.Users.Any(x => x.UserName == user.UserName))
        throw new AppException("Username \"" + user.UserName + "\" is already taken");

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    public void Update(AppUser userParam, string password = null)
    {
      var user = _context.Users.Find(userParam.Id);

      if (user == null)
        throw new AppException("User not found");

      if (userParam.UserName != user.UserName)
      {
        // username has changed so check if the new username is already taken
        if (_context.Users.Any(x => x.UserName == userParam.UserName))
          throw new AppException("Username " + userParam.UserName + " is already taken");
      }

      // update user properties
      user.FirstName = userParam.FirstName;
      user.LastName = userParam.LastName;
      user.UserName = userParam.UserName;

      // update password if it was entered
      if (!string.IsNullOrWhiteSpace(password))
      {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }

      _context.Users.Update(user);
      _context.SaveChanges();
    }

    public void Delete(string id)
    {
      var user = _context.Users.Find(id);
      if (user != null)
      {
        _context.Users.Remove(user);
        _context.SaveChanges();
      }
    }

    // private helper methods

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
      if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
      if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i]) return false;
        }
      }

      return true;
    }
  }
}
