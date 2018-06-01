﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


using EF_CoreExample_Adapter;

using System.Diagnostics;
using System.Threading.Tasks;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        readonly DataAdapter _dataAdapter = new DataAdapter();


        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var watch = new Stopwatch();

            watch.Start();

            var blogs = _dataAdapter.GetBlogs();

            List.ItemsSource = blogs;

            watch.Stop();

            CountLabel.Text = $"Total entities count: {blogs?.Count()}";

            TimeLabel.Text = $"Time taken for GET query: {watch.ElapsedMilliseconds} ms";

            watch.Reset();
        }

        private async void OnInitDbClick(object sender, RoutedEventArgs e)
        {
            CountLabel.Text = "";

            TimeLabel.Text = "";

            List.ItemsSource = null;

            await Task.Delay(TimeSpan.FromMilliseconds(20));

            _dataAdapter.Remove();

            for (int i = 0; i < 1500; i++)
            {
                var blog = new BlogDto()
                {
                    BlogId = i + 1,
                    Url = "https://docs.microsoft.com/en-us/ef/core/get-started/uwp/getting-started"
                };

                _dataAdapter.InsertBlogs(blog);
            }
            
            //Load data after initialize
            LoadData();
        }

        private void OnFetchDbClick(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
