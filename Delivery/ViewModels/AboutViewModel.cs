using Delivery.Services;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Threading.Tasks;

namespace Delivery.ViewModels
{
    public sealed class AboutViewModel : BaseViewModel
    {
        private readonly string _msedge = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
        private readonly string _site = @"https://github.com/dvazdidvachetire";
        private readonly RunProcessService _process;

        public override string Title => "О программе";
        public DelegateCommand StartMSEdgeCommand => new(() => Task.Run(() => _process.StartProcess(_msedge, _site)));
        public DelegateCommand CloseCommand => new(() => RaiseRequestClose(new DialogResult(ButtonResult.OK)));

        public AboutViewModel(RunProcessService process)
        {
            _process = process;
        }
    }
}
