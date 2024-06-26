﻿using Agenda.Domain.Interface;

namespace Agenda.Domain.Models
{
    public class SessionProvider : ISessionProvider
    {
        private UserSession _userSession;

        public SessionProvider()
        {
            _userSession = new UserSession();
        }

        public void Set(int userId, string userName, string profile)
        {
            _userSession.UserId = userId;
            _userSession.UserName = userName;
            _userSession.Profile = profile;
        }

        public UserSession Get()
        {
            return _userSession;
        }
    }
}
