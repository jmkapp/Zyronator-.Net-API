namespace ZyronatorShared.PublicDataModel
{
    public class ZyronatorUser
    {
        private readonly int _userId;
        private readonly string _userName;

        public ZyronatorUser(int userId, string userName)
        {
            _userId = userId;
            _userName = userName;
        }

        public int UserId
        {
            get
            {
                return _userId;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
        }
    }
}
