using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class InfoEvents : MonoBehaviour
{

    private void OnMouseDown()
    {
        Destroy(transform.gameObject);


        var tr = name.Split('_');
        int pointId;
        int.TryParse(tr[1], out pointId);
        
        var _panelComponents = GameObject.Find("PointInformationPanel");
        

        if (_panelComponents == null)
        {
            return;
        }
        var _textComponents = _panelComponents.GetComponentsInChildren<Text>();
        var _title = _textComponents.Where(n => n.name.Equals("InfoPointTitle")).SingleOrDefault();
        var _informationText = _textComponents.Where(n => n.name.Equals("InfoPointContent")).SingleOrDefault();
        if (_title == null || _informationText == null)
        {
            return;
        }
        if (pointId != 0)
        {
            var repo = new TourPointRepository();
            var point = repo.GetByCoordinateID(pointId).SingleOrDefault();

            if (point!=null)
            {
                _title.text = point.Name;
                _informationText.text = point.Description;
                PanelEvent script = (PanelEvent)_panelComponents.GetComponent("PanelEvent");
                script.MovePanel();

                var userPoint = new UserTourPoint() {TourPointId= point.Id, UserTourId=PlayerPrefs.GetInt("UserTourID") };

                var userTourRepo = new UserTourPointRepository();

                userTourRepo.Insert(userPoint);

                int visited=PlayerPrefs.GetInt("Visited");

                PlayerPrefs.SetInt("Visited", ++visited);
            }
            else
            {
                _title.text = "";
                _informationText.text = "";
            }

        }
        else
        {
            _title.text = "";
            _informationText.text = "";
        }



    }


}
