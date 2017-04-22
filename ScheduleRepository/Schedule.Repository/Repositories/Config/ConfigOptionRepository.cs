using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Config;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Config
{
    public class ConfigOptionRepository : BaseRepository<ConfigOption>
    {

        public List<ConfigOption> GetAllConfigOptions()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.Include(co => co.Semester).ToList();
            }
        }

        public List<ConfigOption> GetFiltredConfigOptions(Func<ConfigOption, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.Include(co => co.Semester).ToList().Where(condition).ToList();
            }
        }

        public ConfigOption GetFirstFiltredConfigOption(Func<ConfigOption, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.Include(co => co.Semester).ToList().FirstOrDefault(condition);
            }
        }

        public ConfigOption GetConfigOption(int configOptionId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.Include(co => co.Semester).FirstOrDefault(co => co.ConfigOptionId == configOptionId);
            }
        }

        public ConfigOption GetConfigOptionByKey(string key)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.Include(co => co.Semester).FirstOrDefault(co => co.Key == key);
            }
        }

        public void AddConfigOption(ConfigOption co)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                co.Semester = context.Semesters.FirstOrDefault(coo => coo.SemesterId == co.Semester.SemesterId);

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
                    curCo.Semester = context.Semesters.FirstOrDefault(coo => coo.SemesterId == co.Semester.SemesterId);
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
                    co.Semester = context.Semesters.FirstOrDefault(coo => coo.SemesterId == co.Semester.SemesterId);

                    context.Config.Add(co);
                }

                context.SaveChanges();
            }
        }

        public ConfigOption FindConfigOption(string key, Semester semester)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Config.FirstOrDefault(op => (op.Key == key) && (op.Semester.SemesterId == semester.SemesterId));
            }
        }
    }
}
