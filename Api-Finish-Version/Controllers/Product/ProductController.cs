using Api_Finish_Version.DTO.Product;
using Api_Finish_Version.Exceptions.Product;
using Api_Finish_Version.IServices.Image;
using Api_Finish_Version.IServices.Product;
using API_REST_PROYECT.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Finish_Version.Controllers.Product
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;

        public ProductController(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }


        //ALTA
        [HttpPost("crearProducto")]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductDto dto, IFormFile? image)
        {
            //Antes de realizar la operacion, el sistema ya verifica la existencia de todos los datos
            try
            {
                if (image != null)
                {
                    dto.imageUrl = await _imageService.SaveImageAsync(image, "products");
                }

                var create = await _productService.CreateProductAsync(dto);
                if (!create)
                    return BadRequest(new { status = 400, message = "Error al crear el producto." });
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { satus = 400, message = e.Message });
            }
            catch (ProductException e)
            {
                return BadRequest(new { status = 500, message = "Error interno del servidor: " + e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = 500, message = "Error inesperado: " + e.Message });
            }
        }

        //ELIMINAR
        [HttpDelete("eliminarProducto")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(string codeProduct)
        {
            //Antes de realizar la operacion, el sistema ya verifica la existencia de todos los datos
            try
            {
                var x = await _productService.DeleteProductAsync(codeProduct);
                if (!x) return BadRequest(new { status = 400, message = "Error al eliminar el producto." });

                //Si la operacion se realizo correctamente, devolvemos un mensaje de exito
                return Ok("Producto eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest(new { status = 500, message = "Error inesperado: " + e.Message });
            }
        }

        //ACTUALIZAR PRODUCTO
        [HttpPut("actualizarProducto")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto dto)
        {
            //Antes de realizar la operacion, el sistema ya verifica la existencia de todos los datos
            try
            {
                var update = await _productService.UpdateProductAsync(dto);
                if (!update)
                    return BadRequest(new { status = 400, message = "Error al actualizar el producto." });
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { satus = 400, message = e.Message });
            }
            catch (ProductException e)
            {
                return BadRequest(new { status = 500, message = "Error interno del servidor: " + e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = 500, message = "Error inesperado: " + e.Message });
            }
        }

        //OBTENER TODOS LOS PRODUCTOS
        [HttpGet("obtenerProductos")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                if (products == null || !products.Any())
                    return NotFound(new { status = 404, message = "No hay productos registrados." });
                return Ok(products);
            }
            catch (ProductException e)
            {
                return BadRequest(new { status = 500, message = "Error interno del servidor: " + e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = 500, message = "Error inesperado: " + e.Message });
            }
        }
    }
}
