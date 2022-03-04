using System.Threading;
using Cysharp.Threading.Tasks;
using IO.Ably;
using IO.Ably.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Ably.Examples
{
    public class TestApp : MonoBehaviour
    {
        private AblyRealtime _ably;
        private Text _textContent;
        private Button _connectButton;
        private Text _connectBtnText;
        private static string _apiKey = "";


        void Start()
        {
            AddComponents();
            InitializeAbly();
        }

        // Add components 
        void AddComponents()
        {
            _textContent = GameObject.Find("TxtConsole").GetComponent<Text>();
            _connectBtnText = GameObject.Find("ConnectBtnText").GetComponent<Text>();
            _connectButton = GameObject.Find("ConnectBtn").GetComponent<Button>();
            _connectButton.onClick.AddListener(OnConnectClickHandler);
        }

        void InitializeAbly()
        {
            LogAndDisplay("On connect clicked");
            var options = new ClientOptions();
            options.Key = _apiKey;
            // this will disable the library trying to subscribe to network state notifications
            options.AutomaticNetworkStateMonitoring = false;

            options.CaptureCurrentSynchronizationContext = true;
            options.AutoConnect = false;
            options.CustomContext = SynchronizationContext.Current;

            _ably = new AblyRealtime(options);
        }


        void OnConnectClickHandler()
        {
            _ably.Connection.On( args =>
            {
                LogAndDisplay($"Connection State is {args.Current}");
                _connectButton.GetComponentInChildren<Text>().text = args.Current.ToString();
                if (args.Current == ConnectionState.Connected)
                {
                    _connectButton.GetComponent<Image>().color = Color.green;
                }
            });

            _ably.Connect();
        }

        void LogAndDisplay(string message)
        {
            Debug.Log(message);
            _textContent.text = $"{_textContent.text}\n{message}";
        }

        void Update()
        {
            // errorText.text = text;
            // image.color = color;
        
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Clicked left arrow");
                LogAndDisplay("Hi there");
            }
        
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     var channel = ably.Channels.Get("notifications");
            //     channel.Publish("greeting", "hello!");
            // }
        }
    }
}