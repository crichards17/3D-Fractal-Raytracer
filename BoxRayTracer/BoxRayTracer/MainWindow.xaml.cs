using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using Scene;
using Vector = Scene.Vector;

namespace BoxRayTracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapRaytracer bmpTracer;
        public MainWindow()
        {
            InitializeComponent();
            bmpTracer = new BitmapRaytracer(new Sphere(Vector.origin, 1), 20, (int)imageContainer.Width, (int)imageContainer.Height, 90, new Vector(-5, 0, 0), Vector.origin);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bmp = bmpTracer.Render();
            BitmapImage bmpImg = BitmapToImageSource(bmp);
            imageContainer.Source = bmpImg;

        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
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
    }
}
