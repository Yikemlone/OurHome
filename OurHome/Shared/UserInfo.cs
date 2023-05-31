namespace OurHome.Shared
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public Dictionary<string, string> ExposedClaims { get; set; }
    }
}
