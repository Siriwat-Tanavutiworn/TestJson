using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class AppHandler : MonoBehaviour
{
    InputField inputField;
    private string GetInput;


    private JSONNode parseJson;
    private JSONNode jsonMessage;
    private JSONNode locationDatas;

    [Header("Use for instantitate")]
    public Button SelectPlaceButton;
    public GameObject SelectPlacePanel;

    private bool printAll = true;
    private bool checkSearchComplete = false;

    private bool searchByPlace = true;

    [Header("Control color of button")]
    [SerializeField] Button placeSearchButton;
    [SerializeField] Button provinceSearchButton;


    // Start is called before the first frame update
    void Start()
    {
        inputField = FindObjectOfType<InputField>();

        string json = File.ReadAllText(Application.dataPath + "/Data/LocationData.json");
        parseJson = JSON.Parse(json);
        jsonMessage = parseJson["message"].AsInt;
        locationDatas = parseJson["data"];

        provinceSearchButton.GetComponent<Image>().color = new Color(0.4313726f, 0.3568628f, 0.2627451f, 0.2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (GetInput == "" || GetInput == null)
        {
            Debug.Log(GetInput);
            if (printAll)
            {
                DestroyPlaceButton();
                for (int i = 0; i < locationDatas.Count; i++)
                {
                    CreateButton(i);
                }
                printAll = false;
            }
            checkSearchComplete = false;
        }
        else if (GetInput != "" && GetInput != null && !checkSearchComplete)
        {
            DestroyPlaceButton();
            printAll = true;
            for (int i = 0; i < locationDatas.Count; i++)
            {
                if (searchByPlace)
                {
                    if (locationDatas[i]["placeNameThai"].ToString().Contains(GetInput))
                    {
                        Debug.Log("Found data");
                        CreateButton(i);
                    }
                    else
                    {
                        Debug.Log("Can't found data");
                    }

                }
                else
                {
                    if (locationDatas[i]["province"].ToString().Contains(GetInput))
                    {
                        Debug.Log("Found data");
                        CreateButton(i);
                    }
                    else
                    {
                        Debug.Log("Can't found data");
                    }

                }
            }
            checkSearchComplete = true;
        }
    }

    private static void DestroyPlaceButton()
    {
        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("PlaceButton");
        foreach (GameObject target in buttonObjects)
        {
            GameObject.Destroy(target);
        }
    }

    private void CreateButton(int order)
    {
        Button placeButton = Instantiate(SelectPlaceButton, transform.position, transform.rotation) as Button;
        placeButton.transform.SetParent(SelectPlacePanel.transform);
        placeButton.transform.Find("NameOfTravelLocation").GetComponentInChildren<Text>().text = locationDatas[order]["placeNameThai"];
        placeButton.transform.Find("NameOfProvince").GetComponentInChildren<Text>().text = locationDatas[order]["province"];
    }

    public void InputToText()
    {
        GetInput = inputField.text;
    }

    public JSONNode GetJSON()
    {
        return locationDatas;   
    }

    public void SearchByProvince()
    {
        searchByPlace = false;
        placeSearchButton.GetComponent<Image>().color = new Color(0.4313726f, 0.3568628f, 0.2627451f, 0.2f);
        provinceSearchButton.GetComponent<Image>().color = new Color(0.4313726f, 0.3568628f, 0.2627451f, 1);
    }

    public void SearchByPlace()
    {
        searchByPlace = true;
        provinceSearchButton.GetComponent<Image>().color = new Color(0.4313726f, 0.3568628f, 0.2627451f, 0.2f);
        placeSearchButton.GetComponent<Image>().color = new Color(0.4313726f, 0.3568628f, 0.2627451f, 1);
    }

}

    // Class for data--------------------------------------------------------------------------------------------------------------------
    /*[System.Serializable]
    public class LocationData
    {
        public int message;
        public data[] data;
    }


    [System.Serializable]
    public class data
    {
        public int id;
        public string placeNameThai;
        public string placeNameEng;
        public string latitude;
        public string longtitude;
        public int point;
        public string gameDescription;
        public string shotDescription;
        public string longDescription;
        public string url;
        public SettingLocationSub[] setting_location_sub;
        public SettingLocationPicture[] setting_location_picture;
        public string createDate;
        public string updataDate;
    }

    [System.Serializable]
    public class SettingLocationSub
    {
        public int id;
        public int parent_location_id;
        public string placeNameThai;
        public string placeNameEng;
        public string latitude;
        public string longtitude;
        public string sorting;
        public string gameDescription;
        public SettingLocationPicture[] setting_location_picture;
        public string createDate;
        public string updataDate;
    }


    [System.Serializable]
    public class SettingLocationPicture : MonoBehaviour
    {
        public int id;           
        public int refer_location_id;     
        public string refer_location_table;         
        public string image;            
        public string quality;       
        public string 5.jpg;          
        public string createDate;       
        public string updateData;   
    }
    */

