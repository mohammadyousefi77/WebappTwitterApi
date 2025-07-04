using WebappTwitterApi.Data.DTO;
using WebappTwitterApi.Data.Entity;
using WebappTwitterApi.Data.Models.User;

namespace WebappTwitterApi.Contract
{
    public interface IUserServices
    {
        public Task<UserDTO> GetByIDAsync(string id);

        public Task<List<UserDTO>> GetAllAsync();

        public Task<UserDTO> CreateAsync(UserModel user);

        public Task<UserDTO> UpdateAsync(string userId,UserModel user);

        public Task<bool>  Delete(string id);
    }
}
