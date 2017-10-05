using SoloDev.ManualMapper.ConsoleTest.Entities;
using SoloDev.ManualMapper.ConsoleTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloDev.ManualMapper.ConsoleTest.Mappings
{
    public class UserMap : ManualMap<User, UserModel>
    {
        public override void Map(User source, UserModel destination)
        {
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Age = source.Age;
            destination.TodoList = new List<TodoItemModel>();
            List<TodoItemModel> todoList = destination.TodoList as List<TodoItemModel>;

            foreach (var todoItem in source.TodoList)
            {
                todoList.Add(new TodoItemModel
                {
                    Title = todoItem.Title,
                    Content = todoItem.Content
                });
            }
        }
    }
}
