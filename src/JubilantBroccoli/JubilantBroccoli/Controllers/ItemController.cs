using AutoMapper;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JubilantBroccoli.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : Controller
{
    private readonly IRepository<Item> _restaurantRepository;
    private readonly IMapper _mapper;


    public ItemController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _restaurantRepository = unitOfWork.GetRepository<Item>();
    }


    [HttpGet]
    [Route("/items")]
    public async Task<ActionResult<IPagedList<ItemDtoResponse>>> GetItems(string restaurantId)
    {
        var restaurantsPagedList = await _restaurantRepository.GetPagedListAsync(
            selector: x => x,
            predicate: x => x.Restaurants.Any(r => r.Id == restaurantId)
            );

        var response = _mapper.Map<IPagedList<ItemDtoResponse>>(restaurantsPagedList);

        return Ok(response);
    }
}