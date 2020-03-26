using System;

namespace CoronavirusFunction.Models
{
    public class CardResponse
    {
        public CardResponse(string title, string description, string textToSpeech)
        {
            Title = title;
            Description = description;
            TextToSpeech = textToSpeech;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public string TextToSpeech { get; set; }
        public string DisplayText { get; set; }
        public Uri ImageUri { get; set; }
    }
}
