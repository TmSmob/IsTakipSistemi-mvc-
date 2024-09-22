using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMvc2.Filters
{
    public class AuthFilter : FilterAttribute, IAuthorizationFilter
    {
        protected int yetkiTur;
        public AuthFilter(int yetkiTur) 
        {
            this.yetkiTur=yetkiTur;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            int yetkiTurid = Convert.ToInt32(filterContext.HttpContext.Session["PersonelYetkiTurId"]);
            if (this.yetkiTur!=yetkiTurid)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}