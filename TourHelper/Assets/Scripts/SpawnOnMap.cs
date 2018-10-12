﻿namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
    using TourHelper.Repository;
    using TourHelper.Base.Model.Entity;
    using System;
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
       // public void AddPoints(int tourID)
        private void Start()
        {
            int tourID = 1;
            var pointRepo = new TourPointRepository();
            List<TourPoint> points = pointRepo.GetByTourID(tourID);
            _locations = new Vector2d[points.Count];
            _spawnedObjects = new List<GameObject>();
            CoordinateRepository coordinateRepo = new CoordinateRepository();
            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];
                int coordinateId = point.CoordinateId;
                var pointCoordinate = coordinateRepo.Get(coordinateId);
                var locationString = pointCoordinate.Latitude.ToString("00.0000000") + "," + pointCoordinate.Longitude.ToString("00.0000000");
                _locations[i] = Conversions.StringToLatLon(locationString);
                var instance = Instantiate(_markerPrefab);
                Vector3 position = _map.GeoToWorldPosition(_locations[i], true);
                position[1] = position[1] + 4;
                position[2] = position[2] + 7;
                instance.transform.localPosition = position;

                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

                // Button POIButton = instance.GetComponent<Button>();
                //Button POIButton = instance.GetComponentInChildren<Button>();
                PointInfo script = instance.GetComponentInChildren<PointInfo>();
                script.point = point;
                //POIButton.onClick.AddListener(() => script.ShowInfo());

                _spawnedObjects.Add(instance);

                }
        }


		private void Update()
		{
			/*int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
                Vector3 position = _map.GeoToWorldPosition(_locations[i], true);
                position[1] = position[1] + 4;
                position[2] = position[2] + 7;
                spawnedObject.transform.localPosition = position;
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                
            }*/
		}
	}
}