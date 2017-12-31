namespace ZyronatorShared.PublicDataModel
{
    public class NewZyronatorUser
    {
        private readonly string _userName;
        private readonly string _password;

        public NewZyronatorUser(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }
    }
}
