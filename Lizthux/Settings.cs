using System;
using System.Text;

// All of the settings can be changed from here

namespace Lizthux
{
    public class Settings
    {
        public static readonly string[] randomsel =
{
            "LIZTHUX OWNS YOU",
            "Violating skids",
            "Skidder ridder",
            "Dems loves head",
            "Dems is a professional skid terminator",
            "Hire dems to get rid of skids",
            "Skids cry when they hear his name",
            "Nigga rigged",
            "Dems fucked your mom",
            "Niggas jealous of dems",
            "Beware of dems",
            "Dems ridding skidtards",
            "Skidtard backwards is dratdiks",
            "Lizthux owns everything",
            "Dems on top ngl"
        };
        public static Random rand = new Random();
        public static int index = rand.Next(randomsel.Length);
        public static readonly string MOTD = $"{randomsel[index]}";
        public static readonly string Logo = @"
[rgb(127,22,255)]                                                            ▒▒▒[/]
[rgb(138,44,255)]           ▒▒▒                                      ▒▒▒    ▒▒█▒            ▒▒▒ ▒▒▒▒       ▒▒▒[/]
[rgb(149,66,255)]         ▒▒▒█▒         ▒▒▒▒ ▒▒▒▒▒     ▒▒▒▒▒▒▒▒▒▒▒▒▒ ▒█▒▒   ▒██▒  ▒▒▒     ▒▒▒█▒ ▒██▒▒     ▒▒█▒[/]
[rgb(160,88,255)]       ▒▒▒███▒        ▒▒██▒ ▒███▒▒▒▒▒ ▒███████████▒ ▒██▒   ▒█▒▒ ▒▒█▒     ▒██▒▒ ▒▒██▒▒    ▒██▒[/]
[rgb(171,110,255)]      ▒▒████▒▒       ▒▒██▒▒ ▒▒▒█████▒  ▒▒▒▒▒██▒▒▒▒▒ ▒▒█▒▒ ▒▒█▒  ▒██▒     ▒██▒   ▒▒██▒▒ ▒▒▒█▒▒[/]
[rgb(182,132,255)]      ▒▒██▒▒        ▒▒██▒▒    ▒▒▒▒██▒▒    ▒▒██▒      ▒██▒▒▒██▒  ▒██▒     ▒██▒    ▒▒██▒▒▒██▒▒[/]
[rgb(193,154,255)]      ▒██▒▒         ▒██▒▒   ▒▒▒▒████▒▒   ▒▒██▒       ▒▒██████▒  ▒▒██▒▒  ▒██▒      ▒▒█████▒▒[/]
[rgb(204,176,255)]     ▒▒██▒    ▒▒▒  ▒▒██▒   ▒▒████▒▒▒▒    ▒██▒▒       ▒██▒▒▒██▒▒  ▒▒███▒▒█▒▒      ▒▒████▒▒[/]
[rgb(215,198,255)]    ▒▒███▒▒▒▒▒▒█▒  ▒███▒   ▒█████████▒  ▒▒██▒        ▒██▒ ▒▒██▒   ▒▒▒███▒▒     ▒▒▒██▒▒██▒[/]
[rgb(226,220,255)]    ▒███████████▒  ▒██▒▒   ▒▒▒▒▒▒▒▒▒█▒  ▒██▒▒        ▒▒█▒  ▒▒█▒     ▒▒▒▒▒     ▒▒███▒ ▒███▒[/]
[rgb(237,242,255)]    ▒▒▒▒▒▒▒▒▒▒▒▒▒  ▒▒▒▒            ▒▒▒  ▒█▒▒         ▒██▒   ▒█▒              ▒▒██▒▒   ▒█▒▒[/]
[rgb(248,255,255)]                                        ▒▒▒          ▒█▒▒   ▒▒▒              ▒██▒     ▒██▒[/]
[rgb(255,255,255)]                                                     ▒▒▒                     ▒▒▒▒     ▒▒█▒[/]
[rgb(255,255,255)]                                                                                       ▒▒▒[/]";
        public static readonly int WinSize1 = 97;
        public static readonly int WinSize2 = 29;
        public static readonly int BufHeight = 500;
        public static readonly int BufWidth = WinSize1;
        public static readonly string WinName = $"[ LIZTHUX ] <|> [ {MOTD} ]";
    }
}