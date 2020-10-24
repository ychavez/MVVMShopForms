using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using Plugin.Media;
using System;
using System.IO;
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
            IsBusy = true;
            Product = product ?? new Product();
            SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete);
            UploadPhoto = new Command(TakePhoto);
            PhotoFromFile = new Command(TakeFromFile);
            _Context = new Context(Globals.ServiceApiKey);
            if (product != null)
            {
                ImgSource = ImageSource.FromStream(() => new MemoryStream(product.Picture));
            }
            IsBusy = false;
        }
        private async void Save()
        {
            IsBusy = true;
            if (Product.Id == 0)
                await _Context.AddProduct(Product);
            else
                await _Context.UpdateProduct(Product);
            IsBusy = false;
            await Navigation.PopAsync();
        }
        private async void Delete()
        {
            IsBusy = true;
            bool answer = await Application.Current.MainPage.DisplayAlert("Atencion", "Estas seguro que quieres borrar este articulo", "Yes", "No");
            await _Context.DeteProduct(Product);
            await Navigation.PopAsync();
            IsBusy = false;
        }
        private async void TakePhoto()
        {
            IsBusy = true;
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Camara no disponible", "La camara no esta disponible en este dispositivo", "OK");
                IsBusy = false;
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
                IsBusy = false;
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
                IsBusy = false;
                return stream;
            });
            IsBusy = false;
        }
        private async void TakeFromFile()
        {
            IsBusy = true;
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
                IsBusy = false;
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
                IsBusy = false;
                return stream;
            });
        }
    }
}
