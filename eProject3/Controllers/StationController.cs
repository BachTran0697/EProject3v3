﻿using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private IStationRepo repo;

        public StationController(IStationRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetStations());
        }
        [HttpPost]

        public async Task<ActionResult> Create(Station station)
        {
            try
            {
                var result = await repo.CreateStation(station);
                if (result == null)
                {
                    return BadRequest("Cannot create");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await repo.DeleteStation(id);
                if (result == null)
                {
                    return BadRequest("Cannot delete");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update")]

        public async Task<ActionResult> Update(Station station)
        {
            try
            {
                var result = await repo.UpdateStation(station);
                if (result == null)
                {
                    return BadRequest("Cannot update");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
