using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        TAsset Load<TAsset>(string path) where TAsset : UnityEngine.Object;
        List<string> GetAssetsListByPath(string label, Type type = null);
        TAsset[] LoadAll<TAsset>(List<string> keys) where TAsset : class;
    }
}