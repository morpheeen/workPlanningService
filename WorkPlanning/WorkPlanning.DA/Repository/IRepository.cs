using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkPlanning.DA.Models;

namespace WorkPlanning.DA.Repository
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> Get<T>() where T : BaseEntity;

        /// <summary>
        /// Creates the entity
        /// </summary>
        void Add<T>(T entity) where T : BaseEntity;

        /// <summary>
        /// Updates the entity
        /// </summary>
        void Update<T>(T entity) where T : BaseEntity;

        /// <summary>
        /// Soft deletes the entity
        /// </summary>
        void Delete<T>(T entity) where T : BaseEntity;

        Task SaveChangesAsync();
    }
}
