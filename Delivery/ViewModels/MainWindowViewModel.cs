using Microsoft.Win32;
using System.IO;
using Delivery.Models;
using Delivery.Services;
using Delivery.DAL.Repositories;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.DAL.Entities;
using System.Linq;
using Prism.Services.Dialogs;
using Delivery.Services.Enums;

namespace Delivery.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {        
        public MainWindowViewModel(ISerializerService serializerService, 
                                   IDeliveryOrderRepository orderRepo,
                                   IDeliveryLogRepository logRepo,
                                   IDialogService dialogService,
                                   IMapper mapper,
                                   RunProcessService processService)
        {
            (_serializerService, _orderRepo, _mapper, DialogService, _logRepo, _process) 
            = (serializerService, orderRepo, mapper, dialogService, logRepo, processService);
        }

        private void OpenFileDialog()
        {
            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = "Открытие диалогового окна" }));

            var openFileDialog = new OpenFileDialog
            {
                Title = "Import XML file",
                DefaultExt = ".xml",
                Filter = "xml|*.xml",
                FilterIndex = 1
            };

            if (openFileDialog.ShowDialog() is true)
            {
                using var fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                _serializerService.Deserialize<DeliveryOrders>(fs);
                Task.Run(() => AddOrders());
            }
        }

        private void AddOrders()
        {
            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = "Импорт заказов из файла" }));

            var orders = _serializerService.DeserializeObject as DeliveryOrders;
            foreach (var order in orders.OrdersArray)
                _orderRepo.AddEntity(_mapper.Map<DeliveryOrderEntity>(order));

            GetAllOrders();
        }

        private void GetAllOrders()
        {
            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = "Получение всех заказов из БД" }));

            _tempOrders = _orderRepo.GetAllEntities()
                                    .Select(_mapper.Map<DeliveryOrder>)
                                    .ToList();

            if (_tempOrders is not null && _tempOrders.Any())
            {
                DeliveryOrders = _tempOrders.OrderByDescending(o => o.Date);
                Regions = _tempOrders.Select(o => o.Region)
                                     .OrderBy(o => o)
                                     .Distinct();
                DateTimes = _tempOrders.Select(o => o.Date)
                                       .OrderBy(o => o.Date);
            }
        }

        private void GetAllLogs()
        {
            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = "Получение всех логов из БД" }));

            var logs = _logRepo.GetAllEntities();

            if (logs is not null && logs.Any())
                DeliveryLogs = logs.Select(_mapper.Map<DeliveryLog>)
                                   .OrderByDescending(o => o.Date);
        }

        private void OpenAboutWindow()
        {
            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = "Открытие окна \"О программе\"" }));

            DialogService.ShowDialog(nameof(NamesWindows.AboutWindow), new DialogParameters(), callback => { });
        }

        private void OpenSettingWindow()
        {
            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = "Открытие окна \"Настройки\"" }));

            DialogService.ShowDialog(nameof(NamesWindows.SettingsWindow), new DialogParameters(), callback => { });
        }

        private void FilterRegion()
        {
            if (Region is null or { Length: 0 }) return;
            if (SelectedDate is null) return;

            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = $"Фильтр: {SelectedDate} {Region}" }));

            DeliveryOrders = [];
            DeliveryOrders = _tempOrders.Where(o => o.Region == Region && o.Date.Date == SelectedDate);

            IsEnabledReloadButton = true;
        }

        private void Reload()
        {
            DeliveryOrders = [.. _tempOrders.OrderByDescending(o => o.Date)];

            IsEnabledReloadButton = false;
        }

        private void OpenReadMe()
        {
            _logRepo.AddEntity(_mapper.Map<DeliveryLogEntity>(new DeliveryLogWithIPAddress { Message = "Открытие справки" }));

            _process.StartProcess("notepad.exe", "readme.txt");
        }
    }
}
