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
//using System.Drawing;
using System.Windows.Threading;

namespace whiteboard {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        DispatcherTimer timetrick;
        //StackPanel mainpanel;
        
        DrawingVisual drawingvisual;
        DrawingContext drawingcontext;
        RenderTargetBitmap bmp;
        Object buff;
        Point lastpoint;
        bool sizechangeenable;
        bool lastpointenable;
        int count;
        public MainWindow() {

            InitializeComponent();
        }

        private void Window_TouchMove(object sender, TouchEventArgs e) {
            //bmp=new RenderTargetBitmap((int)this.maingrid.Width+1,(int)this.maingrid.Height+1,96,96,PixelFormats.Pbgra32);
            //bmp.Render(this.maingrid);
            TouchPoint a = e.GetTouchPoint(maingrid);
            testLabel.Content = "X:" + a.Position.X.ToString() + "  Y:" + a.Position.Y.ToString() + '\n';



            //drawingcontext.DrawImage(bmp,new Rect(0,0,image.Width,image.Height));
            if (lastpointenable) {
                drawingcontext.DrawLine(new Pen(Brushes.Black, 2), lastpoint, a.Position);
                lastpoint = a.Position;
                lastpointenable = true;
            } else {
                drawingcontext.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, 0.08), a.Position, 1, 1);
                lastpoint = a.Position;
                lastpointenable = true;
            }


            //drawingcontext.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, 2), a.Position, 1, 1);
            //drawingcontext.DrawRectangle(Brushes.Blue, new Pen(Brushes.White, 1), new Rect(0, 0, 2, 2));
            sizechangeenable = true;
            if (count % 5 == 0) {
                writecontext();
                count++;
            } else {
                count++;
                timetrick.Start();
            }
            if (count == 400) {
                writebuffer();
            }
            //
            //image.Source = bmp;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e) {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            clearboard();
            timetrick = new System.Windows.Threading.DispatcherTimer();
            timetrick.Tick += timetrick_Tick;
            timetrick.Interval = new TimeSpan(0, 0, 0, 0, 300);
            lastpointenable = false;
            sizechangeenable = false;
        }

        void timetrick_Tick(object sender, EventArgs e) {
            writecontext();
        }

        private void Window_TouchUp(object sender, TouchEventArgs e) {
            writecontext();
            writebuffer();
            lastpointenable = false;
            timetrick.Stop();
            //testLabel2.Content = "true";
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            timetrick.Stop();
            clearboard();
        }


        private void Button_TouchDown(object sender, TouchEventArgs e) {
            timetrick.Stop();
            clearboard();
        }

        private void clearboard() {
            count = 0;
            drawingvisual = new DrawingVisual();
            drawingcontext = drawingvisual.RenderOpen();
            drawingcontext.DrawRectangle(Brushes.White, new Pen(Brushes.White, 0), new Rect(0, 0, maingrid.ActualWidth, maingrid.ActualHeight));
            drawingcontext.Close();
            buff = drawingvisual.Drawing;
            maingrid.Background = new DrawingBrush((Drawing)buff);
            drawingcontext = drawingvisual.RenderOpen();
            drawingcontext.DrawDrawing((Drawing)buff);
        }

        private void writebuffer() {
            bmp = new RenderTargetBitmap((int)maingrid.ActualWidth + 1, (int)maingrid.ActualHeight + 1, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingvisual);
            //testLabel2.Content = bmp.Width.ToString() + ' ' + bmp.PixelWidth.ToString() + ' ';
            drawingcontext = drawingvisual.RenderOpen();
            //ScaleTransform st = new ScaleTransform(0.32,0.32);

            // TransformedBitmap tbmp = new TransformedBitmap(bmp, st);

            //ScaleTransform st = new ScaleTransform(300.0/96.0, 300.0/96.0);
            //st.Freeze();
            Rect rect = new Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight);
            //drawingcontext.DrawRectangle(Brushes.White, new Pen(Brushes.White, 0), new Rect(0, 0, maingrid.ActualWidth, maingrid.ActualHeight));
            //drawingcontext.PushTransform(st);
            drawingcontext.DrawImage(bmp, rect);
            //testLabel2.Content ="";

            //st.ScaleX = 300.0 / 96.0;
            //st.ScaleY = 300.0 / 96.0;

            //;

            count = 0;
        }

        private void writecontext() {
            drawingcontext.Close();
            buff = drawingvisual.Drawing;
            maingrid.Background = new DrawingBrush((Drawing)buff);
            drawingcontext = drawingvisual.RenderOpen();
            drawingcontext.DrawDrawing((Drawing)buff);
            timetrick.Stop();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            if (sizechangeenable) {
                writecontext();
                writebuffer();
            }


        }

        private void Window_TouchLeave(object sender, TouchEventArgs e) {
            writecontext();
            writebuffer();
            lastpointenable = false;
            timetrick.Stop();
            
        }
    }
}
