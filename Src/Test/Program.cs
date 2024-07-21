using Can;
using Can.Helper;

var com = new CanCOM("COM4");
var cmd = new CyberGearCanCmd(0xfd, 0x7f);

try
{
    var cmd1 = cmd.AT_Command_Mode();
    var res1 = await com.WriteAsync(cmd1);
    Console.WriteLine($"Send:{Convert.ToHexString(cmd1)}");
    Console.WriteLine($"Rev:{res1}");


    var cmd2 = cmd.CmdSetRunMode(RunMode.运控模式);
    var res2 = await com.WriteAsync(cmd2);
    Console.WriteLine($"Send:{Convert.ToHexString(cmd2)}");
    Console.WriteLine($"Rev:{res2}");


    var cmd3 = cmd.CmdSetEnable();
    var res3 = await com.WriteAsync(cmd3);
    Console.WriteLine($"Send:{Convert.ToHexString(cmd3)}");
    Console.WriteLine($"Rev:{res3}");

    var cmd11 = cmd.CmdSetRun(0, 0);
    await Task.Delay(10);
    var res4 = await com.WriteAsync(cmd11);
    Console.WriteLine($"Send:{Convert.ToHexString(cmd11)}");
    Console.WriteLine($"Rev:{res4}");
    await Task.Delay(10);

    var cmd10 = cmd.CmdSetStop();
    var res10 = await com.WriteAsync(cmd10);
    Console.WriteLine($"Send:{Convert.ToHexString(cmd10)}");
    Console.WriteLine($"Rev:{res10}");
}
catch (Exception)
{
}