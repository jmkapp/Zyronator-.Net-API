using System;
using ZyronatorShared.PublicDataModel;

namespace ZyronatorShared.TokenAuthorization
{
    public class Token
    {
        private readonly Guid _token;
        private readonly DateTime _authorizationDate;

        public Token(Guid token, DateTime authorizationDate)
        {
            _token = token;
            _authorizationDate = authorizationDate;
        }

        public Guid AuthorisationToken
        {
            get
            {
                return _token;
            }
        }

        public DateTime Date
        {
            get
            {
                return _authorizationDate;
            }
        }
    }
}
