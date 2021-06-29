using System.Linq;
using System.Threading.Tasks;
using App2.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace App2.ViewModels
{
    public class CardsEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Cards> Cards { get; set; }
        public ObservableRangeCollection<Grouping<string,Cards>> CardsGroups { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Cards> FavoriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

        public CardsEquipmentViewModel()
        {
            Title = "Cards Equipment";

            Cards = new ObservableRangeCollection<Cards>();
            CardsGroups = new ObservableRangeCollection<Grouping<string, Cards>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Cards>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);


        }

        async Task Favorite(Cards cards)
        {
            if (cards == null)
                return;
            await Application.Current.MainPage.DisplayAlert("Favorite", cards.Name, "OK");
        }

        Cards selectedCards;

        public Cards SelectedCards
        {
            get => selectedCards;
            set => SetProperty(ref selectedCards,value);
        }
         
        async Task Selected(object args)
        {
            var cards = args as Cards;
            if (cards == null)
                return;

            SelectedCards = null;
            await Application.Current.MainPage.DisplayAlert("Selected", cards.Name, "OK");
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);

            Cards.Clear();
            LoadMore();

            IsBusy = false;
        }

        void LoadMore()
        {
            if (Cards.Count >= 20)
                return;
            LoadMore();
            var image = "https://i.pinimg.com/originals/87/b7/34/87b734e6fbbe58302ee9ea7305459d18.png";
            var image2 = "https://i.imgur.com/9JX17Wi.png";

            Cards.Add(new Cards { Name = "yoongi", Group = "Trade", Image = image });
            Cards.Add(new Cards { Name = "jeongyeon", Group = "Sell", Image = image2 });
            Cards.Add(new Cards { Name = "yoongi", Group = "Wishlist", Image = image });
            Cards.Add(new Cards { Name = "jeongyeon", Group = "Trade", Image = image2 });

            CardsGroups.Clear();

            CardsGroups.Add(new Grouping<string, Cards>("Trade", Cards.Where(c => c.Group == "Trade")));
            CardsGroups.Add(new Grouping<string, Cards>("Sell", Cards.Where(c => c.Group == "Sell")));
            CardsGroups.Add(new Grouping<string, Cards>("Wishlist", Cards.Where(c => c.Group == "Wishlist")));


        }

        void DelayLoadMore()
        {
            if (Cards.Count <= 10)
                return;
            LoadMore();

        }

        void Clear()
        {
            Cards.Clear();
            CardsGroups.Clear();
        }
    }
}

