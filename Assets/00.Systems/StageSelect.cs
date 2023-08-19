using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageNum
{
    MainScene,
    StageSelectScene,
    Stage1Scene,
    Stage2Scene,
    LoadScene
}

public class StageSelect : MonoBehaviour
{
    public void StageSelect_Scene()
    {
        int stageSelect_Scene = (int)StageNum.StageSelectScene;
        SceneManager.LoadScene(stageSelect_Scene);
    }

    public void Stage1_Scene()
    {
        int stage1_Scene = (int)StageNum.Stage1Scene;
        SceneManager.LoadScene(stage1_Scene);
    }

    public void Stage2_Scene()
    {
        int stage2_Scene = (int)StageNum.Stage2Scene;
        SceneManager.LoadScene(stage2_Scene);
    }
}
