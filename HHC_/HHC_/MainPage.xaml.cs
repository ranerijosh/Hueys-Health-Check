using HHC_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HHC_
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    { 

    public MainPage()
        {
            InitializeComponent();

            StorePrompt();
        }

        async void StorePrompt()
        {
            string store;
            var storeFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StoreNumber.txt");

            if (!File.Exists(storeFile))
            {
                using (var writer = new StreamWriter(File.Create(storeFile)))
                {
                    var input = await DisplayPromptAsync("Store Number", "Please enter store #");
                    store = input.ToString();
                    writer.WriteLine(store);
                    writer.Close();
                }
            }
            else if (File.Exists(storeFile))
            {
                using (var reader = new StreamReader(File.OpenRead(storeFile)))
                {
                    store = reader.ReadLine();
                    reader.Close();
                }
            }
        }


            async void OnCheckClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CheckEmployee());
        }
        async void OnViewClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ViewChecked());
        }
        async void OnSubmitClicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Are you sure?", "Shift will be submitted and the local file will be deleted.", "Yes", "No");
            if (response)
            {
                try
                {
                    var recipients = new List<string>
                    {
                        "thejewosh@gmail.com"
                    };

                    List<string> list = new List<string>();

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
                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentShift.txt");
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

                    var date = DateTime.Parse(list[1]);
                    string todayFormatter = date.ToString("MM-dd-yyyy");
                    string mailFileName = list[0] + "_" + todayFormatter + "_" + list[2] + ".txt";
                    string mailFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), mailFileName);
                    if (!File.Exists(mailFile))
                    {
                        File.Move(fileName, mailFile);
                    }
                    else if (File.Exists(mailFile))
                    {
                        File.Delete(mailFile);
                        File.Move(fileName, mailFile);
                    }
                    var message = new EmailMessage
                    {
                        Subject = "Shift health summary",
                        Body = "Shift file attached for Store #" + list[0],
                        To = recipients
                    };
                    message.Attachments.Add(new EmailAttachment(mailFile));
                    await Email.ComposeAsync(message);
                    await DisplayAlert("Success", "Shift submitted successfully.", "OK");
                    File.Delete(fileName);
                    File.Delete(mailFile);
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Not Supported", "Email not supported on this device.", "OK");
                    await Navigation.PopModalAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Runtime Exception", ex.ToString(), "OK");
                    await Navigation.PopModalAsync();
                }
            }
        }
        async void OnNewClicked(object sender, EventArgs e)
        {
            var today = DateTime.Today.ToString("MM/dd/yyyy");
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentShift.txt");

            if (File.Exists(fileName))
            {
                bool response = await DisplayAlert("Shift Exists", "There is a current shift open. Would you like to delete this shift and create a new one?", "Yes", "No");
                if (response)
                {
                    File.Delete(fileName);
                    using (var writer = new StreamWriter(File.Create(fileName)))
                    {
                        string shift = await DisplayActionSheet("Select Shift", "Cancel", null, "AM", "PM");
                        writer.WriteLine(today);
                        writer.WriteLine(shift + "_Shift");
                        writer.WriteLine("Emp  |  Shift   |  Q1  |  Q2  |  Q3  |  Q4  |  Q5  ");
                        writer.Close();
                    }

                    await DisplayAlert("Success!", "Shift has been created successfully", "OK");
                }
                else if (!response)
                {
                    await DisplayAlert("", "No action taken", "OK");
                }
            }
            else if (!File.Exists(fileName))
            {
                using (var writer = new StreamWriter(File.Create(fileName)))
                {
                    string shift = await DisplayActionSheet("Select Shift", "Cancel", null, "AM", "PM");
                    writer.WriteLine(today);
                    writer.WriteLine(shift + "_Shift");
                    writer.WriteLine("Emp  |  Shift   |  Q1  |  Q2  |  Q3  |  Q4  |  Q5  ");
                    writer.Close();
                }
            }
        }

    }
}
