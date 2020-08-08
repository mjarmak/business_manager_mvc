using PhoneNumbers;
// ReSharper disable All


namespace business_manager_api.Services
{
    public class PhoneNumberCheckView
    {
        //FORMAT VALIDATION
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return false;
            }
            try
            {
                //Création de l'intance PhoneNumberUtil
                var util = PhoneNumberUtil.GetInstance();
                PhoneNumber number;
                //Si le numéro contient l'indicatif + ou le 00
                if (phoneNumber.StartsWith("+") || phoneNumber.StartsWith("00"))
                {
                    if (phoneNumber.StartsWith("00"))
                    {
                        phoneNumber = "+" + phoneNumber.Remove(0, 2);
                    }

                    number = util.Parse(phoneNumber, "");
                    // Récupération de la région au numéro avec l'indication +
                    string regionCode = util.GetRegionCodeForNumber(number);
                    // Validation du numéro qui correspond à la région trouvées
                    return util.IsValidNumberForRegion(number, regionCode);
                }
                else
                {
                    number = util.Parse(phoneNumber, "BE");
                    // Validation du numéro sans indication mais avec le region code
                    return util.IsValidNumber(number);
                }
            }
            catch (NumberParseException)
            {
                //LOG
                return false;
            }
        }

        //Check if it is a mobile number

        public static bool IsMobileNumber(string phoneNumber)
        {
            try {
                var util = PhoneNumberUtil.GetInstance();
                PhoneNumber number = util.Parse(phoneNumber, "BE");
                return util.GetNumberType(number) == PhoneNumberType.MOBILE;
            } catch (NumberParseException) {
                return false;
            }
        }
    }
}
