using System;

namespace MVVMShopForms
{
    public interface INotification
    {
        void CreateNotification(String title, String message);
    }
}
