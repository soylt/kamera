using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;

namespace work_kamera
{
    public partial class MainPage : ContentPage
    {
        Image img;
        Button takePhotoBtn;
        Button getPhotoBtn;
        public MainPage()
        {

            takePhotoBtn = new Button { Text = "Сделать пикчу" };
            getPhotoBtn = new Button { Text = "Выбрать слив??????" };
            img = new Image();

            // выбор фото
            getPhotoBtn.Clicked += GetPhotoAsync;

            // съемка фото
            takePhotoBtn.Clicked += TakePhotoAsync;

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    new StackLayout
                    {
                         Children = {takePhotoBtn, getPhotoBtn},
                         Orientation =StackOrientation.Horizontal,
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    },
                    img
                }
            };
        }

        async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();
                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке(бессмысленно)", ex.Message, "OK");
            }
        }

        async void TakePhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                // для примера сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке(бессмысленно)", ex.Message, "OK");
            }
        }
    }
}

    

