using System.Collections.Generic;
using UnityEngine;

public class InventoryPopup : MonoBehaviour
{
    public static InventoryPopup S { private set; get; }

    [SerializeField]
    private ItemPanel _itemPrefab;

    [SerializeField]
    private GameObject _inventoryPopup;

    private List<GameObject> _instanciatedItems;

    public void Awake()
    {
        S = this;
    }

    public void Start()
    {
        _instanciatedItems = new List<GameObject>();
    }

    public void ToggleInventory()
    {
        _inventoryPopup.SetActive(!_inventoryPopup.activeInHierarchy);
        if (_inventoryPopup.activeInHierarchy)
        {
            foreach (var item in PlayerController.S.Inventory.GetItems())
            {
                GameObject go = Instantiate(_itemPrefab.gameObject, _inventoryPopup.transform);
                var rTransform = (RectTransform)go.transform;
                rTransform.anchoredPosition = new Vector2(0f, -30 - (_instanciatedItems.Count * 60f));
                _instanciatedItems.Add(go);

                var fItem = ItemsManager.S.GetItem(item.Key);
                go.GetComponent<ItemPanel>().Init(fItem.GetIcon(), fItem.GetName(), fItem.GetDescription());
            }
        }
        else
        {
            foreach (GameObject go in _instanciatedItems)
                Destroy(go);
            _instanciatedItems.RemoveAll(x => true);
        }
    }
}