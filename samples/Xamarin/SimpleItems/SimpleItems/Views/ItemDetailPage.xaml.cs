using SimpleItems.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SimpleItems.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}