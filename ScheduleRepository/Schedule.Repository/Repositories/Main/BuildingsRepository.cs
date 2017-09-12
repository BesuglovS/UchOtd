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

        public Building GetBuildingFromGroupName(string groupName)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var numString = "";

                var digits = new List<string> {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

                for (int i = 0; i < groupName.Length; i++)
                {
                    if (digits.Contains(groupName[i].ToString()))
                    {
                        numString += groupName[i];
                    }
                    else
                    {
                        break;
                    }
                }

                var num = -1;
                try
                {
                    num = int.Parse(numString);
                }
                catch (Exception e)
                {
                }

                var chap = new List<int> {1, 2, 3, 5, 6, 7};
                var jar = new List<int> {4};
                var mol = new List<int> {8, 9, 10, 11};
                var high = new List<int> { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };

                var Mol = context.Buildings.FirstOrDefault(b => b.Name.Contains("Молодогвардейская"));

                if (chap.Contains(num))
                {
                    var building = context.Buildings.FirstOrDefault(b => b.Name.Contains("Чапаевская"));
                    if (building == null)
                    {
                        return null; 
                    }
                    else
                    {
                        return building;
                    }
                }

                if (jar.Contains(num))
                {
                    var building = context.Buildings.FirstOrDefault(b => b.Name.Contains("Ярмарочная"));
                    if (building == null)
                    {
                        return null;
                    }
                    else
                    {
                        return building;
                    }
                }

                if (mol.Contains(num))
                {
                    var building = context.Buildings.FirstOrDefault(b => b.Name.Contains("Молодогвардейская"));
                    if (building == null)
                    {
                        return null;
                    }
                    else
                    {
                        return building;
                    }
                }

                if (high.Contains(num))
                {
                    var Jar = context.Buildings.FirstOrDefault(b => b.Name.Contains("Ярмарочная"));
                    if (groupName[groupName.Length - 1] == 'У' || groupName[groupName.Length - 1] == 'Т')
                    {
                        return Jar;
                    }

                    return Mol;
                }

                return Mol;
            }
        }
    }
}
