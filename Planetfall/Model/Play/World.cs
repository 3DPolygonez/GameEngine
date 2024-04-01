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
        private const double gameObjectSize = 50;
        private const int arraySize = 5;
        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<List<Point3D>> _point3DCollections = new List<List<Point3D>>{
            new List<Point3D>{
                new Point3D(x: -gameObjectSize, y: gameObjectSize, z: -gameObjectSize),
                new Point3D(x: -gameObjectSize, y: gameObjectSize, z: gameObjectSize),
                new Point3D(x: gameObjectSize, y: gameObjectSize, z: gameObjectSize),
                new Point3D(x: gameObjectSize, y: gameObjectSize, z: -gameObjectSize)
            },
            new List<Point3D>{
                new Point3D(x: -gameObjectSize, y: gameObjectSize, z: -gameObjectSize),
                new Point3D(x: -gameObjectSize, y: gameObjectSize, z: gameObjectSize),
                new Point3D(x: -gameObjectSize, y: -gameObjectSize, z: gameObjectSize),
                new Point3D(x: -gameObjectSize, y: -gameObjectSize, z: -gameObjectSize)
            },
            new List<Point3D>{
                new Point3D(x: gameObjectSize, y: gameObjectSize, z: -gameObjectSize),
                new Point3D(x: gameObjectSize, y: gameObjectSize, z: gameObjectSize),
                new Point3D(x: gameObjectSize, y: -gameObjectSize, z: gameObjectSize),
                new Point3D(x: gameObjectSize, y: -gameObjectSize, z: -gameObjectSize)
            },
            new List<Point3D>{
                new Point3D(x: -gameObjectSize, y: -gameObjectSize, z: -gameObjectSize),
                new Point3D(x: -gameObjectSize, y: -gameObjectSize, z: gameObjectSize),
                new Point3D(x: gameObjectSize, y: -gameObjectSize, z: gameObjectSize),
                new Point3D(x: gameObjectSize, y: -gameObjectSize, z: -gameObjectSize)
            }
        };
        private Point3D rotationCentrePoint = new Point3D();
        public World(
            int screenWidth,
            int screenHeight)
        {
            rotationCentrePoint.X = screenWidth / 2;
            rotationCentrePoint.Y = screenHeight / 2;
            rotationCentrePoint.Z = 0;

            for (int x = -(arraySize - 2); x < arraySize - 1; x++)
            {
                for (int y = -(arraySize - 2); y < arraySize - 1; y++)
                {
                    _gameObjects.Add(
                        item: new GameObject(
                            point3DCollections: _point3DCollections.Clone(),
                            XAdjustment: x * (gameObjectSize * 2.5),
                            YAdjustment: y * (gameObjectSize * 2.5),
                            ZAdjustment: 0));
                }
            }
            return;
        }
        public void update(
            GameEngine.Framework.Interface.Game game)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(
                    game: game);
            }
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

            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(
                    game: game,
                    rotationCentrePoint: rotationCentrePoint);
            }

        }
    }
}
