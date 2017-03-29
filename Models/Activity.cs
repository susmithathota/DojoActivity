using System;
using System.Collections.Generic;
using DojoActivity.Models;


namespace DojoActivity{
    public class Activity:BaseEntity{
        public int ActivityId{get;set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfActivity { get; set; }
        public string TimeOfActivity { get; set; }
        public string Duration{get;set;}
        public DateTime CreatedAt{get;set;}

        public int UserId{get;set;}
        public User User{get;set;}

        public List<Participant> Participants{get;set;}
        
        public Activity(){
            CreatedAt=DateTime.Now;  
            Participants = new List<Participant>(); 

        }
    }

}