using Schedule.Constants;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UchOtd.Schedule.Views
{
    public class RingWeekView
    {
        public int RingId { get; set; }
        public string RingTime { get; set; }
        public string MonWishes { get; set; }
        public string TueWishes { get; set; }
        public string WedWishes { get; set; }
        public string ThuWishes { get; set; }
        public string FriWishes { get; set; }
        public string SatWishes { get; set; }
        public string SunWishes { get; set; }

        public static List<RingWeekView> GetRingWeekView(ScheduleRepository repo, Teacher teacher, CancellationToken cToken)
        {
            var result = new List<RingWeekView>();

            cToken.ThrowIfCancellationRequested();

            var filteredWishes = repo
                .TeacherWishes
                .GetFiltredTeacherWishes(w => w.Teacher.TeacherId == teacher.TeacherId)
                .GroupBy(w => w.Ring.RingId, (ringId, ringWishes) =>
                    new
                    {
                        RingId = ringId,
                        RingWishes = ringWishes
                        .GroupBy(rw => Constants.DowRemap[(int)rw.Calendar.Date.DayOfWeek],
                        (dow, wishes) =>
                            new { dayOfWeek = dow, wishes })
                    }
                );

            cToken.ThrowIfCancellationRequested();

            foreach (var ringWishes in filteredWishes)
            {
                cToken.ThrowIfCancellationRequested();

                var ringWeekView = new RingWeekView {RingId = ringWishes.RingId};                

                foreach (var dowWishes in ringWishes.RingWishes)
                {
                    switch(dowWishes.dayOfWeek)
                    {
                        case 1:
                            ringWeekView.MonWishes = WishesToString(repo, dowWishes.wishes);
                            break;
                        case 2:
                            ringWeekView.TueWishes = WishesToString(repo, dowWishes.wishes);
                            break;
                        case 3:
                            ringWeekView.WedWishes = WishesToString(repo, dowWishes.wishes);
                            break;
                        case 4:
                            ringWeekView.ThuWishes = WishesToString(repo, dowWishes.wishes);
                            break;
                        case 5:
                            ringWeekView.FriWishes = WishesToString(repo, dowWishes.wishes);
                            break;
                        case 6:
                            ringWeekView.SatWishes = WishesToString(repo, dowWishes.wishes);
                            break;
                        case 7:
                            ringWeekView.SunWishes = WishesToString(repo, dowWishes.wishes);
                            break;
                    }
                }

                ringWeekView.RingTime = repo.Rings.GetRing(ringWeekView.RingId).Time.ToString("HH:mm");

                result.Add(ringWeekView);
            }

            result = result.OrderBy(rwv => repo.Rings.GetRing(rwv.RingId).Time).ToList();

            return result;
        }

        private static string WishesToString(ScheduleRepository repo, IEnumerable<TeacherWish> list)
        {
            var groupedWishes = list
                .GroupBy(w => w.Wish, 
                (wish, wishes) => 
                    new {wish, weeks = repo.CommonFunctions.GetWeekStringFromWishes(wishes) });

            var result = groupedWishes
                .Aggregate("", (current, wish) => current + (wish.weeks + "@" + wish.wish + "; "));

            result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}
