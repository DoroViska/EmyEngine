using EmyEngine.GUI;
using EmyEngine.Platform;
using OpenTK;
using OpenTK.Graphics;

namespace EmyEngine.OpenGL
{
    public interface IGraphics
    {
        float LineWidth { set; get; }
        void DefuseMap(int color);
        void AmbientMap(int color);
        void SpecularMap(int color);
        void DefuseColor(Color4 color);
        void AmbientColor(Color4 color);
        void SpecularColor(Color4 color);
        void Material(Material mt);
        void Normal(Vector3 nrm);
        void DrawTexturedRectangle(Vector3 v, Vector2 tv, Vector3 v1, Vector2 tv1, Vector3 v2, Vector2 tv2, Vector3 v3, Vector2 tv3);
        void DrawRectangle(Vector3 v, Vector3 v1, Vector3 v2, Vector3 v3);
        void DrawTexturedTriangle(Vector3 v,Vector2 tv, Vector3 v1, Vector2 tv1,Vector3 v2, Vector2 tv2);
        void DrawTriangle(Vector3 v,Vector3 v1,Vector3 v2);
        void DrawLine(Vector3 v, Vector3 v1);
        void DrawPoint(Vector3 v);
        void DrawSolidRectangle(Vector3 v, Vector3 v1, Vector3 v2, Vector3 v3);
        void DrawSolidTriangle(Vector3 v, Vector3 v1, Vector3 v2);
        void DrawRectangle(Vector3 v, Vector3 v1);
        void DrawSolidRectangle(Vector3 v, Vector3 v1);
        void Move(Vector3 v);
        void Push();
        void Pop();
        void DrawText(string text, Point point, Color4 color, object currentFont, float fontSize);
    }
}