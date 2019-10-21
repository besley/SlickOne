using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;

namespace SlickOne.Web.Hubs
{
    public class NotificationHub : Hub
    {
        private static Dictionary<string, dynamic> connectedClients =
            new Dictionary<string, dynamic>();

        public void RegisterClient(string userName)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(userName))
                {
                    connectedClients[userName] = Clients.Caller;
                }
                else
                {
                    connectedClients.Add(userName, Clients.Caller);
                }
            }
            //Clients.Caller.addMessage("'" + userName + "' registered.");
        }

        public void AddNotification(string msg, string toUser)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(toUser))
                {
                    dynamic client = connectedClients[toUser];
                    client.addMessage(msg);
                }
            }
        }
    }
}