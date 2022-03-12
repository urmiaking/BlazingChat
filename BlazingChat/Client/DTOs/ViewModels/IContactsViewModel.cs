﻿using BlazingChat.Shared;

namespace BlazingChat.Client.DTOs.ViewModels;

public interface IContactsViewModel
{
    public List<Contact>? Contacts { get; set; }
    public Task GetContacts();
}