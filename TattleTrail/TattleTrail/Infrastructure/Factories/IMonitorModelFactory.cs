﻿using System;
using System.Collections.Generic;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public interface IMonitorModelFactory {
        MonitorProcess Create(String monitorName, Int32 intervalTime, HashSet<String> subscribers);
    }
}