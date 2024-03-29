﻿using MeetingManager.Domain.Entities;
using MeetingManager.Domain.Interfaces;
using MeetingManager.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MeetingManager.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRoomsController : ControllerBase
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMeetingRoomsRepository _meetingRoomsRepository;

        public MeetingRoomsController(IUnitOfWorkFactory unitOfWorkFactory,
            IMeetingRoomsRepository meetingRoomsRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _meetingRoomsRepository = meetingRoomsRepository;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                using (_unitOfWorkFactory.StartUnitOfWork())
                {
                    var allMeetingRooms = await _meetingRoomsRepository.GetAsync();
                    return JsonConvert.SerializeObject(allMeetingRooms);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            try
            {
                using (_unitOfWorkFactory.StartUnitOfWork())
                {
                    var meetingRooms = await _meetingRoomsRepository.GetByIdAsync(id);
                    return JsonConvert.SerializeObject(meetingRooms);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public void Post([FromBody] MeetingRooms meetingRooms)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.StartUnitOfWorkWithTransaction())
                {
                    _meetingRoomsRepository.Add(meetingRooms);

                    uow.Save();
                    uow.Commit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut]
        public void Put([FromBody] MeetingRooms meetingRooms)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.StartUnitOfWorkWithTransaction())
                {
                    _meetingRoomsRepository.Update(meetingRooms);

                    uow.Save();
                    uow.Commit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}