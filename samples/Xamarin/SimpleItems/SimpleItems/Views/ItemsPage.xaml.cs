using SimpleItems.Models;
using SimpleItems.ViewModels;
using SimpleItems.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderDesign.Net.Threading.HelperClasses;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleItems.Views
{
    public partial class ItemsPage : ContentPage
    {
        #region constructors
        public ItemsPage()
        {
            NewItemCommandAsync = new AsyncCommand(() => OnNewItemCommandAsync(), allowsMultipleExecutions: false);
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }
        #endregion

        #region properties
        public IAsyncCommand NewItemCommandAsync { get; protected set; }
        #endregion

        #region methods
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async Task OnNewItemCommandAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        #endregion

        #region variables
        ItemsViewModel _viewModel;
        #endregion
    }
}