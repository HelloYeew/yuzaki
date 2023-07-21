using osu.Framework.Allocation;
using osu.Framework.Testing;

namespace Yuzaki.Game.Tests.Visual
{
    public partial class YuzakiTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new YuzakiTestSceneTestRunner();

        public new DependencyContainer Dependencies { get; private set; }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            IReadOnlyDependencyContainer baseDependencies = base.CreateChildDependencies(parent);
            Dependencies = new DependencyContainer(baseDependencies);

            return Dependencies;
        }

        private partial class YuzakiTestSceneTestRunner : YuzakiGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
