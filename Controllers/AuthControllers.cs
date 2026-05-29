using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalQueueSystem.Data;
using HospitalQueueSystem.Dtos;
using HospitalQueueSystem.Models;
using HospitalQueueSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQueueSystem.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthControllers:ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly JwtService _jwtservice;
        public AuthControllers(HospitalDbContext context,JwtService jwtservice)
    {
        _context=context;
        _jwtservice=jwtservice;
    }
    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        var user =new User
        {
            Name=dto.Name,
            Email=dto.Email,
            Password=dto.Password
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
    }
    [HttpPost("Login")]
    public IActionResult Login(LoginDto dto)
    {
        var user=_context.Users.FirstOrDefault(x=>x.Email==dto.Email&&x.Password==dto.Password);
        if(user==null) return Unauthorized("Invalid Credentials");
        var token=_jwtservice.GenerateToken(user);
        return Ok(new {token});
    }

    }

