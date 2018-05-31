using System;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

using EF_CoreExample_Adapter;
using System.Diagnostics;

namespace App1
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        readonly Class1 _class1 = new Class1();


        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var watch = new Stopwatch();

            watch.Start();

            Blogs.ItemsSource = _class1.GetBlogs();

            watch.Stop();

            Debug.WriteLine($"Time taken for query: {watch.ElapsedMilliseconds} ms");

            watch.Reset();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                var blog = new BlogDto()
                {
                    BlogId = i + 1,
                    //Posts = new List<PostDto>()
                    //{
                    //    new PostDto
                    //    {
                    //        BlogId = i,
                    //        Content = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + i,
                    //        PostId = i + 1,
                    //    },
                    //    new PostDto
                    //    {
                    //        BlogId = i,
                    //        Content = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb" + i,
                    //        PostId = i + 2,
                    //    },
                    //    new PostDto
                    //    {
                    //        BlogId = i,
                    //        Content = "cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc" + i,
                    //        PostId = i + 3,
                    //    },
                    //},
                    Url = "https://dassssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss"
                };

                _class1.InsertBlogs(blog);
            }
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
