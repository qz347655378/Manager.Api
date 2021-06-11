namespace Common.Enum
{
    /// <summary>
    /// 常用网络请求回复状态
    /// </summary>
    public enum ResponseStatusEnum
    {
        /// <summary>
        /// 请求失败
        /// </summary>
        Fail = 0,
        /// <summary>
        /// 请求成功
        /// </summary>
        Ok = 200,
        /// <summary>
        /// 客户端请求的语法错误，服务器无法理解
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// 请求要求用户的身份认证
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// 禁止请求，一般用于没有权限
        /// </summary>
        Forbidden=403,
        /// <summary>
        /// 未找到请求资源
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// 内部错误
        /// </summary>
        InternalServerError = 500,
    }
}
