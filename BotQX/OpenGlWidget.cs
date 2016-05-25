using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Gtk;
using OpenTK.Graphics;
using OpenTK.Platform;

namespace BotQX
{
    [ToolboxItem(true)]
    public class OpenGlWidget : DrawingArea, IDisposable
    {
        public IGraphicsContext GraphicsContext;
        static int _graphicsContextCount;

        /// <summary>Use a single buffer versus a double buffer.</summary>
        [Browsable(true)]
        public bool SingleBuffer { get; set; }

        /// <summary>Color Buffer Bits-Per-Pixel</summary>
        public int ColorBpp { get; set; }

        /// <summary>Accumulation Buffer Bits-Per-Pixel</summary>
        public int AccumulatorBpp { get; set; }

        /// <summary>Depth Buffer Bits-Per-Pixel</summary>
        public int DepthBpp { get; set; }

        /// <summary>Stencil Buffer Bits-Per-Pixel</summary>
        public int StencilBpp { get; set; }

        /// <summary>Number of samples</summary>
        public int Samples { get; set; }

        /// <summary>Indicates if steropic renderering is enabled</summary>
        public bool Stereo { get; set; }

        private IWindowInfo windowInfo;

        /// <summary>The major version of OpenGL to use.</summary>
        public int GlVersionMajor { get; set; }

        /// <summary>The minor version of OpenGL to use.</summary>
        public int GlVersionMinor { get; set; }

        public GraphicsContextFlags GraphicsContextFlags
        {
            get { return _graphicsContextFlags; }
            set { _graphicsContextFlags = value; }
        }
        private GraphicsContextFlags _graphicsContextFlags;

        /// <summary>Constructs a new GLWidget.</summary>
        public OpenGlWidget() : this(GraphicsMode.Default) { }

        /// <summary>Constructs a new GLWidget using a given GraphicsMode</summary>
        public OpenGlWidget(GraphicsMode graphicsMode) : this(graphicsMode, 1, 0, GraphicsContextFlags.Default) { }

        /// <summary>Constructs a new GLWidget</summary>
        public OpenGlWidget(GraphicsMode graphicsMode, int glVersionMajor, int glVersionMinor, GraphicsContextFlags graphicsContextFlags)
        {
            this.DoubleBuffered = false;

            SingleBuffer = graphicsMode.Buffers == 1;
            ColorBpp = graphicsMode.ColorFormat.BitsPerPixel;
            AccumulatorBpp = graphicsMode.AccumulatorFormat.BitsPerPixel;
            DepthBpp = graphicsMode.Depth;
            StencilBpp = graphicsMode.Stencil;
            Samples = graphicsMode.Samples;
            Stereo = graphicsMode.Stereo;

            GlVersionMajor = glVersionMajor;
            GlVersionMinor = glVersionMinor;
            GraphicsContextFlags = graphicsContextFlags;

            GLib.Idle.Add(this.Load);
        }

