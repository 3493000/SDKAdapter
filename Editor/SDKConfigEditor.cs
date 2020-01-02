//  Desc:        Framework For Game Develop with Unity3d
//  Copyright:   Copyright (C) 2019 Hunter. All rights reserved.
//  ————————————————————————
//  ————————————————————————
//  Author:      Hunter
//  E-mail:      3493000@qq.com
using System;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Hunter.Editor
{
    public class SDKConfigEditor
    {
        [MenuItem("Assets/Hunter/Config/Build SDKConfig")]
        public static void BuildSDKConfig()
        {
            BuildConfig<SDKConfig>("SDKConfig");
        }

        [MenuItem("Assets/Hunter/Config/Build Color Config")]
        public static void BuildColorConfig()
        {
            BuildConfig<ColorConfig>("ColorConfig");
        }

        public static void BuildConfig<T>(string name) where T : ScriptableObject
        {
            T data = null;
            string folderPath = EditorUtils.GetSelectedDirAssetsPath();
            string spriteDataPath = folderPath + string.Format("/{0}.asset", name);

            data = AssetDatabase.LoadAssetAtPath<T>(spriteDataPath);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(data, spriteDataPath);
            }
            Log.i("Create Config In Folder:" + spriteDataPath);
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }
    }
}
