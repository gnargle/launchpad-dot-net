var logAction = delegate (string name)
{
    Console.WriteLine(name);
};

var lp = new LaunchpadNET.Interface(logAction, logAction, logAction);

try
{
    //retrieve all launchpads connected to the computer.
    var launchpadList = LaunchpadNET.Interface.getConnectedLaunchpads(logAction);

    if (launchpadList.Any())
    {
        //connect to the first launchpad found.
        lp.connect(launchpadList[0]);
        try
        {
            //prepare launchpaf for our input
            lp.SetMode(LaunchpadNET.LaunchpadMode.Programmer);
            //wipe any active LEDs.
            lp.clearAllLEDs();
            //Create a scrolling text of "Hello World" across the pad. If the launchpad is a MiniMk3, we can utilise full RGB to display the 
            //text in any colour.
            if (lp.IsLegacy)
            {
                lp.createTextScroll("Hello World", 25, true, 125);
            }
            else
            {
                lp.createTextScrollMiniMk3RGB("Hello World", 25, true, 0xFF, 0x00, 0xFB);
            }
            Console.WriteLine("Press any key to quit");
            Console.Read();
        }
        finally
        {
            //make sure we clean up after we're finished!
            lp.disconnect(launchpadList[0]);
        }
    }
    else
    {
        Console.WriteLine("No launchpads found!");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Failed to retrieve and connect to launchpads with error: " + ex.Message);
}