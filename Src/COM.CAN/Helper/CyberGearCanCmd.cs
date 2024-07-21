namespace Can.Helper
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
        public byte[] CmdSetLimit_Cur(float speed)
        {
            byte[] extend = [(byte)Communicate.单个参数写入, 0x00, computerId, canId];
            extend = extend.UsbToCan();

            var cmd = ((ushort)CyberGearParamsEnum.Limit_Cur).ToBytes();
            var param = speed.ToBytes();
            byte[] data = cmd.Merge([0x00, 0x00]).Merge(param);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }
        #endregion


        public byte[] CmdSetRun(ushort angle, ushort speed)
        {
            byte[] cmd = [(byte)Communicate.运控模式电机控制指令];
            byte[] extend = cmd.Merge(((ushort)0).ToBytes()).Merge([canId]);
            extend = extend.UsbToCan();

            var data = angle.ToBytes().Merge(speed.ToBytes()).Merge([0, 0, 0, 0]);

            var command = FH()
                .Merge(extend)
                .Merge([(byte)data.Length])
                .Merge(data)
                .Merge(FE());
            return command;
        }

    }
}
