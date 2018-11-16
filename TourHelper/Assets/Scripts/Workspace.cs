#undef DEBUG

using System.Reflection;
using TourHelper.Base.Manager.Devices;
using TourHelper.Logic;
using TourHelper.Manager;
using TourHelper.Manager.Calculators.Geolocation;
using TourHelper.Manager.Devices;
using TourHelper.Manager.Devices.Mock;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour
{

    //Devices

    private IGpsManager _gps;
    private IGyroManager _gyro;
    private ICompassManager _compass;

    //Game controllers
    private Player _player;
    private GameSpace _scene;
    private int _lastTour;


    //Game Objects
    public Camera _camera;
    public GameObject _mainPanel;


    private void Awake()
    {

#if (DEBUG)
        //-----------MOCK DEVICES-----------
        var _data = new DevicesFromFile();

        _data.FilePath = "D:\\Uczelnia\\INŻYNIERKA\\TourHelper\\c.txt";

        _data.ReadData();

        _gyro = new MockGyroManager(_data.Rotation,_data.Accelerations);

        _gps = new MockGpsManager(_data.Position);


#else

        //-----------REAL DEVICES-----------        
        _gps = GpsManager.Instance;

        _gyro = GyroManager.Instance;

        _compass = CompassManager.Instance;

#endif


    }

    void Start()
    {

        _player = new Player(_gps, _gyro, 10, 5);

        _lastTour= PlayerPrefs.GetInt("TourID");

        var _assemblies = new Assembly[] {
            typeof(RandomCoinsInRangeManager).Assembly
        };

        _scene = new GameSpace(_gps, _assemblies);
        _scene.MainPanel = _mainPanel;
        _scene.AddPrefab("Coin", defaultActions: true);
        _scene.AddPrefab("InfoPoint", defaultActions: true);

        _scene.RebaseEvent += _player.RebasePlayer;

        _scene.Initialize();
        _player.InitializePlayer(_camera);

    }
    private void FixedUpdate()
    {
        _player.UpdatePlayer();
    }

    void Update()
    {
        if (_lastTour!= PlayerPrefs.GetInt("TourID"))
        {
            _scene.EnforceRebuild = true;
            _player.ResetDistanceAccumulator(PlayerPrefs.GetFloat("Distance"));

        }
        _scene.UpdateGameSpace();
        

        PlayerPrefs.SetFloat("Distance", (float)(_player.AccumulatedDistance / 1000));





    }

}
