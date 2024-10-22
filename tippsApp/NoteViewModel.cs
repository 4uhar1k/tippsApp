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
public class NoteViewModel : INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler? PropertyChanged;

    //
    public Note note = new Note();
    //List<Note> notes = new List<Note>();
    public ObservableCollection<Note> notes { get; set; }
    public ICommand AddNewCommand { get; set; }
    public ICommand AddOldCommand { get; set; }
    public ICommand DelOldCommand { get; set; }
    public ICommand DelNoteCommand { get; set; }
    public ICommand disableDelCommand { get; set; }
    public bool isOld = false;

    public NoteViewModel()
    {
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
                fileNote.name = line;
                fileNote.content = sr.ReadLine();
                notes.Add(fileNote);
                
            }
            sr.Close();
        }

        AddNewCommand = new Command(() =>
        {
            if (name!="" | content!="")
            {
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(name);
                    sw.WriteLine(content);
                    Note newnote = new Note { name = name, content = content };
                    notes.Add(newnote);
                    this.notes = notes;
                    sw.Close();
                }
            }
            
            //new NoteViewModel();
        }, () => name != "" || content != "");
        AddOldCommand = new Command((object args) =>
        {
            /*Note selectedNote = new Note();
            selectedNote = (Note)args;
            if (args is Note)
            {

                notes.Remove(selectedNote);
                notes.Add(new Note() { name = name, content = content });
                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    foreach (Note note in notes)
                    {
                        sw.WriteLine(note.name);
                        sw.WriteLine(note.content);
                    }
                    sw.Close();
                }
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
                        sw.WriteLine(note.name);
                        sw.WriteLine(note.content);
                    }
                    sw.Close();
                }
            }
        });

        disableDelCommand = new Command(() =>

        {
            ((Command)DelNoteCommand).ChangeCanExecute();
            OnPropertyChanged();
        });
    }

    public NoteViewModel(Note note)
    {
        
        if (isOld == false)
        {
            this.note = note;
            isOld = true;
        }
        else
        {
            isOld = false;
        }
        
        
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
                fileNote.name = line;
                fileNote.content = sr.ReadLine();
                notes.Add(fileNote);
            }
            sr.Close();
        }
        notes.RemoveAt(notes.IndexOf(note));
        AddNewCommand = new Command(() =>
        {

            string? line;

            /* using (StreamReader sr = new StreamReader(fileName))
             {
                 while ((line = sr.ReadLine()) != null)
                 {

                     if(line==note.name && )
                     Note fileNote = new Note();
                     fileNote.name = line;
                     fileNote.content = sr.ReadLine();
                     notes.Add(fileNote);
                 }
                 sr.Close();
             }*/
            //int index = notes.IndexOf(note);
            //Note note1 = new Note() { name = noteName, content = noteContent };
            //notes.Remove(this.note);
            //notes = notes;

            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                
                //sw.WriteLine(name);
                //sw.WriteLine(content);
                Note newnote = new Note { name = name, content = content };
                notes.Add(newnote);
                //this.notes = selnotes;
                foreach (Note listNote in notes)
                {
                    sw.WriteLine(listNote.name);
                    sw.WriteLine(listNote.content);
                }
                sw.Close();
            }
            //new NoteViewModel();
        }, () => name!="" || content!="");
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
                        sw.WriteLine(note.name);
                        sw.WriteLine(note.content);
                    }
                    sw.Close();
                }
            }
        
        });

            
    }

    

    public string name
    {
        get => note.name;
        set
        {
            if (note.name != value)
            {
                note.name = value;
                OnPropertyChanged();
            }
        }
    }

    public string content
    {
        get => note.content;
        set
        {
            if (note.content != value)
            {
                note.content = value;
                OnPropertyChanged();
            }
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop="")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        ((Command)AddNewCommand).ChangeCanExecute();
    }
}

