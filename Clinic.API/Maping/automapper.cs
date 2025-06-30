using AutoMapper;
using Clinic.Core.Entities;
using Clinic.Core.DTOs;

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
                .ForMember(p => p.NurseName  ,
                op => op.MapFrom(p => p.Nurse.Staff.FullName));

        }
    }
}
