using System.Linq;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelEvent : MonoBehaviour {

    public Text _titleText;
    public Text _informationText;

    public void SetPointInfo()
        {


            var title = PlayerPrefs.GetString("PointName", "");

            _titleText.text = title;

            var pointId = PlayerPrefs.GetInt("PointId", 0);
            if (pointId != 0)
            {
                var repo = new TourPointRepository();
                var point = repo.GetByTourID(pointId).SingleOrDefault();

                _informationText.text = point.Description;
            }
            else
            {
                _informationText.text = "";
            }
        }
}
