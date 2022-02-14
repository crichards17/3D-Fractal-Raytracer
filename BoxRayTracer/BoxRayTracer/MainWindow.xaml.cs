using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using Scene;
using Vector = Scene.Vector;

namespace BoxRayTracer
{
    public partial class MainWindow : Window
    {
        private BitmapRaytracer bmpTracer;
        public MainWindow()
        {
            InitializeComponent();
            bmpTracer = new BitmapRaytracer(new Sphere(Vector.origin, 1), 20, (int)imageContainer.Width, (int)imageContainer.Height, 90, new Vector(-5, 0, 0), Vector.origin);
            SetParamFields();
        }

        private void Button_Render_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bmp = bmpTracer.Render();
            BitmapImage bmpImg = BitmapToImageSource(bmp);
            imageContainer.Source = bmpImg;
            SetParamFields();
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void SetParamFields()
        {
            // objPos is set in the constructor (currently static at origin)
            objPosX.Text = $"{Vector.origin.x}";
            objPosY.Text = $"{Vector.origin.y}";
            objPosZ.Text = $"{Vector.origin.z}";

            bmpTracer.GetSceneParams(out Vector camPos, out Vector camFrus, out Vector camRoll);
            
            camPosX.Text = $"{camPos.x}";
            camPosY.Text = $"{camPos.y}";
            camPosZ.Text = $"{camPos.z}";

            camFrusY.Text = $"{camFrus.y}";
            camFrusX.Text = $"{camFrus.x}";
            camFrusZ.Text = $"{camFrus.z}";

            camRollX.Text = $"{camRoll.x}";
            camRollY.Text = $"{camRoll.y}";
            camRollZ.Text = $"{camRoll.z}";
        }
    }
}
