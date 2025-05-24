using AutoMapper;
using BusinessModels;
using DomainData.Repositories;
using DomainData.UoW;
using DomainData;
using DomainData.Models;

namespace BusinessLogic.Services
{
    public class ActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ActivityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void CreateActivity(ActivityBusinessModel activityBusinessModel)
        {
            var activity = _mapper.Map<Activity>(activityBusinessModel);
            _unitOfWork.ActivityRepo.Create(activity);
            _unitOfWork.Save();
        }
        public void DeleteActivity(int id)
        {
            var activity = _unitOfWork.ActivityRepo.GetById(id);
            if (activity != null)
            {
                _unitOfWork.ActivityRepo.Delete(id);
                _unitOfWork.Save();
            }
        }
        public List<ActivityBusinessModel> GetAllActivities()
        {
            var activities = _unitOfWork.ActivityRepo.GetAll();
            return _mapper.Map<List<ActivityBusinessModel>>(activities);
        }
    }
}
