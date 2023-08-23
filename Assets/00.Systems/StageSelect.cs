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

public class StageSelect : MonoSingleton<StageSelect>
{
    public void Main_Scene()
    {
        int main_Scene = (int)StageNum.MainScene;
        SceneManager.LoadScene(main_Scene);
    }

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

    public void LoadThisScene()
    {
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisScene);
    }
}
