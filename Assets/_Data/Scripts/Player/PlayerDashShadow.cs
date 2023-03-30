using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashShadow : MonoBehaviour
{
    public static PlayerDashShadow instance;
    [SerializeField] private List<GameObject> shadowClone = new List<GameObject>();
    [SerializeField] private GameObject shadowPrefab;
    [SerializeField] private float speed = 15f;
    [SerializeField] private Color _color = new Color(17f/255f, 113f/255f, 178f/255f);
    //[SerializeField] private GameObject player;

    private Transform spawnPool;    
    private float timer;

    private void Awake()
    {
        PlayerDashShadow.instance = this;
        this.spawnPool = GameObject.Find("SpawnPool").transform;
    }

    private GameObject GetShadow()
    {
        for (int i = 0; i < this.shadowClone.Count; i++)
        {

            if (!this.shadowClone[i].activeInHierarchy)
            {
                this.shadowClone[i].name = "ShadowClone_" + i;
                this.shadowClone[i].SetActive(true);
                this.shadowClone[i].transform.position = transform.position;
                this.shadowClone[i].transform.rotation = transform.rotation;
                //this.shadowClone[i].transform.localScale = player.transform.localScale;
                this.shadowClone[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                this.shadowClone[i].GetComponent<DashShadowScript>()._color = _color;
                return this.shadowClone[i];
            }
        }
        GameObject obj = Instantiate(this.shadowPrefab, transform.position, transform.rotation, this.spawnPool);
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<DashShadowScript>()._color = _color;
        this.shadowClone.Add(obj);

        return obj;
    }

    public void ShadowEffect()
    {
        this.timer += this.speed * Time.deltaTime;
        if (this.timer > 1)
        {
            this.GetShadow();
            this.timer = 0;
        }
    }
}
