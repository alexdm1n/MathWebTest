using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MathWeb.Models;
using MathWeb.Controllers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MathWeb.Data
{
    public class TasksToTable
    {
        private readonly ConnectionStringClass context;

        public TasksToTable(ConnectionStringClass context)
        {
            this.context = context;
        }
        public TaskMath GetTaskById(Guid Id)   //getbyid
        {
            return context.Tasks.Single(x => x.UserId == Id);
        }
        public IQueryable<TaskMath> GetByOwner(string name)
        {
            return context.Tasks.OrderBy(x => x.WhoMade == name);
        }
        public IQueryable<TaskMath> GetTasks()   //getall
        {
            return context.Tasks.OrderBy(x => x.Topic);
        }
      
        public Guid SaveArticle(TaskMath entity)  //edit
        {
            if (entity.UserId == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.UserId;
        }
        public void DeleteArticle(TaskMath entity) //del
        {
            context.Tasks.Remove(entity);
            context.SaveChanges();
        }
    }
}
