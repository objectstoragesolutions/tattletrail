using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_updatemonitorasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitorDetails = fixture.Create<MonitorDetails>();
            _factory = Mock.Of<IMonitorModelFactory>(x => x.Create(monitorDetails) == new MonitorProcess());
            _repository = Mock.Of<IMonitorRepository<MonitorProcess>>();
            //_controller = new ()
            
        };
        Because of = () => { };
        It should_do_smthg = () => { };
		    
        static MonitorsController _controller;
        static MonitorDetails monitorDetails;
        static IMonitorRepository<MonitorProcess> _repository;
        static IMonitorModelFactory _factory;
        static IActionResult result;
    }
}
