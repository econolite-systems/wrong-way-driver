// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.CorbinConnectIts.Dto;
using FluentAssertions;
using System.Text.Json;

namespace CorbinConnectIts.Dto.Tests
{
    public class CameraManualTriggerEventTest
    {
        [Fact]
        public void Deserialize()
        {
            var stream = GetType().Assembly.GetManifestResourceStream("CorbinConnectIts.Dto.Tests.CameraManualTriggerEvent.json");
            var tobj = JsonSerializer.Deserialize<CameraManualTriggerEvent>(stream ?? Stream.Null);
            tobj.Should().NotBeNull();
            tobj.Params.Count.Should().Be(3);
            tobj.Params.Should().Contain(new KeyValuePair<string, string>("vehicle", "wrongway"));
            tobj.Params.Should().Contain(new KeyValuePair<string, string>("lat", "123"));
            tobj.Params.Should().Contain(new KeyValuePair<string, string>("lon", "456"));
            tobj.AdditionalProperties.Count.Should().Be(0);
        }
    }
}
