//using JMS.API.Common;
//using JMS.API.Models;
//using JMS.DAL;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security.DataProtection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using static JMS.API.Common.Enums;
//using System.Linq.Dynamic;

//namespace JMS.API.Repositories
//{

//    public sealed class UsersRepository : IDisposable
//    {
//        private ApplicationDbContext appContext;

//        private UserManager<ApplicationUser> userManager;

//        public UsersRepository()
//        {
//            appContext = new ApplicationDbContext();
//            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appContext));
//        }

//        public async Task<ServiceResponse<IdentityResult>> AddUser(UserModel userModel)
//        {

//            ServiceResponse<IdentityResult> response = new ServiceResponse<IdentityResult>();

//            try
//            {
//                ApplicationUser user = new ApplicationUser
//                {
//                    UserName = userModel.Email,
//                    PhoneNumber = userModel.PhoneNumber,
//                    Email = userModel.Email,
//                    FullName = userModel.FullName,
//                    UserGroupId = userModel.UserGroupId,
//                    UserWorkForceId = userModel.UserWorkForceId,
//                    LicenseExpiryDate = userModel.LicenseExpiryDate,
//                    LicenseNo = userModel.LicenseNo,
//                    TrainingDetails = userModel.TrainingDetails,
//                    GatePassStatus = userModel.GatePassStatus

//                };

//                response.Data = await userManager.CreateAsync(user, userModel.Password);

//                if (userModel.UserRole != null)
//                    userManager.AddToRole(user.Id, userModel.UserRole);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public async Task<ServiceResponse<ApplicationUser>> FindUser(string userName, string password)
//        {

//            ServiceResponse<ApplicationUser> response = new ServiceResponse<ApplicationUser>();

//            try
//            {
//                response.Data = await userManager.FindAsync(userName, password);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public ServiceResponse<List<string>> GetUserRoles(string userId)
//        {

//            ServiceResponse<List<string>> response = new ServiceResponse<List<string>>();

//            try
//            {
//                response.Data = userManager.GetRoles(userId).ToList();
//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public async Task<ServiceResponse<IdentityResult>> UpdateUser(UserModel userModel)
//        {

//            ServiceResponse<IdentityResult> response = new ServiceResponse<IdentityResult>();

//            try
//            {
//                var currentUser = userManager.FindById(userModel.Id);

//                if (!string.IsNullOrEmpty(userModel.Email))
//                {
//                    currentUser.UserName = userModel.Email;
//                    currentUser.Email = userModel.Email;
//                }

//                if (!string.IsNullOrEmpty(userModel.PhoneNumber))
//                    currentUser.PhoneNumber = userModel.PhoneNumber;

//                if (!string.IsNullOrEmpty(userModel.FullName))
//                    currentUser.FullName = userModel.FullName;

//                if (!string.IsNullOrEmpty(userModel.LicenseNo))
//                    currentUser.LicenseNo = userModel.LicenseNo;

//                if (!string.IsNullOrEmpty(userModel.TrainingDetails))
//                    currentUser.TrainingDetails = userModel.TrainingDetails;

//                if (!string.IsNullOrEmpty(userModel.GatePassStatus))
//                    currentUser.GatePassStatus = userModel.GatePassStatus;

//                if (userModel.UserGroupId.HasValue)
//                    currentUser.UserGroupId = userModel.UserGroupId;

//                if (userModel.UserWorkForceId.HasValue)
//                    currentUser.UserWorkForceId = userModel.UserWorkForceId;

//                if (userModel.LicenseExpiryDate.HasValue)
//                    currentUser.LicenseExpiryDate = userModel.LicenseExpiryDate;


//                response.Data = await userManager.UpdateAsync(currentUser);

//                if (userModel.UserRole != null)
//                    userManager.AddToRole(currentUser.Id, userModel.UserRole);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public async Task<ServiceResponse<IdentityResult>> ChangePassword(string userId, string newPassword)
//        {

//            ServiceResponse<IdentityResult> response = new ServiceResponse<IdentityResult>();

//            try
//            {
//                var currentUser = userManager.FindById(userId);

//                var provider = new DpapiDataProtectionProvider("JMS");
//                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("AdminChangePasswordTokenProvider"));

//                var token = await userManager.GeneratePasswordResetTokenAsync(userId);
//                response.Data = await userManager.ResetPasswordAsync(userId, token, newPassword);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public async Task<ServiceResponse<IdentityResult>> SendResetPasswordMail(string email)
//        {

//            ServiceResponse<IdentityResult> response = new ServiceResponse<IdentityResult>();

//            try
//            {

//                var provider = new DpapiDataProtectionProvider("JMS");
//                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("PasswordResetTokenProvider"));

//                var currentUser = userManager.FindByEmail(email);
//                string passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(currentUser.Id);

//                //Send Mail with the reset token 
//                MailMessage passwordResetMail = new MailMessage
//                {
//                    Subject = "Reset Your Password at JMS",
//                    Body = $"Please follow this link to reset your password https://localhost/#/resetpassword?email={email}&token={passwordResetToken}"
//                };

