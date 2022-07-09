using System;
using System.Collections.Generic;
using System.Text;

namespace PuttingChallenge.Lucioli
{
    public class DescriptorAttributeSceneType : Attribute
    {
        public bool IsLevel { get; }
        public DescriptorAttributeSceneType(bool isLevel)
        {
            IsLevel = isLevel;
        }
    }
    public enum SceneType
    {
        [DescriptorAttributeSceneType(isLevel:false)]
        MainMenu,
        [DescriptorAttributeSceneType(isLevel: true)]
        Environment1,
        [DescriptorAttributeSceneType(isLevel: true)]
        Environment2,
        [DescriptorAttributeSceneType(isLevel: true)]
        Environment3,
        [DescriptorAttributeSceneType(isLevel: false)]
        Leaderboard,
        [DescriptorAttributeSceneType(isLevel: false)]
        GameOver,
        [DescriptorAttributeSceneType(isLevel: false)]
        GameWin
    }
}
