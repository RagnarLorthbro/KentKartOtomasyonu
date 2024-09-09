using MauiApp1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class UrlHelper
    {
        private static string BaseUrl = "https://localhost:7158" +
            "";
        public static string kullaniciUrl = $"{BaseUrl}/Kullanici";

    }

    public abstract class BaseServices
    {
        protected HttpClient _client;
        protected JsonSerializerOptions _serializerOptions;
        protected BaseServices()
        {
#if DEBUG && ANDROID
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _client = new HttpClient();
#endif

            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }

    public class HttpsClientHandlerService
    {

        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = IsHttpsLocalhost
        };
        return handler;
#else
     throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if IOS
    public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
    {
        return url.StartsWith("https://localhost");
    }
#endif
    }





    public class KullaniciServices : BaseServices, IKullaniciServices
    {
        async Task<List<Kullanici>> IKullaniciServices.GetTumKullanıcılar()
        {
            Uri uri = new Uri(UrlHelper.kullaniciUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var sonuc = JsonSerializer.Deserialize<List<Kullanici>>(content, _serializerOptions);
                return sonuc;
            }
            return new List<Kullanici>();
        }

        async Task IKullaniciServices.KullaniciEkle(KullaniciEkleDto kullanici)
        {
            Uri uri = new Uri(UrlHelper.kullaniciUrl);
            JsonContent content = JsonContent.Create(kullanici);
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {

            }
        }

        async Task IKullaniciServices.KullaniciGuncelle(KullaniciGüncelleDto kullanici)
        {
            JsonContent jsonContent = JsonContent.Create(kullanici);
            await _client.PutAsync(UrlHelper.kullaniciUrl, jsonContent);

        }

        async Task IKullaniciServices.KullaniciSil(int id)
        {
            Uri uri = new Uri($"{UrlHelper.kullaniciUrl}?id = {id}");
            HttpResponseMessage respone = await _client.GetAsync(uri);
            if (respone.IsSuccessStatusCode) { }
        }
    }
}
