﻿using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tippsApp.Models;

namespace tippsApp.ViewModels;

public class MainViewModel : ViewModelBase
{

    public string notesPath = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    public ObservableCollection<Note> Notes { get; set; }
    public ICommand RemoveCommand { get; set; }
    

    public MainViewModel()
    {
        Notes = new ObservableCollection<Note>();

        if (!File.Exists(notesPath))
        {
            File.Create(notesPath);
        }
        else
        {
            string? line;

            using (StreamReader reader = new StreamReader(notesPath))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    Notes.Add(new Note() { Name = line, Content = reader.ReadLine(), ChangedTime = reader.ReadLine() });
                }

                reader.Close();
            }
        }

        RemoveCommand = new Command((args) =>
        {
            Note selectedNote = new Note();
            selectedNote = (Note)args;
            if (args is Note)
            {

                Notes.Remove(selectedNote);
                using (StreamWriter sw = new StreamWriter(notesPath, false))
                {
                    foreach (Note note in Notes)
                    {
                        sw.WriteLine(note.Name);
                        sw.WriteLine(note.Content);
                        sw.WriteLine(note.ChangedTime);
                    }
                    sw.Close();
                }
            }

        });


    }
}

