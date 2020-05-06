using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Settings.Dtos
{
	public class SettingDto
	{
		public Guid Id { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
