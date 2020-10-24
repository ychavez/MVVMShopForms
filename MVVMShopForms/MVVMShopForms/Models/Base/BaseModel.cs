using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMShopForms.Models.Base
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateVal = null)
        {

            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateVal != null && !validateVal(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     

    }
}
