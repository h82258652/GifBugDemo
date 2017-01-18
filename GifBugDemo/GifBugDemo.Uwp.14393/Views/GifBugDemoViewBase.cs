using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Navigation;
using SoftwareKobo.Views;

namespace GifBugDemo.Uwp.Views
{
    public abstract class GifBugDemoViewBase : ViewBase
    {
        private readonly SystemNavigationManager _systemNavigationManager;

        protected GifBugDemoViewBase()
        {
            if (DesignMode.DesignModeEnabled == false)
            {
                _systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            _systemNavigationManager.AppViewBackButtonVisibility = Frame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
            _systemNavigationManager.BackRequested -= SystemNavigationManager_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _systemNavigationManager.AppViewBackButtonVisibility = Frame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
            _systemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;
        }

        protected override Task PlayEnterAnimationAsync()
        {
            if (Frame.BackStackDepth > 0 && Frame.CanGoForward == false)
            {
                var visual = ElementCompositionPreview.GetElementVisual(this);
                var compositor = visual.Compositor;
                var tcs = new TaskCompletionSource<object>();
                var batch = compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
                TypedEventHandler<object, CompositionBatchCompletedEventArgs> handler = null;
                handler = (sender, args) =>
                {
                    batch.Completed -= handler;
                    visual.Offset = Vector3.Zero;
                    tcs.SetResult(null);
                };
                batch.Completed += handler;
                var linear = compositor.CreateLinearEasingFunction();
                var animation = compositor.CreateVector3KeyFrameAnimation();
                animation.InsertKeyFrame(0f, new Vector3((float)Frame.ActualWidth, 0f, 0f), linear);
                animation.InsertKeyFrame(1f, new Vector3(0f, 0f, 0f), linear);
                animation.Duration = TimeSpan.FromSeconds(0.3);
                visual.StartAnimation("Offset", animation);
                batch.End();
                return tcs.Task;
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        protected override Task PlayLeaveAnimationAsync(NavigationMode currentPageNavigationMode)
        {
            if (currentPageNavigationMode == NavigationMode.New)
            {
                // Navigate to new page, push the old page to left.
                var visual = ElementCompositionPreview.GetElementVisual(this);
                var compositor = visual.Compositor;
                var tcs = new TaskCompletionSource<object>();
                var batch = compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
                TypedEventHandler<object, CompositionBatchCompletedEventArgs> handler = null;
                handler = (sender, args) =>
                {
                    batch.Completed -= handler;
                    visual.Offset = Vector3.Zero;
                    tcs.SetResult(null);
                };
                batch.Completed += handler;
                var linear = compositor.CreateLinearEasingFunction();
                var animation = compositor.CreateVector3KeyFrameAnimation();
                animation.InsertKeyFrame(0f, new Vector3(0f, 0f, 0f), linear);
                animation.InsertKeyFrame(1f, new Vector3((float)(0 - Frame.ActualWidth), 0f, 0f), linear);
                animation.Duration = TimeSpan.FromSeconds(0.3);
                visual.StartAnimation("Offset", animation);
                batch.End();
                return tcs.Task;
            }
            else if (currentPageNavigationMode == NavigationMode.Back)
            {
                // Navigation back, push the page to right.
                var visual = ElementCompositionPreview.GetElementVisual(this);
                var compositor = visual.Compositor;
                var tcs = new TaskCompletionSource<object>();
                var batch = compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
                TypedEventHandler<object, CompositionBatchCompletedEventArgs> handler = null;
                handler = (sender, args) =>
                {
                    batch.Completed -= handler;
                    visual.Offset = Vector3.Zero;
                    tcs.SetResult(null);
                };
                batch.Completed += handler;
                var linear = compositor.CreateLinearEasingFunction();
                var animation = compositor.CreateVector3KeyFrameAnimation();
                animation.InsertKeyFrame(0f, new Vector3(0f, 0f, 0f), linear);
                animation.InsertKeyFrame(1f, new Vector3((float)Frame.ActualWidth, 0f, 0f), linear);
                animation.Duration = TimeSpan.FromSeconds(0.3);
                visual.StartAnimation("Offset", animation);
                batch.End();
                return tcs.Task;
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
    }
}