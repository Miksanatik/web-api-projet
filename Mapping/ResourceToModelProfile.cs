using AutoMapper;
using API.Domain.Models;
using API.Resources;

namespace API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAuthorResource, Author>();
            CreateMap<SaveBookResource, Book>();
        }
    }
}
