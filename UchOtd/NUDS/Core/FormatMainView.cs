using System;
using System.Drawing;
using System.Windows.Forms;

namespace UchOtd.NUDS.Core
{
    public static class FormatMainView
    {
        private static int Percent(double percent, double whole)
        {
            return (int)Math.Round(whole * (percent / 100));
        }

        public static void DailyScheduleView(DataGridView view, ScheduleForm mainForm)
        {
            if (view.DataSource == null)
            {
                return;
            }

            view.ColumnHeadersVisible = false;
            view.RowHeadersVisible = false;
            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            view.AutoResizeColumns();
            view.AutoResizeRows();
            view.AllowUserToResizeColumns = false;
            view.AllowUserToResizeRows = false;

            // LessonId
            view.Columns[0].Visible = false;
            view.Columns[0].Width = 0;

            // Ring
            view.Columns[1].Width = 56;
            view.Columns[1].DefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.FontFamily, 14);
            view.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // LessonSummary
            // view.Columns[2].Width = Percent(88, view.Width);
            view.Columns[2].Width = view.Width - view.Columns[1].Width - 20;
        }
    }
}
