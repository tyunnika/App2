using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App2.Models;
using App2.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace App2.ViewModels
{
   public class MyCardsViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Cards> Cards { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Cards> RemoveCommand { get; }



        public MyCardsViewModel()
        {
            Title = "My Cards";

            Cards = new ObservableRangeCollection<Cards>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Cards>(Remove);
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name of Idol");
            var group = await App.Current.MainPage.DisplayPromptAsync("Group", "Kind of trade");
            await CardsService.AddCards(name, group);
            await Refresh();
        }

        async Task Remove(Cards cards)
        {
            await CardsService.RemoveCards(cards.Id);
            await Refresh();

        }

       async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Cards.Clear();
            var cardss = await CardsService.GetCards();
            Cards.AddRange(cardss);
            IsBusy = false;

            
        
        
        }
    }
}
