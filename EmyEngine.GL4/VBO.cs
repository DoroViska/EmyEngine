using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EmyEngine.GL4
{
    public sealed class VBO 
    {


        public int BufferObject { get; private set; } // Идентификатор VBO
        public BufferTarget Type { get; private set; } // Тип VBO

        public VBO(BufferTarget type = BufferTarget.ArrayBuffer)
        {
            Type = type;
            AcquireHandle();
        }

        // Создаёт новый VBO и сохраняет его идентификатор в свойство Handle
        private void AcquireHandle()
        {
            BufferObject = GL.GenBuffer();
        }

        // Делает данный VBO текущим
        public void Use()
        {
            GL.BindBuffer(Type, BufferObject);
        }

        // Заполняет VBO массивом data
        public void SetData<T>(T[] data) where T : struct
        {
            if (data.Length == 0)
                throw new ArgumentException("Массив должен содержать хотя бы один элемент", "data");

            Use();
            GL.BufferData(Type, (IntPtr)(data.Length * Marshal.SizeOf(typeof(T))), data, BufferUsageHint.StaticDraw);
        }

        public void SubData<T>(T[] data) where T : struct
        {
            if (data.Length == 0)
                throw new ArgumentException("Массив должен содержать хотя бы один элемент", "data");

            Use();
            GL.BufferSubData(Type, (IntPtr)0, (IntPtr)(data.Length * Marshal.SizeOf(typeof(T))), data);
        }



        // Освобождает занятые данным VBO ресурсы
        private void ReleaseHandle()
        {
            if (BufferObject == 0)
                return;

            GL.DeleteBuffer(BufferObject);

            BufferObject = 0;
        }



        ~VBO()
        {
            // При вызове финализатора контекст OpenGL может уже не существовать и попытка выполнить GL.DeleteBuffer приведёт к ошибке
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
                ReleaseHandle();
        }
    }
}
