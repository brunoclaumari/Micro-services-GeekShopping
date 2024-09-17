using GeekShopping.ProductAPI.Data.VOs;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        //public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var productsList = await _service.FindAll();

                return Ok(productsList);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Houve um erro ao buscar os Produtos" });
            }
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> FindById(long id)
        {
            try
            {
                var productVO = await _service.FindProductById(id);

                if(productVO != null)
                    return Ok(productVO);

                return BadRequest(new { Error = $"Produto com id {id} não encontrado" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Houve um erro ao buscar os Produtos." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVO productVO)
        {
            var responseVO = await _service.Create(productVO);

            if(responseVO.Errors.Count > 0)
                return BadRequest(responseVO);

            return Created("api/[controller]",responseVO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductVO productVO)
        {
            var responseVO = await _service.Update(id,productVO);

            if (responseVO.Errors.Count > 0)
                return BadRequest(responseVO);

            return Ok(responseVO);

        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var responseVO = await _service.Delete(id);

                if (responseVO.Errors.Count > 0)
                    return BadRequest(responseVO);

                return Ok(new {Message = $"Produto id: {id} deletado com sucesso"});

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Houve um erro ao deletar produto id: {id}" });
            }

        }
    }
}
