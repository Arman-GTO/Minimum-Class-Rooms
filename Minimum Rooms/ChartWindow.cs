using Minimum_Rooms.Classes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Minimum_Rooms
{
    public partial class ChartWindow : Form
    {
        List<int> xValues = new List<int>();
        List<int> yValues = new List<int>();

        public ChartWindow(List<HourRooms> hourRooms)
        {
            InitializeComponent();
            Chart.Series.Clear();
            foreach (var x in hourRooms) // Add chart axis values 
            {
                xValues.Add(x.Hour);
                yValues.Add(x.Rooms);
            }
        }

        private void ChartWindow_Load(object sender, System.EventArgs e)
        {
            Chart.Series.Clear();
            Chart.ChartAreas["ChartArea1"].AxisX.Title = "Hour (h)";
            Chart.ChartAreas["ChartArea1"].AxisY.Title = "Classes";
            Series classes = new Series()
            {
                Name = "classes",
                ChartType = SeriesChartType.StepLine,
                Color = Color.Blue,
                BorderWidth = 3,
                XValueType = ChartValueType.Int32,
                YValueType = ChartValueType.Int32,
                IsValueShownAsLabel = true,
                Font = new Font("Comic Sans MS", 9),
                LabelToolTip = "class rooms",
                LabelForeColor = Color.MediumVioletRed,
                LabelAngle = 40,
                LabelBackColor = Color.White
            };
            Chart.Series.Add(classes);
            Chart.Series["classes"].Points.DataBindXY(xValues, yValues);
        }
    }
}
