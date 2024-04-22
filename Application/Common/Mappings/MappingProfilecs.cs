using AutoMapper;
using SkeletonApi.Application.Features.Accounts;
using SkeletonApi.Application.Features.Accounts.Profiles.Commands.CreateAccount;
using SkeletonApi.Application.Features.ManagementUser.Permissions;
using SkeletonApi.Application.Features.ManagementUser.Permissions.Commands.CreatePermissions;
using SkeletonApi.Application.Features.ManagementUser.Permissions.Commands.UpdatePermissions;
using SkeletonApi.Application.Features.ManagementUser.Roles;
using SkeletonApi.Application.Features.ManagementUser.Roles.Commands.CreateRoles;
using SkeletonApi.Application.Features.ManagementUser.Users.Commands.CreateUser;
using SkeletonApi.Application.Features.Settings.Break;
using SkeletonApi.Application.Features.Settings.Break.Commands.CreateBreak;
using SkeletonApi.Application.Features.Settings.Operator;
using SkeletonApi.Application.Features.Settings.Operator.Commands.CreateOperator;
using SkeletonApi.Application.Features.Settings.Task;
using SkeletonApi.Application.Features.Settings.Task.Commands.CreateTask;
using SkeletonApi.Application.Features.Settings.Type;
using SkeletonApi.Application.Features.Settings.Type.Commands.CreateType;
using SkeletonApi.Application.Features.Users;
using SkeletonApi.Domain.Entities;
using System.Reflection;

namespace SkeletonApi.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponseDto>();

            CreateMap<CreateRolesRequest, Role>();
            CreateMap<Role, CreateRolesResponseDto>();

            CreateMap<CreatePermissionsRequest, Permission>();
            CreateMap<Permission, CreatePermissionsResponseDto>();

            CreateMap<UpdatePermissionsRequest, Permission>();

            CreateMap<CreateAccountRequest, Account>();
            CreateMap<Account, CreateAccountResponseDto>();

            CreateMap<CreateTypeRequest, Types>();
            CreateMap<Types, CreateTypeResponseDto>();

            CreateMap<CreateOperatorRequest, Operators>();
            CreateMap<Operators, CreateOperatorResponseDto>();

            CreateMap<CreateTaskRequest, SettingTask>();
            CreateMap<SettingTask, CreateTaskResponseDto>();

            CreateMap<CreateBreakRequest, SettingBreak>();
            CreateMap<SettingBreak, CreateBreakeResponseDto>();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}