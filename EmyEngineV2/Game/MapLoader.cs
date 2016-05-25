using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jitter.LinearMath;
using System.Xml.Serialization;
namespace EmyEngine.Game
{

    public class world_object
    {
        [XmlAttribute]
        public int id;
        public float hp;
        public float hitin;
        public JMatrix orientation;
        public JVector location;
        [XmlAttribute]
        public string parm0;
        [XmlAttribute]
        public string parm1;
        [XmlAttribute]
        public string parm2;
    }


    public static class MapLoader
    {
        private static bool _is_setup = _setup();
        private static bool _setup()
        {
            _typeList = new List<Type>();

            AddType(typeof(BoxObject));
            AddType(typeof(PlatformObject));
            AddType(typeof(MiniPlatformObject));
            AddType(typeof(WoodPlank));
            AddType(typeof(WoodDoor));
            AddType(typeof(CarObject));
     
            EE.Log("MapLoader::_setup OK");
            return true;
        }
    
   
        private static List<Type> _typeList;

        public static Type Index(int idx)
        {
            return _typeList[idx];
        }

        public static void AddType(Type tp)
        {
            if(!tp.IsClass)
                throw new ArgumentException("Type not class");
            if (!tp.BaseType.Equals(typeof (GameObject)))
                throw new AddTypeNotSupporedException();
            _typeList.Add(tp);
        }

        private static int _getTypeId(Type t)
        {
            int id = 0;
            foreach (Type objt in _typeList)
            {
                if (objt.Equals(t))
                    return id;
                id++;
            }
            throw new AddTypeNotSupporedException();
        }

        public static void Save(Stream map, GameInstance dot_product)
        {
            List<world_object> rez = new List<world_object>();
            for (int i = 0;i< dot_product.Length;i++)
            {
                GameObject obj = dot_product[i];
                if (_isHaveAttr(obj.GetType()))
                    continue;  
                world_object newrest = new world_object();
                newrest.id = _getTypeId(obj.GetType());
                newrest.location = obj.Position;
                newrest.orientation = obj.Orientation;
                newrest.hp = obj.HP;
                rez.Add(newrest);


            }
            XmlSerializer serial_ata = new XmlSerializer(typeof(List<world_object>));
            serial_ata.Serialize(map, rez);
        }

        private static bool _isHaveAttr(Type t)
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);  // Reflection.
            foreach (System.Attribute attr in attrs)
            {
                if (attr is NotSafeble)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Load(Stream map,GameInstance dot_product)
        {
            dot_product.ClearObjects();
            XmlSerializer serial_ata = new XmlSerializer(typeof(List<world_object>));
            List<world_object> t = (List<world_object>)serial_ata.Deserialize(map);
            foreach (object vs in t)
            {
                if (vs is world_object)
                {      
                    Type obj_type = Index(((world_object)vs).id);
                    GameObject rez = (GameObject)Activator.CreateInstance(obj_type);
                    rez.Orientation = ((world_object) vs).orientation;
                    rez.Position = ((world_object) vs).location;
                    rez.HP = ((world_object) vs).hp;
                    dot_product.AddObject(rez);
                }
            }
        }








    }
}
