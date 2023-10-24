using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Interfaces;
using td.Application.Messages;
using td.Domain.Entities;

namespace td.Application.Features.Users.Commands
{
    public class UserRegisterCommand : IRequest<BaseResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class UserRegisterCommandHandler: IRequestHandler<UserRegisterCommand, BaseResponse>
        {
            private IUserRepository _userRepository;

            public UserRegisterCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;

            }

            public async Task<BaseResponse> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = Shared.Enums.enmRole.User,
                    CreateDate = DateTime.Now
                };

                var result = await _userRepository.AddAsync(user);
                user.Id = result;
                return new BaseResponse() { Message = "Registeration completed", Success = true };
            }
        }
    }
}
