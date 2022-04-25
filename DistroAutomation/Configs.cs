using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistroAutomation
{
    public static class Configs
    {
        public static Dictionary<string, string> topFive = new Dictionary<string, string>()
        {
            {"1", "Ubuntu" },
            {"2", "Mandriva" },
            {"3", "SUSE"},
            {"4", "Fedora" },
            {"5", "MEPIS" }
        };
        public static class ImgLink
        {
            public static string GreenImgLink = "images/other/aup.png";
            public static string RedImgLink = "images/other/adown.png";
            public static string levelImgLink = "images/other/alevel.png";
        }
    }
}
