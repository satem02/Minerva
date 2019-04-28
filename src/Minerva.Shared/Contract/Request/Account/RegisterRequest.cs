namespace Minerva.Shared.Contract.Request.Account
{
    public class RegisterRequest
    {
        public string Password { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}