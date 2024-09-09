
using MauiApp1.Services;

namespace MauiApp1.Views;

public partial class AnaMenu : ContentPage
{
    private readonly IKullaniciServices _kullaniciServices;
    public AnaMenu()
    {
        InitializeComponent();
        _kullaniciServices = new KullaniciServices();
    }


    private async void giris_Clicked(object sender, EventArgs e)
    {
        await giris.FadeTo(0,100);
        await giris.FadeTo(1,100);

        if (telefonNumarasi.Text != null && sifre.Text != null) { 

        double telefonNo = double.Parse(telefonNumarasi.Text);

        var kullanicilar = await _kullaniciServices.GetTumKullanýcýlar();
        var kullanici = kullanicilar.FirstOrDefault( x => x.TelefonNo == telefonNo && x.Password == sifre.Text);

        if (kullanici != null)
        {

            KullaniciArayuzu kullaniciArayuzu = new KullaniciArayuzu();
                kullaniciArayuzu.SetKullanici(kullanici);
                await Navigation.PushAsync(kullaniciArayuzu);
        }
        else {
            await DisplayAlert("", "Telefon Numarasý veya Þifre Hatalý ", "Tamam");
        }
        }
        else
        {
            await DisplayAlert("", "Eksik Bilgi Giriþi", "Tamam");
        }

    }

    private async void kayit_Clicked(object sender, EventArgs e)
    {
        await kayit.FadeTo(0,100);
        await kayit.FadeTo(1,100);

        KayitOl kayitOl = new KayitOl();
        await Navigation.PushAsync(kayitOl);
    }

    private async void sifreUnuttum_Clicked(object sender, EventArgs e)
    {
        await sifreUnuttum.FadeTo(0,140);
        await sifreUnuttum.FadeTo(1, 140);
        SifreUnuttum SifreUnuttum = new SifreUnuttum();
        await Navigation.PushAsync(SifreUnuttum);
    }

    private async void hakkinda_Clicked(object sender, EventArgs e)
    {
        await hakkinda.FadeTo(0,140);
        await hakkinda.FadeTo(1, 140);

        Hakkýnda hakkýnda = new Hakkýnda();
        await Navigation.PushAsync(hakkýnda);
    }

}
