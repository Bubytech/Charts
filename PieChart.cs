using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public enum orderEnum
{
    LargestToSmallest,
    SmallestToLargest,
    OrderOfInput,
}

public class PieChart : MonoBehaviour
{

    //Trigger Options
    public bool Triggers = false;
    public bool TriggerOnClick = false;
    public UnityEvent clickMethod;
    public bool TriggerOnHover = false;
    public UnityEvent hoverMethod;
    public Vector3 textPositonOffset;
    public Vector3 textRotationOffset;
    public float rotationOffset;
    public float overallValue;
    public string valueDisplay;
    //<%> = %Percentage
    //<V> = Value
    //<N> = Name
    public orderEnum order;
    [Serializable]
    public class data
    {
        public string name = "Data";
        public float value = 1;
        public Color color;
    }

    public List<data> dataList = new List<data>();
    public List<GameObject> textList = new List<GameObject>();
    public List<GameObject> dataCircles = new List<GameObject>();
    [Space]

    public Sprite circle;
    private bool doneGenerating = true;
    private void Start()
    {
        drawChart();
    }

    public void drawChart()
    {
        if (doneGenerating && circle != null)
        {
            removeChart();
            doneGenerating = false;
            overallValue = 0;
            dataCircles = new List<GameObject>();
            //calculate overall
            foreach (data da in dataList)
            {
                overallValue += da.value;
            }
            //Create Circles
            if (order == orderEnum.OrderOfInput)
            {
                foreach (data da in dataList)
                {
                    GameObject ImageObj = new GameObject();
                    ImageObj.transform.parent = this.transform;
                    ImageObj.AddComponent<Image>();
                    ImageObj.name = da.name;
                    ImageObj.layer = 5;
                    if (Triggers)
                        ImageObj.AddComponent<PieChartTriggers>();
                    ImageObj.tag = "Circle";
                    dataCircles.Add(ImageObj);
                    Image img = ImageObj.GetComponent<Image>();
                    RectTransform ImageObjRect = ImageObj.GetComponent<RectTransform>();
                    ImageObjRect.anchorMin = new Vector2(0, 0);
                    ImageObjRect.anchorMax = new Vector2(1, 1);
                    ImageObjRect.pivot = new Vector2(.498f, .5f);
                    ImageObjRect.anchoredPosition = new Vector2(.5f, .5f);
                    ImageObjRect.sizeDelta = new Vector2(0, 0);
                    img.sprite = circle;
                    img.preserveAspect = true;
                    img.type = Image.Type.Filled;
                    //Calculate size
                    img.fillAmount = da.value / overallValue;
                    img.color = da.color + new Color32(0, 0, 0, 255);
                }
                float currentRot = 0;
                int currentdat = 0;
                foreach (data dat in dataList)
                {
                    if (dat != dataList[0])
                    {
                        float rotateTo = ((dat.value / overallValue) * 360) + currentRot;
                        currentRot += rotateTo - currentRot;
                        dataCircles[currentdat].transform.eulerAngles = new Vector3(0, 0, rotateTo + rotationOffset);
                    }
                    else
                    {
                        dataCircles[currentdat].transform.eulerAngles = new Vector3(0, 0, rotationOffset);
                    }
                    currentdat++;
                }
                doneGenerating = true;
            }
            else if(order == orderEnum.LargestToSmallest)
            {
                List<data> dataListOrganized = new List<data>();
                foreach(data da in dataList)
                {
                    dataListOrganized.Add(da);
                }
                if (dataListOrganized.Count > 0)
                {
                    dataListOrganized.Sort((IComparer<data>)new sortDown());
                }

                foreach (data da in dataListOrganized)
                {
                    GameObject ImageObj = new GameObject();
                    ImageObj.transform.parent = this.transform;
                    ImageObj.AddComponent<Image>();
                    ImageObj.name = da.name;
                    ImageObj.layer = 5;
                    if (Triggers)
                        ImageObj.AddComponent<PieChartTriggers>();
                    ImageObj.tag = "Circle";
                    dataCircles.Add(ImageObj);
                    Image img = ImageObj.GetComponent<Image>();
                    RectTransform ImageObjRect = ImageObj.GetComponent<RectTransform>();
                    ImageObjRect.anchorMin = new Vector2(0, 0);
                    ImageObjRect.anchorMax = new Vector2(1, 1);
                    ImageObjRect.pivot = new Vector2(.498f, .5f);
                    ImageObjRect.anchoredPosition = new Vector2(.5f, .5f);
                    ImageObjRect.sizeDelta = new Vector2(0, 0);
                    img.sprite = circle;
                    img.preserveAspect = true;
                    img.type = Image.Type.Filled;
                    //Calculate size
                    img.fillAmount = da.value / overallValue;
                    img.color = da.color + new Color32(0, 0, 0, 255);
                }
                float currentRot = 0;
                int currentdat = 0;
                foreach (data dat in dataListOrganized)
                {
                    if (dat != dataList[0])
                    {
                        float rotateTo = ((dat.value / overallValue) * 360) + currentRot;
                        currentRot += rotateTo - currentRot;

                        dataCircles[currentdat].transform.eulerAngles = new Vector3(0, 0, rotateTo + rotationOffset);
                    }
                    else
                    {
                        dataCircles[currentdat].transform.eulerAngles = new Vector3(0, 0, rotationOffset);
                    }
                    currentdat++;
                }
                doneGenerating = true;
            }
            else if (order == orderEnum.SmallestToLargest)
            {
                List<data> dataListOrganized = new List<data>();
                foreach (data da in dataList)
                {
                    dataListOrganized.Add(da);
                }
                if (dataListOrganized.Count > 0)
                {
                    dataListOrganized.Sort((IComparer<data>)new sortUp());
                }

                foreach (data da in dataListOrganized)
                {
                    GameObject ImageObj = new GameObject();
                    ImageObj.transform.parent = this.transform;
                    ImageObj.AddComponent<Image>();
                    ImageObj.name = da.name;
                    ImageObj.layer = 5;
                    if(Triggers)
                        ImageObj.AddComponent<PieChartTriggers>();
                    ImageObj.tag = "Circle";
                    dataCircles.Add(ImageObj);
                    Image img = ImageObj.GetComponent<Image>();
                    RectTransform ImageObjRect = ImageObj.GetComponent<RectTransform>();
                    ImageObjRect.anchorMin = new Vector2(0, 0);
                    ImageObjRect.anchorMax = new Vector2(1, 1);
                    ImageObjRect.pivot = new Vector2(.498f, .5f);
                    ImageObjRect.anchoredPosition = new Vector2(.5f, .5f);
                    ImageObjRect.sizeDelta = new Vector2(0, 0);
                    img.sprite = circle;
                    img.preserveAspect = true;
                    img.type = Image.Type.Filled;
                    //Calculate size
                    img.fillAmount = da.value / overallValue;
                    img.color = da.color + new Color32(0, 0, 0, 255);
                }
                float currentRot = 0;
                int currentdat = 0;
                foreach (data dat in dataListOrganized)
                {
                    if (dat != dataList[0])
                    {
                        float rotateTo = ((dat.value / overallValue) * 360) + currentRot;
                        currentRot += rotateTo - currentRot;
                        dataCircles[currentdat].transform.eulerAngles = new Vector3(0, 0, rotateTo + rotationOffset);
                    }
                    else
                    {
                        dataCircles[currentdat].transform.eulerAngles = new Vector3(0, 0, rotationOffset);
                    }
                    currentdat++;
                }
                doneGenerating = true;
            }
            createText();
        }
        else if(circle == null){
            Debug.LogError("Circle Texture has not been assigned!");
        }
    }
    public void removeChart()
    {
        if (doneGenerating)
        {
            while (transform.childCount != 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }

    public void addData(string Name, float value, Color color)
    {
        data newData = new data();
        newData.name = Name;
        newData.value = value;
        newData.color = color;

        dataList.Add(newData);
    }

    public void addData(string Name, float value)
    {
        data newData = new data();
        newData.name = Name;
        newData.value = value;
        newData.color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        while (checkColor(newData.color))
        {
            newData.color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        }

        dataList.Add(newData);
    }

    public void addData()
    {
        data newData = new data();
        newData.name = "Data " + dataList.Count;
        newData.value = 1;
        newData.color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        while (checkColor(newData.color))
        {
            newData.color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        }

        dataList.Add(newData);
    }

    private bool checkColor(Color color)
    {
        bool condition = false;
        foreach (data da in dataList)
        {
            if (da.color != color)
            {
                condition = false;
            }
            else
            {
                condition = true;
            }
        }
        return condition;
    }


    public void removeData(string Name)
    {
        foreach (data da in dataList)
        {
            if (da.name == name)
            {
                dataList.Remove(da);
            }
        }
    }

    public void removeData(Color color)
    {
        foreach(data da in dataList)
        {
            if(da.color == color)
            {
                dataList.Remove(da);
            }
        }
    }
    
    public float getValueByName(string name)
    {
        float value = 0;
        int valuesWithName = 0;
        foreach (data da in dataList)
        {
            if(da.name == name)
            {
                valuesWithName++;
                value = da.value;
            }

        }

        if(valuesWithName > 1)
            Debug.LogError("There were " + valuesWithName + " Values with the name " + name + ", Consider changing names of values.");

        return value;
    }

    public float getValueByColor(Color color)
    {
        float value = 0;
        int valuesWithName = 0;
        foreach (data da in dataList)
        {
            if (da.color == color)
            {
                valuesWithName++;
                value = da.value;
            }

        }

        if (valuesWithName > 1)
            Debug.LogError("There were " + valuesWithName + " Values with the same color, Consider changing color of values.");

        return value;
    }

    public float getValueByIndex(int index)
    {
        float value = 0;
        if (dataList.Count >= index)
        {
            value = dataList[index].value;
            return value;
        }
        else
        {
            Debug.LogError("Index could not be found!");
        }
        return value;
    }

    [ExecuteInEditMode]
    public string decryptText(string text, int valueIndex)
    {
        //<%> = %Percentage
        //<V> = Value
        //<N> = Name
        string outText = valueDisplay + "";
        if(outText.Contains("<%>"))
            outText = outText.Replace("<%>", "%" + ((dataList[valueIndex].value / overallValue) * 100).ToString());
        if (outText.Contains("<V>"))
            outText = outText.Replace("<V>", "" + dataList[valueIndex].value.ToString());
        if (outText.Contains("<N>"))
            outText = outText.Replace("<N>", "" + dataList[valueIndex].name.ToString());
        return outText;
    }

    [ExecuteInEditMode]
    public string decryptText(string text)
    {
        //!! DO NOT USE FOR PREVIEW PURPOSES ONLY !!
        string outText = text + "";
        if (outText.Contains("<%>"))
            outText = outText.Replace("<%>", "%100");
        if (outText.Contains("<V>"))
            outText = outText.Replace("<V>", "10");
        if (outText.Contains("<N>"))
            outText = outText.Replace("<N>", "Data");
        return outText;
    }

    public void createText()
    {
        if (valueDisplay != "")
        {
            if (dataCircles.Count > 0)
                for (int i = 0; i < dataCircles.Count; i++)
                {
                    GameObject textObj = new GameObject();
                    textList.Add(textObj);
                    textObj.name = "Text";
                    textObj.transform.parent = dataCircles[i].transform;
                    textObj.AddComponent<Text>();
                    textObj.transform.position = textPositonOffset;
                    textObj.transform.eulerAngles = textRotationOffset;
                    Text textstring = textObj.GetComponent<Text>();
                    textstring.text = decryptText(valueDisplay, i);
                }
        }
    }

    private class sortUp : IComparer<data>
    {
        int IComparer<data>.Compare(data _objA, data _objB)
        {
            float t1 = _objA.value;
            float t2 = _objB.value;
            return t1.CompareTo(t2);
        }
    }

    private class sortDown : IComparer<data>
    {
        int IComparer<data>.Compare(data _objA, data _objB)
        {
            float t1 = _objA.value;
            float t2 = _objB.value;
            int ans = 0;
            if (t1.CompareTo(t2) == 0)
            {
                ans = 0;
            }else if(t1.CompareTo(t2) == 1)
            {
                ans = -1;
            }else if(t1.CompareTo(t2) == -1){
                ans = 1;
            }

            return ans;
        }
    }
 
    [ExecuteInEditMode]
    private void Update()
    {
        if(dataCircles.Count > 0)
        foreach (GameObject go in dataCircles)
        {
            if (doneGenerating && go.GetComponent<RectTransform>()) {
                RectTransform recttransform = go.GetComponent<RectTransform>();
                recttransform.pivot = new Vector2(.498f, .5f);
            }
        }
    }

    public void clicked()
    {
        if(clickMethod != null)
        clickMethod.Invoke();
    }

    public void hover()
    {
        if(hoverMethod != null)
        hoverMethod.Invoke();
    }
}
