using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSPDeck.Models
{
    public class CameraFeed
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? IPAddress { get; set; }
        public int Port { get; set; }
        public int CameraNumber { get; set; }

        public string RTSPUrl => $"rtsp://{Username}:{Password}@{IPAddress}:{Port}/Streaming/channels/{CameraNumber}01";
    }
}
