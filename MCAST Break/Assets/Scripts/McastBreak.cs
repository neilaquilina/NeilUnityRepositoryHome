using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class McastBreak : MonoBehaviour
{

    [SerializeField] GameObject elf;
    [SerializeField] GameObject table;
    [SerializeField] GameObject chair;
    [SerializeField] GameObject pc;
    [SerializeField] GameObject door;
    [SerializeField] GameObject room;
    [SerializeField] Text displayText;
    bool screwdriver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //displayText = GetComponent<Text>();
        elf = GameObject.Find("Elf");
        table = GameObject.Find("Table object");
        chair = GameObject.Find("chair Object");
        pc = GameObject.Find("PC object");
        door = GameObject.Find("LockedDoor");
        room = GameObject.Find("roomObject");
    }

    // Update is called once per frame
    void Update()
    {
        print(displayText.name);
    }

    public void WalkToTable()
    {
        elf.transform.position = table.transform.position;
        displayText.text = "There is only Dust here. I should go back to the room!";
    }

    public void WalkToRoom()
    {
        elf.transform.position = room.transform.position;
        displayText.text = "In Room!";
    }

    public void WalkToChair()
    {
        elf.transform.position = chair.transform.position;
        displayText.text = "Yuck!! This is full of borer holes!!";
    }

    public void WalkToPC()
    {
        elf.transform.position = pc.transform.position;
        displayText.text = "Look, someone left a screwdriver here. Better take it!";
        screwdriver = true;
    }

    public void WalkToLockedDoor()
    {
        elf.transform.position = door.transform.position;
        if (!screwdriver)
        {
            displayText.text = "The door is locked and I have no key! I need something to pick the lock with!!";
        }
        else
        {
            displayText.text = "I can pick the lock with the screwdriver! I am FREEE!!!";
        }
        
    }
}
