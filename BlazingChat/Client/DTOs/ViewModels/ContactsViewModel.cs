using System.Net.Http.Json;
using AutoMapper;
using BlazingChat.Client.DTOs.AutoMapper;
using BlazingChat.Shared;
using BlazingChat.Shared.Models;
using IMapper = AutoMapper.IMapper;

namespace BlazingChat.Client.DTOs.ViewModels;

public class ContactsViewModel : BaseDto<ContactsViewModel, User>, IContactsViewModel
{
    public List<Contact>? Contacts { get; set; }

    private readonly HttpClient _httpClient;

    public ContactsViewModel()
    { }

    public ContactsViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Contacts = new List<Contact>();
    }

    public async Task GetContacts()
    {
        var users = await _httpClient.GetFromJsonAsync<List<User>>("api/Users/GetContacts");
        LoadCurrentObject(users);
    }

    private void LoadCurrentObject(IEnumerable<User>? users)
    {
        if (users == null) return;

        foreach (var user in users)
        {
            Contacts?.Add(user);
        }
    }
}