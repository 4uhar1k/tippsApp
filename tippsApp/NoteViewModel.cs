using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Diagnostics;



namespace tippsApp;

public class ShowNoteViewModel : ViewModelBase
{


    public Note selectedNote;

    public ShowNoteViewModel(Note note) => this.selectedNote = note;

    public string Name
    {
        get => selectedNote.Name;
        set
        {
            if (selectedNote.Name != value)
            {
                selectedNote.Name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Content
    {
        get => selectedNote.Content;
        set
        {
            if (selectedNote.Content != value)
            {
                selectedNote.Content = value;
                OnPropertyChanged();
            }
        }
    }


}

public class EditNoteViewModel : ViewModelBase
{

}

public class NoteViewModel : ViewModelBase
{




    public Note note = new Note();

    string name, content;
    public ObservableCollection<Note> Notes { get; set; }
    public ICommand AddNewCommand { get; set; }
    public ICommand EditNoteCommand { get; set; }
    public ICommand AddOldCommand { get; set; }
    public ICommand DelOldCommand { get; set; }
    public ICommand DelNoteCommand { get; set; }
    public ICommand disableDelCommand { get; set; }
    public bool isOld = false;



    public ShowNoteViewModel noteToSave { get; set; }
    public EditNoteViewModel noteToEdit { get; set; }
    public Note editableNote { get; set; }

    public NoteViewModel()
    {
        noteToSave = new ShowNoteViewModel(new Note() { });
        noteToEdit = new EditNoteViewModel();
        editableNote = new Note();
        editableNote.Name = noteToEdit.Name;
        editableNote.Content = noteToEdit.Content;
        string fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
        Notes = new ObservableCollection<Note>();

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
                fileNote.Name = line;
                fileNote.Content = sr.ReadLine();
                Notes.Add(fileNote);

            }
            sr.Close();
        }

        AddNewCommand = new Command(() =>
        {

            if (noteToEdit.Name != "" | noteToEdit.Content != "")
            {
                noteToSave.Name = noteToEdit.Name;
                noteToSave.Content = noteToEdit.Content;

                Note oldNote = new Note();
                try
                {
                    oldNote = Notes.First(n => n.Name == editableNote.Name && n.Content == editableNote.Content);
                    Notes.Remove(oldNote);
                }
                catch (Exception e)
                {

                }

                Notes.Insert(0, new Note() { Name = noteToSave.Name, Content = noteToSave.Content });



                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    foreach (Note note in Notes)
                    {
                        sw.WriteLine(note.Name);
                        sw.WriteLine(note.Content);
                    }


                }
            }





        }, () => name != "" || content != "");





    }











}

