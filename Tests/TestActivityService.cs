using DomainData.Repositories;
using DomainData.UoW;
using DomainData.Models;
using BusinessLogic.Services;
using Moq;
using AutoMapper;
using BusinessLogic.BusinessModels;
using Xunit;
using BusinessLogic;

namespace Tests
{
    public class TestActivityService
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<Activity>> _mockActivityRepo;
        private readonly ActivityService _activityService;

        public TestActivityService()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mockActivityRepo = new Mock<IGenericRepository<Activity>>();
            _unitOfWorkMock.Setup(x => x.ActivityRepo).Returns(_mockActivityRepo.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
            });
            var mapper = config.CreateMapper();

            _activityService = new ActivityService(_unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public void CreateActivityTest()
        {
            var activity = new ActivityBusinessModel
            {
                Id = 1,
                Name = "Test Activity"
            };
            _activityService.CreateActivity(activity);

            _mockActivityRepo.Verify(x => x.Create(It.Is<Activity>( x => 
            x.Name == activity.Name &&
            x.Id == activity.Id)));
            _unitOfWorkMock.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void DeleteActivityTest()
        {
            var activity = new Activity
            {
                Id = 1,
                Name = "Test Activity"
            };
            _mockActivityRepo.Setup(x => x.GetById(activity.Id)).Returns(new Activity { Id = activity.Id });
            _activityService.DeleteActivity(activity.Id);
            _mockActivityRepo.Verify(x => x.Delete(activity.Id), Times.Once);
            _unitOfWorkMock.Verify(x => x.Save(), Times.Once);
        }
    }
}
