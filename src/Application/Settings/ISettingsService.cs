using Application.Settings.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Settings
{
	public interface ISettingsService: ICrudService<Guid, CreateSetting, ReadSetting, UpdateSetting, SettingDto>
	{
	}
}
