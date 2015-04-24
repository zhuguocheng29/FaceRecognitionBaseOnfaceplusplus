using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FaceppSDK;
using System.Windows.Forms;
using System.IO;

namespace FaceppDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] face_id1 = new string[1000];
        public string face_id2 = "0";
        string[] list_test_1 = new string[1000];
        string[] filename = new string[1000];
        string[] filename_2 = new string[1000];
        string test_picpath = "0";
        string debug_filepath = "0";
   
        public MainWindow()
        {
            InitializeComponent();
   
        }

        private double max(double x, double y)
        {
            return (x > y) ? x : y;
        }

        public string[] recursed(string path, string[] patterns)
        {
            string[] arrList = new string[0];  

            foreach (string str in patterns)
            {
                string[] list = Directory.GetFiles(path, str);         
                if (list != null)
                {
                    string[] temp = arrList;
                    arrList = new string[arrList.Length + list.Length];
                    Array.Copy(temp, 0, arrList, 0, temp.Length);
                    Array.Copy(list, 0, arrList, temp.Length, list.Length);
                }
      
            }

            return arrList;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //跳出对话框显示文件夹路径
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";      
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            else 
            {
                string foldePath = dialog.SelectedPath;
                list_test_1 = this.recursed(foldePath, new string[] { "*.jpg", "*.png", "*.bmp" });
                string filepath = "0";
               
                for (int i = 0; i < list_test_1.Length; i++)
                {
                    filepath = list_test_1[i];
                    FaceCompare facecompare = new FaceCompare(filepath);
                    face_id1[i] = facecompare.loadpic();
                    //Console.WriteLine("已取出" + i);
                    filename[i] = System.IO.Path.GetFileName(list_test_1[i]);
                    Console.WriteLine("filename" + filename[i]);
                }
                Console.WriteLine("图片已取出");

            }

    
        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //@"C:\Users\Think\Desktop\人脸识别科研参考资料\图像样本\身份证"
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            else
            {
                string foldePath = dialog.SelectedPath;
                string[] list_test = this.recursed(foldePath, new string[] { "*.jpg", "*.png", "*.bmp" });
                string filepath = "0";
 
                //压缩右图
                if (Directory.Exists(System.Environment.CurrentDirectory + "pic"))
                {
                    debug_filepath = System.Environment.CurrentDirectory + "pic";
                    Console.WriteLine("路径为："+debug_filepath);
                    for (int k = 0; k < list_test.Length; k++)
                    {
                        /*
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(list_test[k]);
                        bitmap.DecodePixelHeight = 252;
                        bitmap.DecodePixelWidth = 204;
                        bitmap.EndInit();
                        PngBitmapEncoder pngE = new PngBitmapEncoder();
                        pngE.Frames.Add(BitmapFrame.Create(bitmap.Clone()));
                        filename_2[k] = System.IO.Path.GetFileName(list_test[k]);
                        test_picpath = debug_filepath + "\\"+filename_2[k];
                        Console.WriteLine(test_picpath);
                        using (Stream stream = File.Create(test_picpath))
                        {
                            pngE.Save(stream);
                        }
                        */

                        //用bitmap方法
                        System.Drawing.Bitmap newPic = new System.Drawing.Bitmap(list_test[k], false);
                        System.Drawing.Bitmap pic = new System.Drawing.Bitmap(204, 252);
                        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(pic);
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(newPic, new System.Drawing.Rectangle(0, 0, 204, 252), new System.Drawing.Rectangle(0, 0, newPic.Width, newPic.Height), System.Drawing.GraphicsUnit.Pixel);
                        g.Dispose();
                        filename_2[k] = System.IO.Path.GetFileName(list_test[k]);
                        test_picpath = debug_filepath + "\\" + filename_2[k];
                        pic.Save(test_picpath, System.Drawing.Imaging.ImageFormat.Png);
            


                    }
                }
                else
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "pic");
                    debug_filepath = System.Environment.CurrentDirectory + "pic";
                    Console.WriteLine("路径为：" + debug_filepath);
                    for (int k = 0; k < list_test.Length; k++)
                    {
                        /*
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(list_test[k]);
                        bitmap.DecodePixelHeight = 252;
                        bitmap.DecodePixelWidth = 204;
                        bitmap.EndInit();
                        PngBitmapEncoder pngE = new PngBitmapEncoder();
                        pngE.Frames.Add(BitmapFrame.Create(bitmap.Clone()));
                        filename_2[k] = System.IO.Path.GetFileName(list_test[k]);
                        test_picpath = debug_filepath + "\\" + filename_2[k];
                        Console.WriteLine(test_picpath);
                        using (Stream stream = File.Create(test_picpath))
                        {
                            pngE.Save(stream);
                        }
                        */
                        //用bitmap方法
                        System.Drawing.Bitmap newPic = new System.Drawing.Bitmap(list_test[k], false);
                        System.Drawing.Bitmap pic = new System.Drawing.Bitmap(204, 252);
                        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(pic);
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(newPic, new System.Drawing.Rectangle(0, 0, 204, 252), new System.Drawing.Rectangle(0, 0, newPic.Width, newPic.Height), System.Drawing.GraphicsUnit.Pixel);
                        g.Dispose();
                        filename_2[k] = System.IO.Path.GetFileName(list_test[k]);
                        test_picpath = debug_filepath + "\\" + filename_2[k];
                        pic.Save(test_picpath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }


                string[] listPic = Directory.GetFiles(debug_filepath);  

                //设置正确相似度的平均数，
                //人脸比较
                int p = 0;
                double similar = 0;
                double avg_similar = 0;
                FaceCompare facecompare;
                for (int i = 0; i < list_test_1.Length; i++)
                {
                    //similar = 0;
                   
                    for (int j = 0; j < list_test.Length; j++)
                    {
                        filepath = listPic[j];
                        filename_2[j] = System.IO.Path.GetFileName(list_test[j]);
                   //     FaceCompare facecompare = new FaceCompare(filepath);
                        facecompare = new FaceCompare(filepath);
                        face_id2 = facecompare.loadpic();

                        facecompare.compare(face_id1[i], face_id2);

                     //   Console.WriteLine(facecompare.face_similar());
                       Console.WriteLine(filename[i] + "与" + filename_2[j] + "的相似度为 " + facecompare.face_similar());
                       Console.WriteLine("No：" + (i + 1) + "与No " + (j + 1) + "的相似度为 " + facecompare.face_similar());
                        /*
                        if (i != j )
                        {
                            similar += facecompare.face_similar();
                           
                            //Console.WriteLine("第"+(i+1)+"个的相似度为: " + facecompare.face_similar());
                        }
                        */

                    }
                    
                  //  avg_similar += (similar / 19);
                  //  Console.WriteLine("第" + (i + 1) + "个的平均差异度为: " + (similar / 19));
                }
                //Console.WriteLine("平均相似度：" + (avg_similar/20));
               
               // Console.WriteLine("平均相似度：" + (similar/20));
            }

           
        }
