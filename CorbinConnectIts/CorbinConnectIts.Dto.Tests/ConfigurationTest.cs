// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.CorbinConnectIts.Dto;
using FluentAssertions;
using System.Text.Json;

namespace CorbinConnectIts.Dto.Tests
{
    public class ConfigurationTest
    {
        [Fact]
        public void Deserialize()
        {
            var stream = GetType().Assembly.GetManifestResourceStream("CorbinConnectIts.Dto.Tests.Configuration.json");
            var tobj = JsonSerializer.Deserialize<Configuration>(stream ?? Stream.Null);
            tobj.Should().NotBeNull();
            tobj.Kvps.Count.Should().Be(3);
            tobj.Kvps.Should().ContainKeys("key1", "key2", "key3");
            tobj.RestartApp.Should().BeFalse();
            tobj.SaveToDisk.Should().BeTrue();
            tobj.AdditionalProperties.Count.Should().Be(0);
        }
    }
}
