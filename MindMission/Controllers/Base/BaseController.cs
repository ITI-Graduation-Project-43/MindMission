﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.Base;
using MindMission.Application.Factories;
using MindMission.Application.Mapping;
using MindMission.Application.Responses;
using MindMission.Domain.Constants;


namespace MindMission.API.Controllers.Base
{
    public abstract class BaseController<TEntity, TDto> : ControllerBase where TEntity : class where TDto : class, IDtoWithId
    {
        private readonly IMappingService<TEntity, TDto> _entityMappingService;

        protected BaseController(IMappingService<TEntity, TDto> entityMappingService)
        {
            _entityMappingService = entityMappingService;
        }


        protected async Task<List<TDto>> MapEntitiesToDTOs(IEnumerable<TEntity> entities)
        {
            return (await Task.WhenAll(entities.Select(entity => _entityMappingService.MapEntityToDto(entity)))).ToList();
        }

        protected async Task<TDto> MapEntityToDTO(TEntity entity)
        {
            return await _entityMappingService.MapEntityToDto(entity);
        }
        protected TEntity MapDTOToEntity(TDto dto)
        {
            return _entityMappingService.MapDtoToEntity(dto);
        }

        protected ActionResult NotFoundResponse(string entityName)
        {
            return NotFound(string.Format(ErrorMessages.ResourceNotFound, entityName));
        }

        protected ResponseObject<TDto> CreateResponse(List<TDto> dtos, PaginationDto pagination, string entityType)
        {

            ResponseObject<TDto> response = ResponseObjectFactory.CreateResponseObject<TDto>(true, string.Format(SuccessMessages.RetrievedSuccessfully, entityType), dtos, pagination.PageNumber, pagination.PageSize);

            return response;
        }

        protected ResponseObject<TDto> CreateResponse(TDto dtos, PaginationDto pagination, string entityType)
        {

            ResponseObject<TDto> response = ResponseObjectFactory.CreateResponseObject<TDto>(true, string.Format(SuccessMessages.RetrievedSuccessfully, entityType), new List<TDto> { dtos }, pagination.PageNumber, pagination.PageSize);

            return response;
        }

        protected async Task<ActionResult> GetEntitiesResponse(Func<Task<IEnumerable<TEntity>>> serviceMethod, PaginationDto pagination, string entityName)
        {
            var entities = await serviceMethod.Invoke();

            if (entities == null)
                return NotFoundResponse(entityName);

            var entityDTOs = await MapEntitiesToDTOs(entities);
            var response = CreateResponse(entityDTOs, pagination, entityName);

            return Ok(response);
        }

        protected async Task<ActionResult> GetEntityResponse(Func<Task<TEntity>> serviceMethod, string entityName)
        {
            var entity = await serviceMethod.Invoke();

            if (entity == null)
                return NotFoundResponse(entityName);

            var entityDto = await MapEntityToDTO(entity);
            var response = CreateResponse(entityDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, entityName);

            return Ok(response);
        }

        protected async Task<ActionResult> DeleteEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<int, Task> serviceDeleteMethod, int id)
        {
            var entity = await serviceGetMethod.Invoke(id);

            if (entity == null)
                return NotFound();

            await serviceDeleteMethod.Invoke(id);

            return NoContent();
        }
        #region MyRegion
        protected async Task<ActionResult> AddEntityResponse(Func<TEntity, Task<TEntity>> serviceMethod, TDto dto, string entityName, string actionName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = MapDTOToEntity(dto);
            var addedEntity = await serviceMethod.Invoke(entity);

            var createdDto = await MapEntityToDTO(addedEntity);

            if (createdDto == null)
                return NotFoundResponse(entityName);

            var response = CreateResponse(createdDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, entityName);

            return CreatedAtAction(actionName, new { id = createdDto.Id }, response);
        }

        protected async Task<ActionResult> PatchEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceUpdateMethod, int id, JsonPatchDocument<TDto> patchDocument)
        {
            var entity = await serviceGetMethod.Invoke(id);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = await MapEntityToDTO(entity);
            patchDocument.ApplyTo(dto, error => ModelState.AddModelError("JsonPatch", error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            entity = _entityMappingService.MapDtoToEntity(dto);
            await serviceUpdateMethod.Invoke(entity);

            return NoContent();
        }

        protected async Task<ActionResult> PatchEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceUpdateMethod, int id, JsonPatchDocument<TDto> patchDocument, Action<TEntity, TDto> patchOperations)
        {
            var entity = await serviceGetMethod.Invoke(id);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = await MapEntityToDTO(entity);
            patchDocument.ApplyTo(dto, error => ModelState.AddModelError("JsonPatch", error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            patchOperations(entity, dto);

            await serviceUpdateMethod.Invoke(entity);

            return NoContent();
        }


        protected async Task<ActionResult> UpdateEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceUpdateMethod, int id, TDto dto, string entityName)
        {
            if (id.Equals(dto.Id))
            {
                var entity = await serviceGetMethod.Invoke(id);

                if (entity == null)
                    return NotFound();

                entity = MapDTOToEntity(dto);
                await serviceUpdateMethod.Invoke(entity);

                return NoContent();
            }
            else
            {
                return BadRequest($"{entityName} ID mismatch.");
            }
        }


        #endregion
    }

}
