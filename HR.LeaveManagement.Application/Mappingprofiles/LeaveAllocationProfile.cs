using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Mappingprofiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
        
        CreateMap<LeaveAllocationDetailsDto, LeaveAllocation>().ReverseMap();

        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>().ReverseMap();
        
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>().ReverseMap();
    }
}