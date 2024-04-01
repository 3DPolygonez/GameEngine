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
    public class GameObject
    {
        private List<List<Point3D>> _point3DCollections = new List<List<Point3D>>();
        public GameObject(
            List<List<Point3D>> point3DCollections,
            double XAdjustment,
            double YAdjustment,
            double ZAdjustment) 
        {
            _point3DCollections = point3DCollections;
            foreach (var point3DCollection in _point3DCollections)
            {
                foreach (var point3D in point3DCollection)
                {
                    point3D.X = point3D.X + XAdjustment;
                    point3D.Y = point3D.Y + YAdjustment;
                    point3D.Z = point3D.Z + ZAdjustment;
                    point3D.OriginalX = point3D.X;
                    point3D.OriginalY = point3D.Y;
                    point3D.OriginalZ = point3D.Z;
                }
            }
        }
        public void Update(
            GameEngine.Framework.Interface.Game game)
        {
            foreach (var point3DCollection in _point3DCollections)
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
            }
        }
        public void Draw(
            GameEngine.Framework.Interface.Game game,
            Point3D rotationCentrePoint)
        {
            foreach (var point3DCollection in _point3DCollections)
            {
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
