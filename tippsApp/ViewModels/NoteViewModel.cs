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
using tippsApp.Models;

namespace tippsApp.ViewModels;
public class NoteViewModel : ViewModelBase
{
    public ObservableCollection<Note> Notes { get; set; }
    public ICommand AddNewCommand { get; set; }
    public Note editableNote { get; set; }
    public Note noteToSave { get; set; }

    public NoteViewModel()
    {
        editableNote = new Note();
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
            if (Name != "" | Content != "")
            {
                Note oldNote = new Note();
                try
                {
                    oldNote = Notes.First(n => n.Name == editableNote.Name && n.Content == editableNote.Content);
                    Notes.Remove(oldNote);
                }
                catch (Exception e)
                {

                }
                Notes.Insert(0, new Note() { Name = Name, Content = Content });
                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    foreach (Note note in Notes)
                    {
                        sw.WriteLine(note.Name);
                        sw.WriteLine(note.Content);
                    }
                }
            }
        }, () => Name != "" || Content != "");
    }
}

