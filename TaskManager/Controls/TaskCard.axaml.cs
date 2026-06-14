using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using TaskManager.Models;

namespace TaskManager.Controls;

public partial class TaskCard : UserControl
{
    private readonly TaskRepository _repo = new();


    public TaskCard()
    {
        InitializeComponent();
    }

    private async void CheckBoxChanged(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (sender is CheckBox cb &&
                DataContext is TodoTask task)
            {
                task.IsCompleted = cb.IsChecked ?? false;

                await _repo.UpdateAsync(task);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());

        }
    }
}