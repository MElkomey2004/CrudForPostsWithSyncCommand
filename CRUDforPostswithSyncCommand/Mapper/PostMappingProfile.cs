using AutoMapper;
using CRUDforPostswithSyncCommand.DTOs;
using CRUDforPostswithSyncCommand.Entities;
using CRUDforPostswithSyncCommand.Models;

namespace CRUDforPostswithSyncCommand.Mapper
{
	public class PostMappingProfile : Profile
	{
        public PostMappingProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<UserForRegistrationDto ,User>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