        ~OpenGlWidget() { Dispose(false); }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
            base.Dispose();
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GraphicsContext.MakeCurrent(windowInfo);
                OnShuttingDown();
                if (OpenTK.Graphics.GraphicsContext.ShareContexts && (Interlocked.Decrement(ref _graphicsContextCount) == 0))
                {
                    OnGraphicsContextShuttingDown();
                    _sharedContextInitialized = false;
                }
                GraphicsContext.Dispose();
            }
        }
        public static event EventHandler GraphicsContextInitialized;
        static void OnGraphicsContextInitialized() { if (GraphicsContextInitialized != null) GraphicsContextInitialized(null, EventArgs.Empty); }
        public static event EventHandler GraphicsContextShuttingDown;
        static void OnGraphicsContextShuttingDown() { if (GraphicsContextShuttingDown != null) GraphicsContextShuttingDown(null, EventArgs.Empty); }
        public event EventHandler Initialized;
        protected virtual void OnInitialized() { if (Initialized != null) Initialized(this, EventArgs.Empty); }
        public event EventHandler RenderFrame;
        protected virtual void OnRenderFrame() { if (RenderFrame != null) RenderFrame(this, EventArgs.Empty); }
        public event EventHandler ShuttingDown;
        protected virtual void OnShuttingDown() { if (ShuttingDown != null) ShuttingDown(this, EventArgs.Empty); }


        static bool _sharedContextInitialized = false;





        private bool Load()
        {

            if (ColorBpp == 0)
            {
                ColorBpp = 32;

                if (DepthBpp == 0) DepthBpp = 16;
            }

            ColorFormat colorBufferColorFormat = new ColorFormat(ColorBpp);

            ColorFormat accumulationColorFormat = new ColorFormat(AccumulatorBpp);

            int buffers = 2;
            if (SingleBuffer) buffers--;

            GraphicsMode graphicsMode = new GraphicsMode(colorBufferColorFormat, DepthBpp, StencilBpp, Samples, accumulationColorFormat, buffers, Stereo);

            //IWindowInfo
            if (Environment.OSVersion.Platform.ToString().ToLower() == "win32nt")
            {
                IntPtr windowHandle = gdk_win32_drawable_get_handle(GdkWindow.Handle);
                windowInfo = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(windowHandle);
            }
            else if (Environment.OSVersion.Platform.ToString().ToLower() == "macosx")
            {
                IntPtr windowHandle = gdk_x11_drawable_get_xid(GdkWindow.Handle);
                bool ownHandle = true;
                bool isControl = true;
                windowInfo = OpenTK.Platform.Utilities.CreateMacOSCarbonWindowInfo(windowHandle, ownHandle, isControl);
            }
            else if (Environment.OSVersion.Platform.ToString().ToLower() == "unix")
            {
                
                IntPtr display = gdk_x11_display_get_xdisplay(Display.Handle);
                int screen = Screen.Number;
                IntPtr windowHandle = gdk_x11_drawable_get_xid(GdkWindow.Handle);
                IntPtr rootWindow = gdk_x11_drawable_get_xid(RootWindow.Handle);

                IntPtr visualInfo;
                if (graphicsMode.Index.HasValue)
                {
                    XVisualInfo info = new XVisualInfo();
                    info.VisualID = graphicsMode.Index.Value;
                    int dummy;
                    visualInfo = XGetVisualInfo(display, XVisualInfoMask.ID, ref info, out dummy);
                }
                else
                {
                    visualInfo = GetVisualInfo(display);
                }

                windowInfo = OpenTK.Platform.Utilities.CreateX11WindowInfo(display, screen, windowHandle, rootWindow, visualInfo);
                XFree(visualInfo);
            }
            else throw new PlatformNotSupportedException();

            // GraphicsContext
            GraphicsContext = new GraphicsContext(graphicsMode, windowInfo, GlVersionMajor, GlVersionMinor, _graphicsContextFlags);
            GraphicsContext.MakeCurrent(windowInfo);

            if (OpenTK.Graphics.GraphicsContext.ShareContexts)
            {
                Interlocked.Increment(ref _graphicsContextCount);

                if (!_sharedContextInitialized)
                {
                    _sharedContextInitialized = true;
                    ((IGraphicsContextInternal)GraphicsContext).LoadAll();
                    OnGraphicsContextInitialized();
                }
            }
            else
            {
                ((IGraphicsContextInternal)GraphicsContext).LoadAll();
                OnGraphicsContextInitialized();
            }

            OnInitialized();


            return false;

        }
        public int Width;
        public int Height;
        // Called when the widget needs to be (fully or partially) redrawn.
        protected override bool OnExposeEvent(Gdk.EventExpose eventExpose)
        {

            bool result = base.OnExposeEvent(eventExpose);
            if (GraphicsContext != null)
            {
                OnRenderFrame();
                eventExpose.Window.Display.Sync(); // Add Sync call to fix resize rendering problem (Jay L. T. Cornwall) - How does this affect VSync?
                GraphicsContext.SwapBuffers();
            }


            return result;
        }

        // Called on Resize
        protected override bool OnConfigureEvent(Gdk.EventConfigure evnt)
        {

            Width = evnt.Width;
            Height = evnt.Height;

            bool result = base.OnConfigureEvent(evnt);
            if (GraphicsContext != null) GraphicsContext.Update(windowInfo);
            return result;
        }

        [SuppressUnmanagedCodeSecurity, DllImport("libgdk-win32-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_win32_drawable_get_handle(IntPtr d);

        public enum XVisualClass : int
        {
            StaticGray = 0,
            GrayScale = 1,
            StaticColor = 2,
            PseudoColor = 3,
            TrueColor = 4,
            DirectColor = 5,
        }

        [StructLayout(LayoutKind.Sequential)]
        struct XVisualInfo
        {
            public IntPtr Visual;
            public IntPtr VisualID;
            public int Screen;
            public int Depth;
            public XVisualClass Class;
            public long RedMask;
            public long GreenMask;
            public long blueMask;
            public int ColormapSize;
            public int BitsPerRgb;

            public override string ToString()
            {
                return String.Format("id ({0}), screen ({1}), depth ({2}), class ({3})",
                    VisualID, Screen, Depth, Class);
            }
        }

        [Flags]
        internal enum XVisualInfoMask
        {
            No = 0x0,
            ID = 0x1,
            Screen = 0x2,
            Depth = 0x4,
            Class = 0x8,
            Red = 0x10,
            Green = 0x20,
            Blue = 0x40,
            ColormapSize = 0x80,
            BitsPerRGB = 0x100,
            All = 0x1FF,
        }

        [DllImport("libX11", EntryPoint = "XGetVisualInfo")]
        static extern IntPtr XGetVisualInfoInternal(IntPtr display, IntPtr vinfo_mask, ref XVisualInfo template, out int nitems);
        static IntPtr XGetVisualInfo(IntPtr display, XVisualInfoMask vinfo_mask, ref XVisualInfo template, out int nitems)
        {
            Console.WriteLine("XGetVisualInfo");
            return XGetVisualInfoInternal(display, (IntPtr)(int)vinfo_mask, ref template, out nitems);
        }

        const string linux_libx11_name = "libX11.so.6";

        [SuppressUnmanagedCodeSecurity, DllImport(linux_libx11_name)]
        static extern void XFree(IntPtr handle);

        const string linux_libgdk_x11_name = "libgdk-x11-2.0.so.0";

        /// <summary> Returns the X resource (window or pixmap) belonging to a GdkDrawable. </summary>
        /// <remarks> XID gdk_x11_drawable_get_xid(GdkDrawable *drawable); </remarks>
        /// <param name="gdkDisplay"> The GdkDrawable. </param>
        /// <returns> The ID of drawable's X resource. </returns>
        [SuppressUnmanagedCodeSecurity, DllImport(linux_libgdk_x11_name)]
        static extern IntPtr gdk_x11_drawable_get_xid(IntPtr gdkDisplay);

        /// <summary> Returns the X display of a GdkDisplay. </summary>
        /// <remarks> Display* gdk_x11_display_get_xdisplay(GdkDisplay *display); </remarks>
        /// <param name="gdkDisplay"> The GdkDrawable. </param>
        /// <returns> The X Display of the GdkDisplay. </returns>
        [SuppressUnmanagedCodeSecurity, DllImport(linux_libgdk_x11_name)]
        static extern IntPtr gdk_x11_display_get_xdisplay(IntPtr gdkDisplay);

        IntPtr GetVisualInfo(IntPtr display)
        {
            try
            {
                int[] attributes = AttributeList.ToArray();
                return glXChooseVisual(display, Screen.Number, attributes);
            }
            catch (DllNotFoundException e)
            {
                throw new DllNotFoundException("OpenGL dll not found!", e);
            }
            catch (EntryPointNotFoundException enf)
            {
                throw new EntryPointNotFoundException("Glx entry point not found!", enf);
            }
        }

        const int GLX_NONE = 0;
        const int GLX_USE_GL = 1;
        const int GLX_BUFFER_SIZE = 2;
        const int GLX_LEVEL = 3;
        const int GLX_RGBA = 4;
        const int GLX_DOUBLEBUFFER = 5;
        const int GLX_STEREO = 6;
        const int GLX_AUX_BUFFERS = 7;
        const int GLX_RED_SIZE = 8;
        const int GLX_GREEN_SIZE = 9;
        const int GLX_BLUE_SIZE = 10;
        const int GLX_ALPHA_SIZE = 11;
        const int GLX_DEPTH_SIZE = 12;
        const int GLX_STENCIL_SIZE = 13;
        const int GLX_ACCUM_RED_SIZE = 14;
        const int GLX_ACCUM_GREEN_SIZE = 15;
        const int GLX_ACCUM_BLUE_SIZE = 16;
        const int GLX_ACCUM_ALPHA_SIZE = 17;

        List<int> AttributeList
        {
            get
            {
                List<int> attributeList = new List<int>(24);

                attributeList.Add(GLX_RGBA);

                if (!SingleBuffer) attributeList.Add(GLX_DOUBLEBUFFER);

                if (Stereo) attributeList.Add(GLX_STEREO);

                attributeList.Add(GLX_RED_SIZE);
                attributeList.Add(ColorBpp / 4); // TODO support 16-bit

                attributeList.Add(GLX_GREEN_SIZE);
                attributeList.Add(ColorBpp / 4); // TODO support 16-bit

                attributeList.Add(GLX_BLUE_SIZE);
                attributeList.Add(ColorBpp / 4); // TODO support 16-bit

                attributeList.Add(GLX_ALPHA_SIZE);
                attributeList.Add(ColorBpp / 4); // TODO support 16-bit

                attributeList.Add(GLX_DEPTH_SIZE);
                attributeList.Add(DepthBpp);

                attributeList.Add(GLX_STENCIL_SIZE);
                attributeList.Add(StencilBpp);

                //attributeList.Add(GLX_AUX_BUFFERS);
                //attributeList.Add(Buffers);

                attributeList.Add(GLX_ACCUM_RED_SIZE);
                attributeList.Add(AccumulatorBpp / 4);// TODO support 16-bit

                attributeList.Add(GLX_ACCUM_GREEN_SIZE);
                attributeList.Add(AccumulatorBpp / 4);// TODO support 16-bit

                attributeList.Add(GLX_ACCUM_BLUE_SIZE);
                attributeList.Add(AccumulatorBpp / 4);// TODO support 16-bit

                attributeList.Add(GLX_ACCUM_ALPHA_SIZE);
                attributeList.Add(AccumulatorBpp / 4);// TODO support 16-bit

                attributeList.Add(GLX_NONE);

                return attributeList;
            }
        }

        const string linux_libgl_name = "libGL.so.1";

        [SuppressUnmanagedCodeSecurity, DllImport(linux_libgl_name)]
        static extern IntPtr glXChooseVisual(IntPtr display, int screen, int[] attr);
    }
}
