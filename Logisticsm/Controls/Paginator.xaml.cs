using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace Logisticsm.Controls
{
    public partial class Paginator : UserControl, INotifyPropertyChanged
    {
        public Paginator()
        {
            InitializeComponent();
            itemControl.ItemsSource = _collection;
        }


        #region Properties

        public event Action<object, PagesChangedEventArgs> PageChanged;
        private ObservableCollection<PageControlItemModel> _collection = new();
        private List<PageControlItemModel> _list = null;

        private int _fontSize = 14;
        /// <summary>
        /// 尺寸
        /// </summary>
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
                CalcPageNumList();
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 總頁數
        /// </summary>
        public int PageCount
        {
            get { return _pageCount = 0; }
            set { SettingsProperty(ref _pageCount, value); }
        }

        private int _page = 0;
        /// <summary>
        /// 當前頁數
        /// </summary>
        public int Page
        {
            get { return _page; }
            set
            { 
                _page = value; 
                OnPropertyChanged();
                CalcPageNumList();
            }
        }

        private int _recordCount;
        /// <summary>
        /// 記錄總數
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set 
            { 
                _recordCount = value; 
                OnPropertyChanged(); 
                CalcPageNumList(); 
            }
        }

        private int _pageSize = 10;
        /// <summary>
        /// 每頁記錄數
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set 
            { 
                _pageSize = value; 
                OnPropertyChanged(); 
                CalcPageNumList(); 
            }
        }

        private int _rightCount = 3;
        /// <summary>
        /// 當前右邊頁碼數
        /// </summary>
        public int RightCount
        {
            get { return _rightCount; }
            set
            {
                _rightCount = value;
                OnPropertyChanged();
                CalcPageNumList();
            }
        }



        #endregion


        #region Private Methods

        /// <summary>
        /// 上一頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrePage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int prePage = Page - 1;

            if (prePage < 1) prePage = 1;
            if (prePage != Page)
            {
                Page = prePage;
                CalcPageNumList();
                PageChanged?.Invoke(sender, new PagesChangedEventArgs(Page));
            }
        }

        /// <summary>
        /// 當前頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNum_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RadioButton? button = sender as RadioButton;
            if (button.CommandParameter is PageControlItemModel itemModel)
            {
                Page = itemModel.Page;
                CalcPageNumList();
                PageChanged?.Invoke(sender, new PagesChangedEventArgs(Page));
            }
        }


        /// <summary>
        /// 下一頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int nextPage = Page + 1;

            if (nextPage > PageCount) nextPage = PageCount;
            if (nextPage != Page)
            {
                Page = nextPage;
                CalcPageNumList();
                PageChanged?.Invoke(sender, new PagesChangedEventArgs(Page));
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// 計算頁碼
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CalcPageNumList()
        {
            PageCount = (RecordCount - 1) / PageSize + 1;
            _list = new List<PageControlItemModel>();

            // 第一頁
            PageControlItemModel item = new(1, Page);
            _list.Add(item);

            // 當前頁碼連續頁碼
            for (int i = Page - RightCount; i <= Page + RightCount; i++)
            {
                if ( i > 0 && i < PageCount)
                {
                    item = new PageControlItemModel(i, Page);
                    if (!_list.Exists(x => x.Page == item.Page))
                    {
                        _list.Add(item);
                    }
                }
            }

            // 最後一頁
            item = new PageControlItemModel(PageCount, Page);
            if (!_list.Exists(x => x.Page == item.Page))
            {
                _list.Add(item);
            }

            for (int i = _list.Count; i > 0; i--)
            {
                if ( (_list[i].Page - _list[i - 1].Page) > 1)
                {
                    _list.Insert(i, new PageControlItemModel(0, Page, 2));
                }
            }

            // 上一頁按鈕狀態
            if (Page == 1)
            {
                btnPrePage.IsEnabled = false;
                btnPrePage.Foreground = new SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                btnPrePage.IsEnabled = true;
                btnPrePage.Foreground = new SolidColorBrush(Colors.White);
            }

            // 下一頁按鈕狀態
            if (Page == 1)
            {
                btnNextPage.IsEnabled = false;
                btnNextPage.Foreground = new SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                btnNextPage.IsEnabled = true;
                btnNextPage.Foreground = new SolidColorBrush(Colors.White);
            }

            _collection.Clear();
            _list.ForEach(x => _collection.Add(x));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion

    }
}
