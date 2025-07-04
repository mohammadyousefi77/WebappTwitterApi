using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebappTwitterApi.Contract;
using WebappTwitterApi.Data;
using WebappTwitterApi.Data.DTO;
using WebappTwitterApi.Data.Entity;
using WebappTwitterApi.Data.Models.User;

namespace WebappTwitterApi.Services
{
    public class UserSevices : BaseService,IUserServices
    {
        private readonly UserManager<User> _userManager;
        
        public UserSevices(IUintOfWork uintOfWork, UserManager<User> userManager) : base(uintOfWork)
        {
            _userManager = userManager;
          
        }

        public async Task<UserDTO> CreateAsync(UserModel user)
        {
            var userEntity = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber


            };
          var result=await  _userManager.CreateAsync(userEntity);
            if (result.Succeeded)
            {
                return new UserDTO
                {
                    FirstName = userEntity.FirstName,

                    LastName = userEntity.LastName,
                    UserName = userEntity.UserName,
                    Email = userEntity.Email,
                    PhoneNumber = userEntity.PhoneNumber,
                    Id=userEntity.Id,
                    IsConfirmed=userEntity.EmailConfirmed

                };
            }
            throw new AccessViolationException();
            
        }

        public async Task<bool> Delete(string id)
        {
            var userEntity =await _unitOfWork.GetByIdAsync<User>(id);
            var result= await _userManager.DeleteAsync(userEntity);

            return result.Succeeded;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return await _unitOfWork.Get<User>().Select(p=>new UserDTO
            {

                FirstName = p.FirstName,

                LastName = p.LastName,
                UserName = p.UserName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Id = p.Id,
                IsConfirmed = p.EmailConfirmed

            }).ToListAsync();
        }

        public async Task<UserDTO> GetByIDAsync(string id)
        {
            var result = await _unitOfWork.GetByIdAsync<User>(id);

            return new UserDTO
            {
                FirstName = result.FirstName,

                LastName = result.LastName,
                UserName = result.UserName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                Id = result.Id,
                IsConfirmed = result.EmailConfirmed

            };
            }

        public async  Task<UserDTO> UpdateAsync(string userId,UserModel user)
        {
            var userEntity = await _unitOfWork.GetByIdAsync<User>(userId)?? throw new  EntryPointNotFoundException();
       

            userEntity.UserName = user.UserName;
            userEntity.Email = user.Email;
            userEntity.PhoneNumber = user.PhoneNumber;
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;

            var result=await _userManager.UpdateAsync(userEntity);

            if (result.Succeeded) {
                return new UserDTO
                {
                    FirstName = userEntity.FirstName,

                    LastName = userEntity.LastName,
                    UserName = userEntity.UserName,
                    Email = userEntity.Email,
                    PhoneNumber = userEntity.PhoneNumber,

                    IsConfirmed = userEntity.EmailConfirmed

                };

            
            }


           throw new EntryPointNotFoundException();

        }
    }
}
