using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
#pragma warning disable


namespace EmyEngine
{

    // класс LIMB отвечает за логические единицы 3D объектов в загружаемой сцене
    class LIMB
    {

        // при инициализации мы должны указать количество вершин (vertex) и полигонов (face) которые описывают геометри под-объекта
        public LIMB(int a, int b)
        {
            if (temp[0] == 0)
                temp[0] = 1;

            // записываем количество вершин и полигонов
            VandF[0] = a;
            VandF[1] = b;

            // выделяем память
            memcompl();

        }

        public int Itog = 0; // флаг успешности

        // массивы для хранения данных (геометрии и текстурных координат)
        public float[,] vert;
        public int[,] face;
        public float[,] t_vert;
        public int[,] t_face;


        // номер материала (текстуры) данного под-объекта
        private int MaterialNom = -1;

        // временное хранение информации
        public int[] VandF = new int[4];
        private int[] temp = new int[2];

        // флаг , говорящий о том, что модель использует текстуру
        private bool ModelHasTexture = false;

        // функция для определения значения флага (о наличии текстуры)
        public bool NeedTexture()
        {
            // возвращаем значение флага
            return ModelHasTexture;
        }

        public void SetMaterialNom(int new_nom)
        {
            MaterialNom = new_nom;
            if (MaterialNom > -1)
                // отмечаем флаг о наличии текстуры
                ModelHasTexture = true;
        }

        // массивы для текстурных координат
        public void createTextureVertexMem(int a)
        {
            VandF[2] = a;
            t_vert = new float[3, VandF[2]];
        }

        // привязка значений текстурных координат к полигонам 
        public void createTextureFaceMem(int b)
        {
            VandF[3] = b;
            t_face = new int[3, VandF[3]];

        }

        // память для геометрии
        private void memcompl()
        {
            vert = new float[3, VandF[0]];
            face = new int[3, VandF[1]];
        }

        // номер текстуры
        public int GetTextureNom()
        {
            return MaterialNom;
        }



    };






    // класс, выполняющий загрузку 3D модели    
    public class Model3DS 
    {


        public static void Serialize()
        {


            //foreach (string d in args)
            //{
            //    Model3dm model = new Model3dm();

            //    BinaryFormatter wrmt = new BinaryFormatter();
            //    DirectoryInfo b = new DirectoryInfo(d);
            //    foreach (FileInfo ti in b.GetFiles())
            //    {
            //        if (ti.Extension.ToLower() == ".ase")
            //        {
            //            model.ASE = File.ReadAllText(ti.FullName, Encoding.ASCII);
            //        }
            //        if (ti.Extension.ToLower() == ".png")
            //        {
            //            ImagePixels pixels = new ImagePixels();

            //            Bitmap image = new Bitmap(ti.FullName);
            //            image.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
            //            System.Drawing.Imaging.BitmapData bitmapdata;
            //            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, image.Width, image.Height);
            //            bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //            byte* poinert = (byte*)bitmapdata.Scan0.ToPointer();
            //            int toSize = image.Width * image.Height * 4;
            //            pixels.RW = new byte[toSize];
            //            for (int bs = 0; bs < toSize; bs++)
            //            {
            //                pixels.RW[bs] = poinert[bs];
            //            }
            //            image.UnlockBits(bitmapdata);
            //            pixels.Name = ti.Name;
            //            pixels.W = image.Width;
            //            pixels.H = image.Height;
            //            pixels.OnePixel = 4;
            //            model.Textures.Add(pixels);


            //        }

            //    }
            //    Stream s = File.Create("./" + b.Name + ".3dm");
            //    wrmt.Serialize(s, model);
            //    s.Close();



            //}





        }


        public Model3DS()
        {

        }

      

        // загружен ли (флаг)
        //private bool isLoad = false;
        // счетчик по-объектов
        private int count_limbs;
        // переменная для зранения номера текстуры
        private int mat_nom = 0;

        // номер дисплейног осписка с данной моделью
        //private int thisList = 0;

        // данная переменная будет указывать на количество прочитанных символов в строке при чтении информации из файла
        private int GlobalStringFrom = 0;

        // массив под-объектов
        LIMB[] limbs = null;

