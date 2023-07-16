using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            ILogger<ProductService> logger,
            IMapper mapper
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseGeneric<int>> AddAsync(RequestDTOProduct request)
        {
            var response = new ResponseGeneric<int>();
            try
            {
                var product = _mapper.Map<Product>(request);
                product.CreatedDate = DateTime.Now;
                await _repository.AddAsync(product);
                response.Data = product.Id;
                response.Success = true;

                _logger.LogInformation("Producto agregado correctamente");
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(AddAsync));
            }

            return response;
        }

        public async Task<ResponseBase> DeleteAsync(int id)
        {
            var response = new ResponseBase();
            try
            {
                await _repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message=_logger.LogMessage(ex, nameof(DeleteAsync));
            }

            return response;
        }

        public async Task<ResponseGeneric<ResponseDTOProduct>> FindByIdAsync(int id)
        {
            var response = new ResponseGeneric<ResponseDTOProduct>();
            try
            {
                var product = await _repository.FindAsync(id) ?? throw new ApplicationException("No se encontró el producto");
                response.Data=_mapper.Map<ResponseDTOProduct>(product);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(FindByIdAsync));
            }
            return response;
        }

        public async Task<ResponsePagination<ResponseDTOProduct>> ListAsync(string? filter, int page, int rows)
        {
            var response = new ResponsePagination<ResponseDTOProduct>();
            try
            {
                var (Collection, Total) = await _repository.ListAsync(filter, page, rows);
                response.Data = _mapper.Map<ICollection<ResponseDTOProduct>>(Collection);
                response.Pages = Utils.GetPages(Total, rows);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(ListAsync));
            }
            return response;
        }

        public async Task<ResponseBase> UpdateAsync(int id, RequestDTOProduct request)
        {
            var response = new ResponseBase();
            try
            {
                var product = await _repository.FindAsync(id) ?? throw new ApplicationException("No se encontró el producto");
                _mapper.Map(request, product);
                product.ModifiedDate = DateTime.Now;
                await _repository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(UpdateAsync));
            }
            return response;
        }
    }
}
