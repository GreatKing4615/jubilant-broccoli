using AutoMapper;
using JubilantBroccoli.Domain.Dtos.Restaurant;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JubilantBroccoli.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : Controller
{
    private readonly IRepository<Restaurant> _restaurantRepository;
    private readonly IMapper _mapper;


    public RestaurantController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _restaurantRepository = unitOfWork.GetRepository<Restaurant>();
    }


    [HttpGet]
    [Route("/restaurants")]
    public async Task<ActionResult<IPagedList<RestaurantDtoResponse>>> GetRestaurants(int pageNumber = 1, int pageSize = 10)
    {
        var restaurantsPagedList = await _restaurantRepository.GetPagedListAsync(
            selector: x => x
            );

        var response = _mapper.Map<IPagedList<RestaurantDtoResponse>>(restaurantsPagedList);

        return Ok(response);
    }
}