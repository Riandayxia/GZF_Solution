using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Quest.Mobile.MVC.ViewModels
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 获取或设置 登录账号
        /// </summary>
        [Required]
        [Display(Name = "登录账号")]
        public String Account { get; set; }

        /// <summary>
        /// 获取或设置 登录密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "登录密码")]
        public String Password { get; set; }

        /// <summary>
        /// 获取或设置 是否记住登录
        /// </summary>
        [Display(Name = "记住登录")]
        public Boolean IsRememberLogin { get; set; }

        /// <summary>
        /// 获取或设置 登录成功后返回地址
        /// </summary>
        public String ReturnUrl { get; set; }
    }
}
