﻿using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Managers
{
    public class NuitContextManager : INuitContext
    {
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

        public void EnsureDeleted()
        {
            using var db = new NuitContext();
            db.Database.EnsureDeleted();
        }

        public async Task CreateNuitAsync(DateTime start, DateTime end, ulong creatorId)
        {
            using var db = new NuitContext();

            var nuit = new NuitModel()
            {
                StartTime = start,
                StopTime = end,
                CreatorId = creatorId,
                CreationDate = DateTime.Now,
            };

            db.Nuits.Add(nuit);
            await db.SaveChangesAsync();
        }

        public async Task<List<NuitModel>> GetAllNuitsAsync()
        {
            using var db = new NuitContext();
            return await db.Nuits.AsQueryable().ToListAsync();
        }

        public async Task<NuitModel?> GetStillRunningNuitAsync()
        {
            using var db = new NuitContext();
            return await db.Nuits.AsQueryable().SingleOrDefaultAsync(x => x.IsRunning);
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
    }
}