using Api_Finish_Version.DTO.Supply;
using Api_Finish_Version.Exceptions.Supply;
using Api_Finish_Version.IRepository.Supply;
using Api_Finish_Version.IServices.Supply;
using supply = Api_Finish_Version.Models.Supply.Supply;
using Api_Finish_Version.Models.Supply;
using AutoMapper;

namespace Api_Finish_Version.Services.Supply
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ISupplyRepository<supply> _supplyRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, ISupplyRepository<supply> supplyRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _supplyRepository = supplyRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddStock(StockDto dto)
        {
            //Nuevamente validamos que el dto pasado por parametro no sea nulo (no se pierda info en el hilo)
            if (dto == null) throw new Exception("Datos incorrectos.");

            //Validamos que el insumo ingresado exista
            var supply = await _supplyRepository.ExistSupplyByCode(dto.codeSupply);

            if (!supply)
                throw new SupplyException("No se encontró algun insumo con el código " + dto.codeSupply + ".");

            //Validamos que no exista un stock referente al codigo del articulo ingresado
            var exist = await _stockRepository.GetStockBySku(dto.codeSupply);
            if (!exist)
                throw new StockException("El articulo " + dto.codeSupply + " cuenta con stock registrado.");

            //Creamos el objeto Stock
            Stock newStock = _mapper.Map<Stock>(dto);

            //Guardamos en la base de datos
            //Retornamos exito en la tarea
            return await _stockRepository.AddAsync(newStock);
        }

        public async Task<IEnumerable<StockDto>> GetAllStock()
        {
            //Obtenemos y verificamos si existen registros
            var stocks = await _stockRepository.GetAllStock();
            if (stocks == null)
                throw new StockException("No hay registro de stock");

            //Antes de devolver un listado, setiamos la salida a informacion del Dto
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }

        public async Task<Stock> GetStockBySku(string sku)
        {
            //Buscamos y validamos que exista registro de stock con el Sku del insumo ingresado
            var stock = await _stockRepository.GetStockByCode(sku);
            if (stock != null)
                throw new StockException("No existe registro de stock relacionado al codigo" + sku + ".");

            return stock;
        }

        public async Task<bool> UpdateStock(StockDto dto)
        {
            //Nuevamente validamos que el dto pasado por parametro no sea nulo (no se pierda info en el hilo)
            if (dto == null) throw new Exception("Datos incorrectos.");

            //Verificamos que exista stock referente al codigo del insumo ingresado
            Stock? exist = await _stockRepository.GetStockByCode(dto.codeSupply);
            if (exist == null)
                throw new SupplyException("No existe registro de stock referente al codigo " + dto.codeSupply + ".");

            exist.stockQuantity = dto.stockQuantity;
            exist.stockUpdate = DateTime.Now;

            return await _stockRepository.UpdateStockAsync(exist);
        }
    }
}
