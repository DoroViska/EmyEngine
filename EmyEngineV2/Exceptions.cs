using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EmyEngine
{
    public class BaseOpenGLException : Exception
    {
        public BaseOpenGLException() : base("Базовое исключение рендер ядра EmyEngine") { }
    }
  
    public class EmyEngineFindException : Exception
    {
        public EmyEngineFindException(string modelName) : base("Не удалось найти элемент: " + modelName) { }
    }

    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string ResourcePath) : base("Не удалось найти ресурс: " + ResourcePath) { }
    }

    public class GLInstanceNotCreated : Exception
    {
        public GLInstanceNotCreated() : base("OpenGL не инициализирован!") { }
    }

    public class AddTypeNotSupporedException : Exception
    {
        public AddTypeNotSupporedException()
        {
        }
    }

    public class PropetryNotInitializeException : Exception
    {
        public PropetryNotInitializeException(string name) : base("Ресурс не инициализирован: " + name) { }
    }
    public class ObjectBodyIsNullException : Exception
    {
        public ObjectBodyIsNullException() : base("Обьект содержит тело равное = NULL") { }
    }
    public class CurrentGraphicsIsNotDrawebleException : Exception
    {
        public CurrentGraphicsIsNotDrawebleException() : base("Установка пыталасть нарисовать, кога рисовать нелзя!") { }
    }


    public class __std_badOpCode_fromatException : Exception
    {
        public __std_badOpCode_fromatException() : base("[]: Метод прервал свою работу из-за отсутсвия бинарного кода")
        {

        }
    }
    //public class Exceptions
    //{
    //}
}
