using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Angular4DotNetMvc.Models;

namespace Angular4DotNetMvc.Controllers
{
    public class CoursesController : JsonController
    {

        private readonly RegistrationViewModelBuilder _registrationVmBuilder = new RegistrationViewModelBuilder();

        public ActionResult Index()
        {
            return Json(_registrationVmBuilder.GetCourseViewModels(), JsonRequestBehavior.AllowGet);
        }

    }
}
