using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoActivity.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DojoActivity.Controllers
{
    public class ParticipantController : Controller
    {
       private DojoActivityContext _context;
        public ParticipantController(DojoActivityContext context)
        {
            _context = context;
        }

    }
}
