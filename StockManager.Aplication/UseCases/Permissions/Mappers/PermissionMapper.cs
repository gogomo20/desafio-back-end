using AutoMapper;
using StockManager.Aplication.UseCases.Permissions.Commands.Create;
using StockManager.Aplication.UseCases.Permissions.Commands.Update;
using StockManager.Aplication.UseCases.Permissions.Responses;
using StockManager.Domain.Entities;

namespace StockManager.Aplication.UseCases.Permissions.Mappers;

public class PermissionMapper : Profile
{
    public PermissionMapper()
    {
        CreateMap<CreatePermissionCommand, Permission>();
        CreateMap<UpdatePermissionCommand, Permission>();
        CreateMap<Permission, PermissionResponse>();
    }
}