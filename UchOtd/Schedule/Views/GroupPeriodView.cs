using System.Collections.Generic;
using System.Linq;
using Schedule.DomainClasses.Analyse;

namespace UchOtd.Schedule.Views
{
    public class GroupPeriodView
    {
        public int GroupPeriodId { get; set; }
        public string Name { get; set; }
        public string StudentGroup { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public GroupPeriodView()
        {
        }

        public GroupPeriodView(GroupPeriod groupPeriod)
        {
            GroupPeriodId = groupPeriod.GroupPeriodId;
            Name = groupPeriod.Name;
            StudentGroup = groupPeriod.StudentGroup.Name;
            Start = groupPeriod.Start.ToString("dd.MM.yyyy");
            End = groupPeriod.End.ToString("dd.MM.yyyy");
        }

        public static List<GroupPeriodView> GroupPeriodsToView(List<GroupPeriod> list)
        {
            return list.Select(gp => new GroupPeriodView(gp)).ToList();
        }
    }
}
