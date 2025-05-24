using AutoMapper;
using BusinessModels;
using DomainData.Repositories;
using DomainData.UoW;
using DomainData;
using DomainData.Models;

namespace BusinessLogic.Services
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void CreateRoom(RoomBusinessModel roomBusinessModel)
        {
            var room = _mapper.Map<Room>(roomBusinessModel);
            _unitOfWork.RoomRepo.Create(room);
            _unitOfWork.Save();
        }
        public void DeleteRoom(int id)
        {
            var room = _unitOfWork.RoomRepo.GetById(id);
            if (room != null)
            {
                _unitOfWork.RoomRepo.Delete(id);
                _unitOfWork.Save();
            }
        }
        public List<RoomBusinessModel> GetAllRooms()
        {
            var rooms = _unitOfWork.RoomRepo.GetAll();
            return _mapper.Map<List<RoomBusinessModel>>(rooms);
        }
        public RoomBusinessModel GetRoomById(int id)
        {
            var room = _unitOfWork.RoomRepo.GetById(id);
            return _mapper.Map<RoomBusinessModel>(room);
        }
    }
}
