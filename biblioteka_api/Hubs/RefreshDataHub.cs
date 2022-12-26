using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace biblioteka_api.Hubs
{
    public class RefreshDataHub: Hub
    {
        public string GetConnectionId(string mail)
        {
            bool e = MyUsers.TryAdd(mail, Context.ConnectionId);
            if (!e)
            {
                string ctx = MyUsers.FirstOrDefault(x => x.Key == mail).Value;
                MyUsers.TryUpdate(mail, Context.ConnectionId, ctx);
            }
            return Context.ConnectionId;

        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);               
        }

        
        public static ConcurrentDictionary<string, string> MyUsers = new ConcurrentDictionary<string, string>();
    }
}
