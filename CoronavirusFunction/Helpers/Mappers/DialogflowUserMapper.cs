using CoronavirusFunction.Models;

namespace CoronavirusFunction.Helpers
{
    public static class DialogflowUserMapper
    {
        public static User ToUser(this DialogflowUserDto userDto)
        {
            return new User()
            {
                UserId = userDto.UserId,
                LastSeen = userDto.LastSeen,
            };
        }
    }
}
