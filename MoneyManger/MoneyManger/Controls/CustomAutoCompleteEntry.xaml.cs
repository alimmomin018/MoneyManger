using MoneyManger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomAutoCompleteEntry : StackLayout
    {
        public static BindableProperty NotesProperty = BindableProperty.Create(nameof(Notes), typeof(List<string>),
                   typeof(CustomAutoCompleteEntry), default(List<string>), BindingMode.TwoWay);

        public static BindableProperty SelectedNoteProperty = BindableProperty.Create(nameof(SelectedNote), typeof(string),
                   typeof(CustomAutoCompleteEntry), default(string), BindingMode.TwoWay);

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string),
                   typeof(CustomAutoCompleteEntry), default(string), BindingMode.OneWay);

        public CustomAutoCompleteEntry()
        {
            InitializeComponent();
        }

        public List<string> Notes
        {
            get { return (List<string>)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        public string SelectedNote
        {
            get { return (string)GetValue(SelectedNoteProperty); }
            set
            {
                SetValue(SelectedNoteProperty, value);
                entry.Text = value;
            }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string tappedNote = e.Item as string;
            if (tappedNote == null)
                return;

            SelectedNote = tappedNote;
            entry.Text = tappedNote;
            listView.IsVisible = false;

            ((ListView)sender).SelectedItem = null;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                listView.IsVisible = true;
                var filteredNotes = Notes.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    listView.IsVisible = false;
                else if (filteredNotes.Count() == 0)
                    listView.IsVisible = false;
                else
                {
                    listView.HeightRequest = filteredNotes.Count() * 40;
                    listView.ItemsSource = filteredNotes;

                }

                SelectedNote = e.NewTextValue;
            }
            catch
            {
                listView.IsVisible = false;
            }
        }
    }
}