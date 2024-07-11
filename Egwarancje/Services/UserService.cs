using Egwarancje.Utils;
using EgwarancjeDbLibrary.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Egwarancje.Services;

public struct MessageResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

public class UserService : IDisposable
{
    public User User { get; private set; }
    public bool IsAuthenticated { get; private set; }

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    private HttpClient _httpClient;

    private readonly string _baseAddress;
    private readonly string _url;

    private bool _disposed = false;


    public UserService()
    {
        User = new();
        var httpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
        };
        _httpClient = new HttpClient(httpClientHandler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? Consts.AndroidAddress : Consts.WindowsAddress;
        _url = $"{_baseAddress}/api";

        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    public async Task<MessageResponse> RestartPassword(string email)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return new MessageResponse() { Success = false, Message = "Brak połączenia z internetem" };

        try
        {
            var requestUri = $"{_url}/Account/ResetPassword";
            var contentPost = SetPostContent(email);
            var response = await _httpClient.PostAsync(requestUri, contentPost);

            string content = await response.Content.ReadAsStringAsync();
            MessageResponse messageResponse = JsonSerializer.Deserialize<MessageResponse>(content, _jsonSerializerOptions);

            return messageResponse;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            return new MessageResponse() { Success = false, Message = "Błąd API" };
        }
    }

    public async Task<MessageResponse> RegisterAsync(User newUser)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return new MessageResponse() { Success = false, Message = "Brak połączenia z internetem" };

        try
        {
            var requestUri = $"{_url}/Account/Register";
            var contentPost = SetPostContent(newUser);
            var response = await _httpClient.PostAsync(requestUri, contentPost);

            string content = await response.Content.ReadAsStringAsync();
            MessageResponse messageResponse = JsonSerializer.Deserialize<MessageResponse>(content, _jsonSerializerOptions);

            return messageResponse;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            return new MessageResponse() { Success = false, Message = "Błąd API" };
        }
    }
    public async Task<MessageResponse> LoginAsync(string email, string password)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return new MessageResponse() { Success = false, Message = "Brak połączenia z internetem" };

        try
        {
            var requestUri = $"{_url}/User/Get?email={email}&password={password}";
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                User? user = JsonSerializer.Deserialize<User>(content, _jsonSerializerOptions);
                if (user is null)
                {
                    IsAuthenticated = false;
                    return new MessageResponse() { Success = false, Message = "Nieprawidłowe dane logowania" };
                }

                if(!user.IsActivated)
                {
                    return new MessageResponse() { Success = false, Message = "Konto nie zostało aktywowane" };
                }

                User = user;
                IsAuthenticated = true;
                return new MessageResponse() { Success = true, Message = "Pomyślnie zalogowano" };
            }

            IsAuthenticated = false;
            return new MessageResponse() { Success = false, Message = "Błąd połączenia z API" };
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            IsAuthenticated = false;
            return new MessageResponse() { Success = false, Message = "Błąd API" };
        }
    }
    public void Logout()
    {
        User = new();
        IsAuthenticated = false;
    }

    public async Task<bool> UpdateUserAsync()
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return false;

        try
        {
            var requestUri = $"{_url}/User/Update";

            User updateUser = new()
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
                Password = User.Password,
            };
            var contentPost = SetPostContent(updateUser);
            var response = await _httpClient.PutAsync(requestUri, contentPost);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            IsAuthenticated = false;
            return false;
        }
    }

    public async Task<Order?> CreateOrderAsync(Order order)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return null;

        try
        {
            var requestUri = $"{_url}/Order/Add";
            var contentPost = SetPostContent(order);
            var response = await _httpClient.PostAsync(requestUri, contentPost);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                Order? dbOrder = JsonSerializer.Deserialize<Order>(content, _jsonSerializerOptions);
                return dbOrder;
            }

            return null;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<bool> CreateOrderSpecAsync(OrderSpec orderSpec)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return false;

        try
        {
            var requestUri = $"{_url}/Order/Spec/Add";
            var content = SetPostContent(orderSpec);
            var response = await _httpClient.PostAsync(requestUri, content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<Warranty?> CreateWarrantyAsync(Warranty warranty)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return null;

        try
        {
            var requestUri = $"{_url}/Warranty/Add";
            var contentPost = SetPostContent(warranty);
            var response = await _httpClient.PostAsync(requestUri, contentPost);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                Warranty? dbWarranty = JsonSerializer.Deserialize<Warranty>(content, _jsonSerializerOptions);
                return dbWarranty;
            }

            return null;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<WarrantySpec?> CreateWarrantySpecAsync(WarrantySpec warrantySpec)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return null;

        try
        {
            var requestUri = $"{_url}/Warranty/Spec/Add";
            var contentPost = SetPostContent(warrantySpec);
            var response = await _httpClient.PostAsync(requestUri, contentPost);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                WarrantySpec? dbWarrantySpec = JsonSerializer.Deserialize<WarrantySpec>(content, _jsonSerializerOptions);
                return dbWarrantySpec;
            }

            return null;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<Attachment?> CreateAttachmentAsync(Attachment attachment)
    {
        bool access = await CheckForNetworkAccess();
        if (!access) return null;

        try
        {
            var requestUri = $"{_url}/Warranty/Attachment/Add";
            var contentPost = SetPostContent(attachment);
            var response = await _httpClient.PostAsync(requestUri, contentPost);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                Attachment? dbAttachment = JsonSerializer.Deserialize<Attachment>(content, _jsonSerializerOptions);
                return dbAttachment;
            }

            return null;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
            return null;
        }
    }

    private StringContent SetPostContent(object value)
    {
        string json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
        StringContent content = new(json, System.Text.Encoding.UTF8, "application/json");
        return content;
    }

    private async Task<bool> CheckForNetworkAccess()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Brak dostępu do internetu", "OK");
            return false;
        }

        return true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
