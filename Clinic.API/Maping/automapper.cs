using AutoMapper;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Entities.demo.Models;

namespace Clinic.API.Maping
{
    public class automapper : Profile
    {

        public automapper()
        {

            CreateMap<Patient, AddPatientDTO>()
                .ForMember(p => p.NurseId, op => op.MapFrom(src => src.NurseId)).ReverseMap();

            CreateMap<AddPatientDTO, Patient>()
    .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Patient, PatientResponse>()
                .ForMember(p => p.NurseName,
                op => op.MapFrom(p => p.Nurse.Staff.User.FullName));


            //CreateMap<Staff, NurseRequest>()
            //         .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            //         .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            //         .ForMember(dest => dest.No_Of_Hour, opt => opt.MapFrom(src => src.No_Of_Hour))
            //         .ForMember(dest => dest.Shift, opt => opt.MapFrom(src => src.Shift))
            //         .ForMember(dest => dest.Qualifications, opt => opt.MapFrom(src => src.Qualifications))
            //         .ForMember(dest => dest.dept_id, opt => opt.MapFrom(src => src.dept_id))
            //         .ReverseMap();

            CreateMap<Staff, NurseRequest>().ReverseMap();

            CreateMap<RegisterDTO, AppUser>().ReverseMap();
        }
    }
}
