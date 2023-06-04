using AutoMapper;
using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.Domain.Core.CustomExceptions;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Dtos.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JubilantBroccoli.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private const string UserWithoutId = "User don't have id. Are you log in?";


        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }


        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetCart()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(UserWithoutId);
            }

            var currentCart = await _orderService.GetCurrentCartAsync(userId);
            var response = _mapper.Map<OrderDto>(currentCart);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostCart(OrderItemDto orderedItem)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(UserWithoutId);
            }

            var currentCart = await _orderService.AddToCartAsync(
                userId,
                orderedItem.RestaurantId,
                orderedItem.Count,
                orderedItem.Id,
                orderedItem.ItemOptionIds);

            var response = _mapper.Map<OrderDto>(currentCart);

            return Ok(response);

        }

        [HttpDelete]
        public async Task<ActionResult<OrderItemDto>> RemoveItem(string itemId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(UserWithoutId);
            }

            var currentCart = await _orderService.RemoveFromCartAsync(
                userId,
                itemId
            );

            var response = _mapper.Map<OrderDto>(currentCart);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> ChangeOrderStatus(string orderId, OrderStatus orderStatus)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(UserWithoutId);
            }

            try
            {
                var currentCart = await _orderService.ChangeOrderStatusAsync(orderId, orderStatus);

                var response = _mapper.Map<OrderDto>(currentCart);

                return Ok(response);
            }
            catch (IncorrectStatusException ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> UserHistory()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(UserWithoutId);
            }

            var currentCart = await _orderService.GetOrderHistoryAsync(userId);

            var response = _mapper.Map<OrderDto>(currentCart);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> ClearCart()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(UserWithoutId);
            }

            var currentCart = await _orderService.ClearCartAsync(userId);

            var response = _mapper.Map<OrderDto>(currentCart);

            return Ok(response);
        }
    }
}
