using System;
using System.Collections.Generic;

namespace CoronavirusFunction.Models
{
    public class TableResponse
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Uri ImageUri { get; set; }
        public string TextToSpeech { get; set; }
        public string DisplayText { get; set; }

        public List<RowItem> Rows { get; set; }
        public List<ColumnProperty> Columns { get; set; }
    }

    public class RowItem
    {
        public List<string> Cells { get; set; }
    }

    public class ColumnProperty
    {
        public string Header { get; set; }
    }
}
