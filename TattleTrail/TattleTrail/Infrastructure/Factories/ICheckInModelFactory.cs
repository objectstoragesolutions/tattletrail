using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public interface ICheckInModelFactory {
        CheckIn Create(Guid monitorId);
    }
}