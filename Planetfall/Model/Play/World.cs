using GameEngine.Framework.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planetfall.Model.Play
{
    public class World
    {
        private List<List<Point3D>> point3DCollections = new List<List<Point3D>>{
            new List<Point3D>{
                new Point3D(x: -50, y: 50, z: -50),
                new Point3D(x: -50, y: 50, z: 50),
                new Point3D(x: 50, y: 50, z: 50),
                new Point3D(x: 50, y: 50, z: -50)
            },
            new List<Point3D>{
                new Point3D(x: -50, y: 50, z: -50),
                new Point3D(x: -50, y: 50, z: 50),
                new Point3D(x: -50, y: -50, z: 50),
                new Point3D(x: -50, y: -50, z: -50)
            },
            new List<Point3D>{
                new Point3D(x: 50, y: 50, z: -50),
                new Point3D(x: 50, y: 50, z: 50),
                new Point3D(x: 50, y: -50, z: 50),
                new Point3D(x: 50, y: -50, z: -50)
            },
            new List<Point3D>{
                new Point3D(x: -50, y: -50, z: -50),
                new Point3D(x: -50, y: -50, z: 50),
                new Point3D(x: 50, y: -50, z: 50),
                new Point3D(x: 50, y: -50, z: -50)
            }
        };
        private Point3D rotationCentrePoint = new Point3D();
        public World(
            int screenWidth,
            int screenHeight)
        {
            rotationCentrePoint.X = screenWidth / 2;
            rotationCentrePoint.Y = screenHeight / 2;
            rotationCentrePoint.Z = 500;
            return;
        }
        public void draw(
            GameEngine.Framework.Interface.Game game)
        {
            game.graphics.drawRect(
                x: 0,
                y: 0,
                width: game.graphics.width,
                height: game.graphics.height,
                brush: Brushes.Black,
                pen: null);

            foreach(var point3DCollection in point3DCollections)
            {
                foreach (var point3D in point3DCollection)
                {
                    point3D.Y = point3D.OriginalY + game.input.mouseY - (game.graphics.height / 2);
                    point3D.X = point3D.OriginalX + game.input.mouseX - (game.graphics.width / 2);
                    if (game.input.isKeyPressed((int)Keys.Up))
                    {
                        point3D.Z += 5;
                    }
                    else if (game.input.isKeyPressed((int)Keys.Down))
                    {
                        point3D.Z -= 5;
                    }
                }
                game.graphics.drawPoly(
                    points: point3DCollection.ToPoints(
                        perspecticeCoefficient: game.graphics.height,
                        rotationCentrePoint: rotationCentrePoint),
                    brush: null,
                    pen: Pens.Orange);
            }

        }
    }
}
