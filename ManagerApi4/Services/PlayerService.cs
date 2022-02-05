using AutoMapper;
using ManagerApi4.Context;
using ManagerApi4.Entities;
using ManagerApi4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagerApi4.Services
{
    public class PlayerService: ServiceAbstractBase<Player, PlayerViewModel>
    {
        private readonly ManagerDbContext _db;
        private readonly IMapper _mapper;

        public PlayerService(ManagerDbContext prm, IMapper mapper):base(prm,mapper)
        {
            _db = prm;
            _mapper = mapper;
        }

      
    }
}
