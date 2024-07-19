using System.Windows.Forms;
using EntityFrameWorkHomeWork01.Interfaces;

namespace EntityFrameWorkHomeWork01.Services;

public class MyMessageBox : IMessageBox
{
    public void Print(string message)
    {
        MessageBox.Show(message);
    }
}