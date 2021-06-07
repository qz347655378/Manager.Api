namespace API.ViewModel.Login
{
    public class LoginResponseModel
    {
        /// <summary>
        /// 授权令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public long Expired { get; set; }
    }
}
