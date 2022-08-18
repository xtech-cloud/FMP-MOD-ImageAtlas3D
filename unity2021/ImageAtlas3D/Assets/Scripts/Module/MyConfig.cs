
using System.Xml.Serialization;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlAttribute("renderer")]
            public string renderer { get; set; } = "";

            [XmlElement("VoiceButton")]
            public UiElement voiceButton{ get; set; } = new UiElement();
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

