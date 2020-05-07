using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable() //Loads the database when the scriptable object is enabled.
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public void AddItem(ItemObject _item, int _amount) //Adds the item to the inventory slot.
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount); //We add the item by an amount (probably just 1 in all cases)
                return;
            }
        }
            Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
    }

    public void SaveInventory()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.sav";
        FileStream file = new FileStream(path, FileMode.Create);
        bf.Serialize(file, saveData);
        file.Close();
    }
    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.sav";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = new FileStream(path, FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize() //On deserializing. 
    {
        for (int i = 0; i < Container.Count; i++) Container[i].item = database.GetItem[Container[i].ID];
    }
    public void OnBeforeSerialize() { } //This is required by ISerializationCallbackReceiver, do not remove this.
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}