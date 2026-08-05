namespace TodoApp.Shared.Utils
{
    public static class RegexOperator
    {
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        public static bool IsEmailValid(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, EmailPattern);
        }

        public static bool IsPasswordValid(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(password, PasswordPattern);
        }
    }
}
