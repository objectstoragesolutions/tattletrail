using AutoFixture;
using Machine.Specifications;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.InfrastructureTests.ExtensionsTests.HashEntryExtensionsTests {

    [Subject(typeof(HashEntryExtensions))]
    public class when_converting_hash_entry_array_into_monitor_process {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>().ToString();
            expectedProcessName = fixture.Create<String>();
            expectedProcessLifeTime = fixture.Create<Int32>();
            expectedAmountOfSubscribers = fixture.Create<String[]>();
            expectedDateOfCreation = fixture.Create<DateTime>();
            expectedLastCheckIn = fixture.Create<DateTime>();
            expectedIsDown = fixture.Create<Boolean>();
            var processName = new HashEntry(nameof(MonitorDetails.ProcessName), expectedProcessName);
            var lifeTime = new HashEntry(nameof(MonitorDetails.IntervalTime), expectedProcessLifeTime);
            var subscribers = new HashEntry(nameof(MonitorDetails.Subscribers), JsonConvert.SerializeObject(expectedAmountOfSubscribers));
            var dateOfCreation = new HashEntry(nameof(MonitorDetails.DateOfCreation), JsonConvert.SerializeObject(expectedDateOfCreation));
            var lastCheckIn = new HashEntry(nameof(MonitorDetails.LastCheckIn), JsonConvert.SerializeObject(expectedLastCheckIn));
            var isDown = new HashEntry(nameof(MonitorDetails.IsDown), (Boolean)expectedIsDown);
            hashEntry = new HashEntry[] { processName, lifeTime, subscribers, dateOfCreation, lastCheckIn, isDown };

        };

        Because of = () =>
            result = hashEntry.AsMonitorProcess(id);

        It should_convert_to_monitor_process_with_expected_id = () =>
            result.Id.ShouldEqual(Guid.Parse(id));

        It should_convert_to_monitor_process_with_expected_processname = () =>
            result.MonitorDetails.ProcessName.ShouldEqual(expectedProcessName);

        It should_convert_to_monitor_process_with_expected_lifetime = () =>
            result.MonitorDetails.IntervalTime.ShouldEqual(expectedProcessLifeTime);

        It should_convert_to_monitor_process_with_expected_subscribers = () =>
            result.MonitorDetails.Subscribers.ShouldEqual(expectedAmountOfSubscribers);

        It should_convert_to_monitor_process_with_expected_date_of_creation = () =>
            result.MonitorDetails.DateOfCreation.ShouldEqual(expectedDateOfCreation);

        It should_convert_to_monitor_process_with_expected_last_checkin_date = () =>
            result.MonitorDetails.LastCheckIn.ShouldEqual(expectedLastCheckIn);

        It should_convert_to_monitor_process_with_expected_is_down = () =>
            result.MonitorDetails.IsDown.ShouldEqual(expectedIsDown);

        static RedisKey id;
        static String expectedProcessName;
        static Int32 expectedProcessLifeTime;
        static String[] expectedAmountOfSubscribers;
        static DateTime expectedDateOfCreation;
        static DateTime expectedLastCheckIn;
        static Boolean expectedIsDown;
        static MonitorProcess result;
        static HashEntry[] hashEntry;
    }
}
