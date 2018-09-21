using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BeFluent
{
	public abstract class BaseTests
    {
		protected ServiceToBeTested Service;

		[SetUp]
		public void Initialize()
		{
			Service = new ServiceToBeTested();
		}

		// common basic tests
		public abstract void IsNull();
		public abstract void IsNotNull();
		public abstract void IsEqual();
		public abstract void ContainsValue();
		public abstract void InstanceOf();
		public abstract void Exception();
		public abstract void Enum();
		public abstract void CodePerformance();
		public abstract void CompareObject();
	}
}
