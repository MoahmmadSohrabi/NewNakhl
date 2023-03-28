using Microsoft.Extensions.Configuration;

namespace NakhleNakhoda.Services.Common
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        public SmsService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendOtp(string Phone, string Otp)
        {
            try
            {
                Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(_configuration["Sms:ApiKey"]);

                var result = await api.VerifyLookup(Phone, Otp, "otp");
                if (result != null)
                {
                    return true;
                }
            }
            catch (Kavenegar.Core.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                var s = ex.Message;
            }
            catch (Kavenegar.Core.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                var s = ex.Message;
            }

            return false;
        }
    }
}
