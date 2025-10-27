using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Invrntory
{
    public class ItemManager : MonoBehaviour
    {
        public Item itemPrefab;

        private Transform itemParent;

        private void OnEnable()
        {
            EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
        }

        private void OnDisable()
        {
            EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
        }

        private void Start()
        {
            itemParent = GameObject.FindWithTag("ItemParent").transform;
        }

        private void OnInstantiateItemInScene(int id, Vector3 vector)
        {
            var item = Instantiate(itemPrefab, vector, Quaternion.identity);
            item.itemID = id;
        }
    }

}