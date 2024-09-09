using MauiApp1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal interface IKullaniciServices
    {
        Task<List<Kullanici>> GetTumKullanıcılar();

        Task KullaniciEkle(KullaniciEkleDto kullanici);

        Task KullaniciGuncelle(KullaniciGüncelleDto kullanici);

        Task KullaniciSil(int id);

    }
}
