using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository repository,
            ILogger<CategoryService> logger,
            IMapper mapper
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseGeneric<int>> AddAsync(RequestDTOCategory request)
        {
            var response = new ResponseGeneric<int>();

            try
            {
                var category = _mapper.Map<Category>(request);
                category.CreatedDate = DateTime.Now;
                await _repository.AddAsync(category);
                response.Data = category.Id;
                response.Success = true;

                _logger.LogInformation("Categoría agregada correctamente");
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
                response.Message = _logger.LogMessage(ex, nameof(DeleteAsync));
            }

            return response;
        }

        public async Task<ResponseGeneric<ResponseDTOCategory>> FindByIdAsync(int id)
        {
            var response = new ResponseGeneric<ResponseDTOCategory>();

            try
            {
                var category = await _repository.FindAsync(id) ?? throw new ApplicationException("No se encontró la categoría");
                response.Data = _mapper.Map<ResponseDTOCategory>(category);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(FindByIdAsync));
            }

            return response;
        }

        public async Task<ResponsePagination<ResponseDTOCategory>> ListAsync(string? filter, int page, int rows)
        {
            var response = new ResponsePagination<ResponseDTOCategory>();

            try
            {
                var (Collection, Total) = await _repository.ListAsync(filter, page, rows);
                response.Data = _mapper.Map<ICollection<ResponseDTOCategory>>(Collection);
                response.Pages = Utils.GetPages(Total, rows);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(ListAsync));
            }

            return response;
        }

        public async Task<ResponseGeneric<ICollection<ResponseDTOCategory>>> ListAsync()
        {
            var response = new ResponseGeneric<ICollection<ResponseDTOCategory>>();
            try
            {
                var categories = await _repository.ListAsync();
                response.Data = _mapper.Map<ICollection<ResponseDTOCategory>>(categories);
                response.Success = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = "Error al listar las categorías";
                _logger.LogError(ex, $"{response.Message} {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseBase> UpdateAsync(int id, RequestDTOCategory request)
        {
            var response = new ResponseBase();

            try
            {
                var category = await _repository.FindAsync(id) ?? throw new Exception("No se encontró la categoría");
                _mapper.Map(request, category);
                category.ModifiedDate = DateTime.Now;
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
