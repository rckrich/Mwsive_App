using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicScroll : MonoBehaviour
{
    public float MaxPrefabsInScreen = 0;
    public ScrollRect ScrollBar;
    private float ScrollbarVerticalPos =-0.001f;
    public GameObject SpawnArea, Prefabs, LastPosition, PrefabPosition;
    private GameObject Instance;  
    public List<GameObject> Instances = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        DynamicPrefabSpawner(MaxPrefabsInScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckForSpawn(){
        Debug.Log(ScrollBar.verticalNormalizedPosition + " " + ScrollbarVerticalPos);
        if(Instances.Count != 0){
            if(ScrollBar.verticalNormalizedPosition  <= ScrollbarVerticalPos){
                
                DynamicPrefabSpawner(MaxPrefabsInScreen);
            }
        }
        
    }



    public void DynamicPrefabSpawner(float howmanyprefabs){
        if(MaxPrefabsInScreen ==0){
            
            Vector3[] v = new Vector3[4];
            SpawnArea.GetComponent<RectTransform>().GetWorldCorners(v);
            float maxY = Mathf.Max (v [0].y, v [1].y, v [2].y, v [3].y);
            SpawnPrefab();
            MaxPrefabsInScreen = Mathf.Round(maxY / Instances[Instances.Count -1].GetComponent<RectTransform>().sizeDelta.y);
            howmanyprefabs = MaxPrefabsInScreen;
        }
        if(howmanyprefabs != MaxPrefabsInScreen){
            for (int i = 0; i <= howmanyprefabs; i++)
            {
                SpawnPrefab();
            }
        }else{
            for (int i = 0; i <= MaxPrefabsInScreen; i++)
            {
                SpawnPrefab();
            }
        }
        LastPosition.transform.SetAsLastSibling();

    }

    public void KillPrefablist(){
        foreach (GameObject Prefab in Instances)
        {
            Destroy(Prefab);
        }
        Instances.Clear();
    }

    private void SpawnPrefab(){
        
        Instance = Instantiate(Prefabs,PrefabPosition.transform.position, Quaternion.identity);
        Instance.transform.SetParent(GameObject.Find("PF_Container").transform);
        Instance.transform.localScale = new Vector3(1,1,1);  
        Instances.Add(Instance);

    }
    
}
