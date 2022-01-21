using AutoMapper;
using Northwind.DataAccess.Abstract;
using Northwind.Entity.Base;
using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Entity.IBase;
using Microsoft.AspNetCore.Http;

namespace Northwind.BusinessLayer.Base
{
    public class BusinessLogicBase<T, TDto> : IGenericService<T, TDto> where T : EntityBase where TDto : DtoBase
    {
        #region Variables

        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _service;
        private readonly IGenericRepository<T> _repository;

        #endregion

        #region Constructor
        public BusinessLogicBase(IServiceProvider service)
        {
            _service = service;
            _unitOfWork = _service.GetService<IUnitOfWork>();
            _repository = _unitOfWork.GetRepository<T>();
        }

        #endregion

        public IResponse<TDto> Add(TDto item,bool saveChanges=true)
        {
            try
            {
                var resolvedResult = "";
                var TResult = _repository.Add(ObjectMapper.Mapper.Map<T>(item)); //item ı Veritabanındaki modele dönüştürüyor
                resolvedResult = String.Join(',', TResult.GetType().GetProperties().Select(x => $"-{x.Name} : {x.GetValue(TResult) ?? ""} - "));

                if (saveChanges)
                    Save();

                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = ObjectMapper.Mapper.Map<T,TDto>(TResult) // Veritabanındaki T yi Dto ya dönüştürür Data Tresult veri setini dönüştür.
                };
            }
            catch (Exception ex)
            {

                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null

                };
            }
        }

        public Task<TDto> AddAsync(TDto item)
        {
            throw new NotImplementedException();
        }

        public IResponse<bool> Delete(TDto item)
        {
            throw new NotImplementedException();
        }

        public IResponse<bool> DeleteById(int id,bool saveChanges = true)
        {
            try
            {
                _repository.Delete(id);
                if (saveChanges) 
                {
                    Save();
                }
                return new Response<bool>
                {
                    Message = "Success",
                    StatusCode = StatusCodes.Status200OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = false
                };
            }
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IResponse<TDto> Find(int id)
        {
            try
            {
                var entity = _repository.Find(id);
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = ObjectMapper.Mapper.Map<T, TDto>(entity)
                };
            }
            catch (Exception ex)
            {
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<List<TDto>> GetAll()
        {
            try
            {
                List<T> list = _repository.GetAll();
                var dtoList = list.Select(x => ObjectMapper.Mapper.Map<TDto>(x)).ToList();

                var response = new Response<List<TDto>>
                {
                    Message = "Success",
                    StatusCode = StatusCodes.Status200OK,
                    Data = dtoList
                };

                return response;
            }
            catch (Exception ex)
            {

                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<List<TDto>> GetAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                List<T> list = _repository.GetAll(expression).ToList();
                var dtoList = list.Select(x => ObjectMapper.Mapper.Map<TDto>(x)).ToList();

                var response = new Response<List<TDto>>
                {
                    Message = "Success",
                    StatusCode = StatusCodes.Status200OK,
                    Data = dtoList
                };

                return response;
            }
            catch (Exception ex)
            {

                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IQueryable<T> GetIQeryable()
        {
            throw new NotImplementedException();
        }


        public TDto Update(TDto item)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> UpdateAsync(TDto item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.SaveChanges();        
        }

    }
}
