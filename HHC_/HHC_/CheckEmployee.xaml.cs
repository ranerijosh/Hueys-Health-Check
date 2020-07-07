using HHC_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HHC_
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckEmployee : ContentPage
    {
        DateTime time;
        
        public CheckEmployee()
        {
             InitializeComponent();
                        
        }

        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                SetTime();
            }
        }

        async void OnSubmitClick(object sender, EventArgs e)
        {
            
            Employee emp = new Employee();
            int id;

            time = DateTime.Today + timePicker.Time;

            if ((ch1.IsChecked || ch12.IsChecked) && (ch2.IsChecked || ch22.IsChecked) && (ch3.IsChecked || ch32.IsChecked) && (ch4.IsChecked || ch42.IsChecked) && (ch5.IsChecked || ch52.IsChecked))
            {
                if (ch1.IsChecked && ch22.IsChecked && ch32.IsChecked && ch4.IsChecked && ch5.IsChecked)
                {
                    var list = new List<string>();
                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentShift.txt");
                    if (File.Exists(fileName))
                    {
                        using (var reader = new StreamReader(File.OpenRead(fileName)))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                list.Add(line);
                            }
                            reader.Close();
                        }
                    }
                    else if (!File.Exists(fileName))
                    {
                        await DisplayAlert("No Open Shift", "Please create a new shift first.", "OK");
                        await Navigation.PopModalAsync();
                    }
                    id = list.Count() - 2;
                    emp.EmpID = id.ToString("00");
                    emp.Shift = time.ToString("hh:mm");
                    if (ch1.IsChecked) { emp.Q1 = "OK"; } else if (ch12.IsChecked) { emp.Q1 = "WA"; }
                    if (ch2.IsChecked) { emp.Q2 = "AL"; } else if (ch22.IsChecked) { emp.Q2 = "OK"; }
                    if (ch3.IsChecked) { emp.Q3 = "AL"; } else if (ch32.IsChecked) { emp.Q3 = "OK"; }
                    if (ch4.IsChecked) { emp.Q4 = "OK"; } else if (ch42.IsChecked) { emp.Q4 = "AL"; }
                    if (ch5.IsChecked) { emp.Q5 = "OK"; } else if (ch52.IsChecked) { emp.Q5 = "AL"; }

                    string writestring = emp.EmpID + "     |  " + emp.Shift + "  |  " + emp.Q1 + "  |  " + emp.Q2 + "  |  " + emp.Q3 + "  |  " + emp.Q4 + "  |  " + emp.Q5;
                    if (File.Exists(fileName))
                    {
                        using (StreamWriter writer = File.AppendText(fileName))
                        {
                            writer.WriteLine(writestring);
                            writer.Close();
                        }
                    }

                    await DisplayAlert("Success", "Employee successfully added to shift.", "OK");
                    await Navigation.PopModalAsync();
                }
                else if (ch12.IsChecked && ch22.IsChecked && ch32.IsChecked && ch4.IsChecked && ch5.IsChecked)
                {
                    var list = new List<string>();
                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentShift.txt");
                    if (File.Exists(fileName))
                    {
                        using (var reader = new StreamReader(File.OpenRead(fileName)))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                list.Add(line);
                            }
                            reader.Close();
                        }
                    }
                    else if (!File.Exists(fileName))
                    {
                        await DisplayAlert("No Open Shift", "Please create a new shift first.", "OK");
                        await Navigation.PopModalAsync();
                    }
                    id = (list.Count - 2);
                    emp.EmpID = id.ToString("00");
                    emp.Shift = time.ToString("hh:mm");
                    if (ch1.IsChecked) { emp.Q1 = "OK"; } else if (ch12.IsChecked) { emp.Q1 = "WA"; }
                    if (ch2.IsChecked) { emp.Q2 = "AL"; } else if (ch22.IsChecked) { emp.Q2 = "OK"; }
                    if (ch3.IsChecked) { emp.Q3 = "AL"; } else if (ch32.IsChecked) { emp.Q3 = "OK"; }
                    if (ch4.IsChecked) { emp.Q4 = "OK"; } else if (ch42.IsChecked) { emp.Q4 = "AL"; }
                    if (ch5.IsChecked) { emp.Q5 = "OK"; } else if (ch52.IsChecked) { emp.Q5 = "AL"; }

                    string writestring = emp.EmpID + "     |  " + emp.Shift + "  |  " + emp.Q1 + "  |  " + emp.Q2 + "  |  " + emp.Q3 + "  |  " + emp.Q4 + "  |  " + emp.Q5;
                    if (File.Exists(fileName))
                    {
                        using (StreamWriter writer = File.AppendText(fileName))
                        {
                            writer.WriteLine(writestring);
                            writer.Close();
                        }
                    }
                    await DisplayAlert("Warning", "Please instruct employee to wash hands or use 60%+ ABHR immediately", "OK");
                    await DisplayAlert("Success", "Employee successfully added to shift.", "OK");
                    await Navigation.PopModalAsync();
                }
                else if (ch2.IsChecked || ch3.IsChecked || ch42.IsChecked || ch52.IsChecked)
                {
                    List<string> list = new List<string>();
                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentShift.txt");
                    string store = "";
                    var storeFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StoreNumber.txt");
                    if (File.Exists(storeFile))
                    {
                        using (var reader = new StreamReader(File.OpenRead(storeFile)))
                        {
                            store = reader.ReadLine();
                            reader.Close();
                        }
                    }
                    if (File.Exists(fileName))
                    {
                        using (var reader = new StreamReader(File.OpenRead(fileName)))
                        {
                            string line;
                            list.Add(store);
                            while ((line = reader.ReadLine()) != null)
                            {
                                list.Add(line);
                            }
                            reader.Close();
                        }
                    }
                    else if (!File.Exists(fileName))
                    {
                        await DisplayAlert("No Open Shift", "Please create a new shift first.", "OK");
                        await Navigation.PopModalAsync();
                    }
                    id = (list.Count - 2);
                    emp.EmpID = id.ToString("00");
                    emp.Shift = time.ToString("hh:mm");
                    if (ch1.IsChecked) { emp.Q1 = "OK"; } else if (ch12.IsChecked) { emp.Q1 = "WA"; }
                    if (ch2.IsChecked) { emp.Q2 = "AL"; } else if (ch22.IsChecked) { emp.Q2 = "OK"; }
                    if (ch3.IsChecked) { emp.Q3 = "AL"; } else if (ch32.IsChecked) { emp.Q3 = "OK"; }
                    if (ch4.IsChecked) { emp.Q4 = "OK"; } else if (ch42.IsChecked) { emp.Q4 = "AL"; }
                    if (ch5.IsChecked) { emp.Q5 = "OK"; } else if (ch52.IsChecked) { emp.Q5 = "AL"; }

                    string writestring = emp.EmpID + "     |  " + emp.Shift + "  |   " + emp.Q1 + "   |   " + emp.Q2 + "    |   " + emp.Q3 + "    |   " + emp.Q4 + "    |   " + emp.Q5;
                    if (File.Exists(fileName))
                    {
                        using (StreamWriter writer = File.AppendText(fileName))
                        {
                            writer.WriteLine(writestring);
                            writer.Close();
                        }
                    }
                    await DisplayAlert("Alert!", "Please instruct employee to return home and take appropriate action. Press OK to email Operations immediately.", "OK");

                    try
                    {
                        var date = DateTime.Parse(list[1]);
                        string todayFormatter = date.ToString("MM-dd-yyyy");
                        string mailFileName = list[0] + "_" + todayFormatter + "_" + list[2] + "_Alert.txt";
                        string mailFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), mailFileName);
                        if (!File.Exists(mailFile))
                        {
                            File.Copy(fileName, mailFile);
                        }
                        else if (File.Exists(mailFile))
                        {
                            File.Delete(mailFile);
                            File.Copy(fileName, mailFile);
                        }
                        var recipients = new List<string>
                        {
                            "operations@hueymagoos.com"
                        };
                        var message = new EmailMessage
                        {
                            Subject = "Employee Health Alert",
                            Body = "Alert detected for Store #" + list[0] + ". Current shift attached.",
                            To = recipients
                        };
                        message.Attachments.Add(new EmailAttachment(mailFile));
                        await Email.ComposeAsync(message);
                        File.Delete(mailFile);
                    }
                    catch (FeatureNotSupportedException)
                    {
                        await DisplayAlert("Not Supported", "Email not supported on this device.", "OK");
                        await Navigation.PopModalAsync();
                    }
                    catch (Exception z)
                    {
                        await DisplayAlert("Runtime Error", z.ToString(), "OK");
                        await Navigation.PopModalAsync();
                    }
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await DisplayAlert("Invalid Input", "Please answer all 5 questions.", "OK");
            }

        }

        void SetTime()
        {
            time = DateTime.Today + timePicker.Time;
        }

        private void Ch1ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch1.IsChecked)
            {
                ch12.IsChecked = false;
            }
            else if (!ch1.IsChecked)
            {
                ch12.IsChecked = true;
            }
        }

        private void Ch12ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch12.IsChecked)
            {
                ch1.IsChecked = false;
            }
            else if (!ch12.IsChecked)
            {
                ch1.IsChecked = true;
            }
        }

        private void ch2ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch2.IsChecked)
            {
                ch22.IsChecked = false;
            }
            else if (!ch2.IsChecked)
            {
                ch22.IsChecked = true;
            }
        }

        private void ch22ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch22.IsChecked)
            {
                ch2.IsChecked = false;
            }
            else if (!ch22.IsChecked)
            {
                ch2.IsChecked = true;
            }
        }

        private void ch3ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch3.IsChecked)
            {
                ch32.IsChecked = false;
            }
            else if (!ch3.IsChecked)
            {
                ch32.IsChecked = true;
            }
        }

        private void ch32ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch32.IsChecked)
            {
                ch3.IsChecked = false;
            }
            else if (!ch32.IsChecked)
            {
                ch3.IsChecked = true;
            }
        }

        private void ch4ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch4.IsChecked)
            {
                ch42.IsChecked = false;
            }
            else if (!ch4.IsChecked)
            {
                ch42.IsChecked = true;
            }
        }

        private void ch42ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch42.IsChecked)
            {
                ch4.IsChecked = false;
            }
            else if (!ch42.IsChecked)
            {
                ch4.IsChecked = true;
            }
        }

        private void ch5ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch5.IsChecked)
            {
                ch52.IsChecked = false;
            }
            else if (!ch5.IsChecked)
            {
                ch52.IsChecked = true;
            }
        }

        private void ch52ch(object sender, CheckedChangedEventArgs e)
        {
            if (ch52.IsChecked)
            {
                ch5.IsChecked = false;
            }
            else if (!ch52.IsChecked)
            {
                ch5.IsChecked = true;
            }
        }
    }
}