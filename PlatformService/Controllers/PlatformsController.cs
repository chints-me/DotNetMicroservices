using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.EndPoints;
using PlatformService.Models;

namespace PlatformService.Controllers{
    
    [ApiController]
    public class  PlatformsController: ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private IMapper _mapper;

        public PlatformsController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(PlatformsUrls.GetAll)]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet]
        [Route(PlatformsUrls.Get, Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);
            if(platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route(PlatformsUrls.Create)]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto plateformDto)
        {
            var platformModel = _mapper.Map<Platform>(plateformDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            
            return CreatedAtRoute("GetPlatformById",new {id = platformReadDto.Id}, platformReadDto);
        }
    }
}