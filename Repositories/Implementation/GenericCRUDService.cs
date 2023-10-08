using AutoMapper;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class CrudService<TModel, TDto>
    where TModel : class
    where TDto : class
{
    private readonly DbContext _dbContext;
    private readonly AutoMapper.IMapper _mapper;

    public CrudService(DbContext dbContext, AutoMapper.IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<TDto> GetAll()
    {
        return _dbContext.Set<TModel>()
            .Select(model => _mapper.Map<TDto>(model))
            .ToList();
    }

    public TDto? GetById(int id)
    {
        var model = _dbContext.Set<TModel>().Find(id);
        return model != null ? _mapper.Map<TDto>(model) : null;
    }

    public TDto Create(TDto dto)
    {
        var model = _mapper.Map<TModel>(dto);
        _dbContext.Set<TModel>().Add(model);
        _dbContext.SaveChanges();
        return _mapper.Map<TDto>(model);
    }

    public bool Update(int id, TDto dto)
    {
        var existingModel = _dbContext.Set<TModel>().Find(id);
        if (existingModel == null)
            return false;

        _mapper.Map(dto, existingModel);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var existingModel = _dbContext.Set<TModel>().Find(id);
        if (existingModel == null)
            return false;

        _dbContext.Set<TModel>().Remove(existingModel);
        _dbContext.SaveChanges();
        return true;
    }
}
