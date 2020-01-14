using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wboy.Domain.PlatformModule.Entities;
using Wboy.Domain.PlatformModule.IRepository;
using Wboy.Infrastructure.Core.Configuration;
using Wboy.Infrastructure.Core.Dependency;

namespace Wboy.MySql.EfCore.Configuration
{
    public class DbSettingInitialization : ISettingInitialization
    {
        private readonly ITypeFinder _typeFinder;
        private readonly ISystemSettingRepository _systemSettingRepository;

        public DbSettingInitialization(ITypeFinder typeFinder,
            ISystemSettingRepository systemSettingRepository)
        {
            _typeFinder = typeFinder;
            _systemSettingRepository = systemSettingRepository;

        }

        public bool Initialization()
        {
            InitDatas();

            return true;
        }

        private void InitDatas()
        {
            var settingClasses = _typeFinder.FindClassesOfType<ISetting>().ToList();

            var props = new List<dynamic>();
            foreach (var c in settingClasses)
            {
                var properties = from prop in c.GetProperties()
                    where prop.CanWrite || prop.CanRead
                    select new
                    {
                        Key = string.Format("{0}.{1}", c.Name, prop.Name),
                        Value = "",
                        SettingAttr = prop.GetCustomAttributes(false).FirstOrDefault(i => i.GetType() == typeof(AttributeSetting)),
                        CanWrite = prop.CanWrite,
                        CanRead = prop.CanRead,
                        DateType = prop,
                    };
                props.AddRange(properties.ToList());
            }
            if (props.Any())
            {
                var allSettings = _systemSettingRepository.GetAll().ToList();
                foreach (var p in props)
                {
                    var sett = allSettings.FirstOrDefault(i => i.Key == p.Key);
                    var sc = new SystemSetting()
                    {
                        Key = p.Key,
                        DataType = p.DateType.PropertyType.IsGenericType ?
                            string.Format("List`{0}", p.DateType.PropertyType.GenericTypeArguments[0].Name) : p.DateType.PropertyType.Name,
                        Description = p.SettingAttr != null ? ((AttributeSetting)p.SettingAttr).Description : ""
                    };
                    if (sett == null)
                    {
                        sc.Value = p.SettingAttr != null ? ((AttributeSetting)p.SettingAttr).DefaultValue : "";
                        sc.AddTime = DateTime.Now;
                        sc.UpdateTime = DateTime.Now;
                        sc.GenerateNewIdentity();
                        _systemSettingRepository.Insert(sc);
                    }
                    else
                    {
                        //sc.Value = sett.Value;
                        //sc.AddTime = sett.AddTime;
                        //sc.UpdateTime = sett.UpdateTime;
                        //sc.ChangeCurrentIdentity(sett.Id);
                        //_settingConfigRepository.Merge(sett, sc);
                    }
                }
                _systemSettingRepository.UnitOfWork.Commit();
            }

        }
    }
}
