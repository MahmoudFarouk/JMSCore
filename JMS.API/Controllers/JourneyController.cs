using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JMS.API.Models;
using JMS.BLL.Common;
using JMS.BLL.Interfaces;
using JMS.DAL.Common.Enums;
using JMS.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JMS.API.Controllers
{
    [Route("api/journey")]
    [ApiController]
    [Authorize]
    public class JourneyController : ControllerBase
    {
        private IJourneyService _journeyService;
        private IUserService _userService;
        private INotificationService _notificationService;

        public JourneyController(IJourneyService journeyService, IUserService userService, INotificationService notificationService)
        {
            _journeyService = journeyService;
            _userService = userService;
            _notificationService = notificationService;
        }
        [HttpPost]
        [Route("initiate")]
        public IActionResult Initiate(JourneyModel model)
        {

            try
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<JourneyModel, Journey>();
                });
                IMapper iMapper = config.CreateMapper();

                var journey = iMapper.Map<JourneyModel, Journey>(model);

                journey.UserId = Guid.Parse(User.Identity.Name);
                var result = _journeyService.InitiateJourney(journey);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [HttpPost]
        [Route("validate")]
        public IActionResult Validate(JourneyModel model)
        {

            try
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<JourneyModel, Journey>();
                });
                IMapper iMapper = config.CreateMapper();

                var journey = iMapper.Map<JourneyModel, Journey>(model);

                journey.UserId = Guid.Parse(User.Identity.Name);
                var result = _journeyService.ValidateJourney(journey);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }


        [HttpPost]
        [Route("update")]
        public IActionResult Update(JourneyModel model)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<JourneyModel, Journey>();
                });
                IMapper iMapper = config.CreateMapper();
                var journey = iMapper.Map<JourneyModel, Journey>(model);
                var result = _journeyService.UpdateJourney(journey);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }
        [HttpGet("{id}")]
        public IActionResult detail(int id)
        {
            try
            {

                var result = _journeyService.GetJourneyDetails(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }
        [HttpPost]
        [Route("assignJourneyDriverVehicle")]
        public IActionResult AssignJourneyDriverVehicle(int journeyId, Guid driverId, string vehcileNo)
        {
            try
            {
                var result = _journeyService.AssignJourneyDriverVehicle(journeyId, driverId, vehcileNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [Route("updateJourneyCheckpoint")]
        public IActionResult UpdateJourneyCheckpoint(int journeyUpdateId, JourneyStatus status)
        {

            try
            {
                var result = _journeyService.UpdateJourneyCheckpoint(journeyUpdateId, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }
        [HttpPost]
        [Route("addJourneyUpdate")]
        public IActionResult AddJourneyUpdate(JourneyUpdateModel model)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<JourneyUpdateModel, JourneyUpdate>();
                });
                IMapper iMapper = config.CreateMapper();
                var journeyUpdate = iMapper.Map<JourneyUpdateModel, JourneyUpdate>(model);
                journeyUpdate.UserId = Guid.Parse(User.Identity.Name);
                var result = _journeyService.AssaignDreiverJourneyUpdate(journeyUpdate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }


        [Route("journeyInfo")]
        [HttpGet]
        public IActionResult JourneyInfo(int id)
        {
            try
            {
                var model = _journeyService.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Journey, JourneyModel>();
                });
                IMapper iMapper = config.CreateMapper();
                var journey = iMapper.Map<Journey, JourneyModel>(model);

                var result = new ServiceResponse<JourneyModel> { Data = journey, Status = ResponseStatus.Success };
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [HttpPost]
        [Route("AssignDriverToJourney")]
        public IActionResult AssignDriverToJourney(JourneyUpdateModel model)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<JourneyUpdateModel, JourneyUpdate>();
                });
                IMapper iMapper = config.CreateMapper();
                var journeyUpdate = iMapper.Map<JourneyUpdateModel, JourneyUpdate>(model);
                journeyUpdate.UserId = Guid.Parse(User.Identity.Name);
                var result = _journeyService.AssaignDreiverJourneyUpdate(journeyUpdate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [Route("JourneySelectDriver")]
        [HttpGet]
        public IActionResult JourneySelectDriver(int journeyId)
        {
            try
            {
                var driverInfo = _journeyService.GetJourneyUpdateDriverInfo(journeyId);
                if (driverInfo != null)
                {
                    return Ok(new { Data = new { driverInfo.DriverId, driverInfo.Id, driverInfo.JourneyId, driverInfo.JourneyStatus, DriverName = driverInfo.DriverId.HasValue ? _userService.GetById(driverInfo.DriverId.Value).FullName : "" }, Status = ResponseStatus.Success });
                }
                object drive = null;
                var driverInfofoModel = new { DriverId = drive, Id = 0, DriverName = "" };
                return Ok(new { Data = driverInfofoModel, Status = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }
        }

        [Route("JourneyCheckPoints")]
        [HttpGet]
        public IActionResult JourneyCheckPoints(int journeyId)
        {
            try
            {
                var driverInfo = _journeyService.GetJourneyUpdateDriverInfo(journeyId);
                if (driverInfo != null)
                {
                    return Ok(new { Data = new { driverInfo.DriverId, driverInfo.Id, driverInfo.JourneyId, driverInfo.JourneyStatus, DriverName = driverInfo.DriverId.HasValue ? _userService.GetById(driverInfo.DriverId.Value).FullName : "" }, Status = ResponseStatus.Success });
                }
                object drive = null;
                var driverInfofoModel = new { DriverId = drive, Id = 0, DriverName = "" };
                return Ok(new { Data = driverInfofoModel, Status = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }
        }

        [Route("updateJourneyStatus")]
        [HttpPost]
        public IActionResult UpdateJourneyStatus(int journeyId, JourneyStatus status)
        {
            try
            {
                var result = _journeyService.UpdateJourneyStatus(journeyId, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [HttpPost]
        [Route("addJourneyUpdate1")]
        public IActionResult AddJourneyUpdate(JourneyUpdate JourneyUpdate)
        {
            try
            {
                JourneyUpdate.CheckpointId = null;
                var result = _journeyService.AddJourneyUpdate(JourneyUpdate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [Route("GetJourneyMontoring")]
        [HttpGet]
        public IActionResult GetJourneyMontoring(int journeyId)
        {
            try
            {
                return Ok(_journeyService.GetJourneyMontoring(journeyId));

            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });
            }
        }


        [HttpGet("")]
        [Authorize(Roles = "Product Line,Dispatcher,JMC,QHSE,GBM,Operation Manager")]
        [Route("getalljourneyinfo/{id}")]
        public IActionResult GetAllJourneyInfo(int id)
        {
            var result = _journeyService.GetAllJourneyInfo(id);

            if (result.Status == ResponseStatus.Success)
                return Ok(result.Data);

            return NotFound();
        }

        [Route("updateJourneyRiskStatus")]
        [HttpPost]
        public IActionResult UpdateJourneyRiskStatus(int journeyId, bool? isNight, RiskStatus? status)
        {
            try
            {
                var result = _journeyService.UpdateJourneyRiskStatus(journeyId,isNight, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }
        }
        [Route("rejectJourney")]
        [HttpPost]
        public IActionResult RejectJourney(int journeyId,string comment)
        {
            try
            {
                var result = _journeyService.RejectJourney(journeyId, comment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }
        }

        [Route("closeJourney")]
        [HttpPost]
        public IActionResult CloseJourney(int journeyId, string comment)
        {
            try
            {
                var result = _journeyService.CloseJourney(journeyId, comment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }
        }
        [Route("getCurrentJourney")]
        [HttpGet]
        public IActionResult CurrentJourney()
        {
            try
            {
                var driverId = Guid.Parse(User.Identity.Name);
                var result = _journeyService.GetDriverCurrentJourney(driverId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }
        }
    
        [AllowAnonymous]
        [HttpGet]
        [Route("getCalendar")]
        public IActionResult GetCalendar()
        {
            try
            {
                return Ok(_journeyService.GetCalendar());
            } 
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse<List<object>> {Data=new List<object>(), Status = DAL.Common.Enums.ResponseStatus.ServerError
    });

            }

        }
    
    }
}
