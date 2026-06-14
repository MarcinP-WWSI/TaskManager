using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private readonly TaskRepository _repo = new();
    private readonly TaskStatisticsService _statsService = new();

    public ObservableCollection<TodoTask> Tasks { get; } = new();

    private TaskStatistics _stats = new();
    public TaskStatistics Stats
    {
        get => _stats;
        set => this.RaiseAndSetIfChanged(ref _stats, value);
    }

    private string _newTitle = string.Empty;
    public string NewTitle
    {
        get => _newTitle;
        set => this.RaiseAndSetIfChanged(ref _newTitle, value);
    }

    private string _newDescription = string.Empty;
    public string NewDescription
    {
        get => _newDescription;
        set => this.RaiseAndSetIfChanged(ref _newDescription, value);
    }

    public ReactiveCommand<Unit, Unit> LoadCommand { get; }
    public ReactiveCommand<Unit, Unit> AddCommand { get; }

    public MainWindowViewModel()
    {
        LoadCommand = ReactiveCommand.CreateFromTask(LoadAsync);
        AddCommand = ReactiveCommand.CreateFromTask(AddAsync);

        _ = LoadAsync();
    }

    private async Task LoadAsync()
    {
        var tasks = await _repo.GetAllAsync();
        Tasks.Clear();
        foreach (var t in tasks)
            Tasks.Add(t);

        Stats = _statsService.CalculateStatsParallel(Tasks);
    }

    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTitle))
            return;

        var task = new TodoTask
        {
            Title = NewTitle,
            Description = NewDescription
        };

        await _repo.AddAsync(task);
        NewTitle = string.Empty;
        NewDescription = string.Empty;

        await LoadAsync();
    }
}
