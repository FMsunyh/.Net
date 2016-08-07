﻿using Wrox.ProCSharp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Wrox.ProCSharp.Repositories;
using Wrox.ProCSharp.Model;
using System.Collections.ObjectModel;
using Wrox.ProCSharp.Storage;
using Windows.UI.Popups;
using Windows.ApplicationModel.DataTransfer;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace Wrox.ProCSharp
{
  /// <summary>
  /// A page that displays a collection of item previews.  In the Split Application this page
  /// is used to display and select one of the available groups.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    private NavigationHelper navigationHelper;
    private ObservableDictionary defaultViewModel = new ObservableDictionary();

    /// <summary>
    /// This can be changed to a strongly typed view model.
    /// </summary>
    public ObservableDictionary DefaultViewModel
    {
      get { return this.defaultViewModel; }
    }

    /// <summary>
    /// NavigationHelper is used on each page to aid in navigation and 
    /// process lifetime management
    /// </summary>
    public NavigationHelper NavigationHelper
    {
      get { return this.navigationHelper; }
    }

    public MainPage()
    {
      this.InitializeComponent();
      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += navigationHelper_LoadState;
      this.navigationHelper.SaveState += navigationHelper_SaveState;
    }

    void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
    {
      DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;
    }

    /// <summary>
    /// Populates the page with content passed during navigation.  Any saved state is also
    /// provided when recreating a page from a prior session.
    /// </summary>
    /// <param name="sender">
    /// The source of the event; typically <see cref="NavigationHelper"/>
    /// </param>
    /// <param name="e">Event data that provides both the navigation parameter passed to
    /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
    /// a dictionary of state preserved by this page during an earlier
    /// session.  The state will be null the first time a page is visited.</param>
    private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
    {
      this.DefaultViewModel["Commands"] = this;

      var storage = new MenuCardStorage();
      MenuCardRepository.Instance.InitMenuCards(new ObservableCollection<MenuCard>(
          await storage.ReadMenuCardsAsync()));
      cards = MenuCardRepository.Instance.Cards;
      this.DefaultViewModel["Items"] = cards;

      DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;

    }

 

    private void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
    {
      args.Request.FailWithDisplayText("Open a menu card before sharing");
    }

    private IEnumerable<MenuCard> cards;

    #region NavigationHelper registration

    /// The methods provided in this section are simply used to allow
    /// NavigationHelper to respond to the page's navigation methods.
    /// 
    /// Page specific logic should be placed in event handlers for the  
    /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
    /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
    /// The navigation parameter is available in the LoadState method 
    /// in addition to page state preserved during an earlier session.

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      navigationHelper.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      navigationHelper.OnNavigatedFrom(e);
    }

    #endregion

    private RelayCommand addCommand;
    public RelayCommand AddCommand
    {
      get
      {
        return addCommand ?? (addCommand = new RelayCommand(OnAdd));
      }
    }

    private void OnAdd()
    {
      Frame.Navigate(typeof(AddMenuCardPage));
    }

    private RelayCommand deleteCommand;
    public RelayCommand DeleteCommand
    {
      get
      {
        return deleteCommand ?? (deleteCommand = new RelayCommand(OnDelete));
      }
    }

    private async void OnDelete()
    {
      var selectedMenuCard = itemGridView.SelectedItem as MenuCard;
      if (selectedMenuCard != null)
      {

        var dlg = new MessageDialog(string.Format("Delete the menu card {0}?", selectedMenuCard.Title));
        dlg.Commands.Add(new UICommand("Delete", new UICommandInvokedHandler(DeleteMenuCard)));
        dlg.Commands.Add(new UICommand("Cancel"));
        await dlg.ShowAsync();
      }
    }

    private void DeleteMenuCard(IUICommand command)
    {
      var menuCards = this.DefaultViewModel["Items"] as ObservableCollection<MenuCard>;
      if (menuCards != null)
      {
        menuCards.Remove(itemGridView.SelectedItem as MenuCard);
        // TODO: remove it from storage
      }
    }

    private void OnMenuCardClick(object sender, ItemClickEventArgs e)
    {
      Frame.Navigate(typeof(MenuItemsPage), e.ClickedItem);
    }

    private async void OnSearchQuery(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
    {
      string query = args.QueryText;
      var cards = WordsLookup[query];
      MenuCard card = cards.FirstOrDefault();

      if (card != null)
      {
        this.Frame.Navigate(typeof(MenuItemsPage), card);
      }
      else
      {
        MessageDialog dlg = new MessageDialog("Word not found");
        await dlg.ShowAsync();
      }
     
    }

    private ILookup<string, MenuCard> GetSearchWords()
    {
      string[] fillWords = { "der", "die", "mit", "und", "im", "auf" };

      return cards.SelectMany(card => card.MenuItems).
        SelectMany(mi => mi.Text.Split(), (mi, word) => new { MenuItem = mi, Word = word }).
        Where(item => !fillWords.Contains(item.Word)).
        ToLookup(item => item.Word, item => item.MenuItem.MenuCard);
    }

    private void OnSuggestionRequested(SearchBox sender, SearchBoxSuggestionsRequestedEventArgs args)
    {
      if (string.IsNullOrEmpty(args.QueryText))
      {
        return;
      }

      var deferral = args.Request.GetDeferral();
      string query = args.QueryText;
      var suggestions = this.Keys.Where(k => k.StartsWith(query)).ToList();
      args.Request.SearchSuggestionCollection.AppendQuerySuggestions(suggestions);

      deferral.Complete();
    }

    private ILookup<string, MenuCard> wordsLookup;
    public ILookup<string, MenuCard> WordsLookup
    {
      get
      {
        return wordsLookup ?? (wordsLookup = GetSearchWords());
      }
    }

    public IEnumerable<string> Keys
    {
      get
      {
        return WordsLookup.Select(w => w.Key);
      }
    }

  }
}
