namespace NakhleNakhoda.Common
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class CommonHelper
    {
        #region Ctor

        static CommonHelper() { }

        #endregion

        #region Methods

        /// <summary>
        /// Returns an random integer number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            using var random = new SecureRandomNumberGenerator();
            return random.Next(min, max);
        }

        public static string? PhoneFormat(string phone)
        {
            if (phone != null && phone.Trim().Length == 11)
                return string.Format("{0} - {1}", phone.Substring(0, 3), phone.Substring(3, 8));
            return phone;
        }

        public static string? MobileFormat(string phone)
        {
            if (phone != null && phone.Trim().Length == 11)
                return string.Format("{0} - {1}", phone.Substring(0, 4), phone.Substring(4, 7));
            return phone;
        }

        #endregion

    }
}
