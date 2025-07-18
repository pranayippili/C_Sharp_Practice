using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExpressions.Models
{
    public record class Student(string Name, int Age, decimal GPA, string Major)
    {
        public override string ToString() =>
            $"{Name} (Age: {Age}, GPA: {GPA}, Major: {Major})";
    }
}
