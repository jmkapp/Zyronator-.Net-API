using System;
using System.Collections.Generic;
using System.Linq;
using ZyronatorShared.ConfigurationProperties;
using ZyronatorShared.Repositories;

namespace ZyronatorShared.TokenAuthorization
{
    public class TokenAuthorizer : ITokenAuthorizer
    {
        private readonly IConfigurationProperties _configurationProperties;
        private readonly IAuthorizationRepository _authorizationRepository;
        private List<Token> _tokens;

        public TokenAuthorizer(IConfigurationProperties configurationProperties, IAuthorizationRepository authorization)
        {
            _configurationProperties = configurationProperties;
            _authorizationRepository = authorization;
            _tokens = new List<Token>();
        }

        public Token Authorize(string userName, string userPassword)
        {
            List<Token> copyList = new List<Token>(_tokens);

            Token newToken = _authorizationRepository.Authorize(userName, userPassword);

            if(newToken != null)
            {
                copyList.Add(newToken);
            }

            _tokens = copyList;

            return newToken;
        }

        public bool Authorize(Guid token)
        {
            RemoveExpiredTokens();

            List<Token> copyList = new List<Token>(_tokens);

            Token existingToken = copyList.SingleOrDefault(tok => tok.AuthorisationToken == token);

            if(existingToken != null )
            {
                if(existingToken.Date >= _configurationProperties.CurrentDate.Date)
                {
                    return true;
                }
                else
                {
                    copyList.Remove(existingToken);
                }
            }

            Token newToken = _authorizationRepository.Authorize(token);

            if (newToken == null)
                return false;

            copyList.Add(newToken);

            _tokens = copyList;

            return true;
        }

        private void RemoveExpiredTokens()
        {
            List<Token> copyList = new List<Token>();

            DateTime date = _configurationProperties.CurrentDate.Date;

            foreach (Token token in _tokens)
            {
                if(token.Date < date == false)
                {
                    copyList.Add(token);
                }
            }

            _tokens = copyList;
        }
    }
}
