using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace LaraxsBot.Database.Managers
{
    public class NuitContextManager : INuitContextManager
    {
        private readonly IConfig _config;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public NuitContextManager(IConfig config)
        {
            _config = config;
        }

        public void BackupAndDrop()
        {
            using var db = new NuitContext();

            var path = Path.Combine(AppContext.BaseDirectory, "data", "nuits.sqlite.db");

            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                var fileName = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString() + "_" + fileInfo.Name + ".bak";
                var newPath = Path.Combine(fileInfo.Directory.FullName, fileName);

                File.Copy(path, newPath);

                db.Database.EnsureDeleted();
            }
        }

        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public async Task CreateNuitAsync(DateTime start, DateTime end, ulong creatorId, DateTime playTime = default)
        {
            using var db = new NuitContext();

            if(playTime == default)
            {
                playTime = SetDefaultPlayTime();
            }

            var nuit = new NuitModel()
            {
                StartTime = start,
                StopTime = end,
                CreatorId = creatorId,
                CreationDate = DateTime.Now,
                PlayTime = playTime
            };

            db.Nuits.Add(nuit);
            await db.SaveChangesAsync();
            _logger.Info($"Nuit INSERT by {creatorId}");
        }

        private DateTime SetDefaultPlayTime()
        {
            DateTime playTime;
            var now = DateTime.Now;

            if (now.DayOfWeek == _config.DefaultPlayDay && now.TimeOfDay < _config.DefaultPlayTime)
            {
                playTime = new DateTime(now.Year, now.Month, now.Day, _config.DefaultPlayTime.Hours, _config.DefaultPlayTime.Minutes, _config.DefaultPlayTime.Seconds);
            }
            else
            {
                playTime = GetNextWeekday(now.AddDays(1), _config.DefaultPlayDay);
                playTime = playTime.Add(_config.DefaultPlayTime - playTime.TimeOfDay);
            }

            return playTime;
        }

        public async Task CreateNuitAsync(ulong creatorId, DateTime playTime)
        {
            using var db = new NuitContext();

            if (playTime == default)
            {
                playTime = SetDefaultPlayTime();
            }

            var nuit = new NuitModel()
            {
                CreatorId = creatorId,
                CreationDate = DateTime.Now,
                PlayTime = playTime,
            };

            db.Nuits.Add(nuit);
            await db.SaveChangesAsync();
            _logger.Info($"Nuit INSERT by {creatorId}");
        }

        public Task<bool> DoesNuitExistsAsync(ulong id)
        {
            using var db = new NuitContext();

            return db.Nuits.AsQueryable().AnyAsync(x => x.NuitId == id);
        }

        public void EnsureDeleted()
        {
            using var db = new NuitContext();
            db.Database.EnsureDeleted();
        }
        public async Task<List<NuitModel>> GetAllNuitsAsync()
        {
            using var db = new NuitContext();
            return await db.Nuits.AsQueryable().ToListAsync();
        }

        public async Task<NuitModel?> GetLastEndedAnimeAsync()
        {
            using var db = new NuitContext();
            return await db.Nuits.AsQueryable().Where(x => x.WinnerAnimeId != 0).OrderByDescending(x => x.PlayTime).FirstOrDefaultAsync();
        }

        public async Task<NuitModel?> GetNuitAsync(ulong nuitId)
        {
            using var db = new NuitContext();
            return await db.Nuits.AsQueryable().FirstOrDefaultAsync(x => x.NuitId == nuitId);
        }

        public async Task ReplaceNuitAsync(NuitModel model)
        {
            using var db = new NuitContext();
            var nuit = await GetNuitAsync(model.NuitId);
            if(nuit != null)
            {
                db.Nuits.Remove(nuit);
                _logger.Info($"Nuit DELETE N{nuit.NuitId}");
            }

            await db.Nuits.AddAsync(model);
            await db.SaveChangesAsync();
            _logger.Info($"Nuit INSERT by {model.NuitId}");
        }

        public async Task<NuitModel?> GetStillRunningNuitAsync()
        {
            using var db = new NuitContext();
            return await db.Nuits.AsQueryable().SingleOrDefaultAsync(x => x.IsRunning);
        }

        public async Task StartNuitAsync(ulong id)
        {
            using var db = new NuitContext();
            if (!await db.Nuits.AsQueryable().AnyAsync(x => x.IsRunning))
            {
                var nuit = db.Nuits.AsQueryable().SingleOrDefault(x => x.NuitId == id);
                if (nuit != null)
                {
                    nuit.IsRunning = true;
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task StopNuitAsync(ulong animeId)
        {
            using var db = new NuitContext();
            var nuit = await db.Nuits.AsQueryable().SingleOrDefaultAsync(x => x.IsRunning);
            if (nuit != null)
            {
                nuit.IsRunning = false;
                nuit.WinnerAnimeId = animeId;
                await db.SaveChangesAsync();
            }
        }

        public async Task StopNuitAsync(ulong animeId, DateTime playTime)
        {
            using var db = new NuitContext();
            var nuit = await db.Nuits.AsQueryable().SingleOrDefaultAsync(x => x.IsRunning);
            if (nuit != null)
            {
                nuit.IsRunning = false;
                nuit.WinnerAnimeId = animeId;
                nuit.PlayTime = playTime;
                await db.SaveChangesAsync();
            }
        }

        public async Task<NuitModel?> GetLastCreatedNuitAsync()
        {
            using var db = new NuitContext();
            return await db.Nuits.AsQueryable().OrderByDescending(x=>x.CreationDate).FirstOrDefaultAsync();
        }
    }
}