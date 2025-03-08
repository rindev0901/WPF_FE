using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_FE.Controls;
using WPF_FE.Models;

namespace WPF_FE.UI
{
    /// <summary>
    /// Interaction logic for DangKyWindow.xaml
    /// </summary>
    public partial class DangKyWindow : Window
    {
        public DangKyWindow()
        {
            InitializeComponent();
        }

        private void cmbMsmh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is DangKyMonHocWindowMVVM viewModel)
            {
                viewModel.lenhChon_Execute(viewModel.monHocSelection);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Lylich selectedHocVien)
            {
                if (DataContext is DangKyMonHocWindowMVVM viewModel)
                {
                    viewModel.huyDangKyCommand.Execute(new UnsubscribeParams { Mshv = selectedHocVien.Mshv, Msmh = viewModel.monHocSelection.Msmh });
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                if (DataContext is DangKyMonHocWindowMVVM viewModel)
                {
                    viewModel.dangKyCommand.Execute(new UnsubscribeParams { Mshv = viewModel.hocVienSelection.Mshv, Msmh = viewModel.monHocSelection.Msmh });
                }
            }
        }
    }
}
