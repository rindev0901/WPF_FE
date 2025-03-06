using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WPF_FE.Models;
using WPF_FE.Services;

namespace WPF_FE.Controls
{
    public class MonHocWindowMVVM : CBaseMVVM
    {
        private readonly MonHocService _monHocService;
        public ObservableCollection<Monhoc> MonHocs { get; set; } = new();
        public RelayCommand lenhThem { get; set; }
        public RelayCommand lenhXoa { get; set; }
        public RelayCommand lenhSua { get; set; }
        public Monhoc monHoc { get; set; } = new();
        private Monhoc _monHocSelection;
        public Monhoc monHocSelection
        {
            set
            {
                this._monHocSelection = value;

                var json = JsonSerializer.Serialize(value);

                monHoc = JsonSerializer.Deserialize<Monhoc>(json);

                NotifyPropertyChanged(nameof(monHoc));
            }
            get
            {
                return this._monHocSelection;
            }
        }
        public MonHocWindowMVVM()
        {
            _monHocService = new MonHocService();
            lenhThem = new RelayCommand(lenhThem_Execute, lenhThem_CanExecute);
            lenhXoa = new RelayCommand(lenhXoa_Execute, lenhXoa_CanExecute);
            lenhSua = new RelayCommand(lenhSua_Execute, lenhSua_CanExecute);
            LoadDataAsync();
        }

        public async Task lenhThem_Execute(object parameter)
        {
            try
            {
                bool success = await _monHocService.AddMonHocAsync(monHoc);

                if (success)
                {
                    await LoadDataAsync();
                    monHoc = new();
                    NotifyPropertyChanged(nameof(monHoc));
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"{httpEx.Message}", $"Network Error: {httpEx.StatusCode}", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool lenhXoa_CanExecute(object parameter)
        {
            if (monHocSelection == null)
            {
                return false;
            }

            return true;
        }

        public async Task lenhXoa_Execute(object parameter)
        {
            try
            {
                bool success = await _monHocService.DeleteMonHocByIdAsync(monHocSelection.Msmh);

                if (success)
                {
                    await LoadDataAsync();
                    monHoc = new();
                    monHocSelection = new();
                    NotifyPropertyChanged(nameof(monHoc));
                    NotifyPropertyChanged(nameof(monHocSelection));
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"{httpEx.Message}", $"Network Error: {httpEx.StatusCode}", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool lenhSua_CanExecute(object parameter)
        {
            if (monHoc == null || string.IsNullOrEmpty(monHoc.Msmh) || string.IsNullOrEmpty(monHoc.Tenmh) || monHoc.Sotiet == null)
            {
                return false;
            }

            return true;
        }

        public async Task lenhSua_Execute(object parameter)
        {
            try
            {
                bool success = await _monHocService.UpdateMonHocAsync(monHoc.Msmh, monHoc);

                if (success)
                {
                    await LoadDataAsync();
                    monHoc = new();
                    monHocSelection = new();
                    NotifyPropertyChanged(nameof(monHoc));
                    NotifyPropertyChanged(nameof(monHocSelection));
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"{httpEx.Message}", $"Network Error: {httpEx.StatusCode}", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool lenhThem_CanExecute(object parameter)
        {
            if (monHoc == null || string.IsNullOrEmpty(monHoc.Msmh) || string.IsNullOrEmpty(monHoc.Tenmh) || monHoc.Sotiet == null)
            {
                return false;
            }

            return true;
        }
        public async Task LoadDataAsync()
        {
            var monHocs = await _monHocService.GetMonHocsAsync();
            MonHocs.Clear();
            foreach (var monHoc in monHocs)
            {
                MonHocs.Add(monHoc);
            }
        }

    }
}
