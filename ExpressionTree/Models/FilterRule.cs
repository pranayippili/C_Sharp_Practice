using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree.Models
{
	public class FilterRule
	{
		public string Field { get; set; }
		public string Operator { get; set; }
		public object Value { get; set; }
	}
}
