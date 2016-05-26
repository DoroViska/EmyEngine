using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public class ShaderNew : IShaderInstance
    {
        public ShaderNew()
        {
            Program = ShaderProgram.FromSrcDir("./shaders/new");

            ViewLoc = Program.GetUniformLocation("View");
            BiasLoc = Program.GetUniformLocation("Bias");
            ModelLoc = Program.GetUniformLocation("Model");
            ProjectionLoc = Program.GetUniformLocation("Projection");

            ColorDefuseLoc = Program.GetUniformLocation("ColorDefuse");
            ColorAmbientLoc = Program.GetUniformLocation("ColorAmbient");
            ColorSpecularLoc = Program.GetUniformLocation("ColorSpecular");
            DefuseMapLoc = Program.GetUniformLocation("DefuseMap");
            AmbientMapLoc = Program.GetUniformLocation("AmbientMap");
            SpecularMapLoc = Program.GetUniformLocation("SpecularMap");
            MapActivityLoc = Program.GetUniformLocation("MapActivity");
            ShadowMapLoc = Program.GetUniformLocation("ShadowMap");
        }

        public int ShadowMapObject { get; set; } = 0;
        public ShaderProgram Program { set; get; }

        public int ProjectionLoc { get; private set; } = 0;
        public int ViewLoc { get; private set; } = 0;
        public int BiasLoc { get; private set; } = 0;
        public int ModelLoc { get; private set; } = 0;

        public int ColorDefuseLoc { get; private set; } = 0;
        public int ColorAmbientLoc { get; private set; } = 0;
        public int ColorSpecularLoc { get; private set; } = 0;
        public int DefuseMapLoc { get; private set; } = 0;
        public int AmbientMapLoc { get; private set; } = 0;
        public int SpecularMapLoc { get; private set; } = 0;
        public int MapActivityLoc { get; private set; } = 0;

        public int ShadowMapLoc { get; private set; } = 0;
        public bool EnableMaterials { get; set; } = true;

        public void UpdateState(Material Material)
        {

         
            GL.ActiveTexture(TextureUnit.Texture3);
            GL.BindTexture(TextureTarget.Texture2D, ShadowMapObject);
            if (EnableMaterials)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, Material.DefuseMap);

                GL.ActiveTexture(TextureUnit.Texture1);
                GL.BindTexture(TextureTarget.Texture2D, Material.AmbientMap);

                GL.ActiveTexture(TextureUnit.Texture2);
                GL.BindTexture(TextureTarget.Texture2D, Material.SpecularMap);



                GL.Uniform4(ColorDefuseLoc, Material.Defuse);
                GL.Uniform4(ColorAmbientLoc, Material.Ambient);
                GL.Uniform4(ColorSpecularLoc, Material.Specular);



                GL.Uniform1(MapActivityLoc, (int)Material.Enables);
            }
            else
            {

                GL.Uniform4(ColorDefuseLoc, OpenTK.Graphics.Color4.White);
                GL.Uniform4(ColorAmbientLoc, OpenTK.Graphics.Color4.Black);
                GL.Uniform4(ColorSpecularLoc, OpenTK.Graphics.Color4.White);

                GL.Uniform1(MapActivityLoc, (int)0);

            }


            GL.Uniform1(DefuseMapLoc, 0);
            GL.Uniform1(AmbientMapLoc, 1);
            GL.Uniform1(SpecularMapLoc, 2);
            GL.Uniform1(ShadowMapLoc, 3);

            GL.UniformMatrix4(ModelLoc, false, ref G.Model);
            GL.UniformMatrix4(ViewLoc, false, ref G.View);
            GL.UniformMatrix4(ProjectionLoc, false, ref G.Projection);
            GL.UniformMatrix4(BiasLoc, false, ref G.Bias);
        }
    }
    public class GUIShader : IShaderInstance
    {
        public GUIShader()
        {
            Program = ShaderProgram.FromSrcDir("./shaders/gui");

            ViewLoc = Program.GetUniformLocation("View");      
            ModelLoc = Program.GetUniformLocation("Model");
            ProjectionLoc = Program.GetUniformLocation("Projection");

            ColorDefuseLoc = Program.GetUniformLocation("ColorDefuse");
            ColorAmbientLoc = Program.GetUniformLocation("ColorAmbient");
            ColorSpecularLoc = Program.GetUniformLocation("ColorSpecular");
            DefuseMapLoc = Program.GetUniformLocation("DefuseMap");
            AmbientMapLoc = Program.GetUniformLocation("AmbientMap");
            SpecularMapLoc = Program.GetUniformLocation("SpecularMap");
            MapActivityLoc = Program.GetUniformLocation("MapActivity");
            ClipMinLoc = Program.GetUniformLocation("ClipMin");
            ClipMaxLoc = Program.GetUniformLocation("ClipMax");


            
        }

        public int ShadowMapObject { get; set; } = 0;
        public ShaderProgram Program { set; get; }

        public int ProjectionLoc { get; private set; } = 0;
        public int ViewLoc { get; private set; } = 0;
        public int ModelLoc { get; private set; } = 0;
   

        public int ColorDefuseLoc { get; private set; } = 0;
        public int ColorAmbientLoc { get; private set; } = 0;
        public int ColorSpecularLoc { get; private set; } = 0;
        public int DefuseMapLoc { get; private set; } = 0;
        public int AmbientMapLoc { get; private set; } = 0;
        public int SpecularMapLoc { get; private set; } = 0;
        public int MapActivityLoc { get; private set; } = 0;
        public int ClipMinLoc { get; private set; } = 0;
        public int ClipMaxLoc { get; private set; } = 0;

        public void UpdateState(Material Material)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Material.DefuseMap);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, Material.AmbientMap);

            GL.ActiveTexture(TextureUnit.Texture2);
            GL.BindTexture(TextureTarget.Texture2D, Material.SpecularMap);

            GL.ActiveTexture(TextureUnit.Texture3);
            GL.BindTexture(TextureTarget.Texture2D, ShadowMapObject);

            GL.Uniform4(ColorDefuseLoc, Material.Defuse);
            GL.Uniform4(ColorAmbientLoc, Material.Ambient);
            GL.Uniform4(ColorSpecularLoc, Material.Specular);

            GL.Uniform2(ClipMinLoc, G.Clip.lt);
            GL.Uniform2(ClipMaxLoc, G.Clip.rb);

            GL.Uniform1(MapActivityLoc, (int)Material.Enables);

            GL.Uniform1(DefuseMapLoc, 0);
            GL.Uniform1(AmbientMapLoc, 1);
            GL.Uniform1(SpecularMapLoc, 2);
       

            GL.UniformMatrix4(ModelLoc, false, ref G.Model);
            GL.UniformMatrix4(ViewLoc, false, ref G.View);
            GL.UniformMatrix4(ProjectionLoc, false, ref G.Projection);
       
        }
    }

    public class ZShader : IShaderInstance
    {
        public ZShader()
        {
            Program = ShaderProgram.FromSrcDir( "./shaders/shadowshader");
            depthMVPLoc = Program.GetUniformLocation("depthMVP");
        }
        public int depthMVPLoc { get; private set; } = 0;

        public ShaderProgram Program {  get; set; }

        public void UpdateState(Material Material)
        {
            Matrix4 depthMVP = G.GetMatrixRezult();
            GL.UniformMatrix4(depthMVPLoc,false,ref depthMVP);
        }
    }






}
