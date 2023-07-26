using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<SaleService> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public SaleService(
            ISaleRepository repository,
            ILogger<SaleService> logger,
            IMapper mapper,
            IProductRepository productRepository,
            ICustomerRepository customerRepository)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ResponseGeneric<int>> AddAsync(string email, RequestDTOSale request)
        {
            var response = new ResponseGeneric<int>();

            try
            {
                await _repository.CreateTransaction();
                var entity = _mapper.Map<Sale>(request);

                var customer = await _customerRepository.GetByEmailAsync(email);
                entity.CustomerId = customer!.Id;

                var product = await _productRepository.FindAsync(request.ProductId);
                if (product == null) throw new InvalidOperationException("No se encontró el producto");

                entity.Total = entity.Quantity * product.UnitPrice;
                await _repository.AddAsync(entity);

                await _repository.UpdateAsync();

                response.Data = entity.Id;

                _logger.LogInformation("La venta se realizói correctamente");

                response.Success = true;
            }
            catch (Exception ex)
            {
                await _repository.RollbackTransaction();
                response.Message = "Error al realizar la venta";
                response.Success = false;
                _logger.LogMessage(ex, nameof(SaleService));
            }

            return response;
        }

        public async Task<ResponseGeneric<ResponseDTOSale>> GetAsync(int id)
        {
            var response = new ResponseGeneric<ResponseDTOSale>();

            try
            {
                var sale = await _repository.FindAsync(id);
                response.Data =_mapper.Map<ResponseDTOSale>(sale);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogMessage(ex, nameof(GetAsync));
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponsePagination<ResponseDTOSale>> ListAsync(string email, string? filter, int page, int rows)
        {
            var response = new ResponsePagination<ResponseDTOSale>();

            try
            {
                var (collection, total) = await _repository.ListAsync(
                    p => p.Customer.Email.Equals(email) && p.Product.Name.Contains(filter ?? string.Empty),
                    s => _mapper.Map<ResponseDTOSale>(s),
                    o => o.Correlative,
                    page,
                    rows);

                response.Data = collection;
                response.Pages = total;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(ListAsync));
            }

            return response;
        }
    }
}
