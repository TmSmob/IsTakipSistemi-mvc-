using IsTakipSistemiMvc2.Filters;
using IsTakipSistemiMvc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMvc2.Controllers
{
    public class SifreKontrolController : Controller
    {
        IsTakipDataBaseEntities entity= new IsTakipDataBaseEntities();
        // GET: SifreKontrol
        public ActionResult Index()
        {
            int personelId = Convert.ToInt32(Session["PersonelId"]);
            if (personelId == 0) return RedirectToAction("Index","Login");
            var personel=(from p in entity.Personeller where p.personelId == personelId select p).FirstOrDefault();
            ViewBag.mesaj = null;
            ViewBag.yetkiTurId = null;
            ViewBag.sitil = null;
            return View(personel);
        }
        [HttpPost, ActFilter("Parola Degistirildi")]
        public ActionResult Index(int personelId, string eskiParola ,string yeniParola ,string yeniParolaKontrol) 
        {
            var personel = (from p in entity.Personeller where p.personelId == personelId select p).FirstOrDefault();
            if (eskiParola!=personel.personelParola)
            {
                ViewBag.mesaj = "Eski Parolanizi Yanlis Girdiniz";
                ViewBag.Sitil = "alert alert-danger";
                return View(personel);
            }
            if (yeniParola!=yeniParolaKontrol)
            {
                ViewBag.mesaj = "Yeni Parola Ve Yeni Parola Tekrari Eslesmedi";
                ViewBag.Sitil = "alert alert-danger";
                return View(personel);
            }
            if (yeniParola.Length<6 || yeniParola.Length>15)
            {
                ViewBag.mesaj = "Yeni Parola En Az 6 Karakter En Cok 15 Karakter Olmalidir";
                ViewBag.Sitil = "alert alert-danger";
                return View(personel);
            }
            personel.personelParola = yeniParola;
            personel.yeniPersoneller=false;
            entity.SaveChanges();
            TempData["bilgi"] = personel.personelKullaniciAdi;
            ViewBag.mesaj = "Parolaniz Basariyla Degistirildi Ana Sayfaya Yonlendiriliyorsunuz";
            ViewBag.sitil = "alert alert-success";
            ViewBag.yetkiTurId=personel.personelYetkiTurId;
            return View(personel);
        }
    }
}