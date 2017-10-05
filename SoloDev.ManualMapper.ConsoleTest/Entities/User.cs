using System;
using System.Collections.Generic;
using System.Text;

namespace SoloDev.ManualMapper.ConsoleTest.Entities
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public IEnumerable<TodoItem> TodoList { get; set; }
    }
}
