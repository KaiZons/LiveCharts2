﻿using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Eto.Forms;

namespace EtoFormsSample.General.TemplatedLegends;

public class CustomLegend : Panel, IChartLegend<SkiaSharpDrawingContext>
{
    public CustomLegend()
    {
    }

    public LegendOrientation Orientation { get; set; }

    public void Draw(Chart<SkiaSharpDrawingContext> chart)
    {
        var wfChart = (Chart)chart.View;

        if (chart.LegendOrientation == LegendOrientation.Auto)
            Orientation = LegendOrientation.Vertical;

        wfChart.LegendPosition = LegendPosition.Right;

        DrawAndMesure(chart.ChartSeries, wfChart);

        BackgroundColor = wfChart.LegendBackColor;
    }

    private void DrawAndMesure(IEnumerable<IChartSeries<SkiaSharpDrawingContext>> series, Chart chart)
    {
#if false
        SuspendLayout();
        Controls.Clear();

        var h = 0f;
        var w = 0f;

        var parent = new Panel();
        parent.BackColor = Color.FromArgb(245, 245, 220);
        Controls.Add(parent);
        using var g = CreateGraphics();
        foreach (var s in series)
        {
            var size = g.MeasureString(s.Name, chart.LegendFont);

            var p = new Panel();
            p.Location = new Point(0, (int)h);
            parent.Controls.Add(p);

            p.Controls.Add(new MotionCanvas
            {
                Location = new Point(6, 0),
                PaintTasks = s.CanvasSchedule.PaintSchedules,
                Width = (int)s.CanvasSchedule.Width,
                Height = (int)s.CanvasSchedule.Height
            });
            p.Controls.Add(new Label
            {
                Text = s.Name,
                ForeColor = Color.Black,
                Font = chart.LegendFont,
                Location = new Point(6 + (int)s.CanvasSchedule.Width + 6, 0)
            });

            var thisW = size.Width + 36 + (int)s.CanvasSchedule.Width;
            p.Width = (int)thisW + 6;
            p.Height = (int)size.Height + 6;
            h += size.Height + 6;
            w = thisW > w ? thisW : w;
        }
        h += 6;
        parent.Height = (int)h;

        Width = (int)w;
        parent.Location = new Point(0, (int)(Height / 2 - h / 2));

        ResumeLayout();
#endif
    }
}
