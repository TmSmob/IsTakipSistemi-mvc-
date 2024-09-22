using IsTakipSistemiMvc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMvc2.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {
        IsTakipDataBaseEntities entity = new IsTakipDataBaseEntities();
        protected string aciklama;
        public ActFilter(string aciklama) 
        {
        this.aciklama = aciklama;
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller.TempData["bilgi"] != null)
            {
                Loglar log = new Loglar();
                log.logAciklama = this.aciklama + " (" + filterContext.Controller.TempData["bilgi"] + ")";
                log.actionAd = filterContext.ActionDescriptor.ActionName;
                log.controllerA = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                log.tarih = DateTime.Now;
                log.personelId = Convert.ToInt32(filterContext.HttpContext.Session["PersonelId"]);
                entity.Loglar.Add(log);
                entity.SaveChanges();
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}