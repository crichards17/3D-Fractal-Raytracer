using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using Scene;
using Vector = Scene.Vector;
using System;

namespace BoxRayTracer
{
    public partial class MainWindow : Window
    {
        private BitmapRaytracer bmpTracer;
        public MainWindow()
        {
            InitializeComponent();
            SetDefaults();
            bmpTracer = GetBRT();
        }

        private void Button_Render_Click(object sender, RoutedEventArgs e)
        {
            bmpTracer = GetBRT();
            Bitmap bmp = bmpTracer.Render();
            BitmapImage bmpImg = BitmapToImageSource(bmp);
            imageContainer.Source = bmpImg;
            SetParamFields();
        }

        private void Button_Default_Click(object sender, RoutedEventArgs e)
        {
            SetDefaults();
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

        private void SetDefaults()
        {
            objPosX.Text = Defaults.objPosX.ToString();
            objPosY.Text = Defaults.objPosY.ToString();
            objPosZ.Text = Defaults.objPosZ.ToString();

            camPosX.Text = Defaults.camPosX.ToString();
            camPosY.Text = Defaults.camPosY.ToString();
            camPosZ.Text = Defaults.camPosZ.ToString();

            camFrusX.Text = Defaults.camFrusX.ToString();
            camFrusY.Text = Defaults.camFrusY.ToString();
            camFrusZ.Text = Defaults.camFrusZ.ToString();

            shapeSelector.ItemsSource = Defaults.shapeList;
            shapeSelector.SelectedIndex = Defaults.shapeDefaultIndex;

            camRollX.Text = "";
            camRollY.Text = "";
            camRollZ.Text = "";
        }

        private BitmapRaytracer GetBRT()
        {
            IDistanceEstimator dE;
            switch (shapeSelector.SelectedItem) {
                case "Sphere":
                    dE = new Sphere(new Vector(Double.Parse(objPosX.Text), Double.Parse(objPosY.Text), Double.Parse(objPosZ.Text)), Defaults.radius);
                    break;
                // TODO: Default behavior?
                default:
                    dE = new Sphere(new Vector(Double.Parse(objPosX.Text), Double.Parse(objPosY.Text), Double.Parse(objPosZ.Text)), Defaults.radius);
                    break;
            }

            // TODO: Handle invalid text entry
            Vector camPos = new Vector(Double.Parse(camPosX.Text), Double.Parse(camPosY.Text), Double.Parse(camPosZ.Text));
            Vector camFrus = new Vector(Double.Parse(camFrusX.Text), Double.Parse(camFrusY.Text), Double.Parse(camFrusZ.Text));

            BitmapRaytracer brt = new BitmapRaytracer(dE, Defaults.maxDist, (int)imageContainer.Width, (int)imageContainer.Height, Defaults.fov, camPos, camFrus);
            return brt;
        }
    }
}
