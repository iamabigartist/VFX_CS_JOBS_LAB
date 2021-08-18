using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AddressGet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string s1 = "Hello World";
        GCHandle gch = GCHandle.Alloc(s1, GCHandleType.Pinned);
        IntPtr pObj = gch.AddrOfPinnedObject();
        Debug.Log($"Memory address:{pObj.ToString()}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
