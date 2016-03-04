﻿using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using PayBay.Model;
using PayBay.View.BottomFunctionGroup.SettingGroup;
using PayBay.View.BottomFunctionGroup;
using PayBay.View.TopFunctionGroup;
using PayBay.View.MiddleFunctionGroup;
using PayBay.View.OrderGroup;
using PayBay.Utilities.Common;
using Windows.UI.Popups;
using System;

namespace PayBay.ViewModel.StartGroup
{
    public class StartViewModel : BaseViewModel
    {
        private ObservableCollection<MenuListItem> _topFunctionItemList;
        private ObservableCollection<MenuListItem> _bottomFunctionItemList;

        private ObservableCollection<MenuListItem> _functionsOfUser;
        private ObservableCollection<MenuListItem> _functionsOfStoreOwner;
        private ObservableCollection<MenuListItem> _functionsOfAdmin;
        private ObservableCollection<MenuListItem> _functionsOfGuest;

        private MenuListItem _logout;
                            
        #region Property with calling to PropertyChanged
        public ObservableCollection<MenuListItem> TopFunctionItemList
        {
            get { return _topFunctionItemList; }
            set
            {
                if (Equals(value, _topFunctionItemList)) return;
				_topFunctionItemList = value;
                OnPropertyChanged();
            }
        }
		
