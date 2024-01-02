using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace Assets.Scripts.Editor
{
    public sealed class MyAssetPostprocessor : AssetPostprocessor
    {
        private readonly static List<(string regexPattern, Action<string> handler)> _handlers = new();

        static MyAssetPostprocessor()
        {
            // Resources/Tables/ 하위의 Item.*.xml 패턴은 모두 ItemTableAssetImporter로 임포팅!
            Register<ItemTableAssetImporter>("Assets/Resources/Tables/Item.*.xml");
        }

        private void OnPreprocessAsset()
        {
            RefreshImporter(assetPath);
        }

        // glob 패턴으로 임포터 등록.
        private static void Register<T>(string glob) where T : ScriptedImporter
        {
            string regex = GlobSearchUtilities.GlobToRegex(glob);
            Action<string> handler = (assetPath) =>
            {
                if (AssetImporter.GetAtPath(assetPath) is T)
                {
                    return;
                }

                AssetDatabase.SetImporterOverride<T>(assetPath);
            };

            _handlers.Add((regex, handler));
        }

        // 해당 path의 임포터 갱신.
        private static void RefreshImporter(string assetPath)
        {
            foreach (var (regexPattern, handler) in _handlers)
            {
                if (!Regex.IsMatch(assetPath, regexPattern))
                {
                    continue;
                }

                handler.Invoke(assetPath);
                return;
            }
        }
    }
}
