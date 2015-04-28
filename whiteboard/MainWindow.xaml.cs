using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using System.Drawing.Imaging;
//using System.Drawing.Graphics;
using System.Windows.Threading;

namespace whiteboard {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //DrawingVisual dv = new DrawingVisual();
            
        }

        private void inkcanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            
        }

        private void inkcanvas_PreviewTouchDown(object sender, TouchEventArgs e) {
            //tlabal1.Content = e.Timestamp.ToString();
            //tlabal1.Content = inkcanvas.Children.Count.ToString();
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (inkcanvas.EditingMode == InkCanvasEditingMode.Ink) {
                inkcanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                penbtn.Content = "书写";
                
            } else {
                inkcanvas.EditingMode = InkCanvasEditingMode.Ink;
                penbtn.Content = "擦除";
            }
        }

        private void colorbtn_Click(object sender, RoutedEventArgs e) {
            if (inkcanvas.DefaultDrawingAttributes.Color == Colors.Black) {
                inkcanvas.DefaultDrawingAttributes.Color = Colors.Blue;
                colorbtn.Content = "黑笔";
            } else {
                inkcanvas.DefaultDrawingAttributes.Color = Colors.Black;
                colorbtn.Content = "蓝笔";
            }
        }

        private void undobtn_Click(object sender, RoutedEventArgs e) {
            if (inkcanvas.Strokes.Count>0)
                inkcanvas.Strokes.RemoveAt(inkcanvas.Strokes.Count - 1);
        }

        private void clearbtn_Click(object sender, RoutedEventArgs e) {
            inkcanvas.Strokes.Clear();
        }



    }
}
