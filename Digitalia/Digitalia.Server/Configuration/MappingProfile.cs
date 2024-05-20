﻿// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2024 www.ebenmonney.com/mit-license
// ---------------------------------------

using AutoMapper;
using Digitalia.Core.Models.Account;
using Digitalia.Core.Models.Shop;
using Digitalia.Core.Services.Account;
using Digitalia.Server.ViewModels.Account;
using Digitalia.Server.ViewModels.Shop;
using Microsoft.AspNetCore.Identity;

namespace Digitalia.Server.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserVM>()
                   .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserVM, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore())
                .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            CreateMap<ApplicationUser, UserEditVM>()
                .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserEditVM, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore())
                .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            CreateMap<ApplicationUser, UserPatchVM>()
                .ReverseMap();

            CreateMap<ApplicationRole, RoleVM>()
                .ForMember(d => d.Permissions, map => map.MapFrom(s => s.Claims))
                .ForMember(d => d.UsersCount, map => map.MapFrom(s => s.Users != null ? s.Users.Count : 0))
                .ReverseMap();
            CreateMap<RoleVM, ApplicationRole>()
                .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            CreateMap<IdentityRoleClaim<string>, ClaimVM>()
                .ForMember(d => d.Type, map => map.MapFrom(s => s.ClaimType))
                .ForMember(d => d.Value, map => map.MapFrom(s => s.ClaimValue))
                .ReverseMap();

            CreateMap<ApplicationPermission, PermissionVM>()
                .ReverseMap();

            CreateMap<IdentityRoleClaim<string>, PermissionVM>()
                .ConvertUsing(s => ((PermissionVM)ApplicationPermissions.GetPermissionByValue(s.ClaimValue))!);

            CreateMap<Customer, CustomerVM>()
                .ReverseMap();

            CreateMap<Product, ProductVM>()
                .ReverseMap();

            CreateMap<Order, OrderVM>()
                .ReverseMap();
        }
    }
}
