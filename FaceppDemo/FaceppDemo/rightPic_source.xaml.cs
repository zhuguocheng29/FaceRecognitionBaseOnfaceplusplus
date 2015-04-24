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
using System.Windows.Shapes;
using System.IO;

namespace FaceppDemo
{
    /// <summary>
    /// rightPic_source.xaml 的交互逻辑
    /// </summary>
    public partial class rightPic_source : Window
    {
        string text_name = "";

        public rightPic_source()
        {
            InitializeComponent();
        }

        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(text_content.Text))
            {
                text_name = text_content.Text;
            }

            this.Close();
        }
    }
}
