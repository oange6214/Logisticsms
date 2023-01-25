﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.DAL;
using Logisticsm.DAL.Models;
using Logisticsm.Helper;
using Logisticsm.Windows;
using System.Windows;

namespace Logisticsm.ViewModels
{
	public class LoginViewModel : ObservableObject
	{
		#region 

		private readonly MemberProvider _memberProvider = new();

		#endregion

		#region Properties


		private Member _member = new() { Name = "admin" };

		public Member Member
		{
			get => _member;
			set
			{
				SetProperty(ref _member, value);
			}
		}

		#endregion

		#region Commands

		public RelayCommand<LoginWindow> LoginCommand
		{
			get
			{
				var command = new RelayCommand<LoginWindow>((window) =>
				{
					if (string.IsNullOrEmpty(Member.Name)) return;
					if (string.IsNullOrEmpty(window?._passwordBox.Password)) return;

					Member? currentUser = _memberProvider.GetAll().FirstOrDefault(item => item.Name == Member.Name);
					if (currentUser == null)
					{
						MessageBox.Show("無此用戶");
						return;
					}

					string password = MD5Helper.GetMD5(window._passwordBox.Password);
					if (currentUser.Password != password)
					{
						MessageBox.Show("密碼錯誤");
						return;
					}

					AppData.Instance.CurrentUser = currentUser;

					MainWindow mainWindow = new();
					mainWindow.Show();

					window.Close();
				});
				return command;
			}
		}

		#endregion

	}
}
