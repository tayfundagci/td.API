using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using td.Application.Dto;
using td.Application.Interfaces;
using td.Application.Wrappers;
using td.Application.Wrappers.Users;

namespace td.Application.Features.Users.Queries
{
    public class UserLoginQuery : IRequest<ServiceResponse<UserLoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class UserLoginQueryHandler: IRequestHandler<UserLoginQuery, ServiceResponse<UserLoginResponse>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IJwtService _jwtService;
            private readonly IMapper _mapper;
            

            public UserLoginQueryHandler(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
            {
                _userRepository = userRepository;
                _jwtService = jwtService;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<UserLoginResponse>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByMail(request.Email);
                var token = _jwtService.GenerateToken(user);
                var userDto = _mapper.Map<UserDto>(user);
                return new ServiceResponse<UserLoginResponse>(new UserLoginResponse { Token = token, User = userDto }, "Login success", true);

            }
        }
    }
}
