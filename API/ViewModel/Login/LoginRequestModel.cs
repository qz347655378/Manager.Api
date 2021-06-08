namespace API.ViewModel.Login
{
    /// <summary>
    /// 登录请求模型
    /// </summary>
    public class LoginRequestModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 存储验证码的key
        /// </summary>
        public string CaptchaKey { get; set; }
    }
}
