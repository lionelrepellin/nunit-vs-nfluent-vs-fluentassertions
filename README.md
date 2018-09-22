# Compares Nunit vs. NFluent vs. FluentAssertions test methods

I just wanted to use this repository as a reminder. Only the common methods I use in my unit tests are exposed here.

I tried to make some tests with the test frameworks listed below :

- [Nunit](https://nunit.org/)
    
    It works, but it seems to be a little bit 'old school' ^^
    Luckily, since the version 2.4, it has the Assert.That notation and is now more fluent.

- [FluentAssertion](https://fluentassertions.com/)

    Has a lot of downloads, it is very fluent and have more functionality than the others.
    It allows to compare two objects and excluding some properties. For deep comparison, [DeepEqual](https://github.com/jamesfoster/DeepEqual) is also very useful.

- [NFluent](http://www.n-fluent.net/)

    The syntax looks like to Nunit (Assert.That / Check.That) is as fluent as FluentAssertion (.And .Not .Throw .Contains). It has maybe less capabilities than FluentAssertion but it is really easy to use. I prefer to begin the test with Check.That instead of using an extension method to the object. I almost changed my mind, but I still use NFluent.
