using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Application.Models;
using TaskManager.Core.Entities;
using Task = TaskManager.Application.Models;

namespace TaskManager.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MapperProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Core.Entities.Task, TaskModel>().ForMember(a=>a.Id,expression => expression.MapFrom(m=>m.Id.ToString())).ReverseMap();
            CreateMap<User, UserModel>().ForMember(a => a.Id, expression => expression.MapFrom(m => m.Id.ToString())).ReverseMap();
        }
    }
}
