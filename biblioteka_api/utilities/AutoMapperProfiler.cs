using AutoMapper;
using biblioteka_api.DTOs;
using biblioteka_api.Models;

namespace biblioteka_api.utilities
{
    public class AutoMapperProfiler:Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<BookCreateDTO, Book>();

            CreateMap<Request, RequestDTO>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.BookTitle, opts => opts.MapFrom(src => src.Book.Title));


            CreateMap<RequestCreateDTO, Request>();

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserCreateDTO, User>();

        }
    }
}
