using System;
using System.Threading.Tasks;

namespace TattleTrail.DAL.Repository {
    public interface IMonitorRepository<T>: IRepository<T> where T:class {
        Task CreateAsync(T model);
        Task DeleteAsync(Guid monitorId);
    }
}
