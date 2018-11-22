namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
    using TourHelper.Repository;
    using TourHelper.Base.Model.Entity;
    using UnityEngine.SceneManagement;
    using System.Linq;
    using UnityEngine.UI;

    public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		private Vector2d[] _locations;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;

        public Sprite visitedPointImg;
        public Sprite unvisitedPointImg;

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Map"));
            int tourID = PlayerPrefs.GetInt("TourID");
            var pointRepo = new TourPointRepository();
            var userPointRepo = new UserTourPointRepository();
            List<TourPoint> points = pointRepo.GetByTourID(tourID).ToList();
            List<UserTourPoint> visitedUserPoints = userPointRepo.GetByUserTourId(PlayerPrefs.GetInt("UserTourID")).ToList();
            _locations = new Vector2d[points.Count];
            _spawnedObjects = new List<GameObject>();
            CoordinateRepository coordinateRepo = new CoordinateRepository();
            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];
                int coordinateId = (int)point.CoordinateId;
                var pointCoordinate = coordinateRepo.Get(coordinateId);
                var locationString = pointCoordinate.Latitude.ToString("00.0000000") + "," + pointCoordinate.Longitude.ToString("00.0000000");
                _locations[i] = Conversions.StringToLatLon(locationString);
                var instance = Instantiate(_markerPrefab);

                if (visitedUserPoints.Where(x => x.TourPointId == point.Id).Count() > 0)
                    instance.GetComponentInChildren<Image>().sprite = visitedPointImg;
                else
                    instance.GetComponentInChildren<Image>().sprite = unvisitedPointImg;

                Vector3 position = _map.GeoToWorldPosition(_locations[i], true);
                position[1] = position[1] + 4;
                position[2] = position[2] + 7;
                instance.transform.localPosition = position;

                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

                PointInfo script = instance.GetComponentInChildren<PointInfo>();
                script.point = point;

                _spawnedObjects.Add(instance);

                }
        }


		private void Update()
		{
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
                Vector3 position = _map.GeoToWorldPosition(_locations[i], true);
                position[1] = position[1] + 4;
                position[2] = position[2] + 7;
                spawnedObject.transform.localPosition = position;
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                
            }
		}
	}
}