using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.ExtensionsTests {
    [Subject(typeof(UserModelExtensions))]
    public class when_converting_user_model_to_hash_entry {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            var userName = fixture.Create<String>();
            user = Mock.Of<User>(
                x => x.Id == Guid.NewGuid() &&
                x.UserName == userName &&
                x.MonitorProcessId == new HashSet<Guid>());
        };

        Because of = () =>
            result = user.ConvertUserToHashEntry();

        It should_contain_userid_field_name = () =>
            result[0].Name.Equals(nameof(user.Id));

        It should_contain_userid_field_value = () =>
            result[0].Value.Equals(user.Id.ToByteArray());

        It should_contain_username_field_name = () =>
            result[1].Name.Equals(nameof(user.UserName));

        It should_contain_username_field_value = () =>
            result[1].Value.Equals(user.UserName);

        It should_contain_processid_field_name = () =>
             result[2].Name.Equals(nameof(user.MonitorProcessId));

        It should_contain_processid_field_value = () =>
             result[2].Value.Equals(user.MonitorProcessId);

        static User user;
        static HashEntry[] result;
    }
}
