using Schedule.DomainClasses.Analyse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UchOtd.Schedule.Views
{
    public class GroupBuildingAuditoriumView
    {
        public int GroupBuildingAuditoriumId { get; set; }
        public string StudentGroup { get; set; }
        public string Building { get; set; }
        public string Auditorium { get; set; }

        public GroupBuildingAuditoriumView()
        {
        }

        public GroupBuildingAuditoriumView(GroupBuildingAuditorium groupBuildingAuditorium)
        {
            GroupBuildingAuditoriumId = groupBuildingAuditorium.GroupBuildingAuditoriumId;
            StudentGroup = groupBuildingAuditorium.StudentGroup.Name;
            Building = groupBuildingAuditorium.Building.Name;
            Auditorium = groupBuildingAuditorium.Auditorium.Name;
        }

        public static List<GroupBuildingAuditoriumView> ItemsToView(List<GroupBuildingAuditorium> list)
        {
            return list.Select(item => new GroupBuildingAuditoriumView(item)).ToList();
        }
    }
}
