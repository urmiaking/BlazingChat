namespace BlazingChat.Client.DTOs.ViewModels;

public interface IProfileViewModel
{
    public string? Message { get; set; }
    public long UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }

    public Task UpdateProfile();

    public Task GetProfile();
}