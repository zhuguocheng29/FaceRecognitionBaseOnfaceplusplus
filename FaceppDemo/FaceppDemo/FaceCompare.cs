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
using System.Windows.Forms;
using System.IO;
using FaceppSDK;

namespace FaceppDemo
{
    public class FaceCompare
    {
        public FaceService fs = new FaceService("d3983b745b2c4e721fbbf9cb9d201297", "Ox57NvNRnyebU7UHTGGYkIy8WtLgDPwl ");
        public DetectResult res = new DetectResult();
        public string face_id1 = "0";
        public string face_id2 = "0";
        public string filepath_source = null;
        public string gender_ = "0";
        public int age_ = 0;
        public string race_ = "0"; 

        public CompareResult comp = new CompareResult();

        public FaceCompare()
        {
        
        }
        public FaceCompare(string filepathfromout)
        {
            filepath_source = filepathfromout;
        }

        public string loadpic()
        {
            string filepath = filepath_source;
            res = fs.Detection_DetectImg(filepath,"oneface");
            for (int i = 0; i < res.face.Count; ++i)
            {
                face_id1 = res.face[i].face_id;
                //检测每个人的性别，年龄，种族
                gender_ = res.face[i].attribute.gender.value;
                age_ = res.face[i].attribute.age.value;
                race_ = res.face[i].attribute.race.value;
            }
            
            return face_id1;
            
        }

        public void compare(string face_id_left, string face_id_right)
        {
            face_id1 = face_id_left;
            face_id2 = face_id_right;

            if (face_id1 == "0" || face_id2 == "0")
            {
                Console.WriteLine("比较失败！");
            }
            else
            { 
                comp = fs.Recognition_Compare(face_id_left, face_id_right);
             //   Console.WriteLine("比较成功");
            }
           
        }

        public double similar_eye()
        {
            Console.WriteLine("眼睛的相似性" + comp.component_similarity.eye);
            return comp.component_similarity.eye;
            
        }

        public double similar_mouth()
        {
            Console.WriteLine("嘴巴的相似性" + comp.component_similarity.mouth);
            return comp.component_similarity.mouth;
        }

        public double similar_nose()
        {
            Console.WriteLine("鼻子的相似性" + comp.component_similarity.nose);
            return comp.component_similarity.nose;
        }

        public double similar_eyebow()
        {
            Console.WriteLine("眉毛的相似性" + comp.component_similarity.eyebrow);
            return comp.component_similarity.eyebrow;
        }

        public double face_similar()
        {
           // Console.WriteLine("两张脸的相似性" + comp.similarity);
            return comp.similarity;
        }

        public string gender_value()
        {
            Console.WriteLine("性别为:" + gender_);
            return gender_;
        }

        public int age_value()
        {
            Console.WriteLine("年龄为:" +　age_);
            return age_;
        }

        public string race_value()
        {
            Console.WriteLine("种族为: " +　race_);
            return race_;
        }
    }
}
