using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class StoryModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int storyID { get; set; }
        public string title { get; set; }
        public string story1 { get; set; }
        public Nullable<int> premium { get; set; }
        public string url { get; set; }
        public virtual Premium Premium1 { get; set; }
    }
}