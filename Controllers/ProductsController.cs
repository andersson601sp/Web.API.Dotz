using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.API.Dotz.Services;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Models;
using AutoMapper;
using System.Threading.Tasks;
using Web.API.Dotz.Dtos;
using Web.API.Dotz.Data.RepoProduct;
using Web.API.Dotz.Helpers;
using System.Collections.Generic;

namespace Web.API.Dotz.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public readonly IRepositoryProduct _repo;

        public ProductsController(IMapper mapper, IRepositoryProduct repo)
        {
            _mapper = mapper;
             _repo = repo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var products = await _repo.GetAllProductsAsync(pageParams);

            var productsResult = _mapper.Map<IEnumerable<ProductModel>>(products);

            Response.AddPagination(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages);

            return Ok(productsResult);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _repo.GetProductById(id);

            return Ok(product);
        }
    }
}
