using HHC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HHC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewChecked : ContentPage
    {
        public ObservableCollection<LineDisplay> employees = new ObservableCollection<LineDisplay>();
        public ObservableCollection<LineDisplay> Employees { get { return employees; } }

        public ViewChecked()
        {
            InitializeComponent();
            employees.Add(new LineDisplay());
            CheckedView.ItemsSource = employees;            
            
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentShift.txt");
            if (File.Exists(fileName))
            {
                using (var reader = new StreamReader(File.OpenRead(fileName)))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        LineDisplay line1 = new LineDisplay() { DisplayLine = line };
                        employees.Add(line1);
                    }
                    reader.Close();
                }
            }
            else if (!File.Exists(fileName))
            {
                DispAl();
            }

        }

        async void DispAl()
        {
            await DisplayAlert("No Open Shift", "Please create a new shift first.", "OK");
            await Navigation.PopModalAsync();
        }

        async void OnReturnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}