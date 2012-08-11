namespace NTestData.Samples
{
    using FluentAssertions;
    using Xunit;

    public class HelloWorld
    {
        private class Greeting
        {
            public string Message { get; set; }
        }

        [Fact]
        public void Demo()
        {
            var helloWorld = TestData.Create<Greeting>(Hello, World);

            helloWorld.Message.Should().Be("Hello, World!");
        }

        private void Hello(Greeting greeting)
        {
            greeting.Message = "Hello, ";
        }

        private void World(Greeting greeting)
        {
            greeting.Message += "World!";
        }
    }
}
