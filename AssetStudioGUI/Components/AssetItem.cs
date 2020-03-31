using System.Windows.Forms;
using AssetStudio;
using Object = AssetStudio.Object;

namespace AssetStudioGUI
{
    internal class AssetItem : ListViewItem
    {
        public Object Asset;
        public SerializedFile SourceFile;
        public string Container = string.Empty;
        public string TypeString;
        public long m_PathID;
        public long FullSize;
        public ClassIDType Type;
        public string InfoText { get; set; }
        public string UniqueID;
        public GameObjectTreeNode TreeNode;
        public string Info = "";

        public AssetItem(Object asset)
        {
            Asset = asset;
            SourceFile = asset.assetsFile;
            Type = asset.type;
            TypeString = Type.ToString();
            m_PathID = asset.m_PathID;
            FullSize = asset.byteSize;
            InitAssetInfo();
        }

        public void SetSubItems()
        {
            SubItems.AddRange(new[]
            {
                Container, //Container
                TypeString, //Type
                m_PathID.ToString(), //PathID
                StringHelper.GetSizeStr(FullSize),
                Info
            });
        }

        public void InitAssetInfo()
        {
            if (Asset == null)
            {
                return;
            }

            switch (Asset)
            {
                case Texture2D t:
                    Info = $"{t.m_Width}*{t.m_Height},{t.m_TextureFormat},Mip{t.m_MipCount}";
                    break;
            }
        }
    }

    public static class StringHelper
    {
        public static string GetSizeStr(long sizeByte)
        {
            const float v = 1024f;
            float size = sizeByte;
            if (size < v)
                return size.ToString("##.##") + " B";
            size = size / v;
            if (size < v)
                return size.ToString("##.##") + " K";
            size = size / v;
            if (size < v)
                return size.ToString("##.##") + " M";
            return size.ToString("##.##") + " G";
        }
    }
}
