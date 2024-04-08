using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            Boolean isName = isNameCorrect(firstName, lastName, email);
            Boolean isAge = isAgeCorrect(dateOfBirth);
            
            if (!isName || !isAge || (user.HasCreditLimit && user.CreditLimit < 500))
            {
                return false;
            }
            
            setCreditLimit(client, user);

            UserDataAccess.AddUser(user);
            return true;
        }

        public Boolean isNameCorrect(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || (!email.Contains("@") && !email.Contains(".")))
            {
                return false;
            }
            return true;
        }

        public Boolean isAgeCorrect(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        public void setCreditLimit(Client client, User user)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
                Console.WriteLine("Card doesn't have credit limit for customer: " + user.Client + " " + user.LastName);
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
                Console.WriteLine("Credit limit has been set to " + user.CreditLimit + " for customer: " + user.Client + " " + user.LastName);
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
                Console.WriteLine("Credit limit has been set to " + user.CreditLimit + " for customer: " + user.Client + " " + user.LastName);
            }
        }
    }
}
