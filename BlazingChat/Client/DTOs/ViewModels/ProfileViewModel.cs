using System.Net.Http.Json;
using AutoMapper;
using BlazingChat.Client.DTOs.AutoMapper;
using BlazingChat.Shared.Models;

namespace BlazingChat.Client.DTOs.ViewModels
{
    public class ProfileViewModel : BaseDto<ProfileViewModel, User>, IProfileViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ProfileViewModel() { }

        public ProfileViewModel(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public string? Message { get; set; }
        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }

        public async Task UpdateProfile()
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"api/users/GetProfile/10");

            _mapper.Map<ProfileViewModel, User>(this, user);

            var response = await _httpClient.PutAsJsonAsync($"api/Users/UpdateProfile/10", user);

            Message = response.IsSuccessStatusCode ? "پروفایل با موفقیت بروزرسانی شد!" : "خطا در بروزرسانی پروفایل!";
        }

        public async Task GetProfile()
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"api/users/GetProfile/10");

            _mapper.Map(user, this);

            Message = "پروفایل با موفقیت لود شد!";
        }


        public static implicit operator ProfileViewModel(User? user)
            => new()
            {
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                EmailAddress = user?.EmailAddress,
                UserId = user.UserId
            };

        public static implicit operator User(ProfileViewModel? profileViewModel)
            => new()
            {
                FirstName = profileViewModel?.FirstName,
                LastName = profileViewModel?.LastName,
                EmailAddress = profileViewModel?.EmailAddress,
                UserId = profileViewModel.UserId
            };
    }
}
