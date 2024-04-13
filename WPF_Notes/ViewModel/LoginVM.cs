using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Notes.Model;
using WPF_Notes.ViewModel.Commands;

namespace WPF_Notes.ViewModel
{
    internal class LoginVM
    {
		private User user;

		public User User
        {
			get { return user; }
			set { user = value; }
		}

		public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand	LoginCommand { get; set; }

        public LoginVM() 
		{
			RegisterCommand = new RegisterCommand(this);
			LoginCommand = new LoginCommand(this);
		}
	}
}
