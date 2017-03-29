using System;
using System.Collections.Generic;
using DojoActivity.Models;


namespace DojoActivity{
    public class User:BaseEntity{
        public int UserId{get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt{get;set;}
        
        public List<Activity> Activities{get;set;}
        public User(){
            CreatedAt=DateTime.Now;  
            Activities = new List<Activity>(); 
        }
    }

}