/*
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            FaceCompare facecompare = new FaceCompare();
            facecompare.compare(face_id1, face_id2);
            label3.Content = facecompare.face_similar();
        }
*/
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          //   list_test = this.recursed(@"C:\Users\Think\Desktop\人脸识别科研参考资料\图像样本\身份证", new string[] { "*.jpg", "*.png", "*.bmp" });

        }

    }
}

///test
/*
            string []face_file = new string[list_test.Length];
            for (int i = 0; i < list_test.Length; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine(list_test[i]);
                face_file[i] = list_test[i];
            }

      //      string filepath = @"D:\test2.jpg";
            string filepath = face_file[2];
*/

/*          BitmapImage bitmap = new BitmapImage();
           bitmap.BeginInit();
           bitmap.UriSource = new Uri(filepath);
           bitmap.DecodePixelHeight = (int)image1.Height;
           bitmap.DecodePixelWidth = (int)image1.Width;
           bitmap.EndInit();
           image1.Source = bitmap;
   */

/*
       private void button1_Click(object sender, RoutedEventArgs e)
       {
            
           OpenFileDialog openFileDialog = new OpenFileDialog();
           openFileDialog.Title = "选择文件";
           openFileDialog.Filter = "所有文件|*.*";
           openFileDialog.FileName = string.Empty;
           openFileDialog.FilterIndex = 1;
           openFileDialog.RestoreDirectory = true;
           openFileDialog.DefaultExt = "jpg";
           DialogResult result = openFileDialog.ShowDialog();
           if (result == System.Windows.Forms.DialogResult.Cancel)
           {
               return ;
           }

           string filepath = filepath_source12;
            
           Console.WriteLine(filepath_source12);
           res = fs.Detection_DetectImg(filepath);

           BitmapImage bitmap = new BitmapImage();
           bitmap.BeginInit();
           bitmap.UriSource = new Uri(filepath);
           bitmap.DecodePixelHeight = (int)image1.Height;
           bitmap.DecodePixelWidth = (int)image1.Width;
           bitmap.EndInit();
           image1.Source = bitmap;
           PngBitmapEncoder pngE = new PngBitmapEncoder();
           pngE.Frames.Add(BitmapFrame.Create(bitmap));
           using (Stream stream = File.Create(System.Environment.CurrentDirectory + "temp.jpg"))
           {
               pngE.Save(stream);
           }
           canvas1.Children.Clear();

           // res = fs.Detection_DetectImg(System.Environment.CurrentDirectory + "temp.jpg");

           for (int i = 0; i < res.face.Count; ++i)
           {
               RectangleGeometry rect = new RectangleGeometry();

               rect.Rect = new Rect(max(res.face[i].position.center.x * image1.Width / 100.0 - res.face[i].position.width * image1.Width / 200.0, 0),
                                    max(res.face[i].position.center.y * image1.Height / 100.0 - res.face[i].position.height * image1.Height / 200.0, 0),
                                    res.face[i].position.width * image1.Width / 100.0, res.face[i].position.height * image1.Height / 100.0);
               System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
               myPath.Stroke = Brushes.Red;
               myPath.StrokeThickness = 3;
               myPath.Data = rect;
               label1.Content = label1.Content + String.Format("({0:F2},{1:F2})", res.face[0].position.center.x, res.face[0].position.center.y);
               label2.Content = label2.Content + String.Format("({0:F2},{1:F2})", res.face[0].position.width, res.face[0].position.height);
               canvas1.Children.Add(myPath);
               //eye_left
               RectangleGeometry rect_1 = new RectangleGeometry();
               rect_1.Rect = new Rect(res.face[i].position.eye_left.x * image1.Width / 100.0 -(max(res.face[i].position.center.x * image1.Width / 100.0 - 
                                      res.face[i].position.eye_left.x * image1.Width / 100.0, 0))/2,
                                   //   res.face[i].position.eye_left.y * image1.Width / 100.0,
                                      res.face[i].position.eye_left.y * image1.Height / 100.0 -(max(res.face[i].position.center.y * image1.Height / 100.0 - 
                                      res.face[i].position.eye_left.y * image1.Height / 100.0, 0))/2,
                                      max(res.face[i].position.center.x * image1.Width / 100.0 - res.face[i].position.eye_left.x * image1.Width / 100.0, 0),
                                      max(res.face[i].position.center.y * image1.Height / 100.0 - res.face[i].position.eye_left.y * image1.Height / 100.0, 0));
               
               System.Windows.Shapes.Path myPath_1 = new System.Windows.Shapes.Path();
               myPath_1.Stroke = Brushes.Blue;
               myPath_1.StrokeThickness = 3;
               myPath_1.Data = rect_1;
               canvas1.Children.Add(myPath_1);
                
               //eye_right

               RectangleGeometry rect_2 = new RectangleGeometry();
               rect_2.Rect = new Rect(res.face[i].position.eye_right.x * image1.Width / 100.0 - (max( res.face[i].position.eye_right.x * image1.Width / 100.0-
                                      res.face[i].position.center.x * image1.Width / 100.0, 0)) / 2,
                                      res.face[i].position.eye_right.y * image1.Height / 100.0 - (max(res.face[i].position.center.y * image1.Height / 100.0 -
                                      res.face[i].position.eye_right.y * image1.Height / 100.0, 0)) / 2,
                                      max(res.face[i].position.eye_right.x * image1.Width / 100.0-res.face[i].position.center.x * image1.Width / 100.0, 0),
                                      max(res.face[i].position.center.y * image1.Height / 100.0 - res.face[i].position.eye_right.y * image1.Height / 100.0, 0));

               System.Windows.Shapes.Path myPath_2 = new System.Windows.Shapes.Path();
               myPath_2.Stroke = Brushes.Blue;
               myPath_2.StrokeThickness = 3;
               myPath_2.Data = rect_2;
               canvas1.Children.Add(myPath_2);

               //nose
               face_id1 = res.face[i].face_id;
            
           }
           

       }
       */


