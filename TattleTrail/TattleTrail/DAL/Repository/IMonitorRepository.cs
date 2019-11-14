using System;
using System.Threading.Tasks;

namespace TattleTrail.DAL.Repository {
    public interface IMonitorRepository<T>: IRepository<T> where T:class {
        Task<Boolean> CreateAsync(T model);
        Task DeleteAsync(Guid monitorId);
    }
}
