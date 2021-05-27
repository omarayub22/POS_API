using API_TEST.Data;
using API_TEST.DTOs;
using API_TEST.Interfaces;
using API_TEST.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API_TEST.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenservice;

        public AccountController(DataContext context, ITokenService tokenservice)
        {
            _tokenservice = tokenservice;
            _context = context;
        }

        [HttpPost("register")]

     /*   public async Task<ActionResult<AppUser>> Register(RegisterDTo registerDTo)
        {
            if (await UserExists(registerDTo.Username)) return BadRequest("Username is taken");
           
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDTo.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTo.Password)),
                PasswordSalt = hmac.Key
            };  // register user using post
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        } */
        public async Task<ActionResult<userDto>> Register(RegisterDTo registerDTo)
        {
            if (await UserExists(registerDTo.Username)) return BadRequest("Username is taken");
           
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDTo.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTo.Password)),
                PasswordSalt = hmac.Key
            };  // register user using post
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return new userDto{
                Username = user.UserName,
                Token = _tokenservice.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<userDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computerHash.Length; i++)
            {
                if(computerHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid passoword");
            }
                        return new userDto{
                Username = user.UserName,
                Token = _tokenservice.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
