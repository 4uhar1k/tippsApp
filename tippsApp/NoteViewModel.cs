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

public class ShowNoteViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

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

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

public class EditNoteViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    string name;
    string content;
    public string Name
    {
        get => name;
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Content
    {
        get => content;
        set
        {
            if (content != value)
            {
                content = value;
                OnPropertyChanged();
            }
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

public class NoteViewModel
{
    
    

    //
    public Note note = new Note();
    //List<Note> notes = new List<Note>();
    string name, content;
    public ObservableCollection<Note> notes { get; set; }
    public ICommand AddNewCommand { get; set; }
    public ICommand EditNoteCommand { get; set; }
    public ICommand AddOldCommand { get; set; }
    public ICommand DelOldCommand { get; set; }
    public ICommand DelNoteCommand { get; set; }
    public ICommand disableDelCommand { get; set; }
    public bool isOld = false;

    //public event PropertyChangedEventHandler? PropertyChanged;

    public ShowNoteViewModel noteToSave {  get; set; }
    public EditNoteViewModel noteToEdit { get; set; }
    public EditNoteViewModel editableNote { get; set; }

    public NoteViewModel()
    {
        noteToSave = new ShowNoteViewModel(new Note() { });
        noteToEdit = new EditNoteViewModel();
        editableNote = new EditNoteViewModel();
        
        string fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
        notes = new ObservableCollection<Note>();
        
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
                notes.Add(fileNote);
                
            }
            sr.Close();
        }

        AddNewCommand = new Command(() =>
        {
            
            if (noteToEdit.Name != "" | noteToEdit.Content != "")
            {
                noteToSave.Name = noteToEdit.Name;
                noteToSave.Content = noteToEdit.Content;
                try
                {
                    Note oldNote = new Note(){ Name = editableNote.Name, Content= editableNote.Content};
                    notes.Remove(oldNote);
                }
                catch
                {

                }
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(noteToSave.Name);
                    sw.WriteLine(noteToSave.Content);
                    Note newnote = new Note { Name = noteToSave.Name, Content = noteToSave.Content };
                    notes.Add(newnote);
                    this.notes = notes;
                    sw.Close();
                }
            }
            
            
            
            
            //new NoteViewModel();
        }, () => name != "" || content != "");

        EditNoteCommand = new Command((object args) =>
        {
            Note selectedNote = new Note();
            selectedNote = (Note)args;
            if (args is Note)
            {
                noteToEdit.Name = selectedNote.Name;
                noteToEdit.Content = selectedNote.Content;
                editableNote.Name = noteToEdit.Name;
                editableNote.Content = noteToEdit.Content;
                //this.noteToEdit = editableNote;
            }
        });
        

        DelNoteCommand = new Command((object args) =>
        {
            Note selectedNote = new Note();
            selectedNote = (Note)args;
            if (args is Note)
            {
                
                notes.Remove(selectedNote);
                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    foreach (Note note in notes)
                    {
                        sw.WriteLine(note.Name);
                        sw.WriteLine(note.Content);
                    }
                    sw.Close();
                }
            }
        });

        disableDelCommand = new Command(() =>

        {
            ((Command)DelNoteCommand).ChangeCanExecute();        });
    }

    public NoteViewModel(Note note)
    {





        string fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
        //ObservableCollection<Note> selnotes = new ObservableCollection<Note>();
        notes = new ObservableCollection<Note>();

        //selnotes = this.notes;
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
                notes.Add(fileNote);
            }
            sr.Close();
        }
        //notes.RemoveAt(notes.IndexOf(note));
        //noteToSave = new ShowNoteViewModel(note);
        EditNoteViewModel noteToEditLocal = this.noteToEdit;
        AddNewCommand = new Command(() =>
        {
            try
            {
                noteToSave.Name = noteToEdit.Name;
                noteToSave.Content = noteToEdit.Content;
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(noteToSave.Name);
                    sw.WriteLine(noteToSave.Content);
                    Note newnote = new Note { Name = noteToSave.Name, Content = noteToSave.Content };
                    notes.Add(newnote);
                    this.notes = notes;
                    sw.Close();
                }
            }
            catch
            {
                if (noteToEdit.Name != "" | noteToEdit.Content != "")
                {
                    using (StreamWriter sw = new StreamWriter(fileName, true))
                    {
                        sw.WriteLine(noteToEdit.Name);
                        sw.WriteLine(noteToEdit.Content);
                        Note newnote = new Note { Name = noteToEdit.Name, Content = noteToEdit.Content };
                        notes.Add(newnote);
                        this.notes = notes;
                        sw.Close();
                    }
                }
            }



            //new NoteViewModel();
        }, () => name != "" || content != "");

        
        AddOldCommand = new Command(() =>
        {
            /*Note foundNote = new Note();
            foundNote = notes.Where(n => n.name == oldName && n.content == oldContent);
            foundNote.name = name;
            foundNote.content = content;
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                foreach (Note note in notes)
                {
                    sw.WriteLine(note.name);
                    sw.WriteLine(note.content);
                }
                sw.Close();
            }*/
        });
        DelOldCommand = new Command(() =>
        {
            /*Note foundNote = new Note();
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
                }*/
        });

        DelNoteCommand = new Command((object args) =>

        {

            Note selectedNote = new Note();
            selectedNote = (Note)args;
            if (args is Note)
            {

                notes.Remove(selectedNote);
                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    foreach (Note note in notes)
                    {
                        sw.WriteLine(note.Name);
                        sw.WriteLine(note.Content);
                    }
                    sw.Close();
                }
            }

        });


    }
    //public string Name
    //{
    //    get => name;
    ////    set
    ////    {
    ////        if (name != value)
    ////        {
    ////            name = value;
    ////            OnPropertyChanged();
    ////        }
    ////    }
    ////}

    ////public string Content
    ////{
    ////    get => content;
    //    set
    //    {
    //        if (content != value)
    //        {
    //            content = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public void OnPropertyChanged([CallerMemberName] string prop = "")
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //}


}

