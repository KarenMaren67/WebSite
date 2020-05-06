using Application.Settings.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Settings
{
	internal class SettingsService : CrudService<Setting, Guid, CreateSetting, ReadSetting, UpdateSetting, SettingDto>, ISettingsService
	{
		public SettingsService(IRepository<Setting, Guid> repository, IMapper mapper) : base(repository, mapper)
		{
		}

		protected override IQueryable<Setting> Read(IQueryable<Setting> entities, ReadSetting readEntity)
		{
			return entities.Where(x => x.Key == readEntity.Key);
		}
	}
}
