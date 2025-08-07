using AutoMapper;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IManagerDAO _managerDAO;
        private readonly IMapper _mapper;

        public AuthService(IManagerDAO managerDAO, IMapper mapper)
        {
            _managerDAO = managerDAO;
            _mapper = mapper;
        }

        public Task<string> GenerateJwtToken(AccountData accountData)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountData> VerifyManager(LoginAccountModel loginAccountModel)
        {
           var manager = await _managerDAO.GetManagerAccount(loginAccountModel);

            return _mapper.Map<AccountData>(manager);
        }

    }
}
