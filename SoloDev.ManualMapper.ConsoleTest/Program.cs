using SoloDev.ManualMapper.ConsoleTest.Entities;
using SoloDev.ManualMapper.ConsoleTest.Mappings;
using SoloDev.ManualMapper.ConsoleTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoloDev.ManualMapper.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Mapper.Configure(cfg =>
            {
                cfg.ServiceProvider = Activator.CreateInstance;
            });
            
            Mapper.RegisterMapsAssembly(typeof(Program).Assembly);

            Console.WriteLine($"Test 1: {Test1()}");
            Console.WriteLine($"Test 2: {Test2()}");

            Console.Read();
        }

        public static bool Test1()
        {
            User user = CreateUser();
            UserModel userModel = Mapper.Map<UserModel>(user);

            return user.FirstName == userModel.FirstName
                && user.LastName == userModel.LastName
                && user.Age == userModel.Age
                && user.TodoList.Count() == userModel.TodoList.Count();
        }
        public static bool Test2()
        {
            User user = CreateUser();
            UserModel userModel = new UserModel();
            Mapper.Map<User, UserModel>(user, userModel);

            return user.FirstName == userModel.FirstName
                && user.LastName == userModel.LastName
                && user.Age == userModel.Age
                && user.TodoList.Count() == userModel.TodoList.Count();
        }

        public static User CreateUser()
        {
            return new User
            {
                FirstName = "Will",
                LastName = "Smith",
                Age = 50,
                TodoList = new List<TodoItem>
                {
                    new TodoItem{ Title = "Todo 1", Content = "Todo Content 1" },
                    new TodoItem{ Title = "Todo 2", Content = "Todo Content 2" },
                    new TodoItem{ Title = "Todo 3", Content = "Todo Content 3" },
                    new TodoItem{ Title = "Todo 4", Content = "Todo Content 4" },
                    new TodoItem{ Title = "Todo 5", Content = "Todo Content 5" },
                }
            };
        }
    }
}
