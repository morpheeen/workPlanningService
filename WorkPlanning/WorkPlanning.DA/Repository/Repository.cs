using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkPlanning.DA.Models;
using WorkPlanning.Infrastructure.Configuration;

namespace WorkPlanning.DA.Repository
{
    public class Repository : IRepository
    {
        private DataContext _dataContext;
        private readonly IOptions<AppConfig> _appConfig;

        public Repository(IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
        }

        public IQueryable<T> Get<T>() where T : BaseEntity
        {
            EnsureInitialized();

            return _dataContext.Set<T>();
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            EnsureInitialized();

            var set = _dataContext.Set<T>();
            set.Add(entity);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            EnsureInitialized();

            if (_dataContext.Entry(entity).State == EntityState.Modified) return;

            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            EnsureInitialized();
            _dataContext.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            if (_dataContext == null)
                return;
            await _dataContext.SaveChangesAsync();
        }

        private void EnsureInitialized()
        {
            if (_dataContext != null)
                return;

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(_appConfig.Value.SqlConnectionString).EnableSensitiveDataLogging();

            _dataContext = new DataContext(optionsBuilder.Options);
        }
        public void Dispose()
        {
            _dataContext?.Dispose();
        }
    }
}
