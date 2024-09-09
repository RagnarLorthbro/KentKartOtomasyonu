using MauiApp1.Dtos;
using MauiApp1.Services;


namespace MauiApp1.Views;

public partial class KayitOl : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	public KayitOl()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await kayitolbutonu.FadeTo(0, 150);
        await kayitolbutonu.FadeTo(1, 150);


        if (telefonNumarasi.Text != null && ad.Text != null && soyad.Text != null && sifre.Text != null)
		{

			if (telefonNumarasi.Text.Length == 10)
			{
				if (sifre.Text.Length > 6)
				{
					var telefonNumarasý = double.Parse(telefonNumarasi.Text);
					var kullanicilar = await _kullaniciServices.GetTumKullanýcýlar();
					var kontrol = kullanicilar.FirstOrDefault(x => x.TelefonNo == telefonNumarasý);
					if (kontrol == null)
					{

						var kullanici = new KullaniciEkleDto
						{
							Ad = ad.Text,
							Soyad = soyad.Text,
							Password = sifre.Text,
							TelefonNo = telefonNumarasý,
							Bakiye = 0,
							KayitTarihi = DateTime.Now,
							KentKartId = GenerateRandomKentKartId()
						};
						await _kullaniciServices.KullaniciEkle(kullanici);
						bool secenek = await DisplayAlert("", "Kayit Ýþlemi Baþarýlý Þeklilde Tamamlandý", "Tamam", "Bilgilerim");
						if (secenek == false)
						{
							var kullaniciTelefonNumarasi = double.Parse(telefonNumarasi.Text);
							var yeniKullaniciListesi = await _kullaniciServices.GetTumKullanýcýlar();
							var ilkKisi = yeniKullaniciListesi.FirstOrDefault(x => x.TelefonNo == kullaniciTelefonNumarasi);
							await DisplayAlert("Bilgilerim", $" Kullanici Adi: {ad.Text} \n Kullanici Soyadi: {soyad.Text} \n Kullanici Telefon Numarasi: {telefonNumarasi.Text} \n Kullanici Kent-Kart Id: {ilkKisi.KentKartId} \n Kullanici Þifresi: {ilkKisi.Password} \n Kullanici Bakiyesi: {ilkKisi.Bakiye} \n Kullanici Kayit Tarihi: {ilkKisi.KayitTarihi}", "Tamam");
						}

					}
					else
					{
						await DisplayAlert("","Bu telefon numrasý zaten Kayýtlý","Tamam");
					}

				}
                else
                {
                   await DisplayAlert("", "Þifre uzunluðu 6'dan uzun deðil", "Tamam");
                }	
            }
            else
            {
               await DisplayAlert("", "Telefon Numarasý 10 haneden uzun veya kýsa", "Tamam");
            }


		}
        else
        {
            DisplayAlert("Hata", "Lütfen tüm alanlarý doldurunuz.", "Tamam");
        }
    }
    private int GenerateRandomKentKartId()
    {
        Random random = new Random();
        return random.Next(100000, 999999); // 6 haneli rastgele bir sayý üretir
    }
}