using System.Collections.Generic;
using System.Web.Mvc;
using School.Data;
using School.Entities;

namespace SchoolWithCodeSmithNettiers.Controllers
{
    public class ClassesController : Controller
    {
        //
        // GET: /Classes/

        public IEnumerable<Classes> GetAll()
        {
            return DataRepository.ClassesProvider.GetAll();
        }

    }
}
