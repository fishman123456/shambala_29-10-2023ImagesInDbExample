using ImagesInDbExample.Model;
using ImagesInDbExample.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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


namespace ImagesInDbExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Services
        private ImagePathService imagePathService = new ImagePathService();
        private ImageService imageService = new ImageService();

        public MainWindow()
        {
            InitializeComponent();
            UpdateImagePathList();
        }

        private void UpdateImagePathList()
        {
            var imagePaths = imagePathService.GetAllImagePaths();
            imagePathListBox.Items.Clear();
            foreach (var imagePath in imagePaths)
            {
                imagePathListBox.Items.Add(imagePath);
            }
        }

        private void UpdateImageList()
        {
            var images = imageService.GetAllImages();
            imageListBox.Items.Clear();
            foreach (var image in images)
            {
                imageListBox.Items.Add(image);
            }
        }

        private void ShowImage(ImagePath imagePath)
        {
            selectedImagePath.Source = new BitmapImage(new Uri(Path.Combine(imagePath.FileLocation)));
        }

        private void ShowImage(Model.Image image)
        {
            selectedImage.Source = ConvertByteArrayToBitmapImage(image.Data);
        }

        private byte[] ConvertBitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] byteArray = null;

            if (bitmapImage != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(stream);
                    byteArray = stream.ToArray();
                }
            }

            return byteArray;
        }

        private BitmapImage ConvertByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bitmapImage = null;

            if (byteArray != null && byteArray.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze(); // Чтобы избежать InvalidOperationException при использовании в многопоточной среде
                }
            }

            return bitmapImage;
        }

        private void addImagePathButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Upload image and save to path
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                string appPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) + @"\assets\";
                var imagePath = Path.Combine(appPath + op.SafeFileName);

                if (!Directory.Exists(appPath))
                {
                    Directory.CreateDirectory(appPath);
                }

                File.Copy(op.FileName, imagePath, true);

                // 2. save data in db
                ImagePath newImagePath = imagePathService.AddImagePath(
                    new ImagePath() { FileName = op.SafeFileName, FileLocation = imagePath }
                    );

                // 3. update list and show image
                UpdateImagePathList();
                ShowImage(newImagePath);
            }
        }

        private void imagePathListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImagePath imagePath = imagePathListBox.SelectedItem as ImagePath;
            if (imagePath != null)
            {
                ShowImage(imagePath);
            }
        }

        private void sddImageButton_Click(object sender, RoutedEventArgs e)
        {

            // 1. Upload image and save to path
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {

                var bitmapImage = new BitmapImage(new Uri(op.FileName));

                // 2. save data in db
                Model.Image image = new Model.Image() { Name = op.SafeFileName, Data = ConvertBitmapImageToByteArray(bitmapImage) };
                image = imageService.AddImage(image);

                // 3. update list and show image
                UpdateImageList();
                ShowImage(image);
            }
        }

        private void imageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Image image = imageListBox.SelectedItem as Model.Image;
            if (image != null ) {
                ShowImage(image);
            }
        }
    }
}
