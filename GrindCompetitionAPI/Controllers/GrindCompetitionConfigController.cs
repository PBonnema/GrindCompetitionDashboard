using AutoMapper;
using GrindCompetitionAPI.DTO;
using GrindCompetitionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrindCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrindCompetitionConfigController : ControllerBase
    {
        private readonly IGrindCompetitionConfigService _grindCompetitionConfigService;
        private readonly IMapper _mapper;

        public GrindCompetitionConfigController(IGrindCompetitionConfigService grindCompetitionConfigService, IMapper mapper)
        {
            _grindCompetitionConfigService = grindCompetitionConfigService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GrindCompetitionConfigDTO>> Get()
        {
            var config = await _grindCompetitionConfigService.GetConfig();
            return Ok(_mapper.Map<GrindCompetitionConfigDTO>(config));
        }
    }
}
