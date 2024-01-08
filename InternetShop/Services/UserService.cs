using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;

namespace InternetShop.Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        UserAuthentication authenticationUser;

        public UserService(IUserRepository userRepository, UserAuthentication authenticationUser)
        {
            this.userRepository = userRepository;
            this.authenticationUser = authenticationUser;
        }

        public void Add(User user)
        {
            userRepository.Add(user);
        }

        public bool Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User? GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public bool LogIn(string name, string password)
        {
            bool isValidPassword = false;
            var user = userRepository.GetByName(name);

            // If the user is found and the password is correct, set the authentication user.
            if (user != null)
            {
                isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            }
            if (isValidPassword)
            {
                authenticationUser.User = user;
                return true;
            }
            return false;
        }

        public void LogOut()
        {
            authenticationUser.User = null;
        }

        public User? SignUp(User user)
        {
            // якщо існує користувач з таким іменем - return null
            if (user is null || userRepository.GetByName(user.Name) != null)
            {
                return null;
            }
            // Hashes the user's password for security.
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            userRepository.Add(user);
            return user;
        }

        public bool AddToBalance(User user, double amount)
        {
            if (user is null)
                return false;

            user.Balance += amount;
            userRepository.Save();
            return true;
        }
        public bool WithdrawFromBalance(User user, double amount)
        {
            return AddToBalance(user, -amount);
        }
    }
}
