using MVVMShopForms.Models.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xamarin.Forms.Xaml;

namespace MVVMShopForms.Models
{
   public class Product:BaseModel
    {
        private int _Id;
        public int Id
        { 
            get => _Id; 
            set => SetProperty(ref _Id, value); 
        }
        private string _Name;
        public string Name 
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }
        private string _Description;
        public string Description
        {
            get => _Description;
            set => SetProperty(ref _Description, value);
        }
        private string _Color;
        public string  Color
        {
            get => _Color;
            set => SetProperty(ref _Color, value);
        }
        private decimal _Price;
        public decimal Price 
        {
            get => _Price;
            set => SetProperty(ref _Price, value);
        }


        private byte[] _Picture;
        public byte[] Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
        }
    }
}
