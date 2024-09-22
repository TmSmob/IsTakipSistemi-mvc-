using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsTakipSistemiMvc2.Filters;
using IsTakipSistemiMvc2.Models;

namespace IsTakipSistemiMvc2.Controllers
{
    public class LoginController : Controller
    {
        IsTakipDataBaseEntities entity = new IsTakipDataBaseEntities();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.mesaj = null;
            return View();
        }
        [HttpPost, ExtFilter]
        public ActionResult Index(string kullaniciAd, string parola)
        {
            var personel = (from p in entity.Personeller where p.personelKullaniciAdi == kullaniciAd && p.personelParola == parola &&p.aktiflik==true select p).FirstOrDefault();

            if (personel != null)
            {
                var birim = (from b in entity.Birimler where b.birimId == personel.personelBirimId select b).FirstOrDefault();
                Session["PersonelAdSoyad"] = personel.personelAdSoyad;
                Session["PersonelId"] = personel.personelId;
                Session["PersonelBirimId"] = personel.personelBirimId;
                Session["PersonelYetkiTurId"] = personel.personelYetkiTurId;
                if (birim == null)
                {
                    return RedirectToAction("Index", "SistemYonetici");
                }
                if (birim.aktiflik == true)
                {
                    if (personel.yeniPersoneller==true)
                    {
                        return RedirectToAction("Index", "SifreKontrol");
                    }
                    switch (personel.personelYetkiTurId)
                    {
                        case 1:
                            return RedirectToAction("Index", "Yonetici");

                        case 2:
                            return RedirectToAction("Index", "Calisan");
                        default:
                            return View();
                    }
                }
                else
                {
                    ViewBag.mesaj = "Birimiziz Silindigi Icin Giris Yapamazsiniz";
                    return View();
                }
            }
            else
            {
                ViewBag.mesaj = "Kullanici Adi ya da Parola Yanlis";
                return View();
            }

        }
    }

}