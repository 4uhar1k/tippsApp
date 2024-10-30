using Microsoft.Maui.Controls.Shapes;
using System;
using System.IO;
using tippsApp.ViewModels;
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

    public AddNote(string name, string content)
    {
        InitializeComponent();
        BindingContext = thisContext;
        thisContext.Name = name;
        thisContext.Content = content;
        thisContext.editableNote.Name = name;
        thisContext.editableNote.Content = content;  
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