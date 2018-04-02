using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceLiveShow.Models
{
    public class CustomFaceModel
    {
        public float Anger { get; set; }
        public float Contempt { get; set; }
        public float Disgust { get; set; }
        public float Fear { get; set; }
        public float Happiness { get; set; }
        public float Neutral { get; set; }
        public float Sadness { get; set; }
        public float Suprise { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public class CustomFaceEmojiModel
    {
        public float EmojiSum { get; set; }
        public float UpperLeft { get; set; }
        public float ButtomLeft { get; set; }
        public float UpperRight { get; set; }
        public float ButtoRight { get; set; }
    }
    public class EmojiNum
    {
        public float Emoji { get; set; }
    }

}
