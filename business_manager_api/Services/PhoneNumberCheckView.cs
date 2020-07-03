using PhoneNumbers;


namespace business_manager_api.Services
{
    public class PhoneNumberCheckView
    {
        //FORMAT VALIDATION
        public static bool IsValidPhoneNumber(string phoneNumber, string defaultRegion = "")
        {
            try
            {
                //Création de l'intance PhoneNumberUtil
                var util = PhoneNumberUtil.GetInstance();
                PhoneNumber number = null;
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
                    number = util.Parse(phoneNumber, defaultRegion);
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

        //FORMAT INTERNATIONAL

        public static string FormatInternational(string phoneNumber, string regionCode)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return string.Empty;

            PhoneNumber number = null;
            var util = PhoneNumberUtil.GetInstance();
            number = util.Parse(phoneNumber, regionCode);
            return util.Format(number, PhoneNumberFormat.INTERNATIONAL);
        }


        //
    }
}
