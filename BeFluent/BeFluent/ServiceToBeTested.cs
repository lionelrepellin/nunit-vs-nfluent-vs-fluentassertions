using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeFluent
{
	public class ServiceToBeTested
	{
		private readonly IEnumerable<Person> _people = new List<Person>
		{
			new Person { Name = "Thomas", Age = 38 },
			new Person { Name = "Achille", Age = 10, Nationality = Nationality.French },
			new Person { Name = "Anton", Age = 7, Nationality = Nationality.French },
			new Person { Name = "Arjun", Age = 7, Nationality = Nationality.Indian }
		};
		
		public string GetNullValue()
		{
			return null;
		}

		public string GetNonNullValue()
		{
			return "Hello World!";
		}

		public int GetInputValue(string input)
		{
			return int.Parse(input);
		}

		public void ThrowException()
		{
			throw new ArgumentException("No argument?");
		}

		public IEnumerable<Person> GetPersons()
		{
			return _people;
		}

		public IEnumerable<string> GetNames()
		{
			return _people.Select(p => p.Name);
		}

		public void DoSomeWorkAndSleep500ms()
		{
			Thread.Sleep(500);
		}
	}

	public abstract class Human
	{
		public Guid Id => Guid.NewGuid();
		public int Age { get; set; }
	}

	public class Person : Human
	{
		public string Name { get; set; }
		public Nationality Nationality { get; set; } = Nationality.Unknown;
	}

	public enum Nationality
	{
		French,
		Indian,
		English,
		Spanish,
		Deutch,
		Italian,
		Unknown
	}
}
