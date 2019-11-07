using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_getmonitorsasync_have_no_data_with_mentioned_id {

        Establish _context = () => {
            Fixture fixture = new Fixture();
            Id = fixture.Create<Guid>();
            _repository = Mock.Of<IRepository>(x => x.GetMonitorAsync(Id) == null);
            _controller = new Builder().WithRepository(_repository).Build();
        };

        Because of = () =>
            _result = _controller.GetMonitorAsync(Id);

        It should_return_not_found_result = () =>
            Mock.Get(_repository).Verify(x => x.GetMonitorAsync(Id), Times.Once);

        static MonitorsController _controller;
        static Guid Id;
        static IRepository _repository;
        static Task<IActionResult> _result;
    }
}
