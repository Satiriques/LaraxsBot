using MalParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MalParser.Services.Interfaces
{
    internal interface IAnimeParser
    {
        Task<IAnime?> GetAnimeAsync(ulong id);
    }
}
