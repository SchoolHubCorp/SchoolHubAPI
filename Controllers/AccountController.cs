﻿using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Models.DTO;
using SchoolHubApi.Services;

namespace SchoolHubApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] UserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        //[HttpPost("login")]
        //public ActionResult Login([FromBody] LoginDto dto)
        //{
        //    string token = _accountService.GenerateJwt(dto);
        //    return Ok(token);
        //}
    }
}
