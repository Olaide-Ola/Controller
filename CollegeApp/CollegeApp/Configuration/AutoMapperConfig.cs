using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Model;

namespace CollegeApp.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Configuration for different properties
            //CreateMap<StudentDto, Data.Student>().ReverseMap().ForMember(n => n.Name, opt => opt.MapFrom(x => x.StudentName));
            //CreateMap<Data.Student, StudentDto>().ForMember(n => n.StudentName, opt => opt.MapFrom(n => n.StudentName)).ReverseMap();

            //Config for ignoring a property
            //CreateMap<Data.Student, StudentDto>().ForMember(n => n.StudentName, opt => opt.Ignore()).ReverseMap();

            //Config for transforming it is wrong
            //CreateMap<Data.Student, StudentDto>().AddTransform<string>(n => string.IsNullOrEmpty(n)? "No Address Found" : n).ReverseMap();

            CreateMap<Data.Student, StudentDto>().ReverseMap().ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "No address found" : n.Address));

            //CreateMap<Data.Student, StudentDto>().ReverseMap(); 

            CreateMap<RoleDto, Role>().ForMember(n => n.Description, opt => opt.MapFrom(n => n.RoleDescription)).ReverseMap();
            CreateMap<RolePrivilegeDto, RolePrivilege>().ReverseMap();
            CreateMap<UserDto,  User>().ReverseMap();
            CreateMap<UserReadonlyDto,  User>().ReverseMap();
        }
    }
}
