namespace ZyronatorShared.PublicDataModel
{
    public class LogInCredentials
    {
        private readonly string _userName;
        private readonly string _userPassword;


        public LogInCredentials(string userName, string userPassword)
        {
            _userName = userName;
            _userPassword = userPassword;
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        public string UserPassword
        {
            get
            {
                return _userPassword;
            }
        }
    }
}