		public ObservableCollection<MenuListItem> BottomFunctionItemList
        {
            get { return _bottomFunctionItemList; }
            set
            {
                if (Equals(value, _bottomFunctionItemList)) return;
                _bottomFunctionItemList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MenuListItem> FunctionsOfUser
        {
            get
            {
                return _functionsOfUser;
            }

            set
            {
                _functionsOfUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MenuListItem> FunctionsOfStoreOwner
        {
            get
            {
                return _functionsOfStoreOwner;
            }

            set
            {
                _functionsOfStoreOwner = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MenuListItem> FunctionsOfAdmin
        {
            get
            {
                return _functionsOfAdmin;
            }

            set
            {
                _functionsOfAdmin = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MenuListItem> FunctionsOfGuest
        {
            get
            {
                return _functionsOfGuest;
            }

            set
            {
                _functionsOfGuest = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public StartViewModel()
        {
            InitializeProperties();
            InitializeData();            
            MediateClass.StartVM = this;            
        }    
                
        ///<summary>
        ///Initialize properties
        /// </summary>
        private void InitializeProperties()
        {
            _functionsOfAdmin = new ObservableCollection<MenuListItem>();
            _functionsOfStoreOwner = new ObservableCollection<MenuListItem>();
            _functionsOfUser = new ObservableCollection<MenuListItem>();
            _functionsOfGuest = new ObservableCollection<MenuListItem>();

            _topFunctionItemList = new ObservableCollection<MenuListItem>();
            _bottomFunctionItemList = new ObservableCollection<MenuListItem>();
        }

        /// <summary>
        /// Initialize function list in splitview control
        /// </summary>
        private void InitializeData()
        {
			//----------------------------------------Top function--------------------------------------------
			
			MenuListItem m = new MenuListItem
			{
				Name = "Home",
				IsEnabled = true,
				MenuF = MenuFunc.HomePage,
				Icon = "/Assets/Icon/home_icon.png"
					//"M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
			};

            FunctionsOfGuest.Add(m);

            m = new MenuListItem
            {
                Name = "My PayBay",
                IsEnabled = true,
                MenuF = MenuFunc.MyPayBay,
                Icon = "/Assets/Icon/mypaybay_icon.png"
                //"M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
            };

            FunctionsOfGuest.Add(m);

			m = new MenuListItem
			{
				Name = "Inbox",
				IsEnabled = true,
				MenuF = MenuFunc.Inbox,
				Icon = "/Assets/Icon/inbox_icon.png"
				//"M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
			};

            FunctionsOfUser.Add(m);            

            m = new MenuListItem
			{
				Name = "My Favorites",
				IsEnabled = true,
				MenuF = MenuFunc.MyFavorites,
				Icon = "/Assets/Icon/favourite_icon.png"
				//"M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
			};

            FunctionsOfUser.Add(m);

            m = new MenuListItem
            {
                Name = "Call",
                IsEnabled = true,
                MenuF = MenuFunc.FreeCall,
                Icon = "/Assets/Icon/call_icon.png"
                //"M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
            };

            FunctionsOfUser.Add(m);

            m = new MenuListItem
            {
                Name = "Function 2",
                IsEnabled = true,
                MenuF = MenuFunc.Func2,
                Icon = "/Assets/Icon/function_icon.png"
				// "M31.348,48.494C32.685356,48.494 33.770001,49.577489 33.770001,50.913598 33.770001,52.249612 32.685356,53.333001 31.348,53.333001 30.013346,53.333001 28.930001,52.249612 28.930001,50.913598 28.930001,49.577489 30.013346,48.494 31.348,48.494z M22.1227,48.494C23.457356,48.494 24.542,49.577489 24.542,50.913598 24.542,52.249612 23.457356,53.333001 22.1227,53.333001 20.785345,53.333001 19.702,52.249612 19.702,50.913598 19.702,49.577489 20.785345,48.494 22.1227,48.494z M35.26405,41.272998C36.60138,41.272998 37.686001,42.357621 37.686001,43.69365 37.686001,45.029576 36.60138,46.113 35.26405,46.113 33.929424,46.113 32.846001,45.029576 32.846001,43.69365 32.846001,42.357621 33.929424,41.272998 35.26405,41.272998z M26.508051,41.272998C27.845278,41.272998 28.930001,42.357621 28.930001,43.69365 28.930001,45.029576 27.845278,46.113 26.508051,46.113 25.173323,46.113 24.09,45.029576 24.09,43.69365 24.09,42.357621 25.173323,41.272998 26.508051,41.272998z M17.2318,41.272998C18.567579,41.272998 19.652001,42.357621 19.652001,43.69365 19.652001,45.029576 18.567579,46.113 17.2318,46.113 15.896121,46.113 14.813,45.029576 14.813,43.69365 14.813,42.357621 15.896121,41.272998 17.2318,41.272998z M44.021054,19.436998L46.029053,20.664867C46.758328,21.111492 46.98745,22.068645 46.540806,22.799085 46.17791,23.393704 45.478031,23.656512 44.837174,23.493527L44.815964,23.486991 44.755482,23.36905C44.331001,22.588596,43.814919,21.86488,43.221752,21.212421L43.130867,21.117109 43.312847,20.826513C43.580288,20.382326,43.817284,19.918125,44.021054,19.436998z M44.828007,13.720999L47.233292,13.786101C48.090233,13.810902 48.76614,14.523125 48.741337,15.381254 48.719235,16.238081 48.005619,16.913905 47.148682,16.890402L44.771996,16.825301C44.846211,16.353886 44.893116,15.874769 44.908718,15.386453 44.92432,14.821335 44.895718,14.266617 44.828007,13.720999z M21.976753,10.773001C26.979286,10.773001 31.208513,14.566015 32.614826,19.786035 33.396027,19.568634 34.218937,19.442332 35.069241,19.442332 40.136975,19.442332 44.245003,23.550448 44.245003,28.616866 44.245003,31.877278 42.539391,34.731389 39.976872,36.357696 38.948166,37.194999 37.640854,37.698899 36.213749,37.710603 35.840042,37.756803 35.459843,37.790002 35.069241,37.790002 34.687737,37.790002 34.312737,37.756803 33.940331,37.7119L10.320378,37.7119C9.942775,37.756803 9.5625736,37.790002 9.1705706,37.790002 4.1054971,37.790002 2.7073101E-07,33.840787 0,28.969668 2.7073101E-07,24.09855 4.1054971,20.150635 9.1705706,20.150635 9.871175,20.150635 10.552179,20.232635 11.208384,20.375936 12.447993,14.856316 16.789219,10.773001 21.976753,10.773001z M45.672386,7.174134C46.223133,7.1814017 46.751801,7.4817498 47.025681,8.0036976 47.424053,8.762845 47.132473,9.7023534 46.374729,10.100129L44.221188,11.232998C43.857914,10.249938,43.361851,9.3319101,42.749996,8.4998286L44.929535,7.352598C45.167145,7.2280803,45.422043,7.1708302,45.672386,7.174134z M22.363896,6.4076095C22.626137,6.4149718,22.889933,6.4890428,23.129202,6.6360126L25.232002,7.9243071C24.983002,8.2199299,24.749006,8.5292008,24.531187,8.8507769L24.325581,9.1660111 23.90588,9.0875378C23.275191,8.9828922 22.630835,8.9289981 21.976551,8.9289984 21.806952,8.9289981 21.637982,8.9326974 21.469708,8.9400355L21.134642,8.961946 21.056421,8.86325C20.704596,8.3712656 20.660401,7.6966183 20.996401,7.148304 21.275526,6.6914134 21.754027,6.4305587 22.251677,6.4085212 22.289,6.4068675 22.326431,6.4065576 22.363896,6.4076095z M33.776642,5.6300144C33.857746,5.6301656 33.939079,5.6313434 34.020622,5.6335616 39.242119,5.7754722 43.354096,10.120625 43.213494,15.340688 43.169556,16.971583 42.715012,18.494635 41.951519,19.813686L41.865544,19.954264 41.657589,19.790779C39.818852,18.414998 37.537552,17.599476 35.068867,17.599476 34.66787,17.599476 34.261574,17.622975 33.857876,17.668475 32.385815,13.866086 29.595959,11.014804 26.199589,9.711732L26.027451,9.6495949 26.288347,9.2962282C28.025217,7.0564322,30.74283,5.6243949,33.776642,5.6300144z M40.950501,2.1025438C41.249367,2.0946379 41.553959,2.1728296 41.825966,2.345706 42.547425,2.8067083 42.760941,3.7664986 42.298603,4.4892459L40.963902,6.5819988C40.17614,5.913939,39.294472,5.3526425,38.339997,4.9202757L39.681301,2.8197699C39.9702,2.3680468,40.452393,2.1157179,40.950501,2.1025438z M27.487883,1.5610685C28.038714,1.5662165,28.569157,1.8650618,28.844816,2.3860846L30.014,4.5899091C29.027098,4.9434433,28.105391,5.4317431,27.266875,6.033999L26.101694,3.8418369C25.700634,3.0827036 25.989704,2.1438856 26.746128,1.7428083 26.982916,1.6170683 27.237505,1.5587282 27.487883,1.5610685z M34.094422,0.00037670135C34.120896,-0.00020503998 34.147526,-0.00011539459 34.174298,0.00065898895 35.032345,0.022751808 35.706803,0.73763561 35.683403,1.593688L35.615608,4.0929995C35.11044,4.0083418 34.593571,3.9536633 34.066303,3.9406538 33.541535,3.9269848 33.023365,3.952373 32.512997,4.0096216L32.580693,1.5110502C32.603359,0.67979717,33.273724,0.018374443,34.094422,0.00037670135z"
			};

            FunctionsOfStoreOwner.Add(m);

            m = new MenuListItem
            {
                Name = "Function 1",
                IsEnabled = true,
                MenuF = MenuFunc.Func1,
                Icon = "/Assets/Icon/function_icon.png"
                // "M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
            };

            FunctionsOfAdmin.Add(m);

            m = new MenuListItem
            {
                Name = "Function 3",
                IsEnabled = true,
                MenuF = MenuFunc.Func3,
                Icon = "/Assets/Icon/function_icon.png"
				// "M31.348,48.494C32.685356,48.494 33.770001,49.577489 33.770001,50.913598 33.770001,52.249612 32.685356,53.333001 31.348,53.333001 30.013346,53.333001 28.930001,52.249612 28.930001,50.913598 28.930001,49.577489 30.013346,48.494 31.348,48.494z M22.1227,48.494C23.457356,48.494 24.542,49.577489 24.542,50.913598 24.542,52.249612 23.457356,53.333001 22.1227,53.333001 20.785345,53.333001 19.702,52.249612 19.702,50.913598 19.702,49.577489 20.785345,48.494 22.1227,48.494z M35.26405,41.272998C36.60138,41.272998 37.686001,42.357621 37.686001,43.69365 37.686001,45.029576 36.60138,46.113 35.26405,46.113 33.929424,46.113 32.846001,45.029576 32.846001,43.69365 32.846001,42.357621 33.929424,41.272998 35.26405,41.272998z M26.508051,41.272998C27.845278,41.272998 28.930001,42.357621 28.930001,43.69365 28.930001,45.029576 27.845278,46.113 26.508051,46.113 25.173323,46.113 24.09,45.029576 24.09,43.69365 24.09,42.357621 25.173323,41.272998 26.508051,41.272998z M17.2318,41.272998C18.567579,41.272998 19.652001,42.357621 19.652001,43.69365 19.652001,45.029576 18.567579,46.113 17.2318,46.113 15.896121,46.113 14.813,45.029576 14.813,43.69365 14.813,42.357621 15.896121,41.272998 17.2318,41.272998z M44.021054,19.436998L46.029053,20.664867C46.758328,21.111492 46.98745,22.068645 46.540806,22.799085 46.17791,23.393704 45.478031,23.656512 44.837174,23.493527L44.815964,23.486991 44.755482,23.36905C44.331001,22.588596,43.814919,21.86488,43.221752,21.212421L43.130867,21.117109 43.312847,20.826513C43.580288,20.382326,43.817284,19.918125,44.021054,19.436998z M44.828007,13.720999L47.233292,13.786101C48.090233,13.810902 48.76614,14.523125 48.741337,15.381254 48.719235,16.238081 48.005619,16.913905 47.148682,16.890402L44.771996,16.825301C44.846211,16.353886 44.893116,15.874769 44.908718,15.386453 44.92432,14.821335 44.895718,14.266617 44.828007,13.720999z M21.976753,10.773001C26.979286,10.773001 31.208513,14.566015 32.614826,19.786035 33.396027,19.568634 34.218937,19.442332 35.069241,19.442332 40.136975,19.442332 44.245003,23.550448 44.245003,28.616866 44.245003,31.877278 42.539391,34.731389 39.976872,36.357696 38.948166,37.194999 37.640854,37.698899 36.213749,37.710603 35.840042,37.756803 35.459843,37.790002 35.069241,37.790002 34.687737,37.790002 34.312737,37.756803 33.940331,37.7119L10.320378,37.7119C9.942775,37.756803 9.5625736,37.790002 9.1705706,37.790002 4.1054971,37.790002 2.7073101E-07,33.840787 0,28.969668 2.7073101E-07,24.09855 4.1054971,20.150635 9.1705706,20.150635 9.871175,20.150635 10.552179,20.232635 11.208384,20.375936 12.447993,14.856316 16.789219,10.773001 21.976753,10.773001z M45.672386,7.174134C46.223133,7.1814017 46.751801,7.4817498 47.025681,8.0036976 47.424053,8.762845 47.132473,9.7023534 46.374729,10.100129L44.221188,11.232998C43.857914,10.249938,43.361851,9.3319101,42.749996,8.4998286L44.929535,7.352598C45.167145,7.2280803,45.422043,7.1708302,45.672386,7.174134z M22.363896,6.4076095C22.626137,6.4149718,22.889933,6.4890428,23.129202,6.6360126L25.232002,7.9243071C24.983002,8.2199299,24.749006,8.5292008,24.531187,8.8507769L24.325581,9.1660111 23.90588,9.0875378C23.275191,8.9828922 22.630835,8.9289981 21.976551,8.9289984 21.806952,8.9289981 21.637982,8.9326974 21.469708,8.9400355L21.134642,8.961946 21.056421,8.86325C20.704596,8.3712656 20.660401,7.6966183 20.996401,7.148304 21.275526,6.6914134 21.754027,6.4305587 22.251677,6.4085212 22.289,6.4068675 22.326431,6.4065576 22.363896,6.4076095z M33.776642,5.6300144C33.857746,5.6301656 33.939079,5.6313434 34.020622,5.6335616 39.242119,5.7754722 43.354096,10.120625 43.213494,15.340688 43.169556,16.971583 42.715012,18.494635 41.951519,19.813686L41.865544,19.954264 41.657589,19.790779C39.818852,18.414998 37.537552,17.599476 35.068867,17.599476 34.66787,17.599476 34.261574,17.622975 33.857876,17.668475 32.385815,13.866086 29.595959,11.014804 26.199589,9.711732L26.027451,9.6495949 26.288347,9.2962282C28.025217,7.0564322,30.74283,5.6243949,33.776642,5.6300144z M40.950501,2.1025438C41.249367,2.0946379 41.553959,2.1728296 41.825966,2.345706 42.547425,2.8067083 42.760941,3.7664986 42.298603,4.4892459L40.963902,6.5819988C40.17614,5.913939,39.294472,5.3526425,38.339997,4.9202757L39.681301,2.8197699C39.9702,2.3680468,40.452393,2.1157179,40.950501,2.1025438z M27.487883,1.5610685C28.038714,1.5662165,28.569157,1.8650618,28.844816,2.3860846L30.014,4.5899091C29.027098,4.9434433,28.105391,5.4317431,27.266875,6.033999L26.101694,3.8418369C25.700634,3.0827036 25.989704,2.1438856 26.746128,1.7428083 26.982916,1.6170683 27.237505,1.5587282 27.487883,1.5610685z M34.094422,0.00037670135C34.120896,-0.00020503998 34.147526,-0.00011539459 34.174298,0.00065898895 35.032345,0.022751808 35.706803,0.73763561 35.683403,1.593688L35.615608,4.0929995C35.11044,4.0083418 34.593571,3.9536633 34.066303,3.9406538 33.541535,3.9269848 33.023365,3.952373 32.512997,4.0096216L32.580693,1.5110502C32.603359,0.67979717,33.273724,0.018374443,34.094422,0.00037670135z"
			};

            FunctionsOfAdmin.Add(m);

            m = new MenuListItem
            {
                Name = "Function 4",
                IsEnabled = true,
                MenuF = MenuFunc.Func4,
                Icon = "/Assets/Icon/function_icon.png"
				// "M31.348,48.494C32.685356,48.494 33.770001,49.577489 33.770001,50.913598 33.770001,52.249612 32.685356,53.333001 31.348,53.333001 30.013346,53.333001 28.930001,52.249612 28.930001,50.913598 28.930001,49.577489 30.013346,48.494 31.348,48.494z M22.1227,48.494C23.457356,48.494 24.542,49.577489 24.542,50.913598 24.542,52.249612 23.457356,53.333001 22.1227,53.333001 20.785345,53.333001 19.702,52.249612 19.702,50.913598 19.702,49.577489 20.785345,48.494 22.1227,48.494z M35.26405,41.272998C36.60138,41.272998 37.686001,42.357621 37.686001,43.69365 37.686001,45.029576 36.60138,46.113 35.26405,46.113 33.929424,46.113 32.846001,45.029576 32.846001,43.69365 32.846001,42.357621 33.929424,41.272998 35.26405,41.272998z M26.508051,41.272998C27.845278,41.272998 28.930001,42.357621 28.930001,43.69365 28.930001,45.029576 27.845278,46.113 26.508051,46.113 25.173323,46.113 24.09,45.029576 24.09,43.69365 24.09,42.357621 25.173323,41.272998 26.508051,41.272998z M17.2318,41.272998C18.567579,41.272998 19.652001,42.357621 19.652001,43.69365 19.652001,45.029576 18.567579,46.113 17.2318,46.113 15.896121,46.113 14.813,45.029576 14.813,43.69365 14.813,42.357621 15.896121,41.272998 17.2318,41.272998z M44.021054,19.436998L46.029053,20.664867C46.758328,21.111492 46.98745,22.068645 46.540806,22.799085 46.17791,23.393704 45.478031,23.656512 44.837174,23.493527L44.815964,23.486991 44.755482,23.36905C44.331001,22.588596,43.814919,21.86488,43.221752,21.212421L43.130867,21.117109 43.312847,20.826513C43.580288,20.382326,43.817284,19.918125,44.021054,19.436998z M44.828007,13.720999L47.233292,13.786101C48.090233,13.810902 48.76614,14.523125 48.741337,15.381254 48.719235,16.238081 48.005619,16.913905 47.148682,16.890402L44.771996,16.825301C44.846211,16.353886 44.893116,15.874769 44.908718,15.386453 44.92432,14.821335 44.895718,14.266617 44.828007,13.720999z M21.976753,10.773001C26.979286,10.773001 31.208513,14.566015 32.614826,19.786035 33.396027,19.568634 34.218937,19.442332 35.069241,19.442332 40.136975,19.442332 44.245003,23.550448 44.245003,28.616866 44.245003,31.877278 42.539391,34.731389 39.976872,36.357696 38.948166,37.194999 37.640854,37.698899 36.213749,37.710603 35.840042,37.756803 35.459843,37.790002 35.069241,37.790002 34.687737,37.790002 34.312737,37.756803 33.940331,37.7119L10.320378,37.7119C9.942775,37.756803 9.5625736,37.790002 9.1705706,37.790002 4.1054971,37.790002 2.7073101E-07,33.840787 0,28.969668 2.7073101E-07,24.09855 4.1054971,20.150635 9.1705706,20.150635 9.871175,20.150635 10.552179,20.232635 11.208384,20.375936 12.447993,14.856316 16.789219,10.773001 21.976753,10.773001z M45.672386,7.174134C46.223133,7.1814017 46.751801,7.4817498 47.025681,8.0036976 47.424053,8.762845 47.132473,9.7023534 46.374729,10.100129L44.221188,11.232998C43.857914,10.249938,43.361851,9.3319101,42.749996,8.4998286L44.929535,7.352598C45.167145,7.2280803,45.422043,7.1708302,45.672386,7.174134z M22.363896,6.4076095C22.626137,6.4149718,22.889933,6.4890428,23.129202,6.6360126L25.232002,7.9243071C24.983002,8.2199299,24.749006,8.5292008,24.531187,8.8507769L24.325581,9.1660111 23.90588,9.0875378C23.275191,8.9828922 22.630835,8.9289981 21.976551,8.9289984 21.806952,8.9289981 21.637982,8.9326974 21.469708,8.9400355L21.134642,8.961946 21.056421,8.86325C20.704596,8.3712656 20.660401,7.6966183 20.996401,7.148304 21.275526,6.6914134 21.754027,6.4305587 22.251677,6.4085212 22.289,6.4068675 22.326431,6.4065576 22.363896,6.4076095z M33.776642,5.6300144C33.857746,5.6301656 33.939079,5.6313434 34.020622,5.6335616 39.242119,5.7754722 43.354096,10.120625 43.213494,15.340688 43.169556,16.971583 42.715012,18.494635 41.951519,19.813686L41.865544,19.954264 41.657589,19.790779C39.818852,18.414998 37.537552,17.599476 35.068867,17.599476 34.66787,17.599476 34.261574,17.622975 33.857876,17.668475 32.385815,13.866086 29.595959,11.014804 26.199589,9.711732L26.027451,9.6495949 26.288347,9.2962282C28.025217,7.0564322,30.74283,5.6243949,33.776642,5.6300144z M40.950501,2.1025438C41.249367,2.0946379 41.553959,2.1728296 41.825966,2.345706 42.547425,2.8067083 42.760941,3.7664986 42.298603,4.4892459L40.963902,6.5819988C40.17614,5.913939,39.294472,5.3526425,38.339997,4.9202757L39.681301,2.8197699C39.9702,2.3680468,40.452393,2.1157179,40.950501,2.1025438z M27.487883,1.5610685C28.038714,1.5662165,28.569157,1.8650618,28.844816,2.3860846L30.014,4.5899091C29.027098,4.9434433,28.105391,5.4317431,27.266875,6.033999L26.101694,3.8418369C25.700634,3.0827036 25.989704,2.1438856 26.746128,1.7428083 26.982916,1.6170683 27.237505,1.5587282 27.487883,1.5610685z M34.094422,0.00037670135C34.120896,-0.00020503998 34.147526,-0.00011539459 34.174298,0.00065898895 35.032345,0.022751808 35.706803,0.73763561 35.683403,1.593688L35.615608,4.0929995C35.11044,4.0083418 34.593571,3.9536633 34.066303,3.9406538 33.541535,3.9269848 33.023365,3.952373 32.512997,4.0096216L32.580693,1.5110502C32.603359,0.67979717,33.273724,0.018374443,34.094422,0.00037670135z"
			};

            FunctionsOfAdmin.Add(m);                     

			//----------------------------------------Bottom function--------------------------------------------                     
            
			m = new MenuListItem
			{
				Name = "Feedback and Apps",
				MenuF = MenuFunc.FeedbackAndApps,
				Icon = "/Assets/Icon/feedback_icon.png"
				// "M14.682,10.493 C12.398,10.493 10.546,12.344 10.546,14.628 C10.546,16.912 12.398,18.764 14.682,18.764 C16.966,18.764 18.818,16.912 18.818,14.628 C18.818,12.344 16.966,10.493 14.682,10.493 z M14.682,8.46201 C18.088,8.46201 20.849,11.223 20.849,14.628 C20.849,18.034 18.088,20.795 14.682,20.795 C11.276,20.795 8.515,18.034 8.515,14.628 C8.515,11.223 11.276,8.46201 14.682,8.46201 z M13.423,2.454 C13.135,3.083 12.67,3.702 12.227,4.922 C12.149,5.142 11.983,5.316 11.771,5.403 L10.084,6.104 C9.883,6.187 9.66,6.185 9.461,6.099 C8.243,5.57 6.8,4.978 6.15,4.758 L4.643,6.263 C4.886,6.917 5.494,8.299 6.038,9.466 C6.138,9.678 6.141,9.925 6.052,10.145 L5.352,11.837 C5.266,12.045 5.103,12.208 4.896,12.289 C3.671,12.772 3.054,13.143 2.431,13.447 L2.431,15.889 C3.06,16.179 3.669,16.623 4.888,17.066 C5.103,17.146 5.276,17.312 5.363,17.526 L6.059,19.217 C6.146,19.427 6.143,19.665 6.054,19.873 C5.532,21.077 4.951,22.499 4.723,23.159 L6.23,24.664 C6.875,24.426 8.275,23.808 9.459,23.258 C9.661,23.164 9.893,23.159 10.1,23.244 L11.793,23.945 C11.998,24.029 12.16,24.195 12.241,24.403 C12.726,25.631 13.118,26.261 13.423,26.881 L15.866,26.881 C16.152,26.254 16.618,25.633 17.061,24.411 C17.14,24.195 17.306,24.018 17.517,23.933 L19.21,23.231 C19.407,23.149 19.633,23.151 19.832,23.239 C21.049,23.766 22.487,24.359 23.139,24.582 L24.645,23.072 C24.404,22.421 23.792,21.038 23.249,19.875 C23.151,19.662 23.144,19.414 23.234,19.195 L23.931,17.502 C24.016,17.295 24.182,17.131 24.387,17.051 C25.613,16.567 26.231,16.191 26.858,15.889 L26.858,13.447 C26.224,13.159 25.617,12.718 24.399,12.276 C24.184,12.198 24.01,12.033 23.921,11.816 L23.224,10.12 C23.139,9.909 23.139,9.671 23.231,9.461 C23.752,8.26 24.333,6.839 24.562,6.176 L23.054,4.672 C22.408,4.909 21.007,5.527 19.824,6.078 C19.624,6.173 19.39,6.179 19.183,6.093 L17.491,5.394 C17.287,5.31 17.122,5.143 17.042,4.934 C16.559,3.706 16.17,3.077 15.866,2.454 z M13.062,0 L16.098,0 C16.676,0 16.844,0 18.42,3.99 L19.47,4.423 C22.708,2.924 23.098,2.924 23.267,2.924 C23.48,2.924 23.719,3.018 23.87,3.169 L26.023,5.313 C26.441,5.736 26.56,5.856 24.857,9.805 L25.28,10.835 C29.288,12.297 29.288,12.472 29.288,13.083 L29.288,16.123 C29.288,16.731 29.286,16.888 25.293,18.468 L24.871,19.495 C26.669,23.362 26.545,23.49 26.117,23.924 L23.963,26.076 C23.809,26.228 23.571,26.323 23.359,26.323 C23.205,26.323 22.802,26.323 19.507,24.899 L18.457,25.333 C16.991,29.337 16.82,29.337 16.226,29.337 L13.19,29.337 C12.611,29.337 12.444,29.337 10.868,25.35 L9.816,24.912 C6.575,26.415 6.186,26.415 6.019,26.415 C5.805,26.415 5.564,26.32 5.413,26.166 L3.266,24.017 C2.839,23.587 2.729,23.476 4.431,19.529 L4.007,18.505 C0,17.039 0,16.862 0,16.251 L0,13.21 C0,12.602 0,12.447 3.993,10.868 L4.417,9.842 C2.62,5.969 2.744,5.844 3.175,5.41 L5.326,3.262 C5.476,3.111 5.717,3.016 5.93,3.016 C6.085,3.016 6.491,3.016 9.786,4.438 L10.831,4.003 C12.297,0 12.483,0 13.062,0 z"
			};
			BottomFunctionItemList.Add(m);

            m = new MenuListItem
            {
                Name = "About Us",
                IsEnabled = true,
                MenuF = MenuFunc.AboutUs,
                Icon = "/Assets/Icon/about_icon.png"
                //"M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
            };

            BottomFunctionItemList.Add(m);

            m = new MenuListItem
            {
                Name = "Settings",
                MenuF = MenuFunc.Settings,
                Icon = "/Assets/Icon/setting_icon.png"
                   // "M14.682,10.493 C12.398,10.493 10.546,12.344 10.546,14.628 C10.546,16.912 12.398,18.764 14.682,18.764 C16.966,18.764 18.818,16.912 18.818,14.628 C18.818,12.344 16.966,10.493 14.682,10.493 z M14.682,8.46201 C18.088,8.46201 20.849,11.223 20.849,14.628 C20.849,18.034 18.088,20.795 14.682,20.795 C11.276,20.795 8.515,18.034 8.515,14.628 C8.515,11.223 11.276,8.46201 14.682,8.46201 z M13.423,2.454 C13.135,3.083 12.67,3.702 12.227,4.922 C12.149,5.142 11.983,5.316 11.771,5.403 L10.084,6.104 C9.883,6.187 9.66,6.185 9.461,6.099 C8.243,5.57 6.8,4.978 6.15,4.758 L4.643,6.263 C4.886,6.917 5.494,8.299 6.038,9.466 C6.138,9.678 6.141,9.925 6.052,10.145 L5.352,11.837 C5.266,12.045 5.103,12.208 4.896,12.289 C3.671,12.772 3.054,13.143 2.431,13.447 L2.431,15.889 C3.06,16.179 3.669,16.623 4.888,17.066 C5.103,17.146 5.276,17.312 5.363,17.526 L6.059,19.217 C6.146,19.427 6.143,19.665 6.054,19.873 C5.532,21.077 4.951,22.499 4.723,23.159 L6.23,24.664 C6.875,24.426 8.275,23.808 9.459,23.258 C9.661,23.164 9.893,23.159 10.1,23.244 L11.793,23.945 C11.998,24.029 12.16,24.195 12.241,24.403 C12.726,25.631 13.118,26.261 13.423,26.881 L15.866,26.881 C16.152,26.254 16.618,25.633 17.061,24.411 C17.14,24.195 17.306,24.018 17.517,23.933 L19.21,23.231 C19.407,23.149 19.633,23.151 19.832,23.239 C21.049,23.766 22.487,24.359 23.139,24.582 L24.645,23.072 C24.404,22.421 23.792,21.038 23.249,19.875 C23.151,19.662 23.144,19.414 23.234,19.195 L23.931,17.502 C24.016,17.295 24.182,17.131 24.387,17.051 C25.613,16.567 26.231,16.191 26.858,15.889 L26.858,13.447 C26.224,13.159 25.617,12.718 24.399,12.276 C24.184,12.198 24.01,12.033 23.921,11.816 L23.224,10.12 C23.139,9.909 23.139,9.671 23.231,9.461 C23.752,8.26 24.333,6.839 24.562,6.176 L23.054,4.672 C22.408,4.909 21.007,5.527 19.824,6.078 C19.624,6.173 19.39,6.179 19.183,6.093 L17.491,5.394 C17.287,5.31 17.122,5.143 17.042,4.934 C16.559,3.706 16.17,3.077 15.866,2.454 z M13.062,0 L16.098,0 C16.676,0 16.844,0 18.42,3.99 L19.47,4.423 C22.708,2.924 23.098,2.924 23.267,2.924 C23.48,2.924 23.719,3.018 23.87,3.169 L26.023,5.313 C26.441,5.736 26.56,5.856 24.857,9.805 L25.28,10.835 C29.288,12.297 29.288,12.472 29.288,13.083 L29.288,16.123 C29.288,16.731 29.286,16.888 25.293,18.468 L24.871,19.495 C26.669,23.362 26.545,23.49 26.117,23.924 L23.963,26.076 C23.809,26.228 23.571,26.323 23.359,26.323 C23.205,26.323 22.802,26.323 19.507,24.899 L18.457,25.333 C16.991,29.337 16.82,29.337 16.226,29.337 L13.19,29.337 C12.611,29.337 12.444,29.337 10.868,25.35 L9.816,24.912 C6.575,26.415 6.186,26.415 6.019,26.415 C5.805,26.415 5.564,26.32 5.413,26.166 L3.266,24.017 C2.839,23.587 2.729,23.476 4.431,19.529 L4.007,18.505 C0,17.039 0,16.862 0,16.251 L0,13.21 C0,12.602 0,12.447 3.993,10.868 L4.417,9.842 C2.62,5.969 2.744,5.844 3.175,5.41 L5.326,3.262 C5.476,3.111 5.717,3.016 5.93,3.016 C6.085,3.016 6.491,3.016 9.786,4.438 L10.831,4.003 C12.297,0 12.483,0 13.062,0 z"
            };
            BottomFunctionItemList.Add(m);

            _logout = new MenuListItem
            {
                Name = "Logout",
                MenuF = MenuFunc.Logout,
                Icon = "/Assets/Icon/logout_icon.png"
                // "M14.682,10.493 C12.398,10.493 10.546,12.344 10.546,14.628 C10.546,16.912 12.398,18.764 14.682,18.764 C16.966,18.764 18.818,16.912 18.818,14.628 C18.818,12.344 16.966,10.493 14.682,10.493 z M14.682,8.46201 C18.088,8.46201 20.849,11.223 20.849,14.628 C20.849,18.034 18.088,20.795 14.682,20.795 C11.276,20.795 8.515,18.034 8.515,14.628 C8.515,11.223 11.276,8.46201 14.682,8.46201 z M13.423,2.454 C13.135,3.083 12.67,3.702 12.227,4.922 C12.149,5.142 11.983,5.316 11.771,5.403 L10.084,6.104 C9.883,6.187 9.66,6.185 9.461,6.099 C8.243,5.57 6.8,4.978 6.15,4.758 L4.643,6.263 C4.886,6.917 5.494,8.299 6.038,9.466 C6.138,9.678 6.141,9.925 6.052,10.145 L5.352,11.837 C5.266,12.045 5.103,12.208 4.896,12.289 C3.671,12.772 3.054,13.143 2.431,13.447 L2.431,15.889 C3.06,16.179 3.669,16.623 4.888,17.066 C5.103,17.146 5.276,17.312 5.363,17.526 L6.059,19.217 C6.146,19.427 6.143,19.665 6.054,19.873 C5.532,21.077 4.951,22.499 4.723,23.159 L6.23,24.664 C6.875,24.426 8.275,23.808 9.459,23.258 C9.661,23.164 9.893,23.159 10.1,23.244 L11.793,23.945 C11.998,24.029 12.16,24.195 12.241,24.403 C12.726,25.631 13.118,26.261 13.423,26.881 L15.866,26.881 C16.152,26.254 16.618,25.633 17.061,24.411 C17.14,24.195 17.306,24.018 17.517,23.933 L19.21,23.231 C19.407,23.149 19.633,23.151 19.832,23.239 C21.049,23.766 22.487,24.359 23.139,24.582 L24.645,23.072 C24.404,22.421 23.792,21.038 23.249,19.875 C23.151,19.662 23.144,19.414 23.234,19.195 L23.931,17.502 C24.016,17.295 24.182,17.131 24.387,17.051 C25.613,16.567 26.231,16.191 26.858,15.889 L26.858,13.447 C26.224,13.159 25.617,12.718 24.399,12.276 C24.184,12.198 24.01,12.033 23.921,11.816 L23.224,10.12 C23.139,9.909 23.139,9.671 23.231,9.461 C23.752,8.26 24.333,6.839 24.562,6.176 L23.054,4.672 C22.408,4.909 21.007,5.527 19.824,6.078 C19.624,6.173 19.39,6.179 19.183,6.093 L17.491,5.394 C17.287,5.31 17.122,5.143 17.042,4.934 C16.559,3.706 16.17,3.077 15.866,2.454 z M13.062,0 L16.098,0 C16.676,0 16.844,0 18.42,3.99 L19.47,4.423 C22.708,2.924 23.098,2.924 23.267,2.924 C23.48,2.924 23.719,3.018 23.87,3.169 L26.023,5.313 C26.441,5.736 26.56,5.856 24.857,9.805 L25.28,10.835 C29.288,12.297 29.288,12.472 29.288,13.083 L29.288,16.123 C29.288,16.731 29.286,16.888 25.293,18.468 L24.871,19.495 C26.669,23.362 26.545,23.49 26.117,23.924 L23.963,26.076 C23.809,26.228 23.571,26.323 23.359,26.323 C23.205,26.323 22.802,26.323 19.507,24.899 L18.457,25.333 C16.991,29.337 16.82,29.337 16.226,29.337 L13.19,29.337 C12.611,29.337 12.444,29.337 10.868,25.35 L9.816,24.912 C6.575,26.415 6.186,26.415 6.019,26.415 C5.805,26.415 5.564,26.32 5.413,26.166 L3.266,24.017 C2.839,23.587 2.729,23.476 4.431,19.529 L4.007,18.505 C0,17.039 0,16.862 0,16.251 L0,13.21 C0,12.602 0,12.447 3.993,10.868 L4.417,9.842 C2.62,5.969 2.744,5.844 3.175,5.41 L5.326,3.262 C5.476,3.111 5.717,3.016 5.93,3.016 C6.085,3.016 6.491,3.016 9.786,4.438 L10.831,4.003 C12.297,0 12.483,0 13.062,0 z"
            };            

            if (MediateClass.UserVM == null || MediateClass.UserVM.UserInfo == null)
            {
                foreach(var item in FunctionsOfGuest)
                {
                    TopFunctionItemList.Add(item);
                }                
            }
		}

        public void EnableFunction(int type)
        {
            if((USERTYPE)type == USERTYPE.GUEST)
            {
                TopFunctionItemList.Clear();
                foreach (var item in FunctionsOfGuest)
                {
                    TopFunctionItemList.Add(item);
                }
            }
            else if((USERTYPE)type == USERTYPE.USER)
            {
                foreach(var item in FunctionsOfUser)
                {
                    TopFunctionItemList.Add(item);
                }
                BottomFunctionItemList.Add(_logout);
            }
            else if((USERTYPE)type == USERTYPE.STOREOWNER)
            {
                foreach (var item in FunctionsOfUser)
                {
                    TopFunctionItemList.Add(item);
                }

                foreach (var item in FunctionsOfStoreOwner)
                {
                    TopFunctionItemList.Add(item);
                }
                BottomFunctionItemList.Add(_logout);
            }
            else
            {
                foreach (var item in FunctionsOfUser)
                {
                    TopFunctionItemList.Add(item);
                }

                foreach (var item in FunctionsOfStoreOwner)
                {
                    TopFunctionItemList.Add(item);
                }

                foreach (var item in FunctionsOfAdmin)
                {
                    TopFunctionItemList.Add(item);
                }
                BottomFunctionItemList.Add(_logout);
            }            
        }

        /// <summary>
        /// Navigate subframe to corresponding page
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="func"></param>
        public async void NavigateToFunction(Frame frame, MenuFunc func)
        {            
            switch (func)
            {
				case MenuFunc.HomePage:
				{
					frame.Navigate(typeof (HomePage));
					break;
				}
				case MenuFunc.Inbox:
				{
					frame.Navigate(typeof(OrderPage));
					break;
				}
				case MenuFunc.MyPayBay:
				{
					frame.Navigate(typeof(MyPayBayPage));
					break;
				}
				case MenuFunc.MyFavorites:
				{
					frame.Navigate(typeof(MyFavoritesPage));
					break;
				}
                case MenuFunc.Func1:
                {
                    frame.Navigate(typeof (Function1Page));
                    break;
                }
                case MenuFunc.Func2:
                {
                    frame.Navigate(typeof (Function2Page));
                    break;
                }
                case MenuFunc.Func3:
                {
                    frame.Navigate(typeof(Function3Page));
                    break;
                }
                case MenuFunc.Func4:
                {
                    frame.Navigate(typeof(Function4Page));
                    break;
                }
                case MenuFunc.FreeCall:
				{
					frame.Navigate(typeof(FreeCallPage));
					break;
				}
				case MenuFunc.AboutUs:
				{
					frame.Navigate(typeof(AboutUsPage));
					break;
				}
				case MenuFunc.Logout:
				{
					//frame.Navigate(typeof(PrintPage));
                    if(MediateClass.UserVM.UserInfo != null)
                    {
                        MediateClass.UserVM.UserInfo = null;
                        MediateClass.StartPage.isLoginControl(false);
                        EnableFunction((int)USERTYPE.GUEST);
                        BottomFunctionItemList.Remove(_logout);                 
                    }
                    else
                    {
                        await new MessageDialog("You are not login!", "Notification!").ShowAsync();
                    }
					break;
				}
				case MenuFunc.FeedbackAndApps:
				{
					frame.Navigate(typeof(FeedbackAndAppsPage));
					break;
				}
                case MenuFunc.Settings:
                {
                    frame.Navigate(typeof (SettingPage));
                    break;
                }
                default:
                {
                    frame.Navigate(typeof (HomePage));
                    break;
                }
            }            
        }
    }
}
