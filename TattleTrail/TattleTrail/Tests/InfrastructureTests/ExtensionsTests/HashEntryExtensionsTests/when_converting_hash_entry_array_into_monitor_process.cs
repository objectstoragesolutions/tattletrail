using AutoFixture;
using Machine.Specifications;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.ExtensionsTests.HashEntryExtensionsTests {

    [Subject(typeof(HashEntryExtensions))]
    public class when_converting_hash_entry_array_into_monitor_process {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            expectedProcessName = fixture.Create<String>();
            expectedProcessLifeTime = fixture.Create<Int32>();
            expectedAmountOfSubscribers = fixture.Create<String[]>();
            var processName = new HashEntry(nameof(MonitorDetails.ProcessName), expectedProcessName);
            var lifeTime = new HashEntry(nameof(MonitorDetails.IntervalTime), expectedProcessLifeTime);
            var subscribers = new HashEntry(nameof(MonitorDetails.Subscribers), JsonConvert.SerializeObject(expectedAmountOfSubscribers));
            hashEntry = new HashEntry[] { processName, lifeTime, subscribers };

        };

        Because of = () =>
            result = hashEntry.AsMonitorProcess(id);

        It conver_to_monitor_process_with_expected_id = () =>
            result.Id.ShouldEqual(id);

        It conver_to_monitor_process_with_expected_processname = () =>
            result.MonitorDetails.ProcessName.ShouldEqual(expectedProcessName);

        It conver_to_monitor_process_with_expected_lifetime = () =>
            result.MonitorDetails.IntervalTime.ShouldEqual(expectedProcessLifeTime);

        It conver_to_monitor_process_with_expected_subscribers = () =>
            result.MonitorDetails.Subscribers.ShouldEqual(expectedAmountOfSubscribers);

        static Guid id;
        static String expectedProcessName;
        static Int32 expectedProcessLifeTime;
        static String[] expectedAmountOfSubscribers;
        static MonitorProcess result;
        static HashEntry[] hashEntry;
    }
}
