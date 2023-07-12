using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Logging;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;
using System;

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
                response.Data = category.Id;
                response.Success = true;

                _logger.LogInformation("Concierto agregado correctamente");
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

        public async Task<ResponseGeneric<ResponseCategoryDTO>> FindByIdAsync(int id)
        {
            var response = new ResponseGeneric<ResponseCategoryDTO>();

            try
            {
                var category = await _repository.FindAsync(id);
                if (category == null) throw new ApplicationException("No se encontró la categoría");
                response.Data = _mapper.Map<ResponseCategoryDTO>(category);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(FindByIdAsync));
            }

            return response;
        }

        public async Task<ResponsePagination<ResponseCategoryDTO>> ListAsync(string? filter, int page, int rows)
        {
            var response = new ResponsePagination<ResponseCategoryDTO>();

            try
            {
                var categories = await _repository.ListAsync(filter, page, rows);
                response.Data = _mapper.Map<ICollection<ResponseCategoryDTO>>(categories.Collection);
                response.Pages = Utils.GetPages(categories.Total, rows);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = _logger.LogMessage(ex, nameof(ListAsync));
            }

            return response;
        }

        public async Task<ResponseBase> UpdateAsync(int id, RequestCategoryDTO request)
        {
            var response = new ResponseBase();

            try
            {
                var category = await _repository.FindAsync(id);
                if (category == null) throw new Exception("No se encontró la categoría");

                _mapper.Map(request, category);

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
