using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using EmyEngine;
using EmyEngine.Game;
using EmyEngine.GUI;
using EmyEngine.OpenGL;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;

namespace SdkGame
{
    public class MainForm : GameUI
    {
        private GameApplication application;


        public Label SelectionName { set; get; } = null;

        public MainForm(GameApplication apseApplication)
        {
            this.application = apseApplication;





            {
                DragDropPanel voxap = new DragDropPanel();
                voxap.Position = new Point(10, 15 + 250);
                voxap.Width = 200;
                voxap.Height = 250;
                {
                    int libe_pack = 10;
                    {
                        SelectionName = new Label();
                        SelectionName.Text = "(NULL; -1)"; SelectionName.AutoSize();
                        SelectionName.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(SelectionName);
                    }
                    {
                        Button bt = new Button();
                        bt.Text = "Создать машину"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {

                            CarObject vs = new CarObject();
                            vs.Position = new JVector(0f, 4f, 0f);
                            application.InstanceFromGame.AddObject(vs);

                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }
                    {
                        Button bt = new Button();
                        bt.Text = "Создать куб"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {

                            BoxObject bbox1 = new BoxObject();
                            bbox1.Position = new JVector(2f, 6f, 0f);

                            application.InstanceFromGame.AddObject(bbox1);

                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }
                    {
                        Button bt = new Button();
                        bt.Text = "Создать планк"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {

                            WoodPlank bbox1 = new WoodPlank();
                            bbox1.Position = new JVector(2f, 6f, 0f);

                            application.InstanceFromGame.AddObject(bbox1);

                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }
                    {
                        Button bt = new Button();
                        bt.Text = "Создать дверь"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {
                            WoodDoor bbox1 = new WoodDoor();
                            bbox1.Position = new JVector(2f, 6f, 0f);

                            application.InstanceFromGame.AddObject(bbox1);
                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }
                }
                this.Items.Add(voxap);
            }


            {
                DragDropPanel voxap = new DragDropPanel();
                voxap.Position = new Point(10, 10);
                voxap.Width = 300;
                voxap.Height = 250;
                {
                    
                    int libe_pack = 10;
                    {
                        Button bt = new Button();
                        bt.Text = "Сохранить карту"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {
                            //if(File.Exists("./map.eemx"))
                            //        File.Delete("./map.eemx");
                            //using (Stream bl = File.OpenWrite("./map.eemx"))
                            //{
                            //    MapLoader.Save(bl, application.InstanceFromGame);
                            //}

                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }
                    {
                        Button bt = new Button();
                        bt.Text = "Загрузить карту"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {
                            //using (Stream bl = File.OpenRead("./map.eemx"))
                            //{
                            //    MapLoader.Load(bl, application.InstanceFromGame);
                            //}

                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }
                    {
                        Button bt = new Button();
                        bt.Text = "Перегрузить шейдер"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {
                            //global::EmyEngine.EE.СurrentResources.GetResource("/shaders/new/shader.vs").Data = File.ReadAllBytes("./bin/shaders/new/shader.vs");
                            //global::EmyEngine.EE.СurrentResources.GetResource("/shaders/new/shader.fs").Data = File.ReadAllBytes("./bin/shaders/new/shader.fs");
                            //application.Shader3DMain = new ShaderNew();
                            //((ShaderNew)application.Shader3DMain).ShadowMapObject = application.FraemBufferMain.DepthTextureObject;
                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }

                    {
                        CheckBox bt = new CheckBox();
                        bt.Text = "Дебуг дравв";
                        bt.ValueChanged += (sender, args) =>
                        {

                            if (bt.Checked)
                                application.InstanceFromGame.EnableDebugDraweble();
                            else
                                application.InstanceFromGame.DisableDebugDraweble();

                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }



                    {
                        CheckBox bt = new CheckBox();
                        bt.Text = "D* Shader Material";
                        bt.ValueChanged += (sender, args) =>
                        {

                            if (bt.Checked)
                                ((ShaderNew)application.Shader3DMain).EnableMaterials = false;
                            else
                                ((ShaderNew)application.Shader3DMain).EnableMaterials = true;

                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }


                    {
                        Button bt = new Button();
                        bt.Text = "Закрыть"; bt.AutoSize();
                        bt.Click += (sender, args) =>
                        {
                            GameClass.TestWindow.Close();
                        };
                        bt.Position = new Point(10, libe_pack); libe_pack += 30;
                        voxap.Items.Add(bt);
                    }
                }
                this.Items.Add(voxap);
            }




        }


        public override void Update(float TimeStep)
        {        
            base.Update(TimeStep);
            
            Widget w = Parent;
            this.Height = w.Height;
            this.Width = w.Width;
       
        }
    }
}
