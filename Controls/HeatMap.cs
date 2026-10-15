using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Graphics;
using MeTracker.Models;

namespace MeTracker.Controls
{
    // Custom Map control inheriting from Microsoft.Maui.Controls.Maps.Map as required by the assignment
    public class HeatMap : Microsoft.Maui.Controls.Maps.Map
    {
        public void RenderHeatPoints(IEnumerable<LocationModel> points)
        {
            HeatMapExtensions.RenderHeatPoints(this, points);
        }
    }

    public static class HeatMapExtensions
    {
        public static void RenderHeatPoints(this Microsoft.Maui.Controls.Maps.Map map, IEnumerable<LocationModel> points)
        {
            map.MapElements.Clear();
            map.Pins.Clear();

            int count = 1;
            foreach (var point in points)
            {
                var loc = new Location(point.Latitude, point.Longitude);
                map.Pins.Add(new Pin
                {
                    Label = $"Waypoint {count++}",
                    Location = loc,
                    Type = PinType.Generic
                });

                map.MapElements.Add(new Circle
                {
                    Center = loc,
                    Radius = Distance.FromMeters(200),
                    StrokeColor = Color.FromArgb("#B41E5AF0"),
                    StrokeWidth = 3,
                    FillColor = Color.FromArgb("#7028E66E")
                });
            }
        }
    }

    // Direct heat map route and density overlay renderer
    public class HeatMapDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float width = dirtyRect.Width;
            float height = dirtyRect.Height;

            // Route points mapped across the visible screen
            var p1 = new PointF(width * 0.25f, height * 0.35f);
            var p2 = new PointF(width * 0.45f, height * 0.46f);
            var p3 = new PointF(width * 0.68f, height * 0.40f);
            var p4 = new PointF(width * 0.78f, height * 0.60f);

            var waypoints = new[] { p1, p2, p3, p4 };

            // 1. Draw dashed connecting route line
            var route = new PathF();
            route.MoveTo(p1);
            route.LineTo(p2);
            route.LineTo(p3);
            route.LineTo(p4);

            canvas.StrokeColor = Color.FromArgb("#C8512BD4");
            canvas.StrokeSize = 5;
            canvas.StrokeDashPattern = new float[] { 8, 4 };
            canvas.DrawPath(route);

            // 2. Render radial translucent heat density rings (Heat Map gradient)
            float[] radiusRings = { 75f, 52f, 32f };
            Color[] heatColors = {
                Color.FromArgb("#3528E66E"), // Outer green aura
                Color.FromArgb("#5500B4D8"), // Mid cyan aura
                Color.FromArgb("#75512BD4")  // Inner purple core
            };

            foreach (var pt in waypoints)
            {
                for (int i = 0; i < radiusRings.Length; i++)
                {
                    canvas.FillColor = heatColors[i];
                    canvas.FillCircle(pt.X, pt.Y, radiusRings[i]);
                }

                // Core waypoint pin marker
                canvas.FillColor = Colors.White;
                canvas.FillCircle(pt.X, pt.Y, 8);
                canvas.FillColor = Color.FromArgb("#512BD4");
                canvas.FillCircle(pt.X, pt.Y, 5);
            }
        }
    }
}
