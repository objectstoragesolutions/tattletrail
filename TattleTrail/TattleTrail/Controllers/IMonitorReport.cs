using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TattleTrail.Controllers {
    interface IMonitorReport {
        Task<ActionResult<String>> SetProcessStatus(String id, double minutes);
    }
}
