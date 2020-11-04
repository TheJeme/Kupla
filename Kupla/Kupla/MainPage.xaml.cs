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

        private void CopySecretPassword_Tapped(object sender, EventArgs e)
        {
            CrossClipboard.Current.SetText(SecretPasswordLabel.Text);            
        }

        private void MasterPasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyHash();
        }

        private void ServiceEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyHash();
        }

        private void ApplyHash()
        {
            if (MasterPasswordEntry.Text != null && ServiceEntry.Text != null)
            {
                SecretPasswordLabel.Text = ComputeMD5Hash(MasterPasswordEntry.Text + ServiceEntry.Text.ToUpper() + "kupla");
            }
            else
            {
                SecretPasswordLabel.Text = "";
            }
        }


        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
                for (int i = Convert.ToInt32(bytes.Length / 2); i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2").ToUpper());
                }
                builder.Append("!");
                return builder.ToString();
            }
        }
    }
}
