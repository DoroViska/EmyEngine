using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    [Flags]
    public enum MapActivity : uint
    {
        None = 0x0001,
        Defuse = 0x0010,
        Ambient = 0x0100,
        Specular = 0x1000
    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Material
    {
        public static Material Defult 
        {
            get
            {
                Material m;
                m.Defuse = new Color4(1f, 1f, 1f, 1f);
                m.Ambient = new Color4(0.1f, 0.1f, 0.1f, 1.0f);
                m.Specular = new Color4(1f, 1f, 1f, 1f);
                m.DefuseMap = 0;
                m.AmbientMap = 0;
                m.SpecularMap = 0;
                //m.Enables = MapActivity.None;
                return m;
            }
        }
        public static Material Gui
        {
            get
            {
                Material m;
                m.Defuse = new Color4(1f, 1f, 1f, 1f);
                m.Ambient = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
                m.Specular = new Color4(1f, 1f, 1f, 1f);
                m.DefuseMap = 0;
                m.AmbientMap = 0;
                m.SpecularMap = 0;
                //m.Enables = MapActivity.None;
                return m;
            }
        }
        public Color4 Defuse;
        public Color4 Ambient;
        public Color4 Specular;

        public int DefuseMap;
        public int AmbientMap;
        public int SpecularMap;

        public MapActivity Enables
        {
            get
            {
                MapActivity r = MapActivity.None;
                if(DefuseMap > 0)
                    r = MapActivity.Defuse | r;
                if (AmbientMap > 0)
                    r = MapActivity.Ambient | r;
                if (SpecularMap > 0)
                    r = MapActivity.Specular | r;
                return r;
            }

        }

    }
}
