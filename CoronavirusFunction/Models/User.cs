namespace CoronavirusFunction.Models
{
    public class User
    {
        private string _userId;

        public User(string userId)
        {
            _userId = userId;
        }
        public string UserId { get => _userId; set => _userId = value; }
    }
}
