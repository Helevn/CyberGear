namespace COM.CAN.CanHelper
{
    /// <summary>
    /// Can电机控制类
    /// </summary>
    /// <param name="computerId">主机ID</param>
    /// <param name="canId">电机ID</param>
    public class CyberGearCanCmd(byte computerId, byte canId)
    {
        public byte[] FH() => [0x41, 0x54];
        public byte[] FE() => [0x0D, 0x0A];
        public byte[] AT_Command_Mode() => FH().Merge([0x2b, 0x41, 0x54]).Merge(FE());

        public byte[] CmdSetEnable()
        {
            byte[] extend = [(byte)Communicate.电机使能, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            byte[] data = new byte[8];//不需要传数据，全部置为0

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }

        public byte[] CmdSetStop()
        {
            byte[] extend = [(byte)Communicate.电机停止运行, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            byte[] data = new byte[8];//不需要传数据，全部置为0

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }


        #region 参数设定
        /// <summary>
        /// 设定运动模式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public byte[] CmdSetRunMode(RunMode mode)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((uint)CyberGearParamsEnum.Run_Mode).ToBytes();
            var param = ((uint)mode).ToBytes();
            byte[] data = cmd.Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 电流模式 Iq 指令
        /// <para/>
        /// -23~23A
        /// </summary>
        /// <param name="electric"></param>
        /// <returns></returns>
        public byte[] CmdSetIq_Ref(float electric)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Iq_Ref).ToBytes();
            var param = electric.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 转速模式转速指令
        /// <para/>
        /// -30~30rad/s  
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public byte[] CmdSetSpd_Ref(float speed)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Spd_Ref).ToBytes();
            var param = speed.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 转矩限制
        /// <para/>
        /// -30~30rad/s  
        /// </summary>
        /// <param name="torque"></param>
        /// <returns></returns>
        public byte[] CmdSetLimit_Torque(float torque)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Limit_Torque).ToBytes();
            var param = torque.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 位置模式角度指令
        /// <para/>
        /// -4Π~4Πrad
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public byte[] CmdSetLoc_Ref(float angle)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Loc_Ref).ToBytes();
            var param = angle.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 位置模式速度限制
        /// <para/>
        /// 0-30rad/s
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public byte[] CmdSetLimit_Spd(float speed)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Limit_Spd).ToBytes();
            var param = speed.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 速度位置模式电流限制
        /// <para/>
        /// 0~23A
        /// </summary>
        /// <param name="electric">电流</param>
        /// <returns></returns>
        public byte[] CmdSetLimit_Cur(float electric)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Limit_Cur).ToBytes();
            var param = electric.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 电流的 Kp
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] CmdSetCur_Kp(float value = 0.125f)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Cur_Kp).ToBytes();
            var param = value.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 电流的 Ki
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public byte[] CmdSetCur_Ki(float value = 0.0158f)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Cur_Ki).ToBytes();
            var param = value.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 电流滤波系数filt_gain
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] CmdSetCur_Filt_Gain(float value = 0.1f)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Cur_Filt_Gain).ToBytes();
            var param = value.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 位置的 Kp
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] CmdSetLoc_Kp(float value = 30f)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Loc_Kp).ToBytes();
            var param = value.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 速度的 Kp
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] CmdSetSpd_Kp(float value = 1f)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Spd_Kp).ToBytes();
            var param = value.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// 速度的 Ki
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] CmdSetSpd_Ki(float value = 0.002f)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Spd_Ki).ToBytes();
            var param = value.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        #endregion

        /// <summary>
        /// 设定运控模式下；速度与角度值
        /// <para/>
        /// -4Π~4Πrad
        /// <para/>
        /// -30~30rad/s
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="speed">速度</param>
        /// <returns></returns>
        public byte[] CmdSetRun(ushort torque, ushort angle, ushort speed)
        {
            byte[] cmd = [(byte)Communicate.运控模式电机控制指令];
            byte[] extend = cmd.Merge(torque.ToBytes()).Merge([canId]);
            extend = extend.UsbToCan();

            var data = angle.ToBytes().Merge(speed.ToBytes()).Merge([0, 0, 0, 0]);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }

        /// <summary>
        /// Jog+
        /// </summary>
        /// <returns></returns>
        public byte[] CmdSetJogForward()
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            byte[] data = new byte[] { 0x05, 0x70, 0x00, 0x00, 0x07, 0x01, 0x95, 0x54 };
            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// Jog-
        /// </summary>
        /// <returns></returns>
        public byte[] CmdSetJogReverse()
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            byte[] data = [0x05, 0x70, 0x00, 0x00, 0x07, 0x01, 0x6a, 0xaa];

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        /// <summary>
        /// Jog_Stop
        /// </summary>
        /// <returns></returns>
        public byte[] CmdSetJogStop()
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            byte[] data = [0x05, 0x70, 0x00, 0x00, 0x07, 0x00, 0x7f, 0xff];
            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
    }
}