//                passwordResetMail.To.Add(email);


//                EmailsHandler.SendEmail(passwordResetMail);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public async Task<ServiceResponse<IdentityResult>> ResetPassword(string email, string token, string newPassword)
//        {

//            ServiceResponse<IdentityResult> response = new ServiceResponse<IdentityResult>();

//            try
//            {
//                var currentUser = userManager.FindByEmail(email);

//                var provider = new DpapiDataProtectionProvider("JMS");
//                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("PasswordResetTokenProvider"));

//                response.Data = await userManager.ResetPasswordAsync(currentUser.Id, token, newPassword);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public async Task<ServiceResponse<IdentityResult>> DeleteUser(string userId)
//        {

//            ServiceResponse<IdentityResult> response = new ServiceResponse<IdentityResult>();

//            try
//            {
//                var currentUser = userManager.FindById(userId);

//                response.Data = await userManager.DeleteAsync(currentUser);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public async Task<ServiceResponse<IdentityResult>> ChangeStatus(string userId, bool isActive)
//        {

//            ServiceResponse<IdentityResult> response = new ServiceResponse<IdentityResult>();

//            try
//            {
//                response.Data = await userManager.SetLockoutEnabledAsync(userId, !isActive);

//                response.Status = ResponseStatus.Success;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }

//        public ServiceResponse<PageResult<UserModel>> GetUsers(UserModel filters)
//        {
//            ServiceResponse<PageResult<UserModel>> response = new ServiceResponse<PageResult<UserModel>>();

//            try
//            {
//                using (var dbContext = new JMSDBEntities(true))
//                {
//                    var result = dbContext.AspNetUsers.AsQueryable();

//                    if (filters != null)
//                    {
//                        if (!string.IsNullOrEmpty(filters.FullName))
//                            result = result.Where(user => !string.IsNullOrEmpty(user.FullName) && user.FullName.Contains(filters.FullName));

//                        if (!string.IsNullOrEmpty(filters.LicenseNo))
//                            result = result.Where(user => !string.IsNullOrEmpty(user.LicenseNo) && user.LicenseNo.Contains(filters.LicenseNo));

//                        if (!string.IsNullOrEmpty(filters.TrainingDetails))
//                            result = result.Where(user => !string.IsNullOrEmpty(user.TrainingDetails) && user.TrainingDetails.Contains(filters.TrainingDetails));

//                        if (!string.IsNullOrEmpty(filters.GatePassStatus))
//                            result = result.Where(user => !string.IsNullOrEmpty(user.GatePassStatus) && user.GatePassStatus.Contains(filters.GatePassStatus));

//                        if (!string.IsNullOrEmpty(filters.Email))
//                            result = result.Where(user => !string.IsNullOrEmpty(user.Email) && user.Email.Contains(filters.Email));

//                        if (!string.IsNullOrEmpty(filters.PhoneNumber))
//                            result = result.Where(user => !string.IsNullOrEmpty(user.PhoneNumber) && user.PhoneNumber.Contains(filters.PhoneNumber));

//                        if (filters.LicenseExpiryDate.HasValue)
//                            result = result.Where(user => user.LicenseExpiryDate.HasValue && user.LicenseExpiryDate >= filters.LicenseExpiryDate);

//                        if (filters.UserGroupId.HasValue)
//                            result = result.Where(user => user.UserGroupId.HasValue && user.UserGroupId == filters.UserGroupId);

//                        if (filters.UserWorkForceId.HasValue)
//                            result = result.Where(user => user.UserWorkForceId.HasValue && user.UserWorkForceId == filters.UserWorkForceId);
//                    }

//                    var mappedResult = result.OrderBy(user => user.FullName).Skip((filters.PageNo - 1) * filters.PageSize).Take(filters.PageSize)
//                        .Select(user => new UserModel
//                        {
//                            Id = user.Id,
//                            Email = user.Email,
//                            FullName = user.FullName,
//                            LicenseExpiryDate = user.LicenseExpiryDate,
//                            LicenseNo = user.LicenseNo,
//                            TrainingDetails = user.TrainingDetails,
//                            GatePassStatus = user.GatePassStatus,
//                            PhoneNumber = user.PhoneNumber,
//                            GroupName = user.UserGroup.Name,
//                            UserGroupId = user.UserGroupId,
//                            WorkForceName = user.UserWorkForce.Name,
//                            UserWorkForceId = user.UserWorkForceId,
//                            UserRole = user.AspNetRoles.FirstOrDefault().Name,
//                        }).ToList();

//                    PageResult<UserModel> currentPage =
//                            new PageResult<UserModel>
//                            {
//                                TotalItems = result.Count(),
//                                PageItems = mappedResult
//                            };

//                    response.Data = currentPage;

//                    response.Status = ResponseStatus.Success;
//                }
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                response.Status = ResponseStatus.ServerError;

//                ExceptionLogger.LogException(ex);
//            }

//            return response;
//        }


//        public void Dispose()
//        {
//            appContext.Dispose();
//            userManager.Dispose();

//        }
//    }
//}