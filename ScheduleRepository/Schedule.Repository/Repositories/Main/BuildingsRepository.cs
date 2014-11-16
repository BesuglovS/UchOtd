using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class BuildingsRepository: BaseRepository<Building>
    {
        public List<Building> GetAllBuildings()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Buildings.ToList();
            }
        }

        public List<Building> GetFiltredBuildings(Func<Building, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Buildings.ToList().Where(condition).ToList();
            }
        }

        public Building GetFirstFiltredBuilding(Func<Building, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Buildings.ToList().FirstOrDefault(condition);
            }
        }

        public Building GetBuilding(int buildingId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Buildings.FirstOrDefault(b => b.BuildingId == buildingId);
            }
        }

        public Building FindBuilding(string name)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Buildings.FirstOrDefault(b => b.Name == name);
            }
        }

        public void AddBuilding(Building building)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                building.BuildingId = 0;

                context.Buildings.Add(building);
                context.SaveChanges();
            }
        }

        public void UpdateBuilding(Building building)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == building.BuildingId);

                if (curBuilding != null)
                {
                    curBuilding.Name = building.Name;
                }

                context.SaveChanges();
            }
        }

        public void RemoveBuilding(int buildingId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var building = context.Buildings.FirstOrDefault(b => b.BuildingId == buildingId);

                context.Buildings.Remove(building);
                context.SaveChanges();
            }
        }

        public void AddBuildingRange(IEnumerable<Building> buildingList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var building in buildingList)
                {
                    building.BuildingId = 0;
                    context.Buildings.Add(building);
                }

                context.SaveChanges();
            }
        }
    }
}
