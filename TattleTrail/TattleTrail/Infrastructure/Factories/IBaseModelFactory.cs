﻿using System;

namespace TattleTrail.Infrastructure.Factories {
    public interface IBaseModelFactory<TModel> where TModel: class {
        TModel Create(String id, String name, TimeSpan lifeTime);
    }
}