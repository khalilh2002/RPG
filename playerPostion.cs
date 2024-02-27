using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPostion : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] RoomFirstMapGenerator obj;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        
        obj.runRoomFirstMapGeneratorClass();
        
        var FirstRoom = RoomFirstMapGenerator.FirstRoom;
        player.transform.position =new Vector3(FirstRoom.center.x ,FirstRoom.center.y ,FirstRoom.center.z );
    }

    // Update is called once per frame
   private void Update() {
        if (Input.GetKey(KeyCode.Y))
        {
            Debug.Log("you enter Y");
            obj.runRoomFirstMapGeneratorClass();
            var FirstRoom = RoomFirstMapGenerator.FirstRoom;
            player.transform.position =new Vector3(FirstRoom.center.x ,FirstRoom.center.y ,FirstRoom.center.z );
        }
   }
}
