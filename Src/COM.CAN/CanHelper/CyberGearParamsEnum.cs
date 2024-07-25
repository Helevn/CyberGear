namespace COM.CAN.CanHelper
{
    /// <summary>
    /// 通讯类型
    /// </summary>
    public enum Communicate
    {
        获取设备Id = 0,
        运控模式电机控制指令 = 1,
        电机反馈数据 = 2,
        电机使能 = 3,
        电机停止运行 = 4,
        设置电机机械零位 = 6,
        设置电机CanId = 7,
        单个参数读取 = 17,
        单个参数写入 = 18,
        故障反馈帧率 = 21,
        波特率修改 = 22,
    }
    /// <summary>
    /// 运行模式
    /// </summary>
    public enum RunMode
    {
        运控模式 = 0,
        位置模式 = 1,
        速度模式 = 2,
        电流模式 = 3,
    }
    /// <summary>
    /// 可读写的参数列表
    /// </summary>
    public enum CyberGearParamsEnum
    {
        /// <summary>
        /// 0: 运控模式
        /// 1: 位置模式
        /// 2: 速度模式
        /// 3: 电流模式
        /// </summary>
        Run_Mode = 0x7005,
        /// <summary>
        /// 电流模式 Iq 指令
        /// </summary>
        Iq_Ref = 0x7006,
        /// <summary>
        /// 转速模式转速指令
        /// </summary>
        Spd_Ref = 0x700A,
        /// <summary>
        /// 转矩限制
        /// </summary>
        Limit_Torque = 0x700B,
        /// <summary>
        /// 电流的 Kp
        /// </summary>
        Cur_Kp = 0x7010,
        /// <summary>
        /// 电流的 Ki
        /// </summary>
        Cur_Ki = 0x7011,
        /// <summary>
        /// 电流滤波系数filt_gain
        /// </summary>
        Cur_Filt_Gain = 0x7014,
        /// <summary>
        /// 位置模式角度指令
        /// </summary>
        Loc_Ref = 0x7016,
        /// <summary>
        /// 位置模式速度限制
        /// </summary>
        Limit_Spd = 0x7017,
        /// <summary>
        /// 速度位置模式电流限制
        /// </summary>
        Limit_Cur = 0x7018,
        /// <summary>
        /// 负载端计圈机械角度
        /// </summary>
        MechPos = 0x7019,
        /// <summary>
        /// iq 滤波值
        /// </summary>
        Iqf = 0x701A,
        /// <summary>
        /// 负载端转速
        /// </summary>
        MechVel = 0x701B,
        /// <summary>
        /// 母线电压
        /// </summary>
        VBUS = 0x701C,
        /// <summary>
        /// 圈数
        /// </summary>
        Rotation = 0x701D,
        /// <summary>
        /// 位置的 Kp
        /// </summary>
        Loc_Kp = 0x701E,
        /// <summary>
        /// 速度的 Kp
        /// </summary>
        Spd_Kp = 0x701F,
        /// <summary>
        /// 速度的 Ki
        /// </summary>
        Spd_Ki = 0x7020,
    }
}
