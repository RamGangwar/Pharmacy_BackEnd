using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PharmacyApp.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //private  IDbConnection dbcon = null;
        // private IDbTransaction dbtran = null;

        private readonly IBaseUnitOfWork _unitOfWork;
        public GenericRepository(IBaseUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }
        protected IDbTransaction _DbTransaction => _unitOfWork.DbTransaction;

        protected IDbConnection _DbConnection => _unitOfWork.DbConnection;

        public async Task<int> Add(TEntity entity)
        {
            var result = await _DbConnection.InsertAsync<TEntity>(entity, _DbTransaction);
            return result;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            var result = await _DbConnection.DeleteAsync<TEntity>(entity, _DbTransaction);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetALL()
        {
            var result = await _DbConnection.GetAllAsync<TEntity>(_DbTransaction);
            return result;
        }
        public async Task<IEnumerable<TEntity>> GetByConditionAsync(Func<TEntity, bool> condition)
        {
            var allEntities = await _DbConnection.GetAllAsync<TEntity>(_DbTransaction);
            var result = allEntities.Where(condition);
            return result;
        }
        public async Task<TEntity> GetById(Object id)
        {
            var result = await _DbConnection.GetAsync<TEntity>(id, _DbTransaction);
            return result;
        }

        public async Task<TEntity> GetEntityAsync(Func<TEntity, bool> condition)
        {
            var allEntities = await _DbConnection.GetAllAsync<TEntity>(_DbTransaction);
            var result = allEntities.Where(condition).FirstOrDefault();
            return result;
        }
        public async Task<int> GetMaxValue(Func<TEntity, int> column, Func<TEntity, string> selector, string prefix)
        {
            var list = await _DbConnection.GetAllAsync<TEntity>(_DbTransaction);
            var result = list.OrderByDescending(column).Select(selector).FirstOrDefault();
            var output = result == null ? "0" : result.Substring(prefix.Length);
            return Convert.ToInt32(output);
        }

        public async Task<bool> Update(TEntity entity)
        {
            var result = await _DbConnection.UpdateAsync<TEntity>(entity, _DbTransaction);
            return result;
        }


    }
}
