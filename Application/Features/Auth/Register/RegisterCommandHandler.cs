using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Enums;
using MediatR;


namespace Application.Features.Auth.Register
{
    public class RegisterCommandHandler: IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<User> _userRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(
            ICurrentUserService currentUserService,
            IRepository<User> userRepository, 
            IPasswordHasher passwordHasher, 
            IUnitOfWork unitOfWork,
            IQuizmanRepository quizmanRepository,
            IAdminRepository adminRepository)
        {
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _quizmanRepository = quizmanRepository;
            _adminRepository = adminRepository;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if(_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может создавать учетные записи новым сотрудникам");


            string login = GenerateLogin(request.Name);
            while (true) {    
                var existingUser = (await _userRepository
                    .GetAllAsync(cancellationToken))
                    .FirstOrDefault(u => u.Login ==  login);
                login = GenerateLogin(request.Name);
                if (existingUser == null)
                    break;
            }

            if (!Enum.TryParse<UserRole>(request.Role, out var role))
                throw new NotBusinessSuitableException($"Недопустимая роль: {request.Role}");

            string tempPassword = GeneratePassword();
            string passwordHash = _passwordHasher.Hash(tempPassword);

            var user = new User(login, tempPassword, request.Name, role);   

            await _userRepository.AddAsync(user);

            if (request.Role == UserRole.Quizman.ToString())
            {
                var quizman = await _quizmanRepository.GetByUserIdIgnoreFiltersAsync(user.Id, cancellationToken);
                if (quizman != null)
                    throw new ExistInDBException("Квизмен с таким UserId уже существует");
                await _quizmanRepository.AddAsync(new Quizman(user.Id));
            }
            else if (request.Role == UserRole.Admin.ToString())
            {
                var admin = await _adminRepository
                    .GetByUserIdIgnoreFiltersAsync(user.Id, cancellationToken);

                if (admin != null)
                    throw new ExistInDBException("Админ с таким UserId уже существует");
                await _adminRepository.AddAsync(new Admin(user.Id));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterResponse{ 
                UserId = user.Id,
                TempPassword = tempPassword,
                Login = login,
                Role = user.Role.ToString()
            };
        }

        private string GenerateLogin(string name)
        {
            var fullName = name.ToLower().Split(' ');
            string firstName = fullName[0];
            string lastName = fullName[1];
            int randomInt = new Random().Next(1,1000);
            string login = firstName.Substring(0, Math.Min(3, firstName.Length)) + lastName.Substring(0, Math.Min(3, firstName.Length)) + randomInt;

            return login;
        }

        private string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";
            var rnd = new Random();
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
