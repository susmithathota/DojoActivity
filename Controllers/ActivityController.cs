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
    public class ActivityController : Controller
    {
       private DojoActivityContext _context;
        public ActivityController(DojoActivityContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/ActivityList")]
        public IActionResult ActivityList(){
            ViewBag.ActivityList = new List<Activity>();
            ViewBag.Participant=HttpContext.Session.GetString("ParticipantError");
            // ViewBag.JoinError="";
            // ViewBag.LeaveError="";
            int? userId= HttpContext.Session.GetInt32("UserId");
            ViewBag.MyUser= _context.Users.SingleOrDefault(u =>u.UserId == (int)userId);
            List<Activity> ActivityList = _context.Activitys.Include(x => x.User).Include(y=>y.Participants).ToList();
            ActivityList = ActivityList.OrderByDescending(o =>o.CreatedAt).ToList();
            if(ActivityList.Count>0){
                ViewBag.ActivityList= ActivityList;
            }
            return View("ActivityList");
        }
        [HttpPost]
        [Route("/NewActivity")]
        public IActionResult NewActivity(ActivityViewModel model, string durationRange){
            ViewBag.ActivityErrors=new List<string>();
            if(ModelState.IsValid){
                int? userId= HttpContext.Session.GetInt32("UserId");
                Activity newActivity =new Activity{
                    Title = model.Title,
                    Description= model.Description,
                    DateOfActivity= model.DateOfActivity,
                    TimeOfActivity=model.TimeOfActivity,
                    Duration=model.Duration +" " +durationRange,
                    UserId=(int)userId
                };
                _context.Activitys.Add(newActivity);
                _context.SaveChanges();
                return RedirectToAction("/ActivityList");
            }
            ViewBag.ActivityErrors= ModelState.Values;
            return View("NewActivity");
        }

        [HttpGet]
        [Route("/Activity/{AId}")]
        public IActionResult ShowActivity(int AId){
            int? userId= HttpContext.Session.GetInt32("UserId");
            ViewBag.MyUser= _context.Users.SingleOrDefault(u =>u.UserId == (int)userId);
            Participant isParticipant= _context.Participants.SingleOrDefault(a => a.ActivityId == AId && a.UserId == (int)userId);
            if(isParticipant == null){
                ViewBag.ShowJoin = true;
            }
            else{
                 ViewBag.ShowJoin = false;
            }
            ViewBag.ActivityById = _context.Activitys.Include(w =>w.User).Include(x => x.Participants).ThenInclude(y =>y.User).SingleOrDefault(a =>a.ActivityId == AId);
            return View("ShowActivity");
        }

        [HttpGet]
        [Route("/Join/{AId}")]
        public IActionResult AddParticipant(int AId){
            ViewBag.JoinError="";
             HttpContext.Session.SetString("ParticipantError"," ");
            int? userId= HttpContext.Session.GetInt32("UserId");
            User CurrentUser = _context.Users.Include(x=> x.Activities).SingleOrDefault( u =>u.UserId == (int)userId);
            List<Activity> CurrentUserActivities = CurrentUser.Activities;
            Activity ActivityToJoin = _context.Activitys.SingleOrDefault(a =>a.ActivityId ==AId);
            Participant CheckUser = _context.Participants.SingleOrDefault(X => X.UserId==(int)userId && X.ActivityId == AId);
            if(CheckUser != null)
            { 
                 HttpContext.Session.SetString("ParticipantError","Already Joined the activity");
            }
            else
            {  
                // if(CurrentUserActivities.Count>0){
                //     foreach(Activity act in CurrentUserActivities){
                //         if(act.DateOfActivity != ActivityToJoin.DateOfActivity){
                //             if(act.TimeOfActivity != act.TimeOfActivity){
                                    Participant newParticipant =new Participant{
                                        UserId= (int)userId,
                                        ActivityId = AId
                                    };
                                    _context.Participants.Add(newParticipant);
                                    _context.SaveChanges();
                //                 }
                //             }
                //         else{
                //             HttpContext.Session.SetString("ParticipantError","Activity times overlapped");
                //         }
                //     }
                // }
            }
            return RedirectToAction("ActivityList");
        }

        [HttpGet]
        [Route("/Leave/{AId}")]
        public IActionResult LeaveActivity(int AId){
            ViewBag.LeaveError="";
            HttpContext.Session.SetString("ParticipantError"," ");
            int? userId= HttpContext.Session.GetInt32("UserId");
            Participant CheckUser = _context.Participants.SingleOrDefault(X => X.UserId==(int)userId && X.ActivityId == AId);
            if(CheckUser != null){
                _context.Participants.Remove(CheckUser);
                _context.SaveChanges();
            }
            else{
                HttpContext.Session.SetString("ParticipantError","You are not part of the activity");
            }
            return RedirectToAction("ActivityList"); 
        }  

        [HttpGet]
        [Route("/Delete/{AId}")]
        public IActionResult DeleteActivity(int AId){
            int? userId= HttpContext.Session.GetInt32("UserId");
            Activity activityToDelete =_context.Activitys.SingleOrDefault(a=> a.UserId == (int)userId && a.ActivityId ==AId); 
            if(activityToDelete != null){
                _context.Activitys.Remove(activityToDelete);
                _context.SaveChanges();
            }
            return RedirectToAction("ActivityList");
        }  

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logoff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "User");
        }

    }
}