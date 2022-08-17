
using System.Xml.Serialization;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class TipImage
        {
            [XmlAttribute("swip")]
            public string swip { get; set; } = "";
        }

        public class VoiceButton
        {
            [XmlAttribute("image")]
            public string image{ get; set; } = "";

            [XmlElement("Anchor")]
            public Anchor anchor { get; set; } = new Anchor();
        }

        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlAttribute("renderer")]
            public string renderer { get; set; } = "";

            [XmlElement("VoiceButton")]
            public VoiceButton voiceButton{ get; set; } = new VoiceButton();

            [XmlElement("TipImage")]
            public TipImage tipImage { get; set; } = new TipImage();
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

