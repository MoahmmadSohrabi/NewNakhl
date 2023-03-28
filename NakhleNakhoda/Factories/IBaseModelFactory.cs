
namespace NakhleNakhoda.Factories
{
    /// <summary>
    /// Represents the base model factory that implements a most common model factories methods
    /// </summary>
    public partial interface IBaseModelFactory
    {
        /// <summary>
        /// Prepare verifying code
        /// </summary>
        /// <returns>Result</returns>
        string GenerateVerifyingCode();

        /// <summary>
        /// Varifying phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Result</returns>
        bool VarifyingPhoneNumber(string phoneNumber);

        /// <summary>
        /// Normalize phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Result</returns>
        //string NormalizePhoneNumber(string phoneNumber);

        /// <summary>
        /// Send a verifying code to the user phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        //Task SendVerifyingCodeSms(string phoneNumber, string verifyingCode);

        /// <summary>
        /// Send a sms message to the user phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="otp">otp</param>
        /// <returns>Result</returns>
        Task SendSmsMessage(string phoneNumber, string otp);

    }
}