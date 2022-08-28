
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.30.2.  DO NOT EDIT!
//*************************************************************************************

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Bridge;
using XTC.FMP.MOD.ImageAtlas3D.LIB.MVCS;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Proto;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{
    public class MyInstanceBase
    {
        public string uid { get; private set; }
        public GameObject rootUI { get; private set; }
        public GameObject rootAttachments { get; private set; }
        public ObjectsPool contentObjectsPool { get; private set; }
        public ObjectsPool themeObjectsPool { get; private set; }


        public IDesignerViewBridge viewBridgeDesigner { get; set; }

        public IHealthyViewBridge viewBridgeHealthy { get; set; }


        protected MyEntryBase entry_ { get; set; }
        protected LibMVCS.Logger logger_ { get; set; }
        protected MyConfig config_ { get; set; }
        protected MyConfig.Style style_ { get; set; }
        protected Dictionary<string, LibMVCS.Any> settings_ { get; set; }
        protected MonoBehaviour mono_ {get;set;}

        public MyInstanceBase(string _uid, string _style, MyConfig _config, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
        {
            uid = _uid;
            config_ = _config;
            logger_ = _logger;
            settings_ = _settings;
            entry_ = _entry;
            mono_ = _mono;
            rootAttachments = _rootAttachments;
            foreach(var style in config_.styles)
            {
                if (style.name.Equals(_style))
                {
                    style_ = style;
                    break;
                }
            }
            contentObjectsPool = new ObjectsPool(uid + ".Content", logger_);
            themeObjectsPool = new ObjectsPool(uid + ".Theme", logger_);
        }

        /// <summary>
        /// 实例化UI
        /// </summary>
        /// <param name="_instanceUI">ui的实例模板</param>
        public void InstantiateUI(GameObject _instanceUI)
        {
            rootUI = Object.Instantiate(_instanceUI, _instanceUI.transform.parent);
            rootUI.name = uid;
        }

        public void SetupBridges()
        {

            var facadeDesigner = entry_.getDynamicDesignerFacade(uid);
            var bridgeDesigner = new DesignerUiBridge();
            bridgeDesigner.logger = logger_;
            facadeDesigner.setUiBridge(bridgeDesigner);
            viewBridgeDesigner = facadeDesigner.getViewBridge() as IDesignerViewBridge;

            var facadeHealthy = entry_.getDynamicHealthyFacade(uid);
            var bridgeHealthy = new HealthyUiBridge();
            bridgeHealthy.logger = logger_;
            facadeHealthy.setUiBridge(bridgeHealthy);
            viewBridgeHealthy = facadeHealthy.getViewBridge() as IHealthyViewBridge;

        }

        /// <summary>
        /// 将目标按锚点在父对象中对齐
        /// </summary>
        /// <param name="_target">目标</param>
        /// <param name="_anchor">锚点</param>
        protected void alignByAncor(Transform _target, MyConfig.Anchor _anchor)
        {
            if (null == _target)
                return;
            RectTransform rectTransform = _target.GetComponent<RectTransform>();
            if (null == rectTransform)
                return;

            RectTransform parent = _target.transform.parent.GetComponent<RectTransform>();
            float marginH = 0;
            if (_anchor.marginH.EndsWith("%"))
            {
                float margin = 0;
                float.TryParse(_anchor.marginH.Replace("%", ""), out margin);
                marginH = (margin / 100.0f) * (parent.rect.width / 2);
            }
            else
            {
                float.TryParse(_anchor.marginH, out marginH);
            }

            float marginV = 0;
            if (_anchor.marginV.EndsWith("%"))
            {
                float margin = 0;
                float.TryParse(_anchor.marginV.Replace("%", ""), out margin);
                marginV = (margin / 100.0f) * (parent.rect.height / 2);
            }
            else
            {
                float.TryParse(_anchor.marginV, out marginV);
            }

            Vector2 anchorMin = new Vector2(0.5f, 0.5f);
            Vector2 anchorMax = new Vector2(0.5f, 0.5f);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            if (_anchor.horizontal.Equals("left"))
            {
                anchorMin.x = 0;
                anchorMax.x = 0;
                pivot.x = 0;
            }
            else if (_anchor.horizontal.Equals("right"))
            {
                anchorMin.x = 1;
                anchorMax.x = 1;
                pivot.x = 1;
                marginH *= -1;
            }

            if (_anchor.vertical.Equals("top"))
            {
                anchorMin.y = 1;
                anchorMax.y = 1;
                pivot.y = 1;
                marginV *= -1;
            }
            else if (_anchor.vertical.Equals("bottom"))
            {
                anchorMin.y = 0;
                anchorMax.y = 0;
                pivot.y = 0;
            }

            Vector2 position = new Vector2(marginH, marginV);
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.pivot = pivot;
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = new Vector2(_anchor.width, _anchor.height);
        }

        protected void loadSpriteFromTheme(string _file, System.Action<Sprite> _onFinish)
        {
            string datapath = settings_["datapath"].AsString();
            string vendor = settings_["vendor"].AsString();
            string dir = System.IO.Path.Combine(datapath, vendor);
            dir = System.IO.Path.Combine(dir, "themes");
            dir = System.IO.Path.Combine(dir, MyEntryBase.ModuleName);
            string filefullpath = System.IO.Path.Combine(dir, _file);
            themeObjectsPool.LoadTexture(filefullpath, null, (_texture) =>
            {
                var sprite = Sprite.Create(_texture as Texture2D, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
                _onFinish(sprite);
            });
        }

         
        protected string combineAssetPath(string _source, string _uri)
        {
            if(_source.Equals("file://assloud"))
            {
                var dir = Path.Combine(settings_["datapath"].AsString(), settings_["vendor"].AsString());
                dir = Path.Combine(dir, "assloud");
                return Path.Combine(dir, _uri);
            }
            return _uri;
        }


        protected virtual void submitDesignerReadStyleSheet(ScopeRequest _request)
        {
            var dto = new ScopeRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnReadStyleSheetSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerWriteStyle(DesignerWriteStylesRequest _request)
        {
            var dto = new DesignerWriteStylesRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnWriteStyleSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerReadInstances(ScopeRequest _request)
        {
            var dto = new ScopeRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnReadInstancesSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerWriteInstances(DesignerWriteInstancesRequest _request)
        {
            var dto = new DesignerWriteInstancesRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnWriteInstancesSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitHealthyEcho(HealthyEchoRequest _request)
        {
            var dto = new HealthyEchoRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeHealthy.OnEchoSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }


    }
}
