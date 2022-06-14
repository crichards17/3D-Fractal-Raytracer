using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using Scene;
using Vector = Scene.Vector;
using System;
using System.Windows.Input;

namespace BoxRayTracer
{
    public partial class MainWindow : Window
    {
        private BitmapImage? currentBmp;
        public MainWindow()
        {
            InitializeComponent();
            // SetDefaults();
            RunRender();
        }

        #region interaction
        private void Button_Render_Click(object sender, RoutedEventArgs e)
        {
            RunRender();
        }

        /* Button_Default_Click deprecated with move from UI-bound params
        private void Button_Default_Click(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }
        */

        private void Button_Save_Image_Click(object sender, RoutedEventArgs e)
        {
            if (imgTitle.Text != "")
            {
                SaveImage(currentBmp, imgTitle.Text);
            }
        }

        // TODO
        private void Button_Load_Config_Click(object sender, RoutedEventArgs e)
        {
            // Load config from file
        }

        //TODO
        private void Button_Save_Config_Click(object sender, RoutedEventArgs e)
        {
            // Save config to file
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                RunRender();
            }
        }
        #endregion

        #region helpers
        private void RunRender()
        {
            imageContainer.Width = Defaults.imgWidth;
            imageContainer.Height = Defaults.imgHeight;
            BitmapRaytracer bmpTracer = GetBRT();
            currentBmp = BitmapToImageSource(bmpTracer.Render());
            imageContainer.Source = currentBmp;
            // SetParamFields(bmpTracer);
        }
        
        private static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using MemoryStream memory = new MemoryStream();
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

        private static void SaveImage(BitmapImage image, string fileName)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            string brtPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BRT";
            System.IO.Directory.CreateDirectory(brtPath);
            using (var fileStream = new System.IO.FileStream($"{brtPath}//{fileName}.PNG", System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        /* SetParamFields deprecated with move from UI-bound params 
        private void SetParamFields(BitmapRaytracer bmpTracer)
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
        } */

        /* SetDefaults deprecated with move from UI-bound params 
        private void SetDefaults()
        {
            //  Set defaults on BRT initialization
            objPosX.Text = Defaults.objPosX.ToString();
            objPosY.Text = Defaults.objPosY.ToString();
            objPosZ.Text = Defaults.objPosZ.ToString();

            camPosX.Text = Defaults.camPosX.ToString();
            camPosY.Text = Defaults.camPosY.ToString();
            camPosZ.Text = Defaults.camPosZ.ToString();

            camFrusX.Text = Defaults.camFrusX.ToString();
            camFrusY.Text = Defaults.camFrusY.ToString();
            camFrusZ.Text = Defaults.camFrusZ.ToString();

            //shapeSelector.ItemsSource = Defaults.shapeList;
            //shapeSelector.SelectedIndex = Defaults.shapeDefaultIndex;

            objColorSelector.ItemsSource = typeof(System.Windows.Media.Colors).GetProperties();
            objColorSelector.SelectedItem = typeof(System.Windows.Media.Colors).GetProperty(Defaults.objColor);

            backColorSelector.ItemsSource = typeof(System.Windows.Media.Colors).GetProperties();
            backColorSelector.SelectedItem = typeof(System.Windows.Media.Colors).GetProperty(Defaults.backColor);

            camRollX.Text = "";
            camRollY.Text = "";
            camRollZ.Text = "";

            fovBox.Text = Defaults.fov.ToString();

            resXBox.Text = Defaults.imgWidth.ToString();
            resYBox.Text = Defaults.imgHeight.ToString();
        }
        */

        private BitmapRaytracer GetBRT()
        {
            /* Deprecated by move from UI-bound params
            System.Windows.Media.Color objColorMedia = (System.Windows.Media.Color)(objColorSelector.SelectedItem as System.Reflection.PropertyInfo).GetValue(null, null);
            Scene.Color objColorScene = new Scene.Color((double)objColorMedia.R / 255, (double)objColorMedia.G / 255, (double)objColorMedia.B / 255);
            */

            SceneObjectEstimatable[] objects = Defaults.sceneObjects;

            Vector camPos = Defaults.camPos;
            
            Vector camFrus = Defaults.camFrus;

            Scene.Color backColorScene = Defaults.globalAmbientColor * Defaults.globalAmbientIntensity;

            SceneStage sceneStage = new SceneStage(objects, Defaults.sceneLights, backColorScene);
            BitmapRaytracer brt = new (sceneStage, Defaults.maxDist, (int)imageContainer.Width, (int)imageContainer.Height, Defaults.fov, camPos, camPos + camFrus);
            return brt;
        }

#endregion
    }
}
