using Api_Finish_Version.DTO.Product;
using Api_Finish_Version.Exceptions.Product;
using Api_Finish_Version.IRepository.Product;
using Api_Finish_Version.IServices.Product;
using Products = Api_Finish_Version.Models.Product.Product;
using AutoMapper;
using API_REST_PROYECT.DTOs.Product;

namespace Api_Finish_Version.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<bool> CreateProductAsync(ProductDto dto)
        {
            if (dto == null) throw new Exception("Datos incorrectos.");

            var product = _mapper.Map<Products>(dto);

            //Verificamos que no exista previamente este producto
            var exist = await _productRepository.ExistProductAsync(product.codeProduct);
            if (!exist)
                throw new ProductException("Ya existe un producto creado con este codigo.");

            var newProduct = await _productRepository.AddAsync(product);
            return newProduct;
        }

        public async Task<bool> DeleteProductAsync(string codeProduct)
        {
            //Validamos nuevamente que no sea null o vacio
            if (string.IsNullOrEmpty(codeProduct))
                throw new ProductException("El codigo del producto no puede ser nulo o vacio.");

            //Verificamos que exista el producto
            var exist = await _productRepository.GetProductByCodeAsync(codeProduct);
            if (exist == null)
                throw new ProductException("No existe un producto con este codigo.");


            //HAY QUE VERIFICAR QUE NO ESTE TOMADO PARA UNA FACTURA!!!!!!!!!


            //Si existe, lo eliminamos
            var deleted = _productRepository.DeleteAsync(exist);
            if (!deleted.Result)
                throw new ProductException("Error al eliminar el producto.");

            return deleted.Result;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            IEnumerable<Products> products = await _productRepository.GetAllAsync();
            if (products == null || !products.Any())
                throw new ProductException("No hay productos registrados.");

            //Mapeamos los productos a ProductDto
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            if (productDtos == null || !productDtos.Any())
                throw new ProductException("Error al mapear los productos.");

            //Retornamos la lista de productos
            return productDtos;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateDto dto)
        {
            //Verificamos que no sea null
            if (dto == null) throw new ProductException("Datos incorrectos.");

            //Verificamos que exista el producto
            Products? exist = await _productRepository.GetProductByCodeAsync(dto.codeProduct);
            if (exist == null)
                throw new ProductException("No existe un producto con este codigo.");

            //Si existe, lo editamos
            exist.productName = dto.nameProduct;
            exist.productDescription = dto.descriptionProduct;
            exist.imageUrl = dto.imgUrl;

            //Guardamos los cambios
            var updated = await _productRepository.UpdateAsync(exist);
            if (!updated)
                throw new ProductException("Error al actualizar el producto.");

            return updated;
        }
    }
}
