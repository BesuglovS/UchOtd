using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Analyse;
using Schedule.Repositories;

namespace UchOtd.Schedule.Views
{
    public class GroupPeriodView
    {
        public int CustomStudentGroupAttributeId { get; set; }
        public string Name { get; set; }
        public string StudentGroup { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public GroupPeriodView()
        {
        }

        public GroupPeriodView(CustomStudentGroupAttribute attr)
        {
            CustomStudentGroupAttributeId = attr.CustomStudentGroupAttributeId;
            var valueParts = attr.Value.Split('@');
            StudentGroup = attr.StudentGroup.Name;
            Name = valueParts[0];            
            Start = valueParts[1];
            End = valueParts[2];
        }

        public static List<GroupPeriodView> GroupPeriodsToView(ScheduleRepository repo, List<CustomStudentGroupAttribute> list)
        {
            return list.Select(csga => new GroupPeriodView(csga)).ToList();
        }
    }
}
