namespace ADManager
{
    public sealed class ADManagerSecurityOptions
    {
        public ADManagerSecurityOptions(
            string login, 
            string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
