using Prism.Commands;
using Delivery.Services;
using Delivery.DAL.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Delivery.Models;
using Prism.Services.Dialogs;
using System;

namespace Delivery.ViewModels
{
    public partial class MainWindowViewModel
    {
        private readonly ISerializerService _serializerService;
        private readonly IDeliveryOrderRepository _orderRepo; 
        private readonly IDeliveryLogRepository _logRepo;
        private readonly IMapper _mapper;
        private readonly RunProcessService _process;
        private IEnumerable<DeliveryOrder> _deliveryOrders = [], _tempOrders = [];
        private IEnumerable<string> _regions = [];
        private IEnumerable<DateTime> _dateTimes = [];
        private IEnumerable<DeliveryLog> _deliveryLogs = [];
        private DateTime? _selectedDate;
        private string _region;
        private bool _isEnabledReloadButton = false;
        

        protected override IDialogService DialogService { get; }
        public override string Title => "Доставка";

        public IEnumerable<DeliveryOrder> DeliveryOrders { get => _deliveryOrders; private set => SetProperty(ref _deliveryOrders, value); }
        public IEnumerable<DeliveryLog> DeliveryLogs { get => _deliveryLogs; private set => SetProperty(ref _deliveryLogs, value); }
        public IEnumerable<string> Regions { get => _regions; private set => SetProperty(ref _regions, value); }
        public IEnumerable<DateTime> DateTimes { get => _dateTimes; set => SetProperty(ref _dateTimes, value); }
        public string Region { get => _region; set => SetProperty(ref _region, value); }
        public DateTime? SelectedDate { get => _selectedDate; set => SetProperty(ref _selectedDate, value); }
        public bool IsEnabledReloadButton { get => _isEnabledReloadButton; set => SetProperty(ref _isEnabledReloadButton, value); }

        public DelegateCommand OpenFileDialogCommand => new(OpenFileDialog);
        public DelegateCommand LoadedCommand => new(() => Task.Run(() => 
        { 
            GetAllOrders();
            GetAllLogs();
        }));
        public DelegateCommand OpenAboutWindowCommand => new(OpenAboutWindow);
        public DelegateCommand OpenSettingsWindowCommand => new(OpenSettingWindow);
        public DelegateCommand FilterCommand => new(() => Task.Run(() => FilterRegion()));
        public DelegateCommand ReloadCommand => new(() => Task.Run(() => Reload()));
        public DelegateCommand OpenReadMeCommand => new(() => Task.Run(() => OpenReadMe()));
    }
}
