using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel.Login
{
    public class CaptchaViewModel
    {
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
