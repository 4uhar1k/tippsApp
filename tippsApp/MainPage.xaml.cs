using System;
using System.Linq;
using tippsApp.Models;
using tippsApp.ViewModels;
namespace tippsApp
{
    public partial class MainPage : ContentPage
    {
        public  List<Note> noteNames { get; set; }
        public NoteViewModel viewModel { get; set; }
        public MainViewModel thisContext { get; set; }

        public string fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
        public MainPage()
        {
            InitializeComponent();
            thisContext = new MainViewModel();
            BindingContext = thisContext;
        }
        

        private async void addNote(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNote(), false);
        }

        private async void showNote(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 0)
            {
                var selectedNote = (Note)e.CurrentSelection[0];
                await Navigation.PushAsync(new AddNote(selectedNote), false);
            }            
        }

        private void deleteNotes(object sender, EventArgs e)
        {
            if(addButton.IsVisible == true)
            {
                addButton.IsVisible = false;
                notesCollection.SelectionChangedCommand = thisContext.RemoveCommand;
                notesCollection.SelectionChanged -= showNote;
               
            }
            else
            {
                addButton.IsVisible = true;
                notesCollection.SelectionChangedCommand = null;
                notesCollection.SelectionChanged += showNote;
            }
            
        }


    }

}
