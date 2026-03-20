using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yokohama_Tyres.Models;
using Microsoft.EntityFrameworkCore;

namespace Yokohama_Tyres.Repositories;

public class UserRepository
{
    private readonly YokohamaTyresDbContext _context;

    public UserRepository()
    {
        _context = new YokohamaTyresDbContext();
    }

    public User? GetUserByUsername(string username)
    {
        return _context.Users
            .FirstOrDefault(u => u.Username == username && u.IsActive);
    }


    public bool ValidateUser(string username, string password, out User? user)
    {
        user = GetUserByUsername(username);

        if (user == null)
            return false;

        return user.PasswordHash == password;
    }
}