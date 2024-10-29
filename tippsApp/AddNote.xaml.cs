using Microsoft.Maui.Controls.Shapes;
using System;
using System.IO;
namespace tippsApp;

public partial class AddNote : ContentPage
{
	public List<Note> notes { get; set; }

    public string fileName = System.IO.Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    public bool oldNote { get; set; } = false;
    public string name { get; set; }
    public string content { get; set; }
    public Note thisNote;
    public NoteViewModel thisContext = new NoteViewModel();

    public AddNote()
	{
		InitializeComponent();
        BindingContext = new NoteViewModel();
        noteName.Placeholder = "Your note...";
        noteContent.Placeholder = "Your note...";
        noteName.PlaceholderColor = Colors.White;
    }

    public AddNote(string name, string content)
    {
        InitializeComponent();
        this.oldNote = true;
        string? line;
        notes = new List<Note>();
        using (StreamReader sr = new StreamReader(fileName))
        {
            while ((line = sr.ReadLine()) != null)
            {
                Note fileNote = new Note();
                fileNote.Name = line;
                fileNote.Content = sr.ReadLine();
                notes.Add(fileNote);
            }
            sr.Close();
        }
        this.notes = notes;
        thisNote = new Note();
        
        
        
        BindingContext = thisContext;
        thisContext.noteToEdit.Name = name;
        thisContext.noteToEdit.Content = content;
        thisContext.editableNote.Name = name;
        thisContext.editableNote.Content = content;
        


    }


    public async void goBack(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }

    private async void saveNote(object sender, EventArgs e)
    {

        string oldName = this.name;
        string oldContent = this.content;
        bool oldNote = this.oldNote;
        await Navigation.PushAsync(new MainPage());

        
       
    }

    public void clickedText(object sender, EventArgs e)
    {
        
    }
}