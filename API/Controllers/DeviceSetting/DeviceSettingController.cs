using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Core.Filters;
using API.ViewModel;
using Common.Net;
using Newtonsoft.Json;
using Serilog;

namespace API.Controllers.DeviceSetting
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceSettingController : ControllerBase
    {
        /// <summary>
        /// 响应心跳包
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(Heartbeat))]
        public DeviceResponse<string> Heartbeat(HeartbeatRequest model)
        {
            // Log.Information($"摄像机唯一标识：{model.vehicleLaneKey}");
            var result = new DeviceResponse<string>
            {
                data = "",
                info = "接收成功",
                resultCode = 100
            };
            return result;
        }

        /// <summary>
        /// 摄像机识别反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("sendScanCar")]
        public DeviceResponse<SendScanCarReq> SendScanCar(SendScanCarModel model)
        {
            Log.Information(JsonConvert.SerializeObject(model));
            var result = new DeviceResponse<SendScanCarReq>
            {
                resultCode = 100
            };
            if (model.type == 0)
            {
                result.type = 0;
                result.data = null;
                return result;
            }

            result.type = 1;

            var req = new SendScanCarReq();

            if (model.license != "鲁KWZ883") return result;
            req.showcontent = $"欢迎{model.license}入场";
            result.data = req;

            OpenGate(1, "6354A19A1CABF391D7B36C343365A6FB");

            Log.Information(JsonConvert.SerializeObject(result));

            return result;


        }




        /// <summary>
        /// 异步开闸，channelNum取值为1和2
        /// </summary>
        /// <param name="channelNum"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet(nameof(OpenGateAsync))]
        public async Task<ResponseResult<string>> OpenGateAsync(int channelNum, string key)
        {
            var result = new ResponseResult<string>
            {
                Code = Common.Enum.ResponseStatusEnum.Ok
            };
            var url = "http://192.168.1.101/Home/OpenGate";

            if (string.IsNullOrEmpty(key)) key = "6354A19A1CABF391D7B36C343365A6FB";

            var data = new
            {
                key,
                channelNum
            };
            var resp = await NetHelper.PostAsync(url, JsonConvert.SerializeObject(data), Encoding.GetEncoding("GBK"), "application/x-www-form-urlencoded");
            var m = JsonConvert.DeserializeObject<DeviceResponse<string>>(resp);
            result.Data = resp;
            result.Msg = m.info;
            Log.Information(resp);
            return result;
        }


        /// <summary>
        /// 异步关闸，channelNum取值为1和2
        /// </summary>
        /// <param name="channelNum"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet(nameof(CloseGateAsync))]
        public async Task<ResponseResult<string>> CloseGateAsync(int channelNum, string key)
        {
            var result = new ResponseResult<string>
            {
                Code = Common.Enum.ResponseStatusEnum.Ok
            };
            var url = "http://192.168.1.100/Home/OpenGate";
            if (string.IsNullOrEmpty(key)) key = "6354A19A1CABF391D7B36C343365A6FB";
            var data = new
            {
                key,
                channelNum
            };
            var resp = await NetHelper.PostAsync(url, JsonConvert.SerializeObject(data), Encoding.GetEncoding("GBK"), "application/x-www-form-urlencoded");
            var m = JsonConvert.DeserializeObject<DeviceResponse<string>>(resp);
            result.Data = resp;
            result.Msg = m.info;
            Log.Information(resp);
            return result;
        }



        /// <summary>
        ///  异步道闸常开
        /// </summary>
        /// <param name="channelNum"></param>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [HttpGet(nameof(OftenOpenGateAsync))]
        public async Task<ResponseResult<string>> OftenOpenGateAsync(int channelNum, string key, int flag)
        {
            var result = new ResponseResult<string>
            {
                Code = Common.Enum.ResponseStatusEnum.Ok
            };
            var url = "http://192.168.1.100/Home/OftenOpenGate";


            var data = new
            {
                key,
                channelNum,
                Flag = flag
            };
            var resp = await NetHelper.PostAsync(url, JsonConvert.SerializeObject(data), Encoding.GetEncoding("GBK"), "application/x-www-form-urlencoded");
            var m = JsonConvert.DeserializeObject<DeviceResponse<string>>(resp);
            result.Data = resp;
            result.Msg = m.info;
            Log.Information(resp);
            return result;
        }

        /// <summary>
        /// 开闸
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(OpenGate))]
        public ResponseResult<string> OpenGate(int channelNum, string key)
        {
            var result = new ResponseResult<string>
            {
                Code = Common.Enum.ResponseStatusEnum.Ok
            };
            var url = "http://192.168.1.101/Home/OpenGate";


            var data = new
            {
                key,
                channelNum
            };
            var resp = NetHelper.Post(url, JsonConvert.SerializeObject(data), Encoding.GetEncoding("GBK"), "application/x-www-form-urlencoded");
            var m = JsonConvert.DeserializeObject<DeviceResponse<string>>(resp);
            result.Data = resp;
            result.Msg = m.info;
            Log.Information(resp);
            return result;
        }

        /// <summary>
        /// 异步往显示屏显示一条消息
        /// </summary>
        /// <param name="content">消息内容，长度不超过80个汉字</param>
        /// <returns></returns>
        [HttpPost(nameof(ShowAtOnce))]
        public async Task<ResponseResult<string>> ShowAtOnce(string content)
        {
            var result = new ResponseResult<string>
            {
                Code = Common.Enum.ResponseStatusEnum.Ok
            };
            var url = "http://192.168.1.101/Home/ShowAtOnce";

            var model = new ShowAtOnceModel
            {
                showcontent = content
            };


            var resp = await NetHelper.PostAsync(url, JsonConvert.SerializeObject(model), Encoding.GetEncoding("GBK"), "application/x-www-form-urlencoded");
            var m = JsonConvert.DeserializeObject<DeviceResponse<string>>(resp);
            result.Data = resp;
            result.Msg = m.info;
            Log.Information(resp);
            return result;
        }

    }



    /// <summary>
    /// 心跳包请求数据
    /// </summary>
    public class HeartbeatRequest
    {
        /// <summary>
        /// 摄像机唯一标识
        /// </summary>
        public string vehicleLaneKey { get; set; }
    }

    /// <summary>
    /// 通用返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceResponse<T>
    {
        public string info { get; set; } = "接收成功";

        public int resultCode { get; set; }

        public T data { get; set; }
        public int type { get; set; } = 0;

    }


    /// <summary>
    /// 摄像机识别结果
    /// </summary>
    public class SendScanCarModel
    {
        /// <summary>
        /// 摄像机唯一标识
        /// </summary>
        public string vehicleLaneKey { get; set; }
        /// <summary>
        /// 设备 ip 地址
        /// </summary>
        public string ipaddr { get; set; }

        /// <summary>
        /// 车牌号码字符串
        /// </summary>
        public string license { get; set; }

        /// <summary>
        /// 车牌颜色 0：未知、1：蓝色、2：黄色、3：白色、 4：黑色、5：绿色
        /// </summary>
        public string colorType { get; set; }

        /// <summary>
        /// 车牌类型 0：未知车牌:、1：蓝牌小汽车、2：: 黑牌小汽车、3：单排黄牌、
        /// 4：双排黄牌、 5： 警车车牌、6：武警车牌、7：个性化车牌、
        /// 8：单 排军车牌、9：双排军车牌、10：使馆车牌、
        ///  11： 香港进出中国大陆车牌、12：农用车牌、13：教练车牌、14：澳门进出中国大陆车牌、15：双层武警车牌、
        ///  16：武警总队车牌、 17：双层武警总队车牌
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 识别结果可行度 1-100
        /// </summary>
        public string confidence { get; set; }

        /// <summary>
        /// 识别时间
        /// </summary>
        public DateTime scanTime { get; set; }

        /// <summary>
        ///触发类型 "0" 视频触发; "1" 地感触发； "2" 手动触发； "3" 手动抓图
        /// </summary>
        public string triggerType { get; set; }

        /// <summary>
        /// base64 编码的图像数据车牌大图
        /// </summary>
        public string imageFile { get; set; }

        /// <summary>
        /// base64 编码的图像数据车牌小图
        /// </summary>
        public string imageFragmentFile { get; set; }
    }

    /// <summary>
    /// 车牌识别结果返回
    /// </summary>
    public class SendScanCarReq
    {
        /// <summary>
        /// 是否开闸 1：开闸 0：不开闸
        /// </summary>
        public int isopen { get; set; } = 0;

        /// <summary>
        /// //开闸端口号 1：Out1，2：Out2
        /// </summary>
        public int channelNum { get; set; } = 2;

        /// <summary>
        /// 485 输出端口号，1：A1B1，2：A2B2
        /// </summary>
        public int channelOut { get; set; } = 2;

        /// <summary>
        /// 是否发送显示 0：不发送，1：发送
        /// </summary>
        public int isshow { get; set; } = 1;

        /// <summary>
        /// 显示屏文字 
        /// </summary>
        public string showcontent { get; set; } = "临时车禁止入内";


        /// <summary>
        /// 是否发送语音 0：不发送，1:发送
        /// </summary>
        public int isvoice { get; set; } = 1;

        /// <summary>
        /// 语音类型 0：多功能语音，
        /// 1：固定语音（为 1 时，voicecontent 内容为固定语音的组合代码查表序号，
        /// 以 16 进制字符串方 式，如:“3B0F010203040566”，
        /// 表示“粤 A12345 欢迎光临”；
        /// </summary>
        public int voiceType { get; set; } = 0;

    }

    /// <summary>
    /// 显示屏显示一次消息
    /// </summary>
    public class ShowAtOnceModel
    {
        /// <summary>
        /// 摄像机 Key（由厂家分配）
        /// </summary>
        public string key { get; set; } = "6354A19A1CABF391D7B36C343365A6FB";

        /// <summary>
        /// 485 输出端口号，1：A1B1，2：A2B2
        /// </summary>
        public int channelout { get; set; } = 2;

        /// <summary>
        /// 文本编号 0-7
        /// </summary>
        public int textmum { get; set; } = 2;

        /// <summary>
        /// 行号：1-4
        /// </summary>
        public int linenumber { get; set; } = 2;

        /// <summary>
        /// 0：立即显示
        ///   1：向上移动
        ///   2：向下移动
        ///   3：向左移动
        ///   4：向右移动
        /// </summary>
        public int mobilemode { get; set; } = 0;

        /// <summary>
        /// 文字满屏停留的时间 0-255（秒）
        /// </summary>
        public int stoptime { get; set; } = 3;

        /// <summary>
        /// 即时显示的时间 0-255（秒）
        /// </summary>
        public int showtime { get; set; } = 5;

        /// <summary>
        /// 显示颜色（0：黑色 ，1: 绿色，2: 红色， 3: 黄色）
        /// </summary>
        public int textcolor { get; set; } = 0;

        /// <summary>
        /// 显示颜色（0：黑色 ，1: 绿色，2: 红色， 3: 黄色）
        /// </summary>
        public int numcolor { get; set; } = 2;

        /// <summary>
        /// 文本，限制显示80个汉字（GBK编码格式）
        /// </summary>
        public string showcontent { get; set; }

        /// <summary>
        /// 值：1~255 默认 15
        /// </summary>
        public int MoveSpeed { get; set; } = 15;
    }


}
