using NUnit.Framework;
using FluentAssertions;
using System;
using System.Linq;
using FluentAssertions.Extensions;
using System.Threading.Tasks;

namespace BeFluent
{
	public class TestWithFluentAssertions : BaseTests
	{
		[Test]
		public override void Asynchronous()
		{
			Func<Task> f = async () => { await Service.GetHtmlAsync(); };
			f.Should().NotThrow(); // it seems to be a bit limited
		}

		[Test]
		public override void CodePerformance()
		{
			// more accurate than Nfluent => it works!
			Service.ExecutionTimeOf(s => s.DoSomeWorkAndSleep500ms()).Should().BeLessOrEqualTo(600.Milliseconds());
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

			person.Should().BeEquivalentTo(anotherPerson, options => options.Excluding(x => x.Age).Excluding(x => x.Id));
		}

		[Test]
		public override void ContainsValue()
		{
			var people = Service.GetPersons().OrderBy(p => p.Age);

			people.Should()
				.NotBeEmpty()
				.And
				.HaveCount(4)
				.And
				.ContainSingle(p => p.Age > 35)
				.And
				.ContainItemsAssignableTo<Person>()
				.And
				.BeInAscendingOrder(p => p.Age);
		}

		[Test]
		public override void Enum()
		{
			var frenchNationality = Service.GetPersons().First(p => p.Nationality == Nationality.French).Nationality;

			frenchNationality.Should().BeEquivalentTo(Nationality.French);
			frenchNationality.Should().BeEquivalentTo(Nationality.French, options => options.ComparingEnumsByName());
			frenchNationality.Should().BeEquivalentTo(Nationality.French, options => options.ComparingEnumsByValue());
		}

		[Test]
		public override void Exception()
		{
			Service.Invoking(s => s.ThrowException())
				.Should().Throw<ArgumentException>()
				.WithMessage("No argument?");
			
			Service.Invoking(s => s.ThrowException())
				.Should().Throw<ArgumentException>()
				.Where(e => e.Message.StartsWith("No"));

			Service.Invoking(s => s.ThrowException())
				.Should().Throw<ArgumentException>()
				.And
				.Message.Should().Be("No argument?");
		}

		[Test]
		public override void InstanceOf()
		{
			var firstPerson = Service.GetPersons().First();

			firstPerson.Should().BeOfType<Person>();
			firstPerson.Should().BeOfType(typeof(Person)); 

			// parent class
			firstPerson.Should().BeAssignableTo<Human>();

			typeof(Human).Should().BeAbstract();
		}

		[Test]
		public override void IsEqual()
		{
			const int expectedValue = 38;

			var value = Service.ConvertStringToInt("38");

			value.Should()
				.BePositive()
				.And
				.BeGreaterOrEqualTo(37)
				.And
				.BeInRange(30, 40)
				.And
				.Be(expectedValue);
		}

		[Test]
		public override void IsNotNull()
		{
			var result = Service.GetNonNullValue();

			result.Should()
				.NotBeNullOrEmpty()
				.And
				.ContainAll("Hello", "World!")
				.And
				.StartWith("Hello")
				.And
				.EndWith("World!")
				.And
				.BeEquivalentTo("Hello World!")
				.And
				.ContainEquivalentOf("hElLo wOrlD!");
		}

		[Test]
		public override void IsNull()
		{
			var result = Service.GetNullValue();

			result.Should().BeNull();
		}

		#region FluentAssertions specific

		[Test]
		public void _FloatValue()
		{
			float pi = 3.1415927F;

			pi.Should().BeApproximately(3.14F, 0.01F);
		}

		#endregion
	}

}
