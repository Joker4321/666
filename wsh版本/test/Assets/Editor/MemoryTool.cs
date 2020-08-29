using UnityEngine;
using System.Collections;
using UnityEditor;

public class MemoryTool
{
    [MenuItem("MemoryTool/ClearMemory")]
    public static void ClearMemory()
    {
        GameTool.DeleteAll();
        Debug.Log("成功清空数据");
    }
}
