using Dto.Other;
using Dto.Payment;
using Dto.Response.Payment;
using Dto.Response.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ZarinPal.Class;

namespace NakhleNakhoda.Services.Pay
{
    public interface IPayService
    {
        Task<PayRequestDTo> Request(string domain, string Mobile, string Description, string Email, int Price, long Id);
        Task<Verification> Validate(string authority, int Amount);
    }
    public class PayRequestDTo
    {
        public string Url { get; set; } = "";
        public string Authority { get; set; } = "";
    }
    public class PayService : IPayService
    {
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IConfiguration _configuration;
        private readonly Payment.Mode _mode = Payment.Mode.sandbox;

        public PayService(IConfiguration configuration)
        {
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            _configuration = configuration;
        }

        /// <summary>
        /// ﻓﺮﺍﻳﻨﺪ ﺧﺮﻳﺪ
        /// </summary>
        /// <returns></returns>
        public async Task<PayRequestDTo> Request(string domain, string Mobile, string Description, string Email, int Price, long Id)
        {

            var result = await _payment.Request(new DtoRequest()
            {
                Mobile = Mobile,
                CallbackUrl = $"{domain}/Book/Validate/{Id}",
                Description = Description,
                Email = Email,
                Amount = Price,
                MerchantId = _configuration["ZarinPal:MerchantId"]
            }, _mode);
            return new PayRequestDTo() { Url = "https://sandbox.zarinpal.com/pg/StartPay/", Authority = result.Authority };
        }

        /// <summary>
        /// ﻓﺮﺍﻳﻨﺪ ﺧﺮﻳﺪ ﺑﺎ ﺗﺴﻮﻳﻪ ﺍﺷﺘﺮﺍﻛﻲ 
        /// </summary>
        /// <returns></returns>
        public async Task<string> RequestWithExtra(string domain, long Id)
        {
            var result = await _payment.Request(new DtoRequestWithExtra()
            {
                Mobile = "09121112222",
                CallbackUrl = $"{domain}/Book/Validate/{Id}",
                Description = "توضیحات",
                Email = "farazmaan@outlook.com",
                Amount = 1000000,
                MerchantId = _configuration["ZarinPal:MerchantId"],
                AdditionalData = "{\"Wages\":{\"zp.1.1\":{\"Amount\":120,\"Description\":\" ﺗﻘﺴﻴﻢ \"}, \" ﺳﻮﺩ ﺗﺮﺍﻛﻨﺶ zp.2.5\":{\"Amount\":60,\"Description\":\" ﻭﺍﺭﻳﺰ \"}}} "
            }, _mode);
            return $"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}";
        }
        /// <summary>
        /// اعتبار سنجی خرید
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public async Task<Verification> Validate(string authority, int Amount)
        {
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = Amount,
                MerchantId = _configuration["ZarinPal:MerchantId"],
                Authority = authority
            }, _mode);
            return verification;
        }

        /// <summary>
        /// ﺩﺭ ﺭﻭﺵ ﺍﻳﺠﺎﺩ ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﺑﺎ ﻃﻮﻝ ﻋﻤﺮ ﺑﺎﻻ ﻣﻤﻜﻦ ﺍﺳﺖ ﺣﺎﻟﺘﻲ ﭘﻴﺶ ﺁﻳﺪ ﻛﻪ ﺷﻤﺎ ﺑﻪ ﺗﻤﺪﻳﺪ ﺑﻴﺸﺘﺮ ﻃﻮﻝ ﻋﻤﺮ ﻳﻚ ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻧﻴﺎﺯ ﺩﺍﺷﺘﻪ ﺑﺎﺷﻴﺪ
        /// ﺩﺭ ﺍﻳﻦ ﺻﻮﺭﺕ ﻣﻲ ﺗﻮﺍﻧﻴﺪ ﺍﺯ ﻣﺘﺪ زیر ﺍﺳﺘﻔﺎﺩﻩ ﻧﻤﺎﻳﻴﺪ 
        /// </summary>
        /// <returns></returns>
        public async Task<Verification> RefreshAuthority()
        {
            var refresh = await _authority.Refresh(new DtoRefreshAuthority
            {
                Authority = "",
                ExpireIn = 1,
                MerchantId = _configuration["ZarinPal:MerchantId"],
            }, _mode);
            return refresh;
        }

        /// <summary>
        /// ﻣﻤﻜﻦ ﺍﺳﺖ ﺷﻤﺎ ﻧﻴﺎﺯ ﺩﺍﺷﺘﻪ ﺑﺎﺷﻴﺪ ﻛﻪ ﻣﺘﻮﺟﻪ ﺷﻮﻳﺪ ﭼﻪ ﭘﺮﺩﺍﺧﺖ ﻫﺎﻱ ﺗﻮﺳﻂ ﻭﺏ ﺳﺮﻭﻳﺲ ﺷﻤﺎ ﺑﻪ ﺩﺭﺳﺘﻲ ﺍﻧﺠﺎﻡ ﺷﺪﻩ ﺍﻣﺎ ﻣﺘﺪ  ﺭﻭﻱ ﺁﻧﻬﺎ ﺍﻋﻤﺎﻝ ﻧﺸﺪﻩ
        /// ، ﺑﻪ ﻋﺒﺎﺭﺕ ﺩﻳﮕﺮ ﺍﻳﻦ ﻣﺘﺪ ﻟﻴﺴﺖ ﭘﺮﺩﺍﺧﺖ ﻫﺎﻱ ﻣﻮﻓﻘﻲ ﻛﻪ ﺷﻤﺎ ﺁﻧﻬﺎ ﺭﺍ ﺗﺼﺪﻳﻖ ﻧﻜﺮﺩﻩ ﺍﻳﺪ ﺭﺍ ﺑﻪ PaymentVerification ﺷﻤﺎ ﻧﻤﺎﻳﺶ ﻣﻲ ﺩﻫﺪ.
        /// </summary>
        /// <returns></returns>

        public async Task<Unverified> Unverified()
        {
            var refresh = await _transactions.GetUnverified(new DtoMerchant
            {
                MerchantId = _configuration["ZarinPal:MerchantId"],
            }, _mode);
            return refresh;
        }
    }
}
