using ECO3_Testing.Settings;

namespace ECO3_Testing.WindowsAuthentication
{
    public class SignInThroughUrl
    {
        private static string baseUrl = ObjectRepository.Config.InternalQaaUrl;

        private static string advancedUser = ObjectRepository.Config.AdvancedUser;

        private static string standardUser = ObjectRepository.Config.StandardUser;

        private static string internalAdminUser = ObjectRepository.Config.InternalAdminUser;

        private static string basicUser = ObjectRepository.Config.BasicUser;

        private static string internalUserPassword = ObjectRepository.Config.GetInternalPassword;

        public static string UsersUrl(LoginUsers user)
        {
            string userBasedUrl = null;
            switch (user)
            {
                case LoginUsers.Advanced:
                    userBasedUrl = ProduceUserBasedUrl(advancedUser);
                    break;
                case LoginUsers.Standard:
                    userBasedUrl = ProduceUserBasedUrl(standardUser);
                    break;
                case LoginUsers.InternalAdmin:
                    userBasedUrl = ProduceUserBasedUrl(internalAdminUser);
                    break;
                case LoginUsers.Basic:
                    userBasedUrl = ProduceUserBasedUrl(basicUser);
                    break;
            }
            return userBasedUrl;
        }
        private static string ProduceUserBasedUrl(string user)
        {
            string protocol = "http://";
            string updatedBaseUrl = baseUrl.Replace(protocol,"");
            return string.Concat(protocol,user,":",internalUserPassword,"@",updatedBaseUrl);
        }
    }
}
