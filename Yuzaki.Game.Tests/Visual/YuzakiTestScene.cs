using osu.Framework.Testing;

namespace Yuzaki.Game.Tests.Visual
{
    public partial class YuzakiTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new YuzakiTestSceneTestRunner();

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
