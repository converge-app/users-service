using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Application.Helpers;
using Application.Models.DataTransferObjects;
using Application.Models.Entities;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private AppSettings _appSettings;

        public UsersController(IUserService userService, IUserRepository userRepository, IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreationDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createUser = _mapper.Map<User>(userDto);
            try
            {
                var createdUser = _userService.Create(createUser);
                return Ok(createdUser);
            }
            catch (Exception e)
            {
                if (e.Message == "Email is already taken")
                    return BadRequest(new {message = "Email is already taken"});
                return BadRequest(new {message = "Couldn't create user'"});
            }
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.Get();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var user = _userRepository.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] UserUpdateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _userService.Update(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new {Message = e.Message});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _userRepository.Remove(id);
            return Ok();
        }
    }
}