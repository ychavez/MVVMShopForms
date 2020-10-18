using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMShopForms.ViewModels
{
    public class ProductItemViewModel : BaseViewModel
    {
        private Context _Context;
        public Product Product { get; set; }
        private ImageSource _ImgSource;
        public ImageSource ImgSource { get => _ImgSource; set { SetProperty(ref _ImgSource, value); } }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand PhotoFromFile { get; set; }
        public ICommand UploadPhoto { get; set; }

        public ProductItemViewModel(Product product = null)
        {
            Product = product ?? new Product();
            SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete);
            UploadPhoto = new Command(TakePhoto);
            PhotoFromFile = new Command(TakeFromFile);
            _Context = new Context();
            if (product != null)
            {
                ImgSource = ImageSource.FromStream(() => new MemoryStream(product.Picture));
            }
        }

        private async void Save()
        {
            if (Product.Id == 0)
                await _Context.AddProduct(Product);
            else
                await _Context.UpdateProduct(Product);

            await Navigation.PopAsync();

        }

        private async void Delete()
        {

            bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");

            await _Context.DeteProduct(Product);

            await Navigation.PopAsync();
        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Camara no disponible", "La camara no esta disponible en este dispositivo", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Shop",
                Name = $"{Guid.NewGuid().ToString()}.jpg",
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                SaveToAlbum = true
            });

            if (file == null)
            {
                return;
            }
            ImgSource = ImageSource.FromStream(() =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    Product.Picture = memoryStream.ToArray();
                }
                var stream = file.GetStream();
                return stream;
            });
        }
        private async void TakeFromFile()
        {

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Fotos no soportadas", "No tiene permisos de almacenamiento", "OK");
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });

            if (file == null)
            {
                return;
            }

            ImgSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    Product.Picture = memoryStream.ToArray();
                }

                file.Dispose();
                return stream;
            });
        }




    }
}
