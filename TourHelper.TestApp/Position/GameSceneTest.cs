using System.Reflection;
using TourHelper.Logic;
using TourHelper.Manager;
using TourHelper.Manager.Devices.Mock;

namespace TourHelper.TestApp.Position
{
    public class GameSceneTest
    {

        public void test()
        {
            var _data = new DevicesFromFile();

            _data.FilePath = "D:\\Uczelnia\\INŻYNIERKA\\TourHelper\\c.txt";

            _data.ReadData();

            var _gyro = new MockGyroManager(_data.Rotation, _data.Accelerations);

            var _gps = new MockGpsManager(_data.Position);


            //-----------------------------------------------------------------------
            var _player = new Player(_gps, _gyro,10,5);

            var _assemblies = new Assembly[] { typeof(RandomCoinsInRangeManager).Assembly };

            var _scene = new GameSpace(_gps, _assemblies);

            _scene.AddPrefab("Coin");

            _scene.RebaseEvent += _player.RebasePlayer;


            foreach(double s in _data.MesurementTime)
            {
                _player.InitializePlayer(new UnityEngine.Camera());
                _scene.Initialize();
            }
        }
    }
}
