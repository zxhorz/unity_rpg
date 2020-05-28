using System;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Button_commit : MonoBehaviour {
	public InputField inputField;
	public Button button;
	public GameObject prefabShow;
	public Image parentsShow;
	//public Image parentsShowUI;
	public Text grade;
	// Use this for initialization
	void Start () {
		button.GetComponent<Button> ().onClick.AddListener (InputOK);
        grade.text = Count.getTime().ToString() +" s";

    }
	
	void InputOK(){
        float point = Count.getTime();
		string result = ResultToJson(inputField.text,point);
        SaveString(result);
        DictionarySort(GetJsonDate());
        Destroy(button);
    }

    public string ResultToJson(string Name, float NowCount)
    {
        StringBuilder sb = new StringBuilder();
        LitJson.JsonWriter WriteDate = new LitJson.JsonWriter(sb);
        WriteDate.WriteObjectStart();
        WriteDate.WritePropertyName("Name");
        WriteDate.Write(Name + "|" + DateTime.Now.ToString());
        WriteDate.WritePropertyName("Count");
        WriteDate.Write(NowCount);
        WriteDate.WriteObjectEnd();
        return sb.ToString();
    }
    private void SaveString(string str)
    {
        FileInfo fi = new FileInfo(Application.persistentDataPath + "/Json.txt");
        StreamWriter sw = null;
        if (fi.Exists)
        {
            sw = fi.AppendText();
        }
        else
        {
            sw = fi.CreateText();
        }
        sw.WriteLine(str);
        sw.Close();
    }
    public Dictionary<string, float> GetJsonDate()
    {

        FileStream fi = new FileStream(Application.persistentDataPath + "/Json.txt", FileMode.Open);
        Dictionary<string, float> jsonDate = new Dictionary<string, float>();
        if (fi.CanRead)
        {
            StreamReader sw = new StreamReader(fi);
            string jsonStr;
            while ((jsonStr = sw.ReadLine()) != null)
            {
                JsonData data = JsonMapper.ToObject(jsonStr);
                jsonDate.Add(data["Name"].ToString(), float.Parse(data["Count"].ToString()));
            }
        }
        return jsonDate;
    }


    private void DictionarySort(Dictionary<string, float> dic)
    {
        if (dic.Count > 0)
        {
            List<KeyValuePair<string, float>> lst = new List<KeyValuePair<string, float>>(dic);
            lst.Sort(delegate (KeyValuePair<string, float> s1, KeyValuePair<string, float> s2)
            {
                return -s2.Value.CompareTo(s1.Value);
            });
            //ParentsShow.rectTransform.sizeDelta = new Vector2(600, lst.Count * 100);
            //ParentsShowUI.GetComponent<Mask>().enabled = true;
            //ParentsShowUI.GetComponentInChildren<Scrollbar>().value = 1;
            dic.Clear();
            float i = 1, r = 0, g = 0, b = 0;

            foreach (KeyValuePair<string, float> kvp in lst)
            {
                Vector3 loc = parentsShow.transform.position;
                loc.y = loc.y - i * 30;
                if (i <= 3)
                {
                    string[] Key = kvp.Key.Split('|');
                    GameObject ga = Instantiate(prefabShow, loc, Quaternion.identity) as GameObject;
                    Debug.Log(ga.transform.position.x);
                    Debug.Log(ga.transform.position.y);
                    ga.transform.SetParent(parentsShow.transform);
                    Debug.Log(ga.transform.position.x);
                    Debug.Log(ga.transform.position.y);
                    Text[] Children = ga.GetComponentsInChildren<Text>();
                    Children[0].color = new Color(r, g, b);
                    Children[1].color = new Color(r, g, b);
                    Children[2].color = new Color(r, g, b);
                    Children[3].color = new Color(r, g, b);


                    Children[1].text = Key[0];
                    Children[3].text = kvp.Value.ToString()+ " s";
                    Children[2].text = Key[1];
                    Children[0].text = (i++).ToString();
                }
                else if (i < 6)
                {
                    string[] Key = kvp.Key.Split('|');
                    GameObject ga = Instantiate(prefabShow, loc, Quaternion.identity) as GameObject;
                    ga.transform.SetParent(parentsShow.transform);
                    Text[] Children = ga.GetComponentsInChildren<Text>();
                    Children[1].text = Key[0];
                    Children[3].text = kvp.Value.ToString() + " s";
                    Children[2].text = Key[1];
                    Children[0].text = (i++).ToString();
                }
                else
                    break;
            }
        }
    }
}
