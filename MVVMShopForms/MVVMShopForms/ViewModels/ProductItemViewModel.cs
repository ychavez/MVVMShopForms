using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using Plugin.Media;
using System;
using System.Collections.Generic;
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

        public ICommand UploadPhoto { get; set; }

        public ProductItemViewModel(Product product = null)
        {
            Product = product ?? new Product();
            SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete);
            UploadPhoto = new Command(BtnTomarFoto_Click);
            _Context = new Context();
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

        private async void BtnTomarFoto_Click()
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
                SaveToAlbum = true
            });

            if (file == null)
            {
                return;
            }
            ImgSource = ImageSource.FromStream(() => { var stream = file.GetStream(); return stream; });
        }

        

    }
}
