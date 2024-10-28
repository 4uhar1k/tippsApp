using System;
using System.Linq;
namespace tippsApp
{
    public partial class MainPage : ContentPage
    {
        public  List<Note> noteNames { get; set; }
        public NoteViewModel viewModel { get; set; }

        public string fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
        public MainPage()
        {
            InitializeComponent();
            //File.Delete(fileName);
            /*noteNames = new List<Note>();
            
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            string? line;
             
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Note fileNote = new Note();
                    fileNote.name = line;
                    fileNote.content = sr.ReadLine();
                    noteNames.Add(fileNote);
                }
                sr.Close();
            }*/


            //this.noteNames = noteNames;
            NoteViewModel viewModel = new NoteViewModel();
            BindingContext = viewModel;

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
                await Navigation.PushAsync(new AddNote(selectedNote.Name, selectedNote.Content), false);
            }
            
        }

        private async void deleteNotes(object sender, EventArgs e)
        {
            //new NoteViewModel().disableDelCommand();
            //notesCollection.SelectionChangedCommand = 
            /*notesCollection.SelectionMode = SelectionMode.Multiple;
            notesCollection.SelectionChangedCommand = null;
            bool answer = await DisplayAlert("", "Are you sure you want to delete these notes?", "Yes", "No");
            if (answer)
            {

            }
            else
            {
                notesCollection.SelectionMode = SelectionMode.Single;
               // notesCollection.SelectionChangedCommand = new Command(showNote);
            }*/
            //notesCollection.SelectionChangedCommand = BindingContext.;
        }


    }

}
