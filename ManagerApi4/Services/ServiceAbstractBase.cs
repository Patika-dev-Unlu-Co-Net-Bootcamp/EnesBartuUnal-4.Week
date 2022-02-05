using AutoMapper;
using ManagerApi4.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagerApi4.Services
{


    // Servisler tarafından kullanılan ve EntityFramework üzerinden oluşturulmuş entity'ler üzerinde CRUD işlemleri yapılmasını sağlayan base class 

    public abstract class ServiceAbstractBase<TEntity, TModel> : IService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
        private readonly ManagerDbContext _db;
        private readonly IMapper _mapper;

        protected ServiceAbstractBase(ManagerDbContext dbParameter, IMapper mapper)
        {
            _db = dbParameter;
            _mapper = mapper;
        }


        public List<TModel> Get(Expression<Func<TModel, bool>> predicate = null)
        {
            try
            {
                var list = _db.Set<TEntity>().ToList();
                var modellist = _mapper.Map<List<TEntity>, List<TModel>>(list);
                return modellist;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public TModel GetById(int id)
        {
            try
            {
                var entity = _db.Set<TEntity>().Find(id);
                var model = _mapper.Map<TModel>(entity);
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async void Add(TModel model, bool saveChanges = true)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                await _db.Set<TEntity>().AddAsync(entity);
                _db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(TModel model, bool saveChanges = true)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            try
            {
                var entity = _db.Set<TEntity>().Find(id);
                _db.Set<TEntity>().Remove(entity);
                _db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            _db.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}