        // массви для хранения текстур
        TextureObject[] text_objects = null;

  

        // загрузка модели
        public int ModelFromResources(string filename)
        {
            // модель может содержать до 256 под-объектов
            limbs = new LIMB[256];
            // счетчик скинут
            int limb_ = -1;



            Stream mem = File.OpenRead(filename);

            //Console.WriteLine();

            // начинаем чтение файла
            //StreamReader sw = new StreamReader(FileName.GetStream(Handle + "/" + Name));
            StreamReader sw = new StreamReader(mem, Encoding.ASCII, false);
            mem.Position = 0;
            // временные буферы
            string a_buff = "";
            string b_buff = "";
            string c_buff = "";

            // счетчики вершин и полигонов
            int ver = 0, fac = 0;

            // если строка успешно прочитана
            while ((a_buff = sw.ReadLine()) != null)
            {

                //Console.WriteLine("ХУй");
                // получаем первое слово
                b_buff = GetFirstWord(a_buff, 0);
                if (b_buff[0] == '*') // определеям, является ли первый символ звездочкой
                {
                    switch (b_buff) // если да, то проверяем какое управляющее слово содержится в первом прочитаном слове
                    {
                        case "*MATERIAL_COUNT": // счетчик материалов
                            {
                                // получаем первое слово от символа указанного в GlobalStringFrom
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat = System.Convert.ToInt32(c_buff);

                                // создаем объект для текстуры в памяти
                                text_objects = new TextureObject[mat];
                                continue;
                            }

                        case "*MATERIAL_REF": // номер текстуры
                            {
                                // записываем для текущего под-объекта номер текстуры
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat_ref = System.Convert.ToInt32(c_buff);

                                // устанавливаем номер материала, соответствующий данной модели.
                                limbs[limb_].SetMaterialNom(mat_ref);
                                continue;
                            }

                        case "*MATERIAL": // указание на материал
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                mat_nom = System.Convert.ToInt32(c_buff);
                                continue;
                            }

                        case "*GEOMOBJECT": // начинается описание геметрии под-объекта
                            {
                                limb_++; // записываем в счетчик под-объектов
                                continue;
                            }

                        case "*MESH_NUMVERTEX": // количесвто вершин в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                ver = System.Convert.ToInt32(c_buff);
                                continue;
                            }

                        case "*BITMAP": // имя текстуры
                            {
                                c_buff = ""; // обнуляем временный буффер

                                for (int ax = GlobalStringFrom + 2; ax < a_buff.Length - 1; ax++)
                                    c_buff += a_buff[ax]; // считываем имя текстуры

                                text_objects[mat_nom] = new TextureObject(); // новый объект для текстуры


                                //
                                text_objects[mat_nom].TextureName = c_buff; // загружаем текстуру

                                continue;
                            }

                        case "*MESH_NUMTVERTEX": // количество текстурных координат, данное слово говорит о наличии текстурных координат - следовательно мы должны выделить память для них
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                if (limbs[limb_] != null)
                                {
                                    limbs[limb_].createTextureVertexMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }

                        case "*MESH_NUMTVFACES":  // память для текстурных координат (faces)
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);

                                if (limbs[limb_] != null)
                                {
                                    // выделяем память для текстурныйх координат
                                    limbs[limb_].createTextureFaceMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }

                        case "*MESH_NUMFACES": // количество полиговов в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                fac = System.Convert.ToInt32(c_buff);

                                // если было объвляющее слово *GEOMOBJECT (гарантия выполнения условия limb_ > -1) и были указаны количство вершин
                                if (limb_ > -1 && ver > -1 && fac > -1)
                                {
                                    // создаем новый под-объект в памяти
                                    limbs[limb_] = new LIMB(ver, fac);
                                }
                                else
                                {
                                    // иначе завершаем неудачей
                                    return -1;
                                }
                                continue;
                            }

                        case "*MESH_VERTEX": // информация о вершине
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // полчучаем информацию о кооринатах и номере вершины
                                // (получаем все слова в строке)
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразовываем в целое цисло
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем информацию о вершине
                                limbs[limb_].vert[0, NomVertex] = (float)System.Convert.ToDouble(a2); // x
                                limbs[limb_].vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                limbs[limb_].vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;

                            }

