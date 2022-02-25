using IO.Ably;
using IO.Ably.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Ably.Examples
{
    public class AblyConnection : MonoBehaviour
    {
        AblyRealtime ably;
        public Text errorText;
        public Image image;

        private Color color;
        private string text;

        void Start()
        {
            image.color = Color.cyan;
        }

        void Connect()
        {
            text = "";
            color = Color.yellow;

            var options = new ClientOptions();
            options.Key = "";
            // this will disable the library trying to subscribe to network state notifications
            options.AutomaticNetworkStateMonitoring = false;
            // you can also turn on if you need to update the UI directly from channel subscription handlers
            options.CaptureCurrentSynchronizationContext = true;

            ably = new AblyRealtime(options);
            ably.Connection.On(ConnectionEvent.Connected, args =>
            {
                Debug.Log("Connected to Ably!");
                color = Color.green;
                text = "Connected to Ably!";
            });

            var channel = ably.Channels.Get("notifications");
            channel.Subscribe(message =>
            {
                Debug.Log("Received a greeting message in realtime: " + message.Data);
                color = Color.blue;
                text = "Received a greeting message in realtime: " + message.Data;
            });

        }

        void Update()
        {
            errorText.text = text;
            image.color = color;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Connect();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var channel = ably.Channels.Get("notifications");
                channel.Publish("greeting", "hello!");
            }
        }
    }
}