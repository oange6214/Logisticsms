using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Logisticsm.Controls
{
    public class PageControlItemModel : ObservableObject
    {
        #region Properties

        private int _type = 1;
        /// <summary>
        /// 類型
        /// </summary>
        public int Type
        {
            get { return _type; }
            set 
            { 
                SetProperty(ref _type, value); 

                NumVisibility = _type == 1 ? Visibility.Visible : Visibility.Collapsed;
                OmitVisibility = _type == 1 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility _numVisibility;
        /// <summary>
        /// 數字可見
        /// </summary>
        public Visibility NumVisibility
        {
            get { return _numVisibility; }
            set { SetProperty(ref _numVisibility, value); }
        }

        private Visibility _omitVisibility;
        /// <summary>
        /// 省略號可見
        /// </summary>
        public Visibility OmitVisibility
        {
            get { return _omitVisibility; }
            set { SetProperty(ref _omitVisibility, value); }
        }

        private int _page;
        /// <summary>
        /// 頁碼
        /// </summary>
        public int Page
        {
            get { return _page; }
            set { SetProperty(ref _page, value); }
        }

        private bool _isCurrentPage;
        /// <summary>
        /// 是否當前頁
        /// </summary>
        public bool IsCurrentPage
        {
            get { return _isCurrentPage; }
            set 
            { 
                SetProperty(ref _isCurrentPage, value); 
                if (_isCurrentPage)
                {
                    CurrentPageColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00F0FF"));
                }
                else
                {
                    CurrentPageColor = new(Colors.White);
                }
            }
        }

        private SolidColorBrush _CurrentPageColor = new(Colors.White);
        /// <summary>
        /// 當前頁面顏色
        /// </summary>
        public SolidColorBrush CurrentPageColor
        {
            get { return _CurrentPageColor; }
            set { SetProperty(ref _CurrentPageColor, value); }
        }


        #endregion

        public PageControlItemModel(int page, int currentPage, int type = 1)
        {
            _type = type;
            _page = page;
            IsCurrentPage = page == currentPage;
        }
    }
}