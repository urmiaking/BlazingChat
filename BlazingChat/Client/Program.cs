using BlazingChat.Client;
using BlazingChat.Client.DTOs.AutoMapper;
using BlazingChat.Client.DTOs.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.InitializeAutoMapper();
builder.Services.AddHttpClient<IProfileViewModel, ProfileViewModel>(
    "BlazingChatClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient<ISettingsViewModel, SettingsViewModel>(
    "BlazingChatClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient<IContactsViewModel, ContactsViewModel>(
    "BlazingChatClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();