using AutoMapper;

namespace BlazingChat.Client.DTOs.AutoMapper
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
