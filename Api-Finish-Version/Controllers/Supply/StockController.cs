﻿using Api_Finish_Version.DTO.Supply;
using Api_Finish_Version.Exceptions.Supply;
using Api_Finish_Version.IServices.Supply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api_Finish_Version.Controllers.Supply
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }


        [HttpPost("crearStock")]
        [Authorize]
        public async Task<IActionResult> CreateStock([FromBody] StockDto dto)
        {
            //Antes de ejecutar la funcion, se realiza la validacion de MODELSTATE correspondiente al Dto
            try
            {
                //Derivamos la tarea de crear el stock a la capa correcta
                var result = await _stockService.AddStock(dto);
                if (!result)
                    return Unauthorized("El stock ya se encuentra creado."); //Codigo 401

                return Ok("Stock registrado correctamente."); //Codigo 200
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { satus = 400, message = e.Message });
            }
            catch (SupplyException e)
            {
                return BadRequest(new { satus = 400, message = e.Message });
            }
            catch (StockException e)
            {
                return BadRequest(new { satus = 400, message = e.Message });
            }
        }

        [HttpPut("editarStock")]
        [Authorize]
        public async Task<IActionResult> UpdateStock([FromBody] StockDto dto)
        {
            //Antes de ejecutar la funcion, se realiza la validacion de MODELSTATE correspondiente al Dto
            try
            {
                //Derivamos la tarea de crear el stock a la capa correcta
                var x = await _stockService.UpdateStock(dto);
                if(x) return Ok("Stock registrado correctamente."); //Codigo 200
                return BadRequest("Error");
            }
            catch (StockException e)
            {
                return BadRequest(new { satus = 400, message = e.Message });
            }
            catch (SupplyException e)
            {
                return BadRequest(new { satus = 400, message = e.Message });
            }
        }



        [HttpGet("verStock")]
        [Authorize]
        public async Task<IActionResult> GetAllStock()
        {
            //Antes de ejecutar la funcion, se realiza la validacion de MODELSTATE correspondiente al Dto
            try
            {
                //Derivamos la tarea de crear el stock a la capa correcta
                var stockList = await _stockService.GetAllStock();
                return Ok(stockList);
            }
            catch (StockException e)
            {
                return BadRequest(new { status = 400, error = e.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el stock", error = ex.Message });
            }
        }


        [HttpGet("stockBySku")]
        [Authorize]
        public async Task<IActionResult> GetStockBySKu(string sku)
        {
            //El sku proviene por paramentro
            try
            {
                if (sku.IsNullOrEmpty())
                    return BadRequest("Es necesario el codigo del insumo");

                //Derivamos la tarea a la capa correspondiente
                var stock = await _stockService.GetStockBySku(sku);

                //Devolvermos la cantidad
                return Ok(stock);
            }
            catch (StockException e)
            {
                return BadRequest(new { status = 400, error = e.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el stock", error = ex.Message });
            }
        }
    }
}
