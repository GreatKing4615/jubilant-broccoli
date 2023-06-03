using AutoMapper;
using JubilantBroccoli.Domain.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JubilantBroccoli.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly IMapper _mapper;

        public RestaurantController(
            IMapper mapper
            )
        {
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetRestaurants(AuthenticationRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var test = HttpContext.User.Claims;

            return Created("", _mapper.Map<UserDto>(user));
        }
    }
}