using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Xamarin.Forms;

using Plugin.Clipboard;

namespace Kupla
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SecretPasswordButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MasterPasswordEntry.Text) && !string.IsNullOrWhiteSpace(ServiceEntry.Text))
            {
                String secretPassword = ComputeMD5Hash(MasterPasswordEntry.Text + ServiceEntry.Text.ToUpper() + "kupla");
                CrossClipboard.Current.SetText(secretPassword);
                CopyMessage.IsVisible = true;
            }

        }

        private void MasterPasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ApplyHash();
        }

        private void ServiceEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ApplyHash();
        }

        private void ApplyHash()
        {
            //if (!string.IsNullOrWhiteSpace(MasterPasswordEntry.Text) && !string.IsNullOrWhiteSpace(ServiceEntry.Text))
            //{
            //    SecretPasswordButton.Text = ComputeMD5Hash(MasterPasswordEntry.Text + ServiceEntry.Text.ToUpper() + "kupla");
            //}
            //else
            //{
            //    SecretPasswordButton.Text = "";
            //}
        }

        static string ComputeMD5Hash(string rawdata)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(rawdata));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < Convert.ToInt32(bytes.Length / 2); i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                for (int i = Convert.ToInt32(bytes.Length / 2); i < bytes.Length - 5; i++)
                {
                    builder.Append(bytes[i].ToString("x2").ToUpper());
                }
                builder.Append("!");
                return builder.ToString();
            }
        }
    }
}
