using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotQX.BotQ;
using EmyEngine;
using EmyEngine.Game;
using EmyEngine.OpenGL;
using EmyEngine.ResourceManagment;
using GLib;
using Jitter.LinearMath;
using NiL.JS;
using NiL.JS.Core;
using NiL.JS.Core.Functions;

namespace BotQX
{
    public static class JSRuntime
    {

        public static void JSPutsAlgorirm(Context cntx, BotPage botHome)
        {

            //cntx.AttachModule(typeof(GameObject));
            //cntx.AttachModule(typeof(MetalBlock));
            //cntx.AttachModule(typeof(BotObject));
            //cntx.AttachModule(typeof(WoodBlock));
            //cntx.AttachModule(typeof(CarObject));
            //cntx.AttachModule(typeof(JVector));
            //cntx.AttachModule(typeof(JMatrix));
            //cntx.AttachModule(typeof(IDraweble));
            //cntx.AttachModule(typeof(GameInstance));
            //cntx.AttachModule(typeof(Resources));
            //cntx.DefineVariable("Resources").Assign(new ExternalFunction((thisBind, arguments) =>
            //{           
            //    return new JSObject(EE.СurrentResources);  // or null
            //}));

            //cntx.DefineVariable("Instance").Assign(new ExternalFunction((thisBind, arguments) =>
            //{
            //    return new JSObject(botHome.Bot.Instance);  // or null
            //}));




            cntx.DefineVariable("Rotate").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                //EE.CurentTransleter.PushTask((e) => { ((BotPage)e).Bot.Rotate(); }, (object)botHome);     
                botHome.Bot.Rotate();
                return JSObject.Undefined; // or null
            }));
            cntx.DefineVariable("Move").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                //EE.CurentTransleter.PushTask((e) => { ((BotPage)e).Bot.Move(); }, (object)botHome);
                botHome.Bot.Move();      
                return JSObject.Undefined; // or null
            }));
            cntx.DefineVariable("Sleep").Assign(new ExternalFunction((thisBind, arguments) =>
            {
               
                System.Threading.Thread.Sleep((int)(Convert.ToSingle(arguments[0].Value) * 1000));
                return JSObject.Undefined; // or null
            }));
            cntx.DefineVariable("Turn").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                botHome.Bot.Rotate();
                return JSObject.Undefined; // or null
            }));
            cntx.DefineVariable("Detect").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                return botHome.Bot.Detect(); ; // or null
            }));

            cntx.DefineVariable("Speed").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                botHome.Bot.BotSpeed = (Convert.ToInt32(arguments[0]));
                return JSObject.Undefined; // or null
            }));


            cntx.DefineVariable("Pen").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                botHome.Bot.BotPen = (Convert.ToBoolean(arguments[0]));
                return JSObject.Undefined; // or null
            }));
            

            cntx.DefineVariable("Print").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                Console.WriteLine(arguments[0].ToString());
                return JSObject.Undefined; // or null
            }));



            cntx.DefineVariable("Random").Assign(new ExternalFunction((thisBind, arguments) =>
            {
                if (arguments.Length == 0)
                {
                    Random rn = new Random();
                    return rn.Next(int.MinValue, int.MaxValue); // or null
                }
                else
                {
                    Random rn = new Random();
                    return rn.Next(Convert.ToInt32(arguments[0]), Convert.ToInt32(arguments[1])); // or null
                }

            }));


       


            //            function Main()
            //{

            //                while (!Detect())
            //                {
            //                    Move();
            //                }

            //            }



        }



    }
}
