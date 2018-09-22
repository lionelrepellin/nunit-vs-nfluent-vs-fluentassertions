using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BeFluent
{
	[TestFixture]
	public class TestWithNunit : BaseTests
	{
		[Test]
		public override void Asynchronous()
		{
			Assert.DoesNotThrowAsync(async () => 
			{
				await Service.GetHtmlAsync();
			});
		}

		[Test, MaxTime(600)]
		public override void CodePerformance()
		{
			Service.DoSomeWorkAndSleep500ms();
		}

		[Test]
		public override void CompareObject()
		{
			Assert.Fail("Cannot compare two object excluding some properties");
		}

		[Test]
		public override void ContainsValue()
		{
			var people = Service.GetPersons();

			CollectionAssert.AllItemsAreInstancesOfType(people, typeof(Person));

			Assert.That(people, Is.TypeOf<List<Person>>());
		}

		[Test]
		public override void Enum()
		{
			var frenchNationality = Service.GetPersons().First(p => p.Nationality == Nationality.French).Nationality;

			Assert.That(frenchNationality, Is.EqualTo(Nationality.French));
		}

		[Test]

		public override void Exception()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				Service.ThrowException();
			}).Message.Contains("No argument?");
		}

		[Test]
		public override void InstanceOf()
		{
			var firstPerson = Service.GetPersons().First();

			Assert.IsInstanceOf<Person>(firstPerson);

			Assert.That(firstPerson, Is.InstanceOf<Person>());
		}

		[Test]
		public override void IsEqual()
		{
			const int expectedValue = 3;

			var value = Service.ConvertStringToInt("3");

			Assert.That(value, Is.Positive.And.GreaterThan(2).And.InRange(2, 4).And.EqualTo(expectedValue));
		}

		[Test]
		public override void IsNotNull()
		{
			var result = Service.GetNonNullValue();

			Assert.IsNotNull(result);

			Assert.That(result, Is.Not.Null.And.TypeOf<string>().And.StartWith("Hello").And.EndWith("World!").And.EquivalentTo("Hello World!"));
		}

		[Test]
		public override void IsNull()
		{
			var result = Service.GetNullValue();

			Assert.IsNull(result);

			Assert.That(result, Is.Null);
		}
	}
}
