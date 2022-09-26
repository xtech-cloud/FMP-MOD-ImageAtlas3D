

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Proto;
using XTC.FMP.MOD.ImageAtlas3D.LIB.MVCS;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        private Material originSkybox_;
        private Camera originCamera_;

        private Material cloneSkybox_;
        private Camera cloneCamera_;

        /// <summary>
        /// 内容中的文件的存储地址
        /// </summary>
        private string storageAddress_;
        private FormatSchemaV1.Block activeBlock_;

        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        public void HandleCreated()
        {
            applyStyle().OnFinish = () =>
            {

            };
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        public void HandleOpened(string _source, string _uri)
        {
            if (style_.renderer.Equals("skybox"))
            {
                if (null == cloneCamera_)
                {
                    originCamera_ = Camera.main;
                    originCamera_.gameObject.SetActive(false);
                    var cameraClone = GameObject.Instantiate(originCamera_.gameObject);
                    cameraClone.name += string.Format(" # {0}.{1}", MyEntryBase.ModuleName, uid);
                    cloneCamera_ = cameraClone.GetComponent<Camera>();
                    cloneCamera_.clearFlags = CameraClearFlags.Skybox;
                    cloneCamera_.gameObject.SetActive(true);
                    wrapGestureCamera(cloneCamera_.transform);
                }

                if (null == cloneSkybox_)
                {
                    originSkybox_ = RenderSettings.skybox;
                    var material = rootAttachments.transform.Find("SkyboxRenderer").GetComponent<MeshRenderer>().material;
                    cloneSkybox_ = GameObject.Instantiate(material);
                    RenderSettings.skybox = cloneSkybox_;
                }
            }

            var assetFullpath = combineAssetPath(_source, _uri);
            if (_source.StartsWith("file://"))
            {
                openAssetFromFile(assetFullpath);
            }
            rootUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);

            if (style_.renderer.Equals("skybox"))
            {
                if (null != cloneCamera_)
                {
                    originCamera_.gameObject.SetActive(true);
                    originSkybox_ = RenderSettings.skybox;
                    GameObject.Destroy(cloneCamera_.gameObject);
                    cloneCamera_ = null;
                }
                if (null != cloneSkybox_)
                {
                    GameObject.Destroy(cloneSkybox_);
                    cloneSkybox_ = null;
                }
            }
        }

        /// <summary>
        /// 应用样式
        /// </summary>
        private CounterSequence applyStyle()
        {
            var sequence = new CounterSequence(0);
            var btnVoice = rootUI.transform.Find("btnVoice").GetComponent<Button>();
            alignByAncor(btnVoice.transform, style_.voiceButton.anchor);
            sequence.Dial();
            loadSpriteFromTheme(style_.voiceButton.image, (_sprite) =>
            {
                btnVoice.GetComponent<Image>().sprite = _sprite;
                btnVoice.GetComponent<Image>().SetNativeSize();
                sequence.Tick();
            });
            btnVoice.onClick.AddListener(() =>
            {
                playAudio("voice", Path.Combine(storageAddress_, activeBlock_.voice.file));
            });
            return sequence;
        }


        private void openAssetFromFile(string _assetFullpath)
        {
            logger_.Debug("ready to open {0}", _assetFullpath);
            //优先使用文件夹
            if (Directory.Exists(_assetFullpath + ".#"))
            {
                logger_.Debug("open {0}", _assetFullpath + ".#");
                openAssetFromFolder(_assetFullpath + ".#");
                return;
            }
            if (File.Exists(_assetFullpath))
            {
                logger_.Debug("open {0}", _assetFullpath);
                return;
            }
            logger_.Error("{0} not found", _assetFullpath);
        }

        private void openAssetFromFolder(string _dir)
        {
            string formatJson = Path.Combine(_dir, "format.json");
            FormatSchemaV1 schema;
            try
            {
                string json = File.ReadAllText(formatJson);
                schema = JsonConvert.DeserializeObject<FormatSchemaV1>(json);
            }
            catch (Exception ex)
            {
                logger_.Exception(ex);
                return;
            }

            storageAddress_ = _dir;
            parseFormatSchema(schema);
        }

        private void parseFormatSchema(FormatSchemaV1 _schema)
        {
            playAudio("bgm", Path.Combine(storageAddress_, _schema.bgm.file));
            changeVolume("bgm", _schema.bgm.volume);

            foreach (var block in _schema.blocks)
            {
                //TODO 
                activateBlock(block);
                break;
            }
        }

        private void activateBlock(FormatSchemaV1.Block _block)
        {
            activeBlock_ = _block;
            string imagePath = Path.Combine(storageAddress_, _block.image.file);
            renderImage3D(imagePath, style_.renderer);
            changeVolume("bgm", _block.bgm.volume);
            if (!string.IsNullOrWhiteSpace(_block.bgm.file))
            {
                playAudio("bgm", Path.Combine(storageAddress_, _block.bgm.file));
            }

            stopAudio("voice");
            var btnVoice = rootUI.transform.Find("btnVoice");
            btnVoice.gameObject.SetActive(!string.IsNullOrWhiteSpace(_block.voice.file));
        }

        private void playAudio(string _audioSource, string _file)
        {
            var audioSource = rootUI.transform.Find("AudioSources/" + _audioSource).GetComponent<AudioSource>();
            string exclusiveNumber = _audioSource;
            contentObjectsPool.LoadAudioClip(_file, exclusiveNumber, (_audioClip) =>
            {
                audioSource.clip = _audioClip;
                audioSource.Play();
            });
        }

        private void stopAudio(string _audioSource)
        {
            var audioSource = rootUI.transform.Find("AudioSources/" + _audioSource).GetComponent<AudioSource>();
            audioSource.Stop();
        }

        private void changeVolume(string _audioSource, int _volume)
        {
            var audioSource = rootUI.transform.Find("AudioSources/" + _audioSource).GetComponent<AudioSource>();
            audioSource.volume = _volume / 100.0f;
        }

        private void renderImage3D(string _file, string _renderer)
        {
            contentObjectsPool.LoadTexture(_file, "image3D", (_texture) =>
            {
                cloneSkybox_.mainTexture = _texture;
            });
        }

        /// <summary>
        /// 为摄像机添加手势操作
        /// </summary>
        /// <param name="_camera"></param>
        private void wrapGestureCamera(Transform _camera)
        {
            var camera = _camera.GetComponent<Camera>();
            // 水平滑动
            var swipeH = _camera.gameObject.AddComponent<HedgehogTeam.EasyTouch.QuickSwipe>();
            swipeH.swipeDirection = HedgehogTeam.EasyTouch.QuickSwipe.SwipeDirection.Horizontal;
            swipeH.onSwipeAction = new HedgehogTeam.EasyTouch.QuickSwipe.OnSwipeAction();
            swipeH.onSwipeAction.AddListener((_gesture) =>
            {
                // 忽略摄像机视窗外
                if (_gesture.position.x < camera.pixelRect.x ||
                _gesture.position.x > camera.pixelRect.x + camera.pixelRect.width ||
                _gesture.position.y < camera.pixelRect.y ||
                _gesture.position.y > camera.pixelRect.y + camera.pixelRect.height)
                    return;
                var vec = _camera.localRotation.eulerAngles;
                vec.y = vec.y + _gesture.swipeVector.x;
                _camera.localRotation = Quaternion.Euler(vec.x, vec.y, vec.z);
            });
            // 垂直滑动
            var swipeV = _camera.gameObject.AddComponent<HedgehogTeam.EasyTouch.QuickSwipe>();
            swipeV.swipeDirection = HedgehogTeam.EasyTouch.QuickSwipe.SwipeDirection.Vertical;
            swipeV.onSwipeAction = new HedgehogTeam.EasyTouch.QuickSwipe.OnSwipeAction();
            swipeV.onSwipeAction.AddListener((_gesture) =>
            {
                // 忽略摄像机视窗外
                if (_gesture.position.x < camera.pixelRect.x ||
                _gesture.position.x > camera.pixelRect.x + camera.pixelRect.width ||
                _gesture.position.y < camera.pixelRect.y ||
                _gesture.position.y > camera.pixelRect.y + camera.pixelRect.height)
                    return;
                var vec = _camera.localRotation.eulerAngles;
                vec.x = vec.x - _gesture.swipeVector.y;
                // 限制仰俯角
                if (vec.x > 70 && vec.x < 180)
                    vec.x = 70;
                if (vec.x < 290 && vec.x > 180)
                    vec.x = 290;
                _camera.rotation = Quaternion.Euler(vec.x, vec.y, vec.z);
            });
            // 捏合
            /*
            var pinch = _camera.gameObject.AddComponent<HedgehogTeam.EasyTouch.QuickPinch>();
            pinch.onPinchAction.AddListener((_gesture) =>
            {
                // 忽略摄像机视窗外
                if (_gesture.position.x < camera.pixelRect.x ||
                _gesture.position.x > camera.pixelRect.x + camera.pixelRect.width ||
                _gesture.position.y < camera.pixelRect.y ||
                _gesture.position.y > camera.pixelRect.y + camera.pixelRect.height)
                    return;
                _camera.GetComponent<Camera>().fieldOfView *= _gesture.deltaPinch;
            });
            */
        }
    }
}
