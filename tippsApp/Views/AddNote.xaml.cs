using Microsoft.Maui.Controls.Shapes;
using System;
using System.IO;
using tippsApp.ViewModels;
using tippsApp.Models;
namespace tippsApp;

public partial class AddNote : ContentPage
{
    public NoteViewModel thisContext = new NoteViewModel();
    public AddNote()
	{
		InitializeComponent();
        BindingContext = new NoteViewModel();
        noteName.Placeholder = "Your note...";
        noteContent.Placeholder = "Your note...";
        noteName.PlaceholderColor = Colors.White;
    }

    public AddNote(Note note)
    {
        InitializeComponent();
        BindingContext = thisContext;
        thisContext.Name = note.Name;
        thisContext.Content = note.Content;
        thisContext.ChangedTime = note.ChangedTime;
        thisContext.editableNote.Name = note.Name;
        thisContext.editableNote.Content = note.Content;
        thisContext.editableNote.ChangedTime = note.ChangedTime;
    }

    public async void goBack(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }

    private async void saveNote(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());      
    }
}