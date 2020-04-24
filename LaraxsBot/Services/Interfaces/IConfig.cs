﻿using Discord;
using LaraxsBot.Services.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Interfaces
{
    public interface IConfig
    {
        ulong VoteChannelId { get; }
        ulong StaffRoleId { get; }
        void SetVoteChannelId(ulong id);
    }
}
