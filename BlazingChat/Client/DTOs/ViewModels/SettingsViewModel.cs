using System.Net.Http.Json;
using AutoMapper;
using BlazingChat.Client.DTOs.AutoMapper;
using BlazingChat.Shared.Models;

namespace BlazingChat.Client.DTOs.ViewModels;

public class SettingsViewModel : BaseDto<SettingsViewModel, User>, ISettingsViewModel
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public SettingsViewModel(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public SettingsViewModel()
    { }

    public bool Notifications { get; set; }
    public bool DarkTheme { get; set; }

    public async Task GetProfile()
    {
        var user = await _httpClient.GetFromJsonAsync<User>($"api/Users/GetProfile/{10}");
        LoadCurrentObject(FromEntity(_mapper, user));
    }

    public async Task Save()
    {
        await _httpClient.GetFromJsonAsync<User>($"api/Users/UpdateTheme?userId={10}&value={DarkTheme}");

        await _httpClient.GetFromJsonAsync<User>($"api/Users/UpdateNotifications?userId={10}&value={Notifications}");
    }

    private void LoadCurrentObject(ISettingsViewModel settingsViewModel)
    {
        DarkTheme = settingsViewModel.DarkTheme;
        Notifications = settingsViewModel.Notifications;
    }

    public override void CustomMappings(IMappingExpression<User, SettingsViewModel> mapping)
    {
        mapping.ForMember(dest => dest.DarkTheme,
            config =>
                config.MapFrom(user => user.DarkTheme != null && (long)user.DarkTheme != 0));

        mapping.ForMember(dest => dest.Notifications,
            config =>
                config.MapFrom(user => user.Notifications != null && (long) user.Notifications != 0));
    }
}