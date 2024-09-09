using MauiApp1.Dtos;
using MauiApp1.Services;


namespace MauiApp1.Views;

public partial class BakiyeYukle : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	private Kullanici _Kullanici;
    private int _selectedAmount = 0;
    public BakiyeYukle()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		
		if (_selectedAmount != 0)
		{

			

			if(kartNumarasý.Text is not null )
			{
				if (kartNumarasý.Text.Length == 12) { 
					if(ayPicker.SelectedIndex != -1 && yilPicker.SelectedIndex != -1 ) {
						
							
						if(cvv.Text	 is not null) { 
								if (cvv.Text.Length == 3) {
									if (check.IsToggled == true) { 
										var yuklenmekIstenenBakiye = _selectedAmount;

										var kullaniciGuncelleDtos = new KullaniciGüncelleDto
										{
											Ad = _Kullanici.Ad,
											Soyad = _Kullanici.Soyad,
											Bakiye = yuklenmekIstenenBakiye + _Kullanici.Bakiye,
											Id = _Kullanici.Id,
											KentKartId = _Kullanici.KentKartId,
											Password = _Kullanici.Password,
											TelefonNo =_Kullanici.TelefonNo,
							
										};
										await _kullaniciServices.KullaniciGuncelle(kullaniciGuncelleDtos);
										await DisplayAlert("BAÞARILI",$"Bakiyenize {yuklenmekIstenenBakiye} Tl baþarýlý þekilde yükleme yapýlmýþtýr","Tamam");
									}
									else
									{
										await DisplayAlert("HATA", "Sözleþmeyi kabul etmediniz ","Tamam");
									}

								}
                            else
                            {
                                await DisplayAlert("HATA", "CVV eksik girdiniz", "Tamam");
                            }
                        }
						else
						{
							await DisplayAlert("","CVV Alanýný Doldurmadýnýz","Tamam");
						}
                        

				}
				else
				{
					await DisplayAlert("HATA", "Son kullanma tarihini girmediniz", "Tamam");
				}
                }
				else
				{
                    await DisplayAlert("HATA", "Kart numaranýzý doðru giriniz 12 karakter olmak zorunda ", "Tamam");
                }
            }
			else
			{
				await DisplayAlert("HATA", "Kart Numaralý Alaný Doldurunuz.", "Tamam");
			}
		}
		else
		{
			await DisplayAlert("HATA", "Lütfen Seçim Yapýnýz...","Tamam");
		}
    }
	public void setKullanicilar(Kullanici kullanici)
	{
		_Kullanici = kullanici;
	}

    private async void Radiobutton1_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = sender as RadioButton;
		if(radioButton is not null)
		{
		 _selectedAmount = int.Parse(radioButton.Content.ToString());
		}
		else
		{
            await DisplayAlert("", "Lütfen Seçim Yapýnýz...", "Tamam");
        }
    }
}