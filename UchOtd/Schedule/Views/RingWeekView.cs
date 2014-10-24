using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UchOtd.Schedule.Views
{
    public class RingWeekView
    {
        public int RingId { get; set; }
        public string MonWishes { get; set; }
        public string TueWishes { get; set; }
        public string WedWishes { get; set; }
        public string ThuWishes { get; set; }
        public string FriWishes { get; set; }
        public string SatWishes { get; set; }
        public string SunWishes { get; set; }

        public static List<RingWeekView> GetRingWeekView(ScheduleRepository repo, Teacher teacher)
        {
            var result = new List<RingWeekView>();

            var filteredWishes = repo
                .GetFiltredTeacherWishes(w => w.Teacher.TeacherId == teacher.TeacherId)
                .GroupBy(w => w.Ring.RingId, (ringId, ringWishes) =>
                    new
                    {
                        RingId = ringId,
                        RingWishes = ringWishes.GroupBy(rw => rw.Calendar.Date.DayOfWeek, (dow, wishes) =>
                        new { dayOfWeek = dow, wishes = wishes })
                    }
                );

            foreach (var ringWishes in filteredWishes)
            {
                var ringWeekView = new RingWeekView();
                ringWeekView.RingId = ringWishes.RingId;

                foreach (var dowWishes in ringWishes.RingWishes)
                {
                    switch((int)dowWishes.dayOfWeek)
                    {
                        case 1:
                            ringWeekView.MonWishes = RingWeekView.WishesToString(repo, dowWishes.wishes);
                            break;
                        case 2:
                            ringWeekView.TueWishes = RingWeekView.WishesToString(repo, dowWishes.wishes);
                            break;
                        case 3:
                            ringWeekView.WedWishes = RingWeekView.WishesToString(repo, dowWishes.wishes);
                            break;
                        case 4:
                            ringWeekView.ThuWishes = RingWeekView.WishesToString(repo, dowWishes.wishes);
                            break;
                        case 5:
                            ringWeekView.FriWishes = RingWeekView.WishesToString(repo, dowWishes.wishes);
                            break;
                        case 6:
                            ringWeekView.SatWishes = RingWeekView.WishesToString(repo, dowWishes.wishes);
                            break;
                        case 7:
                            ringWeekView.SunWishes = RingWeekView.WishesToString(repo, dowWishes.wishes);
                            break;
                    }
                }                
            }

            return result;
        }

        private static string WishesToString(ScheduleRepository repo, IEnumerable<TeacherWish> list)
        {
            String result = "";

            var groupedWishes = list.GroupBy(w => w.Wish, (wish, wishes) => new { wish = wish, weeks = repo.GetWeekStringFromWishes(wishes) });

            foreach (var wish in groupedWishes)                
            {
                result += wish.wish + " - " + wish.weeks + "; ";
            }

            result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}
