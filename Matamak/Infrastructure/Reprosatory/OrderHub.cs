
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Reprosatory
{
    public class OrderHub:Hub 
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;

            // الكاشير
            if (user.IsInRole("Cashier"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Cashiers");
            }

            // اليوزر
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "User_" + userId);
            }

            await base.OnConnectedAsync();
        }
    }
}
