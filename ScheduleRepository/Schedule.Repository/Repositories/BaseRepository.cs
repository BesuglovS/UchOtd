using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Schedule.DataLayer;

namespace Schedule.Repositories.Repositories
{
    public class BaseRepository<TObject> where TObject : class
    {
        public string ConnectionString { get; set; }

        public BaseRepository()
        {
        }

        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public virtual ICollection<TObject> GetAll()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Set<TObject>().ToList();
            }
        }

        public virtual async Task<ICollection<TObject>> GetAllAsync()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return await context.Set<TObject>().ToListAsync();
            }
        }

        public virtual TObject Get(int id)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Set<TObject>().Find(id);
            }
        }

        public virtual async Task<TObject> GetAsync(int id)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return await context.Set<TObject>().FindAsync(id);
            }
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> match)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Set<TObject>().SingleOrDefault(match);
            }
        }

        public virtual async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return await context.Set<TObject>().SingleOrDefaultAsync(match);
            }
        }

        public virtual ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Set<TObject>().Where(match).ToList();
            }
        }

        public virtual async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return await context.Set<TObject>().Where(match).ToListAsync();
            }
        }

        public virtual TObject Add(TObject t)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                context.Set<TObject>().Add(t);
                context.SaveChanges();
                return t;
            }
        }

        public virtual async Task<TObject> AddAsync(TObject t)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                context.Set<TObject>().Add(t);
                await context.SaveChangesAsync();
                return t;
            }
        }

        public virtual TObject Update(TObject updated, int key)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                if (updated == null)
                    return null;

                TObject existing = context.Set<TObject>().Find(key);
                if (existing != null)
                {
                    context.Entry(existing).CurrentValues.SetValues(updated);
                    context.SaveChanges();
                }
                return existing;
            }
        }

        public virtual async Task<TObject> UpdateAsync(TObject updated, int key)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                if (updated == null)
                    return null;

                TObject existing = await context.Set<TObject>().FindAsync(key);
                if (existing != null)
                {
                    context.Entry(existing).CurrentValues.SetValues(updated);
                    await context.SaveChangesAsync();
                }
                return existing;
            }
        }

        public virtual void Delete(TObject t)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                context.Set<TObject>().Remove(t);
                context.SaveChanges();
            }
        }

        public virtual async Task<int> DeleteAsync(TObject t)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                context.Set<TObject>().Remove(t);
                return await context.SaveChangesAsync();
            }
        }

        public virtual int Count()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Set<TObject>().Count();
            }
        }

        public virtual async Task<int> CountAsync()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return await context.Set<TObject>().CountAsync();
            }
        }
    }
}
