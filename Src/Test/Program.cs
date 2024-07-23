using Can;
using Can.Helper;

var com = new CanCOM("COM3");
var cmd = new CyberGearCanCmd(0xfd, 0x7f);

try
{
    var cmd1 = cmd.AT_Command_Mode();
    var mode = cmd.CmdSetRunMode(RunMode.运控模式);
    var speedlimt = cmd.CmdSetLimit_Spd(5);
    var curlimt = cmd.CmdSetLimit_Cur(5);
    var torlimt = cmd.CmdSetLimit_Torque(5);
    var enable = cmd.CmdSetEnable();
    var stop = cmd.CmdSetStop();


    var curkp = cmd.CmdSetCur_Kp();
    var curki = cmd.CmdSetCur_Ki();
    var curfilt = cmd.CmdSetCur_Filt_Gain();
    var lockp = cmd.CmdSetLoc_Kp();
    var spedkp = cmd.CmdSetSpd_Kp();
    var spedki = cmd.CmdSetSpd_Ki();


    var speed = cmd.CmdSetSpd_Ref(0);
    var angle = cmd.CmdSetLoc_Ref(5);
    var iq = cmd.CmdSetIq_Ref(-0.5f);

    var sa = cmd.CmdSetRun(0, 0, 0);

    await com.WriteAsync(cmd1);
    await com.WriteAsync(mode);
    await com.WriteAsync(speedlimt);
    await com.WriteAsync(curlimt);
    await com.WriteAsync(torlimt);

    await com.WriteAsync(curkp);
    await com.WriteAsync(curki);
    await com.WriteAsync(curfilt);
    await com.WriteAsync(lockp);
    await com.WriteAsync(spedkp);
    await com.WriteAsync(spedki);

    await com.WriteAsync(enable);
    //ushort i = 32767;
    //while (true)
    //{
    //    await com.WriteAsync(sa);
    //    await Task.Delay(10);
    //    sa = cmd.CmdSetRun(i, i, i);
    //    i+=100;
    //    if (i >= ushort.MaxValue)
    //        break;
    //}
    await com.WriteAsync(sa);
    await Task.Delay(3000);
    await com.WriteAsync(stop);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}