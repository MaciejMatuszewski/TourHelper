#undef DEBUG

using System.Reflection;
using TourHelper.Base.Manager.Devices;
using TourHelper.Logic;
using TourHelper.Manager;
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

    //Game Objects
    public Camera _camera;
    public GameObject _mainPanel;
    public Text _distanceText;
    public Text _visitedText;
    public Text _scoreText;

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
        _gyro = GyroManager.Instance;
        StartCoroutine(_gyro.StartService(5));

        _gps = GpsManager.Instance;
        StartCoroutine(_gps.StartService(5));

        _compass = CompassManager.Instance;
        StartCoroutine(_compass.StartService(5));
#endif


    }

    void Start()
    {
        _player = new Player(_gps, _gyro);

        var _assemblies = new Assembly[] {
            typeof(RandomCoinsInRangeManager).Assembly
        };

        _scene = new GameSpace(_gps, _assemblies);
        _scene.MainPanel = _mainPanel;
        _scene.AddPrefab("Coin",defaultActions:true);
        _scene.AddPrefab("InfoPoint", new RandomCoinsInRangeManager(), defaultActions: false);

        _scene.RebaseEvent += _player.RebasePlayer;

        _player.InitializePlayer(_camera);
        _scene.Initialize();

        PlayerPrefs.SetInt("Visited", 0);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("Distance", 0f);

    }

    void Update()
    {
        _scene.UpdateGameSpace();
        _player.UpdatePlayer();

        PlayerPrefs.SetFloat("Distance",(float)_player.AccumulatedDistance);

        _distanceText.text = PlayerPrefs.GetFloat("Distance").ToString() + " km";
        _visitedText.text = PlayerPrefs.GetInt("Visited").ToString() + " pkt.";
        _scoreText.text = PlayerPrefs.GetInt("Score").ToString() + " pkt.";

    }

}
