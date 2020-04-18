﻿using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface INuitManagerService
    {
        /// <summary>
        /// Method called on bot startup
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
        /// <summary>
        /// Creates a new nuit, throws if a nuit is already running.
        /// </summary>
        /// <returns></returns>
        Task<INuit> CreateNuitAsync();
        Task<INuit> GetRunningNuitAsync();
        Task<INuit> GetNuitAsync(ulong nuitId);
    }
}