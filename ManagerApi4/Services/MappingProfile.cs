using AutoMapper;
using ManagerApi4.Entities;
using ManagerApi4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerApi4.Services;

namespace ManagerApi4.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<PlayerViewModel, Player>();
            CreateMap<Player, PlayerViewModel>();
            CreateMap<TeamViewModel, Team>();
            CreateMap<Team, TeamViewModel>();
      
        }
    }
 
}
