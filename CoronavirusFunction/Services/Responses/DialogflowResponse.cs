using CoronavirusFunction.Models;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.Collections;
using System.Linq;
using static Google.Cloud.Dialogflow.V2.Intent.Types.Message.Types;

namespace CoronavirusFunction.Services
{
    public static class DialogflowResponse
    {
        public static WebhookResponse BuildSimpleResponse(string fulfillmentText)
        {
            return new WebhookResponse()
            {
                FulfillmentText = fulfillmentText
            };
        }
        public static WebhookResponse BuildSimpleResponse(string textToSpeech, string displayText)
        {
            var simpleResponse = new SimpleResponse()
            {
                TextToSpeech = textToSpeech,
                DisplayText = displayText
            };
            var simpleResponseMsg = buildSimpleResponseMsg(simpleResponse);

            return new WebhookResponse()
            {
                FulfillmentMessages = { simpleResponseMsg }
            };
        }
        public static WebhookResponse BuildEndResponse(string exitText) => BuildSimpleResponse(exitText);

        public static WebhookResponse BuildCardResponse(CardResponse card, string displayText = "Ecco i dati")
        {
            var basicCard = new BasicCard()
            {
                Title = card.Title,
                FormattedText = card.Description
            };

            basicCard.Subtitle = !string.IsNullOrEmpty(card.Subtitle) ? card.Subtitle : string.Empty;
            basicCard.Image = card.ImageUri != null ? new Image() { ImageUri = card.ImageUri.ToString(), AccessibilityText = card.ImageUri.Segments.LastOrDefault()} : null;

            var simpleResponse = new SimpleResponse() { TextToSpeech = card.TextToSpeech, DisplayText = displayText };
            var simpleResponseMessage = buildSimpleResponseMsg(simpleResponse);

            var cardMessage = new Intent.Types.Message() { Platform = Platform.ActionsOnGoogle };
            cardMessage.BasicCard = basicCard;

            return new WebhookResponse
            {
                FulfillmentMessages = { simpleResponseMessage, cardMessage },
                FulfillmentText = displayText,
            };
        }
        public static WebhookResponse BuildTable(TableResponse table, string textToSpeech, string displayText = "Ecco i dati")
        {
            var rows = new RepeatedField<TableCardRow>();
            foreach(var row in table.Rows)
            {
                var tableCardRow = new TableCardRow();
                foreach (var cell in row.Cells)
                    tableCardRow.Cells.Add(new TableCardCell() { Text = cell });
                rows.Add(tableCardRow);
            }

            var tableCard = new TableCard();
            tableCard.Title = !string.IsNullOrEmpty(table.Title) ? table.Title : string.Empty;
            tableCard.Subtitle = !string.IsNullOrEmpty(table.Subtitle) ? table.Subtitle : string.Empty;
            tableCard.Image = table.ImageUri != null ? new Image() { ImageUri = table.ImageUri.ToString(), AccessibilityText = table.ImageUri.Segments.LastOrDefault() } : null;

            foreach (var column in table.Columns)
                tableCard.ColumnProperties.Add(new ColumnProperties() { Header = column.Header });

            foreach (var row in rows)
                tableCard.Rows.Add(row);

            var simpleResponse = new SimpleResponse() { TextToSpeech = table.TextToSpeech, DisplayText = displayText };
            var simpleResponseMessage = buildSimpleResponseMsg(simpleResponse);

            var tableMessage = new Intent.Types.Message
            {
                TableCard = tableCard,
                Platform = Platform.ActionsOnGoogle
            };

            return new WebhookResponse
            {
                FulfillmentMessages = { simpleResponseMessage, tableMessage },
                FulfillmentText = displayText,
            };
        }

        public static Intent.Types.Message BuildSuggestionChips(params string[] suggestions)
        {
            var message = new Intent.Types.Message() { Platform = Platform.ActionsOnGoogle };
            message.Suggestions = new Suggestions();
            for (int i = 0; i< 9 || i<suggestions.Length; i++)                                      // MAX 8
                message.Suggestions.Suggestions_.Add(new Suggestion() { Title = suggestions[i]});

            return message;
        }

        private static Intent.Types.Message buildSimpleResponseMsg(params SimpleResponse[] simpleResponses)
        {
            var message = new Intent.Types.Message() { Platform = Platform.ActionsOnGoogle };
            message.SimpleResponses = new SimpleResponses();
            foreach (var response in simpleResponses)
            {
                var simpleResponse = new SimpleResponse() { TextToSpeech = response.TextToSpeech, DisplayText = response.DisplayText };
                message.SimpleResponses.SimpleResponses_.Add(simpleResponse);
            }

            return message;
        }
    }
}
