using MauiApp1.Dtos;
using MauiApp1.Services; 

namespace MauiApp1.Views;

public partial class Transfer : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	private Kullanici _Kullanici;
	public Transfer()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();
	}
	public void setKullanici(Kullanici kullanici)
	{
		_Kullanici = kullanici;
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		var kullanicilar = await _kullaniciServices.GetTumKullanýcýlar();
		var kullanici = kullanicilar.FirstOrDefault(x => x.KentKartId == _Kullanici.KentKartId);
		if (kullanici != null)
		{
			kullaniciBakiyesi.Text = kullanici.Bakiye.ToString(); 
		}
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		bool secenek = await DisplayAlert("TRANSFER ÝÞLEMÝ","Göndermek istediðinze eminmisiniz ?","Evet","Hayýr");
		if(secenek == true) {
			if (girilenKentkartID.Text is not null ) { 
				var kullanicilar = await _kullaniciServices.GetTumKullanýcýlar();
				

                var kullanici = kullanicilar.FirstOrDefault(x => x.KentKartId == double.Parse(girilenKentkartID.Text));
				if (kullanici is not null && kullanici.Ad == ad.Text && kullanici.Soyad == soyad.Text)
				{
					if (gonderlimekÝstenenMiktar.Text is not null) { 
						if(kullanici.KentKartId.ToString() != _Kullanici.KentKartId.ToString()) { 
							if (kullanici.Bakiye >= int.Parse(gonderlimekÝstenenMiktar.Text))
							{


								var gonderenKullaniciGuncelleDto = new KullaniciGüncelleDto
								{
									Ad = _Kullanici.Ad,
									Soyad = _Kullanici.Soyad,
									Bakiye = _Kullanici.Bakiye - int.Parse(gonderlimekÝstenenMiktar.Text),
									Id = _Kullanici.Id,
									KentKartId = _Kullanici.KentKartId,
									TelefonNo = _Kullanici.TelefonNo,
									Password = _Kullanici.Password,
								};
								await _kullaniciServices.KullaniciGuncelle(gonderenKullaniciGuncelleDto);

								var aliciKullaniciGuncelleDto = new KullaniciGüncelleDto
								{
									Ad = kullanici.Ad,
									Soyad = kullanici.Soyad,
									Bakiye = kullanici.Bakiye + int.Parse(gonderlimekÝstenenMiktar.Text),
									Id = kullanici.Id,
									KentKartId = kullanici.KentKartId,
									Password = kullanici.Password,
									TelefonNo = kullanici.TelefonNo,
								};
								await _kullaniciServices.KullaniciGuncelle(aliciKullaniciGuncelleDto);

								await DisplayAlert("BAÞARI", $"Para Ýþlemi {kullanici.Ad} {kullanici.Soyad} Baþarýlý Þekilde Gönderilmiþtir \n Güncel Bakiyeniz {_Kullanici.Bakiye - int.Parse(gonderlimekÝstenenMiktar.Text)}", "Tamam");

							}
							else
							{
								await DisplayAlert("", "Bakiyeniz yetersiz", "Tamam");
							}
                        }
						else
						{
							await DisplayAlert("","Kullanici Kendine Para Gönderemez","Tamam");
						}
                    }
					else
					{
						await DisplayAlert("","Gönderilmek istenen miktarý giriniz !!","Tamam");
					}
				}
					else
					{
						await DisplayAlert("","Kullanici Bulunamadý","Tamam");
					}
            }
			else
			{
				await DisplayAlert("","Alanlar Boþ Býrakýlamaz","Tamam");
			}
        }
		
		else
		{
			await DisplayAlert("","Ýþlem iptal edildi","Tamam");
		}
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var kullanicilar = await _kullaniciServices.GetTumKullanýcýlar();
        var kullanici = kullanicilar.FirstOrDefault(x => x.KentKartId == _Kullanici.KentKartId);
        if (kullanici != null)
        {
            kullaniciBakiyesi.Text = kullanici.Bakiye.ToString();
        }
    }
}