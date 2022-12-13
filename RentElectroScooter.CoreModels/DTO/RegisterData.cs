namespace RentElectroScooter.CoreModels.DTO
{
    public class RegisterData : ValidableModel
    {
        private string _login;
        private string _passoword;
        private string _name;

        public RegisterData()
        {
            Login = string.Empty;
            Password = string.Empty;
        }

        public string Login
        {
            get => _login;
            set
            {
                _errors[nameof(Login)] = string.IsNullOrEmpty(value)
                    ? "Login cannot be empty."
                    : string.Empty;

                _login = value;
            }
        }

        public string Password
        {
            get => _passoword;
            set
            {
                _errors[nameof(Password)] = string.IsNullOrEmpty(value)
                    ? "Password cannot be empty."
                    : string.Empty;

                _passoword = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _errors[nameof(Name)] = string.IsNullOrEmpty(value)
                    ? "Name cannot be empty."
                    : string.Empty;

                _name = value;
            }
        }
    }
}
