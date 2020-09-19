using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SharpEcs.Test
{
    [TestClass]
    public sealed class SharpEcsAgentEndToEnd
    {
        private class TestComponent
        {
            public int TestField { get; set; } = 0;
        }

        private class TestTwoComponent
        {
            public int TestField { get; set; } = 0;
        }

        private class TestSystem : SharpEcsSystem
        {
            public string TestMethod()
                => "Test";
        }

        [TestMethod]
        public void EndToEnd_CreateEntityCreateComponentCreateSystem_SystemPopulatedWithEntity()
        {
            // Arrange
            var entity1 = SharpEcsAgent.CreateEntity();
            var entity2 = SharpEcsAgent.CreateEntity();
            var entity3 = SharpEcsAgent.CreateEntity();
            SharpEcsAgent.RegisterComponent<TestComponent>();
            SharpEcsAgent.RegisterComponent<TestTwoComponent>();
            var system = SharpEcsAgent.RegisterSystem<TestSystem>();
            {
                var signature = new Signature();
                signature.AddSignature(SharpEcsAgent.GetComponentSignature<TestComponent>());
                SharpEcsAgent.SetSystemSignature<TestSystem>(signature);
            }
            SharpEcsAgent.AddComponent(entity1, new TestComponent
            {
                TestField = 5
            });
            SharpEcsAgent.AddComponent(entity2, new TestComponent
            {
                TestField = 6
            }); SharpEcsAgent.AddComponent(entity2, new TestTwoComponent
            {
                TestField = 6
            });

            Assert.IsTrue(system.Entities.Count == 2, $"System did not populate with entities. Count: {system.Entities.Count}");
            var returnedComponent = SharpEcsAgent.GetComponent<TestComponent>(system.Entities.First());
            Assert.IsTrue(returnedComponent.TestField == 5, $"Returned component did not have have the expected value. Value: {returnedComponent.TestField}");
        }
    }
}