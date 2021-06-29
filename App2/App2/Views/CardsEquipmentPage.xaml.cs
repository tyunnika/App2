using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsEquipmentPage : ContentPage
    {
        public CardsEquipmentPage()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cards = ((ListView)sender).SelectedItem as Cards;
            if (cards == null)
                return;

            await DisplayAlert("Card Selected", cards.Name, "OK");
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var cards = ((MenuItem)sender).BindingContext as Cards;
                if (cards == null)
                return;

            await DisplayAlert("Card Favorited", cards.Name, "OK");
        }
    }
}