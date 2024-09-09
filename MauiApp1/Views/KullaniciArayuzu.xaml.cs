using MauiApp1.Dtos;
using MauiApp1.Services;


namespace MauiApp1.Views;

public partial class KullaniciArayuzu : ContentPage
{
	private readonly IKullaniciServices _services;
	private Kullanici _Kullanici;
	public KullaniciArayuzu()
	{
		InitializeComponent();
        _services = new KullaniciServices();
	}

	public void SetKullanici(Kullanici kullanici)
	{
		_Kullanici = kullanici;
        
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		var Kullanicilar = await _services.GetTumKullanýcýlar();
		var Kullanici = Kullanicilar.FirstOrDefault(x => x.KentKartId == _Kullanici.KentKartId);
		moneybalance.Text = Kullanici.Bakiye.ToString();
		kenkartId.Text = Kullanici.KentKartId.ToString();
		kullaniciAdi.Text = Kullanici.Ad.ToString();
		kullaniciSoyadi.Text = Kullanici.Soyad.ToString();
		kayitTarihi.Text = Kullanici.KayitTarihi.ToString();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await buton1.FadeTo(0,140);
        await buton1.FadeTo(1, 140);
		SifreDegistir þifreDegistir = new SifreDegistir();
        þifreDegistir.setkulanici(_Kullanici);
		await Navigation.PushAsync(þifreDegistir);
    }

    private async void buton2_Clicked(object sender, EventArgs e)
    {
        await buton2.FadeTo(0, 140);
        await buton2.FadeTo(1, 140);
		BakiyeYukle bakiyeYukle = new BakiyeYukle();
        bakiyeYukle.setKullanicilar(_Kullanici);
		await Navigation.PushAsync(bakiyeYukle);

    }

    private async void buton3_Clicked(object sender, EventArgs e)
    {
        await buton3.FadeTo(0, 140);
        await buton3.FadeTo(1, 140);
        Transfer transfer = new Transfer();
        transfer.setKullanici(_Kullanici);
        await Navigation.PushAsync(transfer);

    }

    private async void buton4_Clicked(object sender, EventArgs e)
    {
        await buton4.FadeTo(0, 140);
        await buton4.FadeTo(1, 140);
        Hakkýnda hakkýnda = new Hakkýnda();
        await Navigation.PushAsync(hakkýnda);

    }
}