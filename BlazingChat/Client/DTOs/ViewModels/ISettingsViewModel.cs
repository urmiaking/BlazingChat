namespace BlazingChat.Client.DTOs.ViewModels;

public interface ISettingsViewModel
{
    public bool Notifications { get; set; }
    public bool DarkTheme { get; set; }

    public Task Save();
    public Task GetProfile();
}