namespace NakhleNakhoda.Services.Common
{
    public interface ISmsService
    {
        public Task<bool> SendOtp(string Phone, string Otp);
    }
}
