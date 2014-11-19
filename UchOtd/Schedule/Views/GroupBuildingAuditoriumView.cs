using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace UchOtd.Schedule.Views
{
    public class GroupAttributesView
    {
        public int StudentGroupId { get; set; }
        public string StudentGroup { get; set; }
        public string Building { get; set; }
        public string Auditorium { get; set; }
        public string Shift { get; set; }

        public GroupAttributesView()
        {
        }

        public GroupAttributesView(ScheduleRepository repo, List<CustomStudentGroupAttribute> attrList)
        {
            if (attrList.Count != 0)
            {
                StudentGroupId = attrList[0].StudentGroup.StudentGroupId;

                StudentGroup = attrList[0].StudentGroup.Name;

                var building = attrList.FirstOrDefault(csga => csga.Key == "Building");
                Building = (building != null) ? repo.Buildings.GetBuilding(int.Parse(building.Value)).Name : "";

                var auditorium = attrList.FirstOrDefault(csga => csga.Key == "Auditorium");
                Auditorium = (auditorium != null) ? repo.Auditoriums.Get(int.Parse(auditorium.Value)).Name : "";

                var shift = attrList.FirstOrDefault(csga => csga.Key == "Shift");
                Shift = (shift != null) ? repo.Shifts.GetShift(int.Parse(shift.Value)).Name : "";
            }
        }


        public static List<GroupAttributesView> ItemsToView(ScheduleRepository repo, List<CustomStudentGroupAttribute> attrlist)
        {
            return attrlist.GroupBy(a => a.StudentGroup.StudentGroupId)
                .Select(@group => new GroupAttributesView(repo, @group.ToList())).ToList();
        }
    }
}
