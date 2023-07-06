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

        public async Task<ResponseGeneric<int>> AddAsync(RequestCategoryDTO request)
        {
            var response = new ResponseGeneric<int>();

            try
            {
                var category = _mapper.Map<Category>(request);
                await _repository.AddAsync(category);
                response.Success = true;
                response.Data = category.Id;

                _logger.LogInformation("Categoría agregada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogMessage(ex, nameof(AddAsync));
            }

            return response;
        }

        public Task<ResponseBase> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseGeneric<ResponseCategoryDTO>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseGeneric<ICollection<ResponseCategoryDTO>>> ListAsync(string? filter, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> UpdateAsync(int ind, RequestCategoryDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
