﻿using System;
using System.Drawing;
using Eto.Forms;
using MonoMac.AppKit;
using MonoMac.CoreVideo;
using MonoMac.Foundation;
using MonoMac.OpenGL;
using OpenTK.Graphics;
using Size = Eto.Drawing.Size;

namespace Eto.Gl.Mac
{
    public class MacGLView5 : NSView {
        public delegate void GLEventHandler(MacGLView2 sender, NSEvent args);

        public delegate void DrawRectHandler(System.Drawing.RectangleF dirtyRect);

        public event GLEventHandler GLKeyDown = delegate { };

        public event EventHandler<KeyEventArgs> GlKeyUp = delegate { };

        public event GLEventHandler GLMouseDown = delegate { };

        public event GLEventHandler GLMouseUp = delegate { };
        public event GLEventHandler GLMouseDragged = delegate { };
        public event GLEventHandler GLMouseMoved = delegate { };
        public event GLEventHandler GLScrollWheel = delegate { };



        public override void DrawRect(RectangleF dirtyRect)
        {
            if( !IsInitialized )
                InitGL();

            this.DrawNow( this, EventArgs.Empty);
        }

        public Size GLSize { get; set; }
        public bool IsInitialized { get; private set; }
        public void MakeCurrent()
        {
            //this.openGLContext.MakeCurrentContext();
            this.openTK.MakeCurrent(this.windowInfo);
        }

        public void SwapBuffers()
        {
            //openglFLush
            //this.openGLContext.FlushBuffer();
            this.openTK.SwapBuffers();
        }

        public event EventHandler Initialized = delegate { };
        public event EventHandler Resize = delegate { };
        public event EventHandler ShuttingDown = delegate { };
        public event EventHandler DrawNow = delegate { };


        private NSOpenGLPixelFormat pixelFormat;

        private NSOpenGLContext openGLContext;

        private OpenTK.Graphics.GraphicsContext openTK = null;
        private OpenTK.Platform.IWindowInfo windowInfo = null;

        public static NSOpenGLContext GlobalSharedContext = null;

        private CVDisplayLink displayLink;

        public void InitGL()
        {
            pixelFormat = NSOpenGLView.DefaultPixelFormat;

            if( pixelFormat == null )
                Console.WriteLine("No OpenGL pixel format");

            // NSOpenGLView does not handle context sharing, so we draw to a custom NSView instead

            this.openGLContext = new NSOpenGLContext(pixelFormat, GlobalSharedContext);
            if( GlobalSharedContext == null )
                GlobalSharedContext = this.openGLContext;

            //Initialize OpenTK structures using OSX GL core handles.
            this.windowInfo = OpenTK.Platform.Utilities.CreateMacOSCarbonWindowInfo(this.Handle, false, true);

            var ctxHandle = new OpenTK.ContextHandle(this.openGLContext.CGLContext.Handle);


            this.openTK = new OpenTK.Graphics.GraphicsContext(ctxHandle, windowInfo);

            this.MakeCurrent();
            openTK.LoadAll();

            this.IsInitialized = true;

            this.Initialized(this, EventArgs.Empty);
        }

    }
}