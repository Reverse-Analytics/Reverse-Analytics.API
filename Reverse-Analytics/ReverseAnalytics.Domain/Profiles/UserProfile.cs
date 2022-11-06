using AutoMapper;
using ReverseAnalytics.Domain.DTOs.User;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserForCreateDto, User>();
            CreateMap<UserForUpdateDto, User>();
        }
    }
}
