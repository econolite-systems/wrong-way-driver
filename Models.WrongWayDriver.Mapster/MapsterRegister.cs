// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System.Reflection;
using Econolite.Ode.Models.WrongWayDriver.Db;
using Mapster;

namespace Models.WronmgWayDriver
{
    public class MapsterRegister : ICodeGenerationRegister
    {
        public void Register(CodeGenerationConfig config)
        {
            config.AdaptTo("[name]Dto")
                .ApplyDefaultRule();

            config.AdaptFrom("[name]Add", MapType.Map)
                .ApplyDefaultRule()
                .ForType<WrongWayDriverConfig>(cfg => { cfg.Ignore(WrongWayDriver => WrongWayDriver.Id); });

            config.AdaptFrom("[name]Update", MapType.MapToTarget)
                .ApplyDefaultRule();

            config.GenerateMapper("[name]Mapper")
                .ForType<WrongWayDriverConfig>();
        }
    }

    internal static class RegisterExtensions
    {
        public static AdaptAttributeBuilder ApplyDefaultRule(this AdaptAttributeBuilder builder)
        {
            return builder
                .ForAllTypesInNamespace(Assembly.GetExecutingAssembly(), "Econolite.Ode.Models.WrongWayDriver.Db")
                .ExcludeTypes(type => type.IsEnum)
                .AlterType(type => type.IsEnum || Nullable.GetUnderlyingType(type)?.IsEnum == true, typeof(string))
                .ShallowCopyForSameType(true);
        }
    }
}
