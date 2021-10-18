using MathWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathWeb.Controllers
{
    public class TaskController : Controller
    {
        private readonly ConnectionStringClass context;

        public TaskController(ConnectionStringClass context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Details(Guid? Id, int? answer)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.UserId == Id);
            ViewBag.Right = task.Answer;
            ViewBag.Your = answer;
            if (answer != null)
            {
                if (answer == task.Answer)
                {
                    ViewBag.Message = "Your answer is correct!";
                }
                else
                {
                    ViewBag.Message = "Your answer is not correct, try again!";
                }
            }
            return View(task);
        }

        
        public async Task<IActionResult> Edit(Guid Id)
        {
           if(Id == null)
            {
                return NotFound();
            }
            var TaskUpdate = await context.Tasks.FirstOrDefaultAsync(s => s.UserId == Id);
            if (await TryUpdateModelAsync<TaskMath>(TaskUpdate,"",s => s.NameOfTask, s=> s.ShortDesc, s => s.Topic, s => s.WhoMade, s => s.Answer))
            {
                try
                {
                    await context.SaveChangesAsync();
                }
                catch
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(TaskUpdate);
        }

        
        public async Task<IActionResult> Delete(Guid Id)
        {
            var task = await context.Tasks.FindAsync(Id);
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            return View();
        }
    }
}
