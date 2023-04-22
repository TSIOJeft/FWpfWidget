using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DirCleaner.pages
{
    public partial class NavigationView : UserControl
    {
        public event EventHandler navigationItemClick;
        private List<ItemData> itemDatas = new List<ItemData>();

        public NavigationView()
        {
            InitializeComponent();
            initData();
            initView();
        }

        public void initView()
        {
            navigationItems.ItemsSource = itemDatas;
        }

        public void initData()
        {
            int[] divider = { 3,5 };
            string[] itemTitle =
            {
                "快速开始", "常规清理", "闲杂文件清理", "常规文件夹清理", "规则管理", "文件占用", "文件大小分析"
            };
            string[] itemID =
            {
                "fastClean", "formalClean", "chaosClean", "badFolder", "taskManage", "unlockFile", "filesizeAnalysis"
            };
            string[] itemIcon =
            {
                "\xec4a", "\xe8bf" , "\xe9f9","\xe8b7", "\xe8c0", "\xe785", "\xe8a4"
            };
            for (int i = 0; i < itemTitle.Length; i++)
            {
                ItemData itemData = new ItemData();
                itemData.itemTitle = itemTitle[i];
                itemData.itemIcon = itemIcon[i];
                itemData.itemID = itemID[i];
                itemData.index = i;
                if (i == 0)
                {
                    itemData.select = true;
                }

                itemDatas.Add(itemData);
                if (divider.Contains(i))
                {
                    ItemData itemData1 = new ItemData();
                    itemData1._isDivider = true;
                    itemDatas.Add(itemData1);
                }
            }
        }

        public class ItemData : INotifyPropertyChanged
        {
            public bool _isDivider;
            public string itemTitle { get; set; }

            public string itemIcon { get; set; }

            public Visibility isDivider
            {
                get
                {
                    if (_isDivider)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
            }

            public Visibility risDivider
            {
                get
                {
                    if (_isDivider)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
            }

            public int index { get; set; }

            public bool _select;

            public string itemID { get; set; }

            public bool select
            {
                get { return _select; }
                set
                {
                    _select = value;
                    OnPropertyChanged("select");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        private void NavigationItem_Click(object sender, RoutedEventArgs e)
        {
            if (navigationItemClick != null) navigationItemClick(sender, e);
            var button = (Button)sender;
            int index = Convert.ToInt32(button.Uid);
            for (int i = 0; i < itemDatas.Count; i++)
            {
                ItemData itemData = itemDatas[i];
                if (index == i)
                {
                    if (!itemData.select)
                        itemData.select = true;
                }
                else
                {
                    itemData.select = false;
                }
            }
        }
    }
}