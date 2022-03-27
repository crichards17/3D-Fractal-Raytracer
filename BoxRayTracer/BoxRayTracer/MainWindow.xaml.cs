#define DefaultObjects

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
            SetDefaults();
            RunRender();
        }

        #region interaction
        private void Button_Render_Click(object sender, RoutedEventArgs e)
        {
            RunRender();
        }

        private void Button_Default_Click(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (imgTitle.Text != "")
            {
                Save(currentBmp, imgTitle.Text);
            }
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
            imageContainer.Width = double.Parse(resXBox.Text);
            imageContainer.Height = double.Parse(resYBox.Text);
            BitmapRaytracer bmpTracer = GetBRT();
            currentBmp = BitmapToImageSource(bmpTracer.Render());
            imageContainer.Source = currentBmp;
            SetParamFields(bmpTracer);
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

        private static void Save(BitmapImage image, string fileName)
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
        }

        private void SetDefaults()
        {
            // TODO: Will remove the need for this method
            //  by binding UI fields to BRT properties directly.
            //  Will set defaults on BRT initialization
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

        private BitmapRaytracer GetBRT()
        {
            System.Windows.Media.Color objColorMedia = (System.Windows.Media.Color)(objColorSelector.SelectedItem as System.Reflection.PropertyInfo).GetValue(null, null);
            Scene.Color objColorScene = new Scene.Color((double)objColorMedia.R / 255, (double)objColorMedia.G / 255, (double)objColorMedia.B / 255);
#if DefaultObjects 
            SceneObjectEstimatable[] objects = Defaults.sceneObjects;
#else

            // TODO: Hook to object list and count from UI
            ISceneObjectEstimatable[] objects = new ISceneObjectEstimatable[count];
            for (int i = 0; i < count; i++) 
            { 
                switch (shapeSelector.SelectedItem) {
                    case "Sphere":
                        objects[i] = new Sphere(new Vector(double.Parse(objPosX.Text), double.Parse(objPosY.Text), double.Parse(objPosZ.Text)), objColorScene, radius);
                        break;
                    // TODO: Default behavior?
                    default:
                        break;
                }
            }
#endif

            // TODO: Handle invalid text entry
            Vector camPos = new Vector(double.Parse(camPosX.Text), double.Parse(camPosY.Text), double.Parse(camPosZ.Text));
            
            // TODO: Refactor to "Look At" entry rather than camFrus. 
            Vector camFrus = new Vector(double.Parse(camFrusX.Text), double.Parse(camFrusY.Text), double.Parse(camFrusZ.Text));

            // Get Color selections from the UI color picker, convert to Scene.Color objects
            
            System.Windows.Media.Color backColorMedia = (System.Windows.Media.Color)(backColorSelector.SelectedItem as System.Reflection.PropertyInfo).GetValue(null, null);
            Scene.Color backColorScene = new Scene.Color((double)backColorMedia.R / 255, (double)backColorMedia.G / 255, (double)backColorMedia.B / 255);

            // TODO: Pass lights from UI / state to brt
            SceneStage sceneStage = new SceneStage(objects, Defaults.sceneLights, backColorScene);
            BitmapRaytracer brt = new (sceneStage, Defaults.maxDist, (int)imageContainer.Width, (int)imageContainer.Height, double.Parse(fovBox.Text), camPos, camPos + camFrus);
            return brt;
        }

#endregion
    }
}
