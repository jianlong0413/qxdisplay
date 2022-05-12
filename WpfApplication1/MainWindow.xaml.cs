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
using System.IO.Ports;
using System.Windows.Threading;


namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public SerialPort serialPort_QXY = new SerialPort();
        public SerialPort serialPort_FXFS = new SerialPort();
        public String Rcv_QXY, Rcv_FXFS;
        public String Ask = "!W01Dt\r\n";
        public String T, T2, U, U2, P, P2, d, d2, d10, f, f2, f10, cy, HH, MM;
        public double CY, CY2;
        public string DateStr { get; set; }
        public string TimeStr { get; set; }
        Point YX = new Point(400, 220);
        int R = 200;
        int c = 0;

        public MainWindow()
        {
            InitializeComponent();
            Com_Init();//初始化
            ShowCurrentTime();
            DrawCircle();
            DrawTuLi();
        }

        public void Com_Init()//初始化
        {
            serialPort_QXY.PortName = "COM8";//串口号
            serialPort_QXY.BaudRate = 19200;//波特率
            serialPort_QXY.DataBits = 8;//数据位
            serialPort_QXY.StopBits = StopBits.One;//停止位
            serialPort_QXY.Parity = Parity.None;//校验位

            serialPort_FXFS.PortName = "COM3";//串口号
            serialPort_FXFS.BaudRate = 9600;//波特率
            serialPort_FXFS.DataBits = 8;//数据位
            serialPort_FXFS.StopBits = StopBits.One;//停止位
            serialPort_FXFS.Parity = Parity.None;//校验位
        }

        private void DrawTuLi()
        {
            int FontSize = 50;
            int HJJ = 20;

            TextBlock SS = new TextBlock();
            SS.FontSize = FontSize;
            SS.Text = "瞬时";
            Canvas.SetLeft(SS, 105);
            Canvas.SetTop(SS, HJJ);
            TuLi.Children.Add(SS);

            TextBlock EF = new TextBlock();
            EF.FontSize = FontSize;
            EF.Text = "2分平均";
            Canvas.SetLeft(EF, 100);
            Canvas.SetTop(EF, FontSize + 2 * HJJ);
            TuLi.Children.Add(EF);

            TextBlock SF = new TextBlock();
            SF.FontSize = FontSize;
            SF.Text = "10分平均";
            Canvas.SetLeft(SF, 100);
            Canvas.SetTop(SF, 2 * FontSize + 3 * HJJ);
            TuLi.Children.Add(SF);

            Line TLS = new Line();
            TLS.X1 = 0;
            TLS.Y1 = 0;
            TLS.X2 = 100;
            TLS.Y2 = 0;
            TLS.Stroke = Brushes.Red;
            TLS.StrokeThickness = 4;
            Canvas.SetLeft(TLS, -30);
            Canvas.SetTop(TLS, HJJ + 0.7 * FontSize);
            TuLi.Children.Add(TLS);

            Line TL2 = new Line();
            TL2.X1 = 0;
            TL2.Y1 = 0;
            TL2.X2 = 100;
            TL2.Y2 = 0;
            TL2.Stroke = Brushes.Orange;
            TL2.StrokeThickness = 7;
            Canvas.SetLeft(TL2, -30);
            Canvas.SetTop(TL2, 2 * HJJ + FontSize + 0.7 * FontSize);
            TuLi.Children.Add(TL2);

            Line TL10 = new Line();
            TL10.X1 = 0;
            TL10.Y1 = 0;
            TL10.X2 = 100;
            TL10.Y2 = 0;
            TL10.Stroke = Brushes.Violet;
            TL10.StrokeThickness = 10;
            Canvas.SetLeft(TL10, -30);
            Canvas.SetTop(TL10, 3 * HJJ + 2 * FontSize + 0.7 * FontSize);
            TuLi.Children.Add(TL10);
        }

        private void DrawCircle()
        {

            Ellipse OutCircle = new Ellipse();
            OutCircle.Stroke = Brushes.Black;
            OutCircle.StrokeThickness = 4;
            OutCircle.Width = 2 * R;
            OutCircle.Height = 2 * R;
            OutCircle.Fill = Brushes.White;
            Canvas.SetLeft(OutCircle, YX.X - R);
            Canvas.SetTop(OutCircle, YX.Y - R);
            Pan.Children.Add(OutCircle);

            TextBlock N = new TextBlock();
            N.FontSize = 40;
            N.Text = "N";
            Canvas.SetLeft(N, 380);
            Canvas.SetTop(N, -30);
            Pan.Children.Add(N);

            TextBlock S = new TextBlock();
            S.FontSize = 40;
            S.Text = "S";
            Canvas.SetLeft(S, 380);
            Canvas.SetTop(S, 415);
            Pan.Children.Add(S);

            TextBlock W = new TextBlock();
            W.FontSize = 40;
            W.Text = "W";
            Canvas.SetLeft(W, 155);
            Canvas.SetTop(W, 200);
            Pan.Children.Add(W);

            TextBlock E = new TextBlock();
            E.FontSize = 40;
            E.Text = "E";
            Canvas.SetLeft(E, 605);
            Canvas.SetTop(E, 200);
            Pan.Children.Add(E);

            TextBlock bs39 = new TextBlock();
            bs39.FontSize = 40;
            bs39.Text = "39";
            Canvas.SetLeft(bs39, 520);
            Canvas.SetTop(bs39, 20);
            Pan.Children.Add(bs39);

            TextBlock bs129 = new TextBlock();
            bs129.FontSize = 40;
            bs129.Text = "129";
            Canvas.SetLeft(bs129, 550);
            Canvas.SetTop(bs129, 340);
            Pan.Children.Add(bs129);

            TextBlock bs219 = new TextBlock();
            bs219.FontSize = 40;
            bs219.Text = "219";
            Canvas.SetLeft(bs219, 200);
            Canvas.SetTop(bs219, 370);
            Pan.Children.Add(bs219);

            TextBlock bs309 = new TextBlock();
            bs309.FontSize = 40;
            bs309.Text = "309";
            Canvas.SetLeft(bs309, 165);
            Canvas.SetTop(bs309, 60);
            Pan.Children.Add(bs309);

            Line PDF = new Line();
            PDF.X1 = (R - 4) * Math.Sin((Math.PI / 180) * 129);
            PDF.Y1 = -(R - 4) * Math.Cos((Math.PI / 180) * 129);
            PDF.X2 = -(R - 4) * Math.Sin((Math.PI / 180) * 129);
            PDF.Y2 = (R - 4) * Math.Cos((Math.PI / 180) * 129);
            PDF.Stroke = Brushes.YellowGreen;
            PDF.StrokeThickness = 10;
            Canvas.SetLeft(PDF, YX.X);
            Canvas.SetTop(PDF, YX.Y);
            Pan.Children.Add(PDF);

            Line PD = new Line();
            PD.X1 = (R - 4) * Math.Sin((Math.PI / 180) * 39);
            PD.Y1 = -(R - 4) * Math.Cos((Math.PI / 180) * 39);
            PD.X2 = -(R - 4) * Math.Sin((Math.PI / 180) * 39);
            PD.Y2 = (R - 4) * Math.Cos((Math.PI / 180) * 39);
            PD.Stroke = Brushes.Gray;
            PD.StrokeThickness = 30;
            Canvas.SetLeft(PD, YX.X);
            Canvas.SetTop(PD, YX.Y);
            Pan.Children.Add(PD);

        }

        public void GetCurrentTime(object sender, EventArgs e)
        {
            DateStr = DateTime.Now.ToString("yyyy-MM-dd");
            TimeStr = DateTime.Now.ToString("HH:mm:ss");//除直接送至文本，也可以写进日志等
        }

        public void ShowCurrentTime()
        {
            DispatcherTimer ShowTimer = new DispatcherTimer();
            ShowTimer.Tick += new EventHandler(GetCurrentTime);//起个ShowTimer一直获取当前时间 
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
        }

        
        public void calResult()
        {

            string[] sQ = Rcv_QXY.Split(',');
            HH = Rcv_QXY.Substring(17, 2);
            MM = Rcv_QXY.Substring(20, 2);
            T = sQ[2];
            U = sQ[3];
            P = sQ[4];
            T2 = sQ[14];
            U2 = sQ[15];
            P2 = sQ[16];
            CY = double.Parse(P) * 0.75;
            CY2 = double.Parse(P2) * 0.75;

        }

        public void calFResult()
        {

            string[] sF = Rcv_FXFS.Split(',');
            d = sF[1];
            f = sF[2];
            d2 = sF[3];
            f2 = sF[4];
            d10 = sF[6];
            f10 = sF[7];

        }

        public void updt()
        {
            tb_FS.Text = f + "m/s";
            tb_FS2.Text = f2 + "m/s";
            tb_CY.Text = CY.ToString("f1");
            tb_CY2.Text = CY2.ToString("f1");
            tb_P.Text = P;
            tb_P2.Text = P2;
            tb_T.Text = T + "℃";
            tb_T2.Text = T2 + "℃";
            tb_U.Text = U + "%";
            tb_U2.Text = U2 + "%";
            tb_TimeNow.Text = DateStr + " " + HH + ":" + MM;
        }

        public void updtF()
        {
            tb_FX.Text = d + "°";
            tb_FX2.Text = d2 + "°";
            tb_FX10.Text = d10 + "°";
            tb_FS.Text = f + "m/s";
            tb_FS2.Text = f2 + "m/s";
            tb_FS10.Text = f10 + "m/s";

            Line FXX = new Line();
            FXX.X1 = (R - 4) * Math.Sin((Math.PI / 180) * int.Parse(d));
            FXX.Y1 = -(R - 4) * Math.Cos((Math.PI / 180) * int.Parse(d));
            FXX.Stroke = Brushes.Red;
            FXX.StrokeThickness = 4;
            Canvas.SetLeft(FXX, YX.X);
            Canvas.SetTop(FXX, YX.Y);


            Line FXX2 = new Line();
            FXX2.X1 = (R - 20) * Math.Sin((Math.PI / 180) * int.Parse(d2));
            FXX2.Y1 = -(R - 20) * Math.Cos((Math.PI / 180) * int.Parse(d2));
            FXX2.Stroke = Brushes.Orange;
            FXX2.StrokeThickness = 7;
            Canvas.SetLeft(FXX2, YX.X);
            Canvas.SetTop(FXX2, YX.Y);

            Line FXX10 = new Line();
            FXX10.X1 = (R - 40) * Math.Sin((Math.PI / 180) * int.Parse(d10));
            FXX10.Y1 = -(R - 40) * Math.Cos((Math.PI / 180) * int.Parse(d10));
            FXX10.Stroke = Brushes.Violet;
            FXX10.StrokeThickness = 10;
            Canvas.SetLeft(FXX10, YX.X);
            Canvas.SetTop(FXX10, YX.Y);

            DrawCircle();

            Pan.Children.Add(FXX10);
            Pan.Children.Add(FXX2);
            Pan.Children.Add(FXX);
        }

        private void bt_SerialSwitch_Click(object sender, RoutedEventArgs e)//串口开关
        {

            string strContent = this.bt_SerialSwitch.Content.ToString();
            if (strContent == "打开串口")
            {
                serialPort_QXY.Open();
                serialPort_QXY.DataReceived += new SerialDataReceivedEventHandler(QXY_DataReceivedHandler);//添加数据接收事件
                serialPort_FXFS.Open();
                serialPort_FXFS.DataReceived += new SerialDataReceivedEventHandler(FXFS_DataReceivedHandler);//添加数据接收事件

                bt_SerialSwitch.Content = "关闭串口";
                e_status.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else
            {
                serialPort_QXY.DataReceived -= QXY_DataReceivedHandler;
                serialPort_QXY.Close();
                serialPort_QXY.DataReceived -= FXFS_DataReceivedHandler;
                serialPort_FXFS.Close();

                bt_SerialSwitch.Content = "打开串口";
                e_status.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            }
        }

        public void QXY_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)//读取下位机的数据，显示在textBlock中
        {

            try
            {

                App.Current.Dispatcher.Invoke((Action)delegate()
                {
                    Rcv_QXY = serialPort_QXY.ReadLine();
                    c++;
                    if (c < 5) { }
                    else
                    {
                        e_status.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
                        calResult();
                        updt();
                        //serialPort_FXFS.Write(Ask);
                        c = 0;
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //处理超时错误
            }
        }

        public void FXFS_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)//读取下位机的数据，显示在textBlock中
        {

            try
            {

                App.Current.Dispatcher.Invoke((Action)delegate()
                {
                    Rcv_FXFS = serialPort_FXFS.ReadLine();
                    //e_status.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
                    e_status.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                    calFResult();
                    updtF();

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //处理超时错误
            }
        }

    }
}