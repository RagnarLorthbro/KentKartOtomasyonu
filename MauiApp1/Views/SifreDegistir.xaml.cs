
using MauiApp1.Dtos;
using MauiApp1.Services;

namespace MauiApp1.Views;

public partial class SifreDegistir : ContentPage
{
    private readonly IKullaniciServices _kullaniciServices;
    private Kullanici _kullanici;
    public SifreDegistir()
    {
        InitializeComponent();
        _kullaniciServices = new KullaniciServices();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await buton.FadeTo(0, 140);
        await buton.FadeTo(1, 140);



        var kullanicilar = await _kullaniciServices.GetTumKullanýcýlar();
        var kullanici = kullanicilar.FirstOrDefault(x => x.Id == _kullanici.Id);
        if (kullanici != null)
        {
            new Kullanici
            {
                Ad = kullanici.Ad,
                Soyad = kullanici.Soyad,
                Id = _kullanici.Id,
                KayitTarihi = _kullanici.KayitTarihi,
                KentKartId = _kullanici.KentKartId,
                Bakiye = _kullanici.Bakiye,
                Password = _kullanici.Password,


            };


        }

        
    }
    public void setkulanici(Kullanici kullanici)
    {
        _kullanici = kullanici;
    }
}