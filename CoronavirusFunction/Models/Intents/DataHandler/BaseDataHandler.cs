using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Alexa.NET.Request;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using CoronavirusFunction.Helpers;

namespace CoronavirusFunction.Models
{
    public abstract class BaseDataHandler : BaseHandler
    {
        public BaseDataHandler(Conversation conversation) : base(conversation) { }

        #region Protected Methods
        protected DataRequest InitRequestData(MapField<string, Value> fields)
        {
            var location = this.extractLocation(fields);
            var date = this.extractDate(fields);

            return new DataRequest()
            {
                Location = location,
                Date = date
            };
        }

        protected DataRequest InitRequestData(Dictionary<string, Slot> slots)
        {
            var location = this.extractLocation(slots);
            var date = this.extractDate(slots);

            return new DataRequest()
            {
                Location = location,
                Date = date
            };
        }

        protected CardResponse InitCardResponse(LocationData data, string textToSpeech, string displayText)
        {
            var speechMessage = buildSpeechResponse(data, textToSpeech);
            var cardResponse = new CardResponse(data?.Description, displayText, speechMessage);
            if(data != null)
            {
                cardResponse.Subtitle = !data.Date.HasValue ? DateTimeOffset.Now.Date.ToShortDateString() : data.Date.Value.Date.ToShortDateString();
                cardResponse.ImageUri = conversation.Source == Source.Dialogflow && data.FlagUri != null ? data.FlagUri : null;
            }

            return cardResponse;
        }

        #endregion

        #region Private Methods
        private Location extractLocation(MapField<string, Value> fields)
        {
            var locationParam = fields.ContainsKey("location") && fields["location"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                fields["location"].ToString() :
                string.Empty;

            var locationDto = !string.IsNullOrEmpty(locationParam) ?
                JsonConvert.DeserializeObject<DialogflowLocationDto>(locationParam) :
                new DialogflowLocationDto() { };

            return locationDto.ToLocation();
        }
        private DateTimeOffset extractDate(MapField<string, Value> fields)
        {
            var dateTimeStr = fields.ContainsKey("date") && fields["date"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                fields["date"].ToString().Replace('\"', ' ').Trim() :
                string.Empty;

            return !string.IsNullOrEmpty(dateTimeStr) ? DateTimeOffset.Parse(dateTimeStr) : DateTimeOffset.Now.Date;
        }
        private Location extractLocation(Dictionary<string, Slot> slots)
        {
            var country = slots["Country"].Value;
            var adminArea = slots["AdminArea"].Value;
            var city = slots["City"].Value;
            var locationDefinition = slots["LocationDefinition"].Resolution?.Authorities[0].Values[0].Value.Id;

            // TODO: change for not italian request
            var subAdminArea = !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(locationDefinition) && (LocationDefinition)System.Enum.Parse(typeof(LocationDefinition), locationDefinition) == LocationDefinition.SubAdminArea ?
                $"{LocationDefinition.SubAdminArea.ToDescription()} di {city}" :
                string.Empty;

            var locationDto = new DialogflowLocationDto()
            {
                Country = country,
                AdminArea = adminArea,
                SubadminArea = subAdminArea,
                City = city,
            };

            return locationDto.ToLocation();
        }
        private DateTimeOffset extractDate(Dictionary<string, Slot> slots)
        {
            var dateTimeStr = slots["Date"].Value;
            return !string.IsNullOrEmpty(dateTimeStr) ? DateTimeOffset.Parse(dateTimeStr) : DateTimeOffset.Now.Date;
        }
        private string buildSpeechResponse(LocationData location, string speechMsg)
        {
            var preposition = location == null ? default(string) : location is WorldData ? "Nel" : location is CityData ? "A" : "In";
            return location != null && !string.IsNullOrEmpty(speechMsg) ? 
                $"{preposition} {location.Description} {speechMsg}" : 
                "Dati non disponibili";
        }
        #endregion
    }
}
