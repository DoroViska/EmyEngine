using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

//Данный код собран из ашмётков размазанного по стенке OpenTK
namespace EmyEngine
{
    public class EventController
    {


        private readonly Stopwatch watch = new Stopwatch();
        private const double MaxFrequency = 500.0;
        private FrameEventArgs _updateArgs = new FrameEventArgs();
        private FrameEventArgs _renderArgs = new FrameEventArgs();     
        private double _updatePeriod;
        private double _renderPeriod;
        private double _targetUpdatePeriod;
        private double _targetRenderPeriod;
        private double _updateTime;
        private double _renderTime;
        private double _updateTimestamp;
        private double _renderTimestamp;
        private double _updateEpsilon;
        private bool _isRunningSlowly;


        public event EventHandler<FrameEventArgs> RenderFraems;
        public event EventHandler<FrameEventArgs> UpdateFraems;
        public event EventHandler<FrameEventArgs> LoadFraems;

        private bool _isLoaded;

        public EventController() : this(100,100) { }
        public EventController(float updatesFramesPerSecond,float renderFramesPerSecond)
        {
            if (updatesFramesPerSecond < 0.0 || updatesFramesPerSecond > 200.0)
                throw new ArgumentOutOfRangeException("updatesFramesPerSecond", (object)updatesFramesPerSecond, "Parameter should be inside the range [0.0, 200.0]");
            if (renderFramesPerSecond < 0.0 || renderFramesPerSecond > 200.0)
                throw new ArgumentOutOfRangeException("renderFramesPerSecond", (object)renderFramesPerSecond, "Parameter should be inside the range [0.0, 200.0]");
            if (updatesFramesPerSecond != 0.0)
                this.TargetUpdateFrequency = updatesFramesPerSecond;
            if (renderFramesPerSecond != 0.0)
                this.TargetRenderFrequency = renderFramesPerSecond;
            _isLoaded = false;
        }



        public void Update(object argv)
        {
            if(!_isLoaded)
            {
                _isLoaded = true;
                this.watch.Start();
                OnLoadFraems(null);
            }
            this.DispatchUpdateAndRenderFrame(argv,EventArgs.Empty);
        }

    

        public double TargetRenderPeriod
        {
            get
            {
                
                return this._targetRenderPeriod;
            }
            set
            {
               
                if (value <= 0.002)
                {
                    this._targetRenderPeriod = 0.0;
                }
                else
                {
                    if (value > 1.0)
                        return;
                    this._targetRenderPeriod = value;
                }
            }
        }



        public double TargetRenderFrequency
        {
            get
            {
                
                if (this.TargetRenderPeriod == 0.0)
                    return 0.0;
                return 1.0 / this.TargetRenderPeriod;
            }
            set
            {
               
                if (value < 1.0)
                {
                    this.TargetRenderPeriod = 0.0;
                }
                else
                {
                    if (value > 500.0)
                        return;
                    this.TargetRenderPeriod = 1.0 / value;
                }
            }
        }


        public double TargetUpdateFrequency
        {
            get
            {
               
                if (this.TargetUpdatePeriod == 0.0)
                    return 0.0;
                return 1.0 / this.TargetUpdatePeriod;
            }
            set
            {
                
                if (value < 1.0)
                {
                    this.TargetUpdatePeriod = 0.0;
                }
                else
                {
                    if (value > 500.0)
                        return;
                    this.TargetUpdatePeriod = 1.0 / value;
                }
            }
        }

        



        public double TargetUpdatePeriod
        {
            get
            {
               
                return this._targetUpdatePeriod;
            }
            set
            {
                
                if (value <= 0.002)
                {
                    this._targetUpdatePeriod = 0.0;
                }
                else
                {
                    if (value > 1.0)
                        return;
                    this._targetUpdatePeriod = value;
                }
            }
        }



        private double ClampElapsed(double elapsed)
        {
            return MathHelper.Clamp(elapsed, 0.0, 1.0);
        }

        private void DispatchUpdateAndRenderFrame(object sender, EventArgs e)
        {
            int num = 4;
            double totalSeconds = this.watch.Elapsed.TotalSeconds;
            double elapsed1 = this.ClampElapsed(totalSeconds - this._updateTimestamp);
            while (elapsed1 > 0.0 && elapsed1 + this._updateEpsilon >= this.TargetUpdatePeriod)
            {
                this.RaiseUpdateFrame(elapsed1, ref totalSeconds);
                this._updateEpsilon += elapsed1 - this.TargetUpdatePeriod;
                elapsed1 = this.ClampElapsed(totalSeconds - this._updateTimestamp);
                if (this.TargetUpdatePeriod > double.Epsilon)
                {
                    this._isRunningSlowly = this._updateEpsilon >= this.TargetUpdatePeriod;
                    if (this._isRunningSlowly && --num == 0)
                        break;
                }
                else
                    break;
            }
            double elapsed2 = this.ClampElapsed(totalSeconds - this._renderTimestamp);
            if (elapsed2 <= 0.0 || elapsed2 < this.TargetRenderPeriod)
                return;
            this.RaiseRenderFrame(elapsed2, ref totalSeconds);
        }
        private void RaiseUpdateFrame(double elapsed, ref double timestamp)
        {
          
            this.OnUpdateFrameInternal(this._updateArgs);
            this._updatePeriod = elapsed;
            this._updateTimestamp = timestamp;
            timestamp = this.watch.Elapsed.TotalSeconds;
            this._updateTime = timestamp - this._updateTimestamp;
        }
        private void RaiseRenderFrame(double elapsed, ref double timestamp)
        {
   
            this.OnRenderFrameInternal(this._renderArgs);
            this._renderPeriod = elapsed;
            this._renderTimestamp = timestamp;
            timestamp = this.watch.Elapsed.TotalSeconds;
            this._renderTime = timestamp - this._renderTimestamp;
        }



        private void OnUpdateFrameInternal(FrameEventArgs updateArgs)
        {
            UpdateFraems?.Invoke(this, updateArgs);
        }

        private void OnRenderFrameInternal(FrameEventArgs render_args)
        {
            RenderFraems?.Invoke(this, render_args);
        }

        private void OnLoadFraems(FrameEventArgs e)
        {
            LoadFraems?.Invoke(this, e);
        }
    }
}
