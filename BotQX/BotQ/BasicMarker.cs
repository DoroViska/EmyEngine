using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine;
using EmyEngine.Game;
using EmyEngine.OpenGL;
using Jitter.LinearMath;

namespace BotQX.BotQ
{
    public struct BasicMarker 
    {
        private JVector _position;
        private IDraweble _model;

        public BasicMarker(JVector _position, IDraweble _model)
        {
            this._position = _position;
            this._model = _model;
        }

        public  void Draw()
        {
            G.PushMatrix();
            G.SetTransform(_position,JMatrix.Identity);
            _model.Draw();
            G.PopMatrix();
        }
    }
}
