using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_FE.Models;
using WPF_FE.Services;

namespace WPF_FE.Controls
{
    public class DangKyMonHocWindowMVVM : CBaseMVVM
    {
        public List<Monhoc> monhocs { get; set; }
        public List<Lylich> hocviens { get; set; }
        private Monhoc _monHocSelection;
        public Monhoc monHocSelection
        {
            get => _monHocSelection;
            set
            {
                _monHocSelection = value;
                NotifyPropertyChanged(nameof(monHocSelection));
            }
        }   
        private Lylich _hocVienSelection;
        public Lylich hocVienSelection
        {
            get => _hocVienSelection;
            set
            {
                _hocVienSelection = value;
                NotifyPropertyChanged(nameof(hocVienSelection));
            }
        }
        public List<Lylich> hocviensRegisted { get; set; }

        private readonly MonHocService _monHocService;
        private readonly HocVienService _hocVienService;
        public RelayCommand lenhChon { get; set; }
        public RelayCommand huyDangKyCommand { get; set; }
        public RelayCommand dangKyCommand { get; set; }

        public DangKyMonHocWindowMVVM()
        {
            _monHocService = new MonHocService();
            _hocVienService = new HocVienService();

            lenhChon = new RelayCommand(lenhChon_Execute, lenhChon_CanExecute);

            huyDangKyCommand = new RelayCommand(async hocVien => {
                await _hocVienService.HuyDangKyAsync(hocVien);
                await loadHocViensRegisted(monHocSelection.Msmh);
                await loadCmbHocViens(monHocSelection.Msmh);
            });

            dangKyCommand = new RelayCommand(async hocVien => {
                await _hocVienService.DangKyAsync(hocVien);
                await loadHocViensRegisted(monHocSelection.Msmh);
                await loadCmbHocViens(monHocSelection.Msmh);
            });

            _ = loadCmbMonHocs();
        }

        public bool lenhChon_CanExecute(object parameter)
        {
            return true;
        }

        public async Task lenhChon_Execute(object parameter)
        {
            await loadHocViensRegisted((parameter as Monhoc).Msmh);
            await loadCmbHocViens((parameter as Monhoc).Msmh);
        }
        public async Task loadHocViensRegisted(string msmh)
        {
            hocviensRegisted = await _hocVienService.GetHocViensRegistedAsync(msmh);
            NotifyPropertyChanged(nameof(hocviensRegisted));
        }

        private async Task loadCmbMonHocs()
        {
            monhocs = await _monHocService.GetMonHocsAsync();
            NotifyPropertyChanged(nameof(monhocs));
        }

        public async Task loadCmbHocViens(string? msmh = "-1")
        {
            hocviens = await _hocVienService.GetHocViensAsync(msmh);
            NotifyPropertyChanged(nameof(hocviens));
        }
    }
    public class UnsubscribeParams
    {
        public string Mshv { get; set; } = string.Empty;
        public string Msmh { get; set; } = string.Empty;
    }
}
