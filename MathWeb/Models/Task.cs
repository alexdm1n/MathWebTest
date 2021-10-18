using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MathWeb.Models;
using System.Threading.Tasks;

namespace MathWeb.Models
{
    public class TaskMath
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [Required]
        
        public string NameOfTask { get; set; }
        [Required]
        
        public string ShortDesc { get; set; }
        [Required]

        public string Topic { get; set; }
        [Required]
        
        public int Answer { get; set; }

        [Required]
        public string WhoMade { get; set; }
    }

}