                        case "*MESH_FACE": // информация о полигоне
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);
                                a5 = GetFirstWord(a_buff, GlobalStringFrom);
                                a6 = GetFirstWord(a_buff, GlobalStringFrom);
                                a7 = GetFirstWord(a_buff, GlobalStringFrom);

                                // получаем нмоер полигона из первого слова в строке, заменив последний символ ":" после номера на флаг окончания строки.
                                int NomFace = System.Convert.ToInt32(a1.Replace(':', '\0'));

                                // записываем номера вершин, которые нас интересуют
                                limbs[limb_].face[0, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].face[1, NomFace] = System.Convert.ToInt32(a5);
                                limbs[limb_].face[2, NomFace] = System.Convert.ToInt32(a7);

                                continue;

                            }

                        // текстурые координаты
                        case "*MESH_TVERT":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер вершины
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем значение вершины
                                limbs[limb_].t_vert[0, NomVertex] = (float)System.Convert.ToDouble(a2); // x
                                limbs[limb_].t_vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                limbs[limb_].t_vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;

                            }

                        // привязка текстурных координат к полигонам
                        case "*MESH_TFACE":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер полигона
                                int NomFace = System.Convert.ToInt32(a1);

                                // записываем номера вершин, которые опиывают полигон
                                limbs[limb_].t_face[0, NomFace] = System.Convert.ToInt32(a2);
                                limbs[limb_].t_face[1, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].t_face[2, NomFace] = System.Convert.ToInt32(a4);

                                continue;

                            }


                    }



                }



            }
            // пересохраняем количесвто полигонов
            count_limbs = limb_;

            // отрисовываем геометрию

            mem.Close();
            // загрузка завершена
            //isLoad = true;

            return 0;

        }
        // загрузка модели
        
        

        // функция отрисовки
        public FlatModel Draw()
        {
            FlatModel model = new FlatModel();
            model.Textures = new List<TextureObject>(this.text_objects);
            model.SubObjects = new List<ModelSubObject>();
            // сохраняем тек матрицу 
            //  glPushMatrix();
            /// glRotatef(-90f, 1f, 0f, 0f);

            // масштабирование по умолчанию
            // glScalef(0.05f, 0.05f, 0.05f);
            // проходим циклом по всем подобъектам 
            for (int l = 0; l <= count_limbs; l++)
            {

                ModelSubObject tobj = new ModelSubObject();
                tobj.Faces = new List<Face>();
                tobj.TFaces = new List<Face>();

                int nom_index = limbs[l].GetTextureNom();
                tobj.TextureId = limbs[l].GetTextureNom();

                if (nom_index > -1)
                    if (limbs[l].NeedTexture() && text_objects[nom_index] != null)
                    {
                        ///Gl.glEnable(Gl.GL_TEXTURE_2D); // включаем режим текстурирования 
                       //                               // ID текстуры в памяти 
                        uint nn = text_objects[limbs[l].GetTextureNom()].NativeHandle;
                        // активируем (привязываем) эту текстуру 
                        //Gl.glBindTexture(Gl.GL_TEXTURE_2D, nn);
                    }

               // Gl.glEnable(Gl.GL_NORMALIZE);

                // начинаем отрисовку полигонов 
                //Gl.glBegin(Gl.GL_TRIANGLES);

                // по всем полигонам 
                for (int i = 0; i < limbs[l].VandF[1]; i++)
                {

                    // временные переменные, чтобы код был более понятен 
                    float x1, x2, x3, y1, y2, y3, z1, z2, z3 = 0;

                    // вытаскиваем координаты треугольника (полигона) 
                    x1 = limbs[l].vert[0, limbs[l].face[0, i]];
                    x2 = limbs[l].vert[0, limbs[l].face[1, i]];
                    x3 = limbs[l].vert[0, limbs[l].face[2, i]];
                    y1 = limbs[l].vert[1, limbs[l].face[0, i]];
                    y2 = limbs[l].vert[1, limbs[l].face[1, i]];
                    y3 = limbs[l].vert[1, limbs[l].face[2, i]];
                    z1 = limbs[l].vert[2, limbs[l].face[0, i]];
                    z2 = limbs[l].vert[2, limbs[l].face[1, i]];
                    z3 = limbs[l].vert[2, limbs[l].face[2, i]];

                   
                    tobj.Faces.Add(Face.Vector(
                        Vector3D.Float(x1,y1,z1),
                        Vector3D.Float(x2, y2, z2),
                        Vector3D.Float(x3, y3, z3)
                        ));

                    // рассчитываем нормаль 
                    float n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                    float n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                    float n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                    // устанавливаем нормаль 
                 //   Gl.glNormal3f(n1, n2, n3);

                    // если установлена текстура 

                    // если установлена текстура
                    if (limbs[l].NeedTexture() && (limbs[l].t_vert != null) && (limbs[l].t_face != null))
                    {
                        tobj.TFaces.Add(Face.Vector(
                       Vector3D.Float(limbs[l].t_vert[0, limbs[l].t_face[0, i]], limbs[l].t_vert[1, limbs[l].t_face[0, i]], 0),
                       Vector3D.Float(limbs[l].t_vert[0, limbs[l].t_face[1, i]], limbs[l].t_vert[1, limbs[l].t_face[1, i]], 0),
                       Vector3D.Float(limbs[l].t_vert[0, limbs[l].t_face[2, i]], limbs[l].t_vert[1, limbs[l].t_face[2, i]], 0)
                       ));
                        // устанавливаем текстурные координаты для каждой вершины, ну и сами вершины 
                        // устанавливаем текстурные координаты для каждой вершины, ну и сами вершины
                        //       glTexCoord2f(, );

                        //       glVertex3f(x1, y1, z1);



                        //      glTexCoord2f(, );

                        //       glVertex3f(x2, y2, z2);



                        //      glTexCoord2f(, );

                        //        glVertex3f(x3, y3, z3);


                    }
                    else // иначе - отрисовка только вершин 
                    {
                    //    Gl.glVertex3f(x1, y1, z1);
                     //   Gl.glVertex3f(x2, y2, z2);
                     //   Gl.glVertex3f(x3, y3, z3);
                    }

                   

                }

                model.SubObjects.Add(tobj);
                // завершаем отрисовку 
                //Gl.glEnd();
                // Gl.glDisable(Gl.GL_NORMALIZE);

                // отключаем текстурирование 
                // Gl.glDisable(Gl.GL_TEXTURE_2D);


            }

            return model;
            // возвращаем сохраненную ранее матрицу 
            // Gl.glPopMatrix();
        }

        // функиц я получения первого слова строки
        private string GetFirstWord(string word, int from)
        {

            // from указывает на позицию, начиная с которой будет выполнятся чтение файла
            char a = word[from]; // первый символ
            string res_buff = ""; // временный буффер
            int L = word.Length; // длина слова

            if (word[from] == ' ' || word[from] == '\t') // если первый символ, с которого предстоит искать слово является пробелом или знаком табуляции
            {
                // необходимо вычисслить наличие секции проблеов или знаков табуляции и откинуть их
                int ax = 0;
                // проходим до конца слова
                for (ax = from; ax < L; ax++)
                {
                    a = word[ax];
                    if (a != ' ' && a != '\t') // если встречаем символ пробела или табуляции
                        break; // выходим из цикла. 
                    // таким образом мы откидываем все последовательности пробелов или знаков табуляции, с которых могла начинатся переданная строка
                }

                if (ax == L) // если вся представленная строка является набором пробелов или знаков табуляции - возвращаем res_buff
                    return res_buff;
                else
                    from = ax; // иначе сохраняем значение ax
            }
            int bx = 0;

            // теперь, когда пробелы и табуляция откинуты мы непосредственно вычисляем слово
            for (bx = from; bx < L; bx++)
            {
                // если встретили знак пробела или табуляции - завершаем чтение слова
                if (word[bx] == ' ' || word[bx] == '\t')
                    break;
                // записываем символ в бременный буффер, постепенно получая таким образом слово
                res_buff += word[bx];
            }

            // если дошли до конца строки
            if (bx == L)
                bx--; // убераем посл значение

            GlobalStringFrom = bx; // позиция в данной строке, для чтения следующего слова в данной строке

            return res_buff; // возвращаем слово
        }

        // функция отрисовки 3D модели
       
 

    }
}
