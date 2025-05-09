using AutoMapper;
using StockManager.Aplication.UseCases.PermissionGroups.Commands.Create;
using StockManager.Aplication.UseCases.PermissionGroups.Commands.Update;
using StockManager.Aplication.UseCases.PermissionGroups.Responses;
using StockManager.Aplication.UseCases.Permissions.Commands.Update;
using StockManager.Domain.Entities;

namespace StockManager.Aplication.UseCases.PermissionGroups.Mappers
{
    public class PermissionGroupsMapper : Profile
    {
        public PermissionGroupsMapper() {
            CreateMap<PermissionGroup, PermissionGroupResponse>();
            CreateMap<CreatePermissionGroupCommand, PermissionGroup>();
            CreateMap<UpdatePermissionGroupCommand, PermissionGroup>();
        }
    }
}
