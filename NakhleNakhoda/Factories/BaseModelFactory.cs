using Microsoft.AspNetCore.Identity;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.Services.Common;
using NakhleNakhoda.Services.Identity;
using System.Text.RegularExpressions;

namespace NakhleNakhoda.Factories
{
    /// <summary>
    /// Represents the implementation of the base model factory that implements a most common model factories methods
    /// </summary>
    public partial class BaseModelFactory : IBaseModelFactory
    {

        #region Fields

        private readonly UserManager<Member> _userManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ISmsService _messageService;
        #endregion

        #region Ctor

        public BaseModelFactory(
            UserManager<Member> userManager,
            IUserService userService,
            IConfiguration configuration,
            ISmsService messageService
            )
        {
            _userManager = userManager;
            _userService = userService;
            _configuration = configuration;
            _messageService = messageService;
        }

        #endregion


        #region Utilities

        /// <summary>
        /// Prepare verifying code
        /// </summary>
        public string GenerateVerifyingCode()
        {
            // get verifying code length from appsettings.json file
            var verifyingCodeLength = _configuration["Sms:VerifyingCodeLength"]!;

            // generate verifying code in random
            Random random = new();
            int min = (int)Math.Pow(10, int.Parse(verifyingCodeLength) - 1);
            int max = min * 10;
            var verifyingCode = random.Next(min, max).ToString();

            return verifyingCode;
        }

        /// <summary>
        /// Varifying phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Result</returns>
        public bool VarifyingPhoneNumber(string phoneNumber)
        {
            /*            if (phoneNumber.Length.Equals(13))
                        {
                            phoneNumber = phoneNumber.Replace("+98", "");
                        }*/

            if (PhoneRegex().Match(phoneNumber).Success)
                return true;
            return false;
        }

        /// <summary>
        /// Normalize phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Result</returns>
        /*public string NormalizePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length.Equals(12))
            {
                phoneNumber = phoneNumber.Substring(2); 
                //phoneNumber.Replace("98", "");
            }
            return phoneNumber;
        }*/

        #endregion

        #region Methods

        /// <summary>
        /// Send a verifying code to the user phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Result</returns>
        /*public async Task SendVerifyingCodeSms(string phoneNumber, string verifyingCode)
        {
            //var verifyingCode = GenerateVerifyingCode();
            // save verifyingCode in user table
            *//*            if (await CheckUser(phoneNumber, verifyingCode))
                        {*//*
            var senderNumber = _configuration["AppSettings:Sms:SenderNumber"];
            var smsUsername = _configuration["AppSettings:Sms:Username"];
            var smsPassword = _configuration["AppSettings:Sms:Password"];
            var service = new PublicApiV1(smsUsername, smsPassword);

            var verifyingMessage = _configuration["AppSettings:Sms:VerifyingMessage"];
            string body = string.Format(verifyingMessage, verifyingCode);

            await service.GroupSms(senderNumber, new List<string> { phoneNumber }, body, DateTime.Now, 2, null);

            var message = new Message
            {
                ReceivePhoneNumber = phoneNumber,
                Body = body,
                MessageType = MessageType.Otp
            };

            await _messageService.InsertAsync(message);
            //}
        }*/

        /// <summary>
        /// Send a sms message to the user phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="body">body</param>
        /// <returns>Result</returns>
        public async Task SendSmsMessage(string phoneNumber, string otp)
        {
            await _messageService.SendOtp(phoneNumber, otp);
        }

        /// <summary>
        /// Send a verifying code to the user phone number
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="verifyingCode">verifyingCode</param>
        /// <returns>Result</returns>
        public async Task<bool> CheckUser(string phone, string verifyingCode)
        {
            //var user = await _userManager.FindByNameAsync(Constants.PhoneCode + phone);
            var user = await _userService.GetByPhoneNumber(phone);
            //var user = await _userManager.GetUserNameAsync();
            //var result = await _userManager.CreateAsync(user, Input.Password);

            // if user not exist 
            if (user == null)
                return false;
            else
            {
                //await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, verifyingCode);
                return true;
            }
        }

        [GeneratedRegex("^([0]{1}[9]{1}[0-9]{9})|([0]{0}[9]{1}[0-9]{9})$")]
        private static partial Regex PhoneRegex();

        #endregion

    }
}