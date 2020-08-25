using UnityEngine;
using System.Collections;
// Unity3D游戏引擎一共支持4个音乐格式的文件

//.AIFF 适用于较短的音乐文件可用作游戏打斗音效

//.WAV  适用于较短的音乐文件可用作游戏打斗音效

//.MP3 适用于较长的音乐文件可用作游戏背景音乐

//.OGG 适用于较长的音乐文件可用作游戏背景音乐
public class AudioManager : UnitySingleton<AudioManager>
{
    private AudioSource audioSource;
    private float audioValue = 0;
    private bool isMute = false;
    void Awake()
    {
      //  GameTool.DeleteAll();//测试用
      // 初始化音效
        InitAudio();      
    }
    private void InitAudio()
    {
        //初始化的时候，去获取音源组件
        audioSource = GameTool.GetTheChildComponent<AudioSource>(this.gameObject, "AudioManager");
        if (!GameTool.HasKey("AudioValue"))
        {
            GameTool.SetFloat("AudioValue", 0.5f);
            audioValue = 0.5f;
        }
        else
        {
            audioValue = GameTool.GetFloat("AudioValue");
            audioSource.volume = audioValue;
        }

        if (!GameTool.HasKey("Mute"))
        {
            GameTool.SetString("Mute", "false");
            isMute = false;
        }
        else
        {
            isMute = bool.Parse(GameTool.GetString("Mute"));
            audioSource.mute = isMute;
        }
    }
    //设置音量
    public void SetAudioValue(float value)
    {
        audioValue = value;
        //把外界传进来的值设置给组件
        audioSource.volume = value;
        //把最新的值存储到内存中
        GameTool.SetFloat("AudioValue", value);
    }
    public void SetMute(bool isMute)
    {
        this.isMute = isMute;
        //把外界传进来的值设置给组件
        audioSource.mute = isMute;
        //把最新的值存储到内存中
        GameTool.SetString("Mute", isMute.ToString());

    }
    public float GetAudioValue()
    {
        return audioValue;
    }
    public bool GetMute()
    {
        return isMute;
    }
}
