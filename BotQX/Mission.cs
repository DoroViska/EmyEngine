using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jitter.LinearMath;

namespace BotQX
{
    [Serializable]
    public class CodeSettings
    {
        public int MaxFunctions = Int32.MaxValue;
        public int MaxClasses = Int32.MaxValue;
        public int MaxJfor = Int32.MaxValue;
        public int MaxJwhile = Int32.MaxValue;
        public int MaxJif = Int32.MaxValue;
        public int MaxJMove = Int32.MaxValue;
        public int MaxJTurnRotate = Int32.MaxValue;
        public int MaxJX = Int32.MaxValue;
    }


    [Serializable]
    public class BotPageFromMission 
    {
        public int PlayerId = 1;
        public string ObjectName = "new bot";
        public bool IsVisable = true;
        public bool RootBot = true;
        public string OnLoadScript = "function Main()\n{\n}";
        public JVector StartPosition = JVector.Zero;
        public float Rotation = 0;
        public bool DefultPen = false;
        public int DefultSpeed = 50;
        public CodeSettings Settings = new CodeSettings();

    }
    [Serializable]
    public class Mission
    {
        public string Name = "new mission";
        public string Description = "it's new mission";
        public string DescriptionHelp = "it's new mission";
        public Byte Level = Byte.MaxValue;
        public float Ball = 1;
        public DateTime Date = DateTime.Now;
        public double TimeFromWork = 0;
        public List<BotPageFromMission> Bots = new List<BotPageFromMission>();
        public string Script = "function Main()\n{\n}";


    }
}
