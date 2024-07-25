using COM.CAN.COMHelper;

try
{
    var str = await COMHelper.GetPortName();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}