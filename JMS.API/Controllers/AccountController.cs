//using JMS.API.Common;
//using JMS.API.Models;
//using JMS.API.Repositories;
//using Microsoft.AspNet.Identity;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using static JMS.API.Common.Enums;

//namespace JMS.API.Controllers
//{
//    [RoutePrefix("api/Account")]
//    public class AccountController : ApiController
//    {
//        private UsersRepository userRepo = null;

//        public AccountController()
//        {
//            userRepo = new UsersRepository();
//        }

//        // POST api/Account/Register
//        [Authorize(Roles = "Admin")]
//        [Route("Register")]
//        public async Task<IHttpActionResult> Register(UserModel userModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var result = await userRepo.AddUser(userModel);
//            IHttpActionResult errorResult = GetErrorResult(result.Data);

//            if (errorResult != null)
//            {
//                return errorResult;
//            }

//            return Ok();
//        }

//        [Authorize(Roles = "Admin")]
//        [Route("Update")]
//        public async Task<IHttpActionResult> Update(UserModel userModel)
//        {
//            var result = await userRepo.UpdateUser(userModel);
//            IHttpActionResult errorResult = GetErrorResult(result.Data);

//            if (errorResult != null)
//            {
//                return errorResult;
//            }

//            return Ok();
//        }

//        [Authorize(Roles = "Admin")]
//        [Route("ChangePassword")]
//        public async Task<IHttpActionResult> ChangePassword(ChangePassswordModel cpModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }


//            var result = await userRepo.ChangePassword(cpModel.Id, cpModel.Password);
//            IHttpActionResult errorResult = GetErrorResult(result.Data);

//            if (errorResult != null)
//            {
//                return errorResult;
//            }

//            return Ok();
//        }

//        [AllowAnonymous]
//        [Route("SendResetPasswordMail")]
//        public async Task<IHttpActionResult> SendResetPasswordMail(string email)
//        {
//            var result = await userRepo.SendResetPasswordMail(email);

//            IHttpActionResult errorResult = GetErrorResult(result.Data);

//            if (errorResult != null)
//            {
//                return errorResult;
//            }

//            return Ok();
//        }

//        [AllowAnonymous]
//        [Route("ResetPassword")]
//        public async Task<IHttpActionResult> ResetPassword(ResetPassswordModel rpModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }


//            var result = await userRepo.ResetPassword(rpModel.Email, rpModel.Token, rpModel.Password);
//            IHttpActionResult errorResult = GetErrorResult(result.Data);

//            if (errorResult != null)
//            {
//                return errorResult;
//            }

//            return Ok();
//        }

//        [Authorize(Roles = "Admin")]
//        [Route("DeleteUser")]
//        public async Task<IHttpActionResult> DeleteUser(string userId)
//        {
//            var result = await userRepo.DeleteUser(userId);
//            IHttpActionResult errorResult = GetErrorResult(result.Data);

//            if (errorResult != null)
//            {
//                return errorResult;
//            }

//            return Ok();
//        }

//        [Authorize(Roles = "Admin")]
//        [Route("ChangeStatus")]
//        public async Task<IHttpActionResult> ChangeStatus(string userId, bool isActive)
//        {
//            var result = await userRepo.ChangeStatus(userId, isActive);
//            IHttpActionResult errorResult = GetErrorResult(result.Data);

//            if (errorResult != null)
//            {
//                return errorResult;
//            }

//            return Ok();
//        }


//        [Authorize(Roles = "Admin")]
//        [Route("GetUsers")]
//        [HttpPost]
//        public HttpResponseMessage GetUsers(UserModel filters)
//        {
//            var result = userRepo.GetUsers(filters);

//            HttpResponseMessage responseMessage = result.Status == ResponseStatus.Success ?
//                Request.CreateResponse(HttpStatusCode.OK, result) :
//                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed To Get Users");

//            return responseMessage;
//        }


//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                userRepo.Dispose();
//            }

//            base.Dispose(disposing);
//        }
//        private IHttpActionResult GetErrorResult(IdentityResult result)
//        {
//            if (result == null)
//            {
//                return InternalServerError();
//            }

//            if (!result.Succeeded)
//            {
//                if (result.Errors != null)
//                {
//                    foreach (string error in result.Errors)
//                    {
//                        ModelState.AddModelError("", error);
//                    }
//                }

//                if (ModelState.IsValid)
//                {
//                    // No ModelState errors are available to send, so just return an empty BadRequest.
//                    return BadRequest();
//                }

//                return BadRequest(ModelState);
//            }

//            return null;
//        }
//    }
//}
