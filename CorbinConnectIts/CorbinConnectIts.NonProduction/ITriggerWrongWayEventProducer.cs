// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace CorbinConnectIts.NonProduction
{
    public interface ITriggerWrongWayEventProducer
    {
        Task Trigger(string deviceId, double latitude, double longitude);
    }
}
