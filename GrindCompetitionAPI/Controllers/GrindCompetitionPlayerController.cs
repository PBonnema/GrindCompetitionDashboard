using AutoMapper;
using GrindCompetitionAPI.DTO;
using GrindCompetitionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrindCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrindCompetitionPlayerController : ControllerBase
    {
        private readonly IGrindCompetitionService _grindCompetitionService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public GrindCompetitionPlayerController(ILogger logger, IGrindCompetitionService grindCompetitionService, IMapper mapper)
        {
            _logger = logger;
            _grindCompetitionService = grindCompetitionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrindCompetitionPlayerDTO>>> Get()
        {
            var grindCompetitionPlayers = await _grindCompetitionService.GetGrindCompetitionPlayers();
            return Ok(_mapper.ProjectTo<GrindCompetitionPlayerDTO>(grindCompetitionPlayers.AsQueryable()));
        }
    }
}
