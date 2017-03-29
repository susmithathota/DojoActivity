using System;
using System.ComponentModel.DataAnnotations;
using DojoActivity.Models;


namespace DojoActivity{
    public class Participant:BaseEntity{
        [Key]
        public int ParticipantId{get;set;}

        public int UserId{get;set;}
        public User User{get;set;}

        public int ActivityId{get;set;}
        public Activity Activity{get;set;}
    }
}