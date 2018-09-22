using System;
using System.Linq;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace BeFluent
{
	[TestFixture]
	public class TestWithNfluent : BaseTests
	{
		[Test]
		public override void Asynchronous()
		{
			Check.ThatAsyncCode(() =>
				Service.GetHtmlAsync()
			)
			.DoesNotThrow()
			.And
			.WhichResult()
			.Contains("!doctype");
		}

		[Test]
		public override void CodePerformance()
		{
			// test pass but should not !
			Check.ThatCode(() =>
			{
				Service.DoSomeWorkAndSleep500ms(); // 500ms
			})
			.ConsumesLessThan(20, TimeUnit.Seconds) // CPU time
			.And
			.LastsLessThan(300, TimeUnit.Milliseconds); // execution time (not work!)

			// Check.ThatCode(() => Thread.Sleep(60)).LastsLessThan(30, TimeUnit.Milliseconds);
		}

		[Test]
		public override void CompareObject()
		{
			var person = Service.GetPersons().First(p => p.Nationality == Nationality.Indian);

			var anotherPerson = new Person
			{
				Name = "Arjun",
				Nationality = Nationality.Indian
			};

			Check.That(person)
				.IsNotEqualTo(anotherPerson) // cannot compare two objects excluding properties like FluentAssertions :(
				.And
				.IsInstanceOf<Person>();

			// for deep comparison, use DeepEqual nuget package
		}

		[Test]
		public override void ContainsValue()
		{
			var ages = Service.GetPersons().Select(p => p.Age);

			Check.That(ages)
				.IsNotNull()
				.And
				.CountIs(4)
				.And
				.HasSize(4) // the same as CountIs !
				.And
				.Contains(7, 10);
		}

		[Test]
		public override void Enum()
		{
			var frenchNationality = Service.GetPersons().First(p => p.Nationality == Nationality.French).Nationality;

			Check.ThatEnum(frenchNationality).IsNotEqualTo(Nationality.Unknown);
		}

		[Test]
		public override void Exception()
		{
			Check.ThatCode(() => Service.ThrowException())
				.ThrowsAny();

			Check.ThatCode(() => Service.ThrowException())
				.ThrowsType(typeof(ArgumentException))
				.WithProperty(e => e.Message, "No argument?");

			Check.ThatCode(() => Service.ThrowException())
				.Throws<ArgumentException>()
				.WithMessage("No argument?");
		}

		[Test]
		public override void InstanceOf()
		{
			var firstPerson = Service.GetPersons().First();

			Check.That(firstPerson)
				.IsInstanceOf<Person>()
				.And
				.InheritsFromType(typeof(Human));
		}

		[Test]
		public override void IsEqual()
		{
			const int expectedValue = 3;

			var value = Service.ConvertStringToInt("3");

			Check.That(value)
				.IsStrictlyPositive()
				.And
				.IsStrictlyGreaterThan(2)
				.And
				.HasDifferentValueThan(4)
				.And
				.IsEqualTo(expectedValue);
		}

		[Test]
		public override void IsNotNull()
		{
			var result = Service.GetNonNullValue();

			Check.That(result)
				.IsNotNull()
				.And
				.HasContent()
				.And
				.Not.Contains("something")
				.And
				.Contains("Hello", "World!")
				.And
				.IsEqualIgnoringCase("hElLo wOrlD!");
		}

		[Test]
		public override void IsNull()
		{
			var value = Service.GetNullValue();

			Check.That(value)
				.IsNullOrEmpty()
				.And
				.IsNotInstanceOf<int>();
		}

		#region Nfluent specific

		[Test]
		public void _ContainsExcatly()
		{
			var people = Service.GetPersons().OrderBy(p => p.Name);

			Check.That(people.Extracting(p => p.Name)).ContainsExactly("Achille", "Anton", "Arjun", "Thomas");
		}

		[Test]
		public void _IsOnlyMadeOf()
		{
			var ages = Service.GetPersons().Select(p => p.Age);

			Check.That(ages).IsOnlyMadeOf(10, 38, 7, 7);
		}

		#endregion
	}

}