/*
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (res == null)
            {
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "jpg文件|*.jpg|png文件|*.png|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "jpg";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            String filepath = openFileDialog.FileName;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filepath);
            bitmap.DecodePixelHeight = (int)image2.Height;
            bitmap.DecodePixelWidth = (int)image2.Width;
            bitmap.EndInit();
            image2.Source = bitmap;
            PngBitmapEncoder pngE = new PngBitmapEncoder();
            pngE.Frames.Add(BitmapFrame.Create(bitmap));
            using (Stream stream = File.Create(System.Environment.CurrentDirectory + "temp.jpg"))
            {
                pngE.Save(stream);
            }

            canvas2.Children.Clear();

            DetectResult res_2 = fs.Detection_DetectImg(System.Environment.CurrentDirectory + "temp.jpg");

            #region 圈出一张人脸
            for (int i = 0; i < res_2.face.Count; ++i)
            {
                RectangleGeometry rect = new RectangleGeometry();

                rect.Rect = new Rect(max(res_2.face[i].position.center.x * image1.Width / 100.0 - res_2.face[i].position.width * image1.Width / 200.0, 0),
                                     max(res_2.face[i].position.center.y * image1.Height / 100.0 - res_2.face[i].position.height * image1.Height / 200.0, 0),
                                     res_2.face[i].position.width * image1.Width / 100.0, res_2.face[i].position.height * image1.Height / 100.0);
                System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
                myPath.Stroke = Brushes.Red;
                myPath.StrokeThickness = 3;
                myPath.Data = rect;

                canvas2.Children.Add(myPath);


                //eye_left
                RectangleGeometry rect_1 = new RectangleGeometry();
                rect_1.Rect = new Rect(res_2.face[i].position.eye_left.x * image1.Width / 100.0 - (max(res_2.face[i].position.center.x * image1.Width / 100.0 -
                                       res_2.face[i].position.eye_left.x * image1.Width / 100.0, 0)) / 2,
                    //   res.face[i].position.eye_left.y * image1.Width / 100.0,
                                       res_2.face[i].position.eye_left.y * image1.Height / 100.0 - (max(res_2.face[i].position.center.y * image1.Height / 100.0 -
                                       res_2.face[i].position.eye_left.y * image1.Height / 100.0, 0)) / 2,
                                       max(res_2.face[i].position.center.x * image1.Width / 100.0 - res_2.face[i].position.eye_left.x * image1.Width / 100.0, 0),
                                       max(res_2.face[i].position.center.y * image1.Height / 100.0 - res_2.face[i].position.eye_left.y * image1.Height / 100.0, 0));

                System.Windows.Shapes.Path myPath_1 = new System.Windows.Shapes.Path();
                myPath_1.Stroke = Brushes.Blue;
                myPath_1.StrokeThickness = 3;
                myPath_1.Data = rect_1;
                canvas2.Children.Add(myPath_1);

                //eye_right
                RectangleGeometry rect_2 = new RectangleGeometry();
                rect_2.Rect = new Rect(res_2.face[i].position.eye_right.x * image1.Width / 100.0 - (max(res_2.face[i].position.eye_right.x * image1.Width / 100.0 -
                                       res_2.face[i].position.center.x * image1.Width / 100.0, 0)) / 2,
                                       res_2.face[i].position.eye_right.y * image1.Height / 100.0 - (max(res_2.face[i].position.center.y * image1.Height / 100.0 -
                                       res_2.face[i].position.eye_right.y * image1.Height / 100.0, 0)) / 2,
                                       max(res_2.face[i].position.eye_right.x * image1.Width / 100.0 - res_2.face[i].position.center.x * image1.Width / 100.0, 0),
                                       max(res_2.face[i].position.center.y * image1.Height / 100.0 - res_2.face[i].position.eye_right.y * image1.Height / 100.0, 0));

                System.Windows.Shapes.Path myPath_2 = new System.Windows.Shapes.Path();
                myPath_2.Stroke = Brushes.Blue;
                myPath_2.StrokeThickness = 3;
                myPath_2.Data = rect_2;
                canvas2.Children.Add(myPath_2);

                face_id2 = res_2.face[i].face_id;

                Console.WriteLine("face id " + face_id1);
            }
            #endregion  
            
        }
        */