using AutoMapper;
using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.Domain.Dtos.User;
using JubilantBroccoli.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JubilantBroccoli.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountController(
            UserManager<IdentityUser> userManager,
            IJwtGenerator jwtGenerator,
            IMapper mapper)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(SignInRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTemplate = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address
            };
            var result = await _userManager.CreateAsync(
                userTemplate,
                user.Password
            );


            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Created("", _mapper.Map<UserDto>(userTemplate));
        }

        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtGenerator.CreateToken(user);

            return Ok(token);
        }

    }
}