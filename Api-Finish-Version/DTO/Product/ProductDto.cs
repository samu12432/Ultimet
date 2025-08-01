﻿using System.ComponentModel.DataAnnotations;
using Api_Finish_Version.Models.Enums;

namespace Api_Finish_Version.DTO.Product
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Es necesario ingresar el nombre del producto.")]
        [MaxLength(100)]
        public string productName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario ingresar una descripcion del producto.")]
        [MaxLength(100)]
        public string productDescription { get; set; } = string.Empty;

        [EnumDataType(typeof(ProductType), ErrorMessage = "Tipo de producto no valido.")]
        public ProductType productCategory { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal productPrice { get; set; }

        public string imageUrl { get; set; } = string.Empty;


        [MinLength(1, ErrorMessage = "Debe haber al menos un insumo.")]
        public List<SupplyNecessaryDto> supplies { get; set; } = new();
    }
}
