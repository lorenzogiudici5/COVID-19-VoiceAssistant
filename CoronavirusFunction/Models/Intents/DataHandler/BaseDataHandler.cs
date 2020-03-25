using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using CoronavirusFunction.Helpers;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoronavirusFunction.Models
{
    public abstract class BaseDataHandler : BaseHandler
    {
        public BaseDataHandler(Conversation conversation) : base(conversation) { }

        #region GoogleAssistant
        protected DataRequest InitRequestData(MapField<string, Value> fields)
        {
            var location = this.ExtractLocation(fields);
            var date = this.ExtractDate(fields);

            return new DataRequest()
            {
                Location = location,
                Date = date
            };
        }
        protected Location ExtractLocation(MapField<string, Value> fields)
        {
            var locationParam = fields.ContainsKey("location") && fields["location"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                fields["location"].ToString() : 
                string.Empty;

            var locationDto = !string.IsNullOrEmpty(locationParam) ? 
                JsonConvert.DeserializeObject<DialogflowLocationDto>(locationParam) : 
                new DialogflowLocationDto() { Country = "Italia" };

            return locationDto.ToLocation();
        }
        protected DateTimeOffset ExtractDate(MapField<string, Value> fields)
        {
            var dateTimeStr = fields.ContainsKey("date") && fields["date"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                fields["date"].ToString().Replace('\"', ' ').Trim() :
                string.Empty;

            return !string.IsNullOrEmpty(dateTimeStr) ? DateTimeOffset.Parse(dateTimeStr) : DateTimeOffset.Now.Date;
        }

        protected virtual WebhookResponse BuildWebhookResponse(Location location, string speechMsg, string textMsg)
        {
            return new WebhookResponse
            {
                FulfillmentText = this.BuildSpeechResponse(location, speechMsg)
            };
        }
        #endregion


        #region Alexa
        protected DataRequest InitRequestData(Dictionary<string, Slot> slots)
        {
            var location = this.ExtractLocation(slots);
            var date = this.ExtractDate(slots);

            return new DataRequest()
            {
                Location = location,
                Date = date
            };
        }
        protected Location ExtractLocation(Dictionary<string, Slot> slots)
        {
            var country = slots["Country"].Value;
            var adminArea = slots["AdminArea"].Value;
            var city = slots["City"].Value;
            var locationDefinition = slots["LocationDefinition"].Resolution?.Authorities[0].Values[0].Value.Id;

            // TODO: Default value if empty (remove when other countries will be added)
            if (string.IsNullOrEmpty(adminArea) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                country = "Italia";

            // TODO: change for not italian request
            var subAdminArea = !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(locationDefinition) && (LocationDefinition) System.Enum.Parse(typeof(LocationDefinition), locationDefinition) == LocationDefinition.SubAdminArea ?
                $"{LocationDefinition.SubAdminArea.ToDescription()} di {city}" :
                string.Empty;

            return new Location() 
            { 
                Country = country, 
                AdminArea = adminArea, 
                City = city,
                SubadminArea = subAdminArea
            };
        }
        protected DateTimeOffset ExtractDate(Dictionary<string, Slot> slots)
        {
            var dateTimeStr = slots["Date"].Value;
            return !string.IsNullOrEmpty(dateTimeStr) ? DateTimeOffset.Parse(dateTimeStr) : DateTimeOffset.Now.Date;
        }

        protected virtual SkillResponse BuildAlexaResponse(Location location, string speechMsg, string textMsg)
        {
            var reprompt = new Reprompt("Vuoi sapere altri dati?");
            return !string.IsNullOrEmpty(textMsg) ?
                ResponseBuilder.AskWithCard(speechMsg, location.Description, textMsg, reprompt) :
                ResponseBuilder.Ask("Dati non disponibili", reprompt);
        }
        #endregion

        protected virtual string BuildSpeechResponse(Location location, string speechMsg)
        {
            var preposition = location.Definition == LocationDefinition.City ? "A" : "In";
            return !string.IsNullOrEmpty(speechMsg) ? $"{preposition} {location.Description} {speechMsg}" : "Dati non disponibili";
        }
    }
}
