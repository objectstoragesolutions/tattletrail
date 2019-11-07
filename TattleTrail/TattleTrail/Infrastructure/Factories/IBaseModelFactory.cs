using System;

namespace TattleTrail.Infrastructure.Factories {
    public interface IBaseModelFactory<TModel> where TModel: class {
        TModel Create(Guid id, String name, TimeSpan lifeTime);
    }
}