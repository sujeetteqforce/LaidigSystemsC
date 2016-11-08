using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LaidigSystemsC.Models;
using System.Data.Entity;
using System.Web.Caching;
using System.Net;

namespace LaidigSystemsC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private const JobCacheAction= "";
        protected void Application_Start()
        {
            //Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<LaidigSystemsC.Models.OurDbContext>());


            //Database.SetInitializer<OurDbContext>(null);
            //Database.SetInitializer<OurDbContext>(new CreateDatabaseIfNotExists<OurDbContext>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
       // protected void Application_BeginRequest(object sender, EventArgs e)
       // {
       //     if (HttpContext.Cache["jobkey"] == null)
       //     {
       //         HttpContext.Current.Cache.Add("jobkey",
       //         "jobvalue", null,
       //         DateTime.MaxValue,
       //         TimeSpan.FromMinutes(2),
       //         CacheItemPriority.Default, JobCacheRemoved);
       //    }    
       //}
       // private static void JobCacheRemoved(string key,object value, CacheItemRemovedReason reason )
       // {          
            
       //     var client = new WebClient();
       //     client.Downloaddata(JobCacheAction);
       //     ScheduledJob();
       // }
       // private void ScheduledJob()
       // {

       // }
    }
}
