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
                fileNote.name = line;
                fileNote.content = sr.ReadLine();
                notes.Add(fileNote);
            }
            sr.Close();
        }
        this.notes = notes;
        thisNote = new Note();
        thisNote.name = name;
        thisNote.content = content;
        //this.thisNote = thisNote;
        var thisContext = new NoteViewModel(thisNote);
        BindingContext = new NoteViewModel();
        //saveButton.
        
        
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
       // await Navigation.PushAsync(new MainPage());

        if (noteName.Text != "" || noteContent.Text != "")
        {
            if(oldNote==false) // if user creates a new note
            {
                
                //saveButton.Command = Binding 
                /*using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(noteName.Text);
                    sw.WriteLine(noteContent.Text);
                    sw.Close();
                }*/
                await Navigation.PushAsync(new MainPage());
            }
            else // if user changes an old note
            {
                
                Note foundNote = new Note();
                foundNote = notes.Find(n => n.name==oldName && n.content==oldContent);
                foundNote.name = noteName.Text;
                foundNote.content = noteContent.Text;
                using (StreamWriter sw = new StreamWriter(fileName,false))
                {
                    foreach (Note note in notes)
                    {
                        sw.WriteLine(note.name);
                        sw.WriteLine(note.content);
                    }
                    sw.Close();
                }
                await Navigation.PushAsync(new MainPage());
            }
            
        }
        else
        {
            if (oldNote == false) //if user wanted to create new note but made it empty
            {
                await Navigation.PushAsync(new MainPage());
            }
            else // if user made title and content of an old note empty
            {
                
                Note foundNote = new Note();
                foundNote = notes.Find(n => n.name == name && n.content == content);
                notes.Remove(foundNote);
                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    foreach (Note note in notes)
                    {
                        sw.WriteLine(note.name);
                        sw.WriteLine(note.content);
                    }
                    sw.Close();
                }
                await Navigation.PushAsync(new MainPage());
            }
        }




    }

    public void clickedText(object sender, EventArgs e)
    {
        
    }
}