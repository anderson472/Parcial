﻿using Microsoft.EntityFrameworkCore;
using Parcial.API.Helpers;
using Parcial.API.Services;
using Parcial.Shared.Entities;
using Parcial.Shared.Enums;
using Parcial.Shared.Responses;

namespace Parcial.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CalculateEventControler();
        }


        public async Task CalculateEventControler()
        {
            List<EventControl> Event = new List<EventControl>();


            for (int x = 0; x <= 50000; x++)
            {
                Event.Add(new EventControl { Fecha_De_Uso = null, Fue_Usada = false, Porteria = null });
            }
              _context.eventControl.AddRange(Event);
                await _context.SaveChangesAsync();

        }
    }
}