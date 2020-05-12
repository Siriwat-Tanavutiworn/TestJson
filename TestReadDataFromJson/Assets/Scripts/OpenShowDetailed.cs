using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEditor;


public class OpenShowDetailed : MonoBehaviour
{
    [SerializeField] GameObject showDetailedPanel;
    [SerializeField] Text nameText;
    [SerializeField] Image placePicturePrefeb;
    AppHandler appHandler;

    private string nameOfTravelingPlace;
    
    private JSONNode locationDatas;

    private void Start()
    {
        appHandler = FindObjectOfType<AppHandler>();
        locationDatas = appHandler.GetJSON();
    }

    public void ShowDetailed()
    {
        Vector2 centerOfScreen = new Vector2(512f, 683f);
        GameObject showDetail = Instantiate(showDetailedPanel, centerOfScreen, Quaternion.identity) as GameObject;
        showDetail.transform.SetParent(GameObject.Find("Canvas").transform);
        nameOfTravelingPlace = nameText.text;
        

        for (int i = 0; i < locationDatas.Count; i++)
        {
            if (locationDatas[i]["placeNameThai"] == nameOfTravelingPlace)
            {
                string detailText = locationDatas[i]["longDescription"];

                showDetail.transform.Find("NameOfTravelingLocation").GetComponent<Text>().text = nameOfTravelingPlace;
                showDetail.transform.Find("Detail").GetComponent<Text>().text = detailText;

                CreateImage(showDetail, i);

                break;
            }
        }


    }

    private void CreateImage(GameObject showDetail, int i)
    {
        for (int j = 0; j < locationDatas[i]["setting_location_picture"][j].Count; j++)
        {
            Image placeImage = Instantiate(placePicturePrefeb, transform.position, transform.rotation) as Image;
            placeImage.transform.SetParent(showDetail.transform.Find("Picture View Panel").transform.Find("Viewport").transform.Find("Picture View").transform);

            string imageName = locationDatas[i]["setting_location_picture"][j]["picture"].ToString().Remove(locationDatas[i]["setting_location_picture"][j]["picture"].ToString().Length - 5, 5);
            imageName = imageName.Remove(0, 1);

            var path = "Picture/" + imageName;

            placeImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        }
    }

    public void DestroyDetailedPanel()
    {
        Destroy(this.gameObject);
    }
}
