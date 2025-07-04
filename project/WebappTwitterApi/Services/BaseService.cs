using WebappTwitterApi.Data;

namespace WebappTwitterApi.Services
{
    public class BaseService
    {
        public readonly IUintOfWork _unitOfWork;
        public BaseService(IUintOfWork uintOfWork)
        {
            _unitOfWork= uintOfWork;
        }
    }
}
