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
        AblyRealtime ably;
        public Text TextContent;
        void Start()
        {
            AddComponents();
            InitializeAbly();
        }

        // Add components 
        void AddComponents()
        {

        }

        void InitializeAbly()
        {

        }

        // void Connect()
        // {
        //     text = "";
        //     color = Color.yellow;
        //
        //     var options = new ClientOptions();
        //     options.Key = "";
        //     // this will disable the library trying to subscribe to network state notifications
        //     options.AutomaticNetworkStateMonitoring = false;
        //     // you can also turn on if you need to update the UI directly from channel subscription handlers
        //     options.CaptureCurrentSynchronizationContext = true;
        //
        //     ably = new AblyRealtime(options);
        //     ably.Connection.On(ConnectionEvent.Connected, args =>
        //     {
        //         Debug.Log("Connected to Ably!");
        //         color = Color.green;
        //         text = "Connected to Ably!";
        //     });
        //
        //     var channel = ably.Channels.Get("notifications");
        //     channel.Subscribe(message =>
        //     {
        //         Debug.Log("Received a greeting message in realtime: " + message.Data);
        //         color = Color.blue;
        //         text = "Received a greeting message in realtime: " + message.Data;
        //     });
        //
        // }


        public void OnConnectClick()
        {
            var options = new ClientOptions();
            options.Key = "";
            // this will disable the library trying to subscribe to network state notifications
            options.AutomaticNetworkStateMonitoring = false;
            // you can also turn on if you need to update the UI directly from channel subscription handlers
            options.CaptureCurrentSynchronizationContext = true;
            options.CustomContext = SynchronizationContext.Current;

            ably = new AblyRealtime(options);

            ably.Connection.On(ConnectionEvent.Connected, args =>
            {
                LogAndDisplay("Connected to ably");
            });
        }

        void LogAndDisplay(string message)
        {
            Debug.Log(message);
            TextContent.text = $"{TextContent.text}\n{message}";
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