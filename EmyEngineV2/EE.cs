using EmyEngine.ResourceManagment;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Jitter.LinearMath;
using EmyEngine.GUI;

namespace EmyEngine
{
    /// <summary>
    /// GameInstance
    /// </summary>
    public class EE 
    {
        public static string EnginePath {
            get { return Environment.CurrentDirectory; } 
        }

        public static event Action<string> PrintLog;
        public static void Log(string args)
        {
            if (PrintLog != null)
                PrintLog(args);
            else
                Console.WriteLine("[{0}]: {1}",DateTime.Now, args);
        }
        public static void Log(string args,params object[] sr)
        {
            if (PrintLog != null)
                PrintLog(string.Format(args,sr));
            else
                Console.WriteLine("[{0}]: {1}", DateTime.Now, string.Format(args,sr));
        }


        public static Vector3 Vector(JVector j)
        {
            return  new Vector3(j.X,j.Y,j.Z);
        }
        public static JVector Vector(Vector3 j)
        {
            return new JVector(j.X, j.Y, j.Z);
        }

        
        static ITaskTransleter _curentTransleter = null;
        public static ITaskTransleter CurentTransleter
        {
            get
            {

                if (_curentTransleter == null)
                    throw new PropetryNotInitializeException(nameof(СurrentFont));
                return _curentTransleter;
            }

            set
            {
                _curentTransleter = value;
            }
        }

        static Resources _сurrentGuiResources = null;
        public static Resources СurrentGuiResources
        {
            get
            {
                if (_сurrentGuiResources == null)
                    throw new PropetryNotInitializeException(nameof(СurrentGuiResources));
                return _сurrentGuiResources;
            }
            set
            {
                _сurrentGuiResources = value;
            }
        }



        static Resources _сurrentResources = null;
        public static Resources СurrentResources
        {
            get
            {
                if (_сurrentResources == null)
                    throw new PropetryNotInitializeException(nameof(СurrentResources));
                return _сurrentResources;
            }
            set
            {
                _сurrentResources = value;
            }
        }


        static Font _сurrentFont = null;
        public static Font СurrentFont
        {
            get
            {
                if (_сurrentFont == null)
                    throw new PropetryNotInitializeException(nameof(СurrentFont));
                return _сurrentFont;
            }
            set
            {
                _сurrentFont = value;
            }
        }

       
    }

}
