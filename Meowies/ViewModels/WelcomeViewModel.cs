using System;

namespace Meowies.ViewModels;

public class WelcomeViewModel : PageViewModelBase
{
    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
}
