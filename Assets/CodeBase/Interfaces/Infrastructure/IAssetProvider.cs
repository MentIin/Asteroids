using System;
using System.Collections.Generic;

namespace CodeBase.Interfaces.Infrastructure
{
    public interface IAssetProvider
    {
        TAsset Load<TAsset>(string path) where TAsset : UnityEngine.Object;
        List<string> GetAssetsListByPath(string label, Type type = null);
        TAsset[] LoadAll<TAsset>(List<string> keys) where TAsset : class;
        TAsset Instantiate<TAsset>(string prefabPath) where TAsset : UnityEngine.Object;
    }
}