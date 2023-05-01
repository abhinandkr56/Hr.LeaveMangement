using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Mappingprofiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypesQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetAllLeaveTypesQuery>> _appLogger;

    public GetLeaveTypesQueryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _appLogger = new Mock<IAppLogger<GetAllLeaveTypesQuery>>();
    }

    [Fact]
    public async Task GetLeaveTypesListTest()
    {
        var handler = new GetAllLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _appLogger.Object);

        var data = await handler.Handle(new GetAllLeaveTypesQuery(), CancellationToken.None);

        data.ShouldBeOfType<List<LeaveTypeDto>>();
        data.Count.ShouldBe(3);
    }
}