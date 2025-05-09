using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockManager.Aplication.JWTRepository;
using StockManager.Aplication.Responses;
using StockManager.Aplication.UseCases.Auth.Responses;
using StockManager.Aplication.Utils;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Auth.Command;

public class LoginCommand : IRequest<GenericResponse<LoginResponse>>
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, GenericResponse<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtKey _jwtKey;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IOptions<JwtKey> jwtKey)
        {
            _unitOfWork = unitOfWork;
            _jwtKey = jwtKey.Value;
        }

        public async Task<GenericResponse<LoginResponse>> Handle(LoginCommand request,
            CancellationToken cancellationToken)
        {
            var response = new GenericResponse<LoginResponse>();
            try
            {
                var user = await _unitOfWork.GetRepositoryAsync<User>().SingleOrDefaultAsync(
                                                                    x => x.UserName == request.UserName,
                                                                    cancellationToken: cancellationToken);
                if (user == null)
                {
                    response.Errors = ["Usuário não encontrado"];
                    return response;
                }

                if (!StringUtils.VerifyBcryptHash(request.Password, user.Password))
                {
                    
                    response.Errors = ["Senha inválida"];
                    return response;
                }

                response.Data = new LoginResponse()
                {
                    Name = user.Name,
                    Username = user.UserName,
                    Token = TokenService.GenerateToken(user, _jwtKey.Secret ?? "")
                };
                response.Message = "Usuário logado com sucesso";
            }
            catch (Exception e)
            {
                response.Errors = ["Erro ao criar registro", e.Message];
            } 
            return response;
            
        }
    }
}