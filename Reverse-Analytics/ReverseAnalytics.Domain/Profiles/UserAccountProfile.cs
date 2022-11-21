using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReverseAnalytics.Domain.DTOs.UserAccount;

namespace ReverseAnalytics.Domain.Profiles
{
    internal class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<IdentityUser, UserAccountDto>();
            CreateMap<UserAccountDto, IdentityUser>();
            CreateMap<UserAccountForCreateDto, IdentityUser>();
            CreateMap<UserAccountForUpdateDto, IdentityUser>();
        }
    }
}
