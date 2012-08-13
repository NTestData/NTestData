# NTestData

## Natural Test Data for .NET

It's a very subtle test data factory with flexible capabilities to customize created objects. Permanent customizations per type can be defined in order to be always applied on-the-fly to objects being created. At the same time any number of additional ad-hoc customizations can be applied in particular test case.

Generally the syntax is extremely simple. Since every customization is an `Action<T>` where `T` is the type of created object. This leads to the very clean and expressive test code.

Note that this micro-framework is NOT opposed to data generation libraries or frameworks such as NBuilder or AutoPoco but can nicely complement them in most of the cases.

## "Hello World" &trade;

Here is an (extremely simplified) example from `Samples\HelloWorld.cs` file. Given following class defined:

	public class Greeting
	{
	    public string Message { get; set; }
	}
	
test below should pass:

	[Fact]
    public void Demo()
    {
        var helloWorld = TestData.Create<Greeting>(Hello, World);

        helloWorld.Message.Should().Be("Hello, World!");
    }

Note how following methods `Hello` and `World` are actually declared:

    private void Hello(Greeting greeting)
    {
        greeting.Message = "Hello, ";
    }

    private void World(Greeting greeting)
    {
        greeting.Message += "World!";
    }

## Not impressed by now? Right.

From what we have seen so far it is nearly impossible to observe any valuable outcomes in here. But they do exist. Keep reading.

## What value it actually brings to the table

Here are few most typical scenarios in modern approach to software development where use of that library might help.

- **Scenario 1:** let's call it **DDD-Arrange** for convenience

 Usually domain objects, especially aggregate roots, are objects with complex internal structure and the only way to set them into some desider state is by calling public methods with different passed arguments which in turn can be pretty complex too. *(Or if event sourcing is used then it is required to apply some stream of events which is technically the same thing.)*

 So in that case ... TODO

- **Scenario 2:** let's call it **Side Effects For The Win** for convenience

 There are at least two major possible variations:

 - Setting mocks in pure unit-testing
 - Setting services / Ensure auxilialy data in integration testing

 And here is how it works ... TODO


## Advanced usage / Extensibility

Actually it is not only library but sort of framework with explicitly defined core pieces which allows you to build your own custom implementation ... TODO

