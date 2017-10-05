using System;
using System.Collections.Generic;
using System.Text;

namespace SoloDev.ManualMapper.ConsoleTest.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public IEnumerable<TodoItemModel> TodoList { get; set; }
    }
}
