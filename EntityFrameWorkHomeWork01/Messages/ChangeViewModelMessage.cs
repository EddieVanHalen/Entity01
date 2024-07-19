using EntityFrameWorkHomeWork01.ViewModel;

namespace EntityFrameWorkHomeWork01.Messages;

public class ChangeViewModelMessage : Message
{
    public BaseViewModel ViewModel { get; set; }

    public ChangeViewModelMessage(BaseViewModel viewModel)
    {
        ViewModel = viewModel;
    }
}