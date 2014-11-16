using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DataLayer;
using Schedule.DomainClasses.Config;

namespace Schedule.Repositories.Repositories.Config
{
    public class ConfigOptionRepository : BaseRepository<ConfigOption>
    {

        #region ConfigOptionRepository
        public List<ConfigOption> GetAllConfigOptions()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.ToList();
            }
        }

        public List<ConfigOption> GetFiltredConfigOptions(Func<ConfigOption, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.ToList().Where(condition).ToList();
            }
        }

        public ConfigOption GetFirstFiltredConfigOption(Func<ConfigOption, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.ToList().FirstOrDefault(condition);
            }
        }

        public ConfigOption GetConfigOption(int configOptionId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.FirstOrDefault(co => co.ConfigOptionId == configOptionId);
            }
        }

        public ConfigOption GetConfigOptionByKey(string key)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.FirstOrDefault(co => co.Key == key);
            }
        }

        public void AddConfigOption(ConfigOption co)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                context.Config.Add(co);
                context.SaveChanges();
            }
        }

        public void UpdateConfigOption(ConfigOption co)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curCo = context.Config.FirstOrDefault(opt => opt.ConfigOptionId == co.ConfigOptionId);

                if (curCo != null)
                {
                    curCo.Key = co.Key;
                    curCo.Value = co.Value;
                }

                context.SaveChanges();
            }
        }

        public void RemoveConfigOption(int configOptionId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var co = context.Config.FirstOrDefault(opt => opt.ConfigOptionId == configOptionId);

                context.Config.Remove(co);
                context.SaveChanges();
            }
        }

        public void AddConfigOptionRange(IEnumerable<ConfigOption> coList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var co in coList)
                {
                    co.ConfigOptionId = 0;
                    context.Config.Add(co);
                }

                context.SaveChanges();
            }
        }

        public ConfigOption FindConfigOption(string key)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.FirstOrDefault(op => op.Key == key);
            }
        }
        #endregion

    }
}
