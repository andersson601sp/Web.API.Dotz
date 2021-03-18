using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.API.Dotz.Services;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Models;
using AutoMapper;
using System.Threading.Tasks;
using Web.API.Dotz.Dtos;
using Web.API.Dotz.Data.RepoOrder;
using Web.API.Dotz.Data.RepoOrderItems;
using Web.API.Dotz.Helpers;
using System.Collections.Generic;

namespace Web.API.Dotz.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;

        public readonly IRepositoryOrder _repo;

        public OrderController(IMapper mapper, IRepositoryOrder repo)
        {
            _mapper = mapper;
             _repo = repo;
        }

         [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var orders = await _repo.GetAllOrdersAsync(pageParams);

            var ordersResult = _mapper.Map<IEnumerable<OrderModel>>(orders);

            Response.AddPagination(orders.CurrentPage, orders.PageSize, orders.TotalCount, orders.TotalPages);

            return Ok(ordersResult);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _repo.GetOrderById(id);

            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] OrderModel model)
        {
            Order order = _mapper.Map<Order>(model);

            _repo.Add(order);
            if (_repo.SaveChanges())
            {
                return Created($"/order/{model.Id}", _mapper.Map<OrderModel>(order));
            }

            return BadRequest("Pedido não cadastrado");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _repo.GetOrderById(id);
            if (order == null) return BadRequest("pedido não encontrado");

            _repo.Delete(order);
            if (_repo.SaveChanges())
            {
                return Ok("Pedido deletado");
            }

            return BadRequest("Pedido não deletado");
        }
    }
}
