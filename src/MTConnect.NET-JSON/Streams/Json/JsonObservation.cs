// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using MTConnect.Observations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MTConnect.Streams.Json
{
    public class JsonObservation
    {
        [JsonPropertyName("dataItemId")]
        public string DataItemId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Category { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("subType")]
        public string SubType { get; set; }

        [JsonPropertyName("compositionId")]
        public string CompositionId { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        [JsonPropertyName("resetTriggered")]
        public string ResetTriggered { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("samples")]
        public IEnumerable<string> Samples { get; set; }

        [JsonPropertyName("entries")]
        public IEnumerable<JsonEntry> Entries { get; set; }

        [JsonPropertyName("count")]
        public long? Count { get; set; }

        [JsonPropertyName("nativeCode")]
        public string NativeCode { get; set; }

        [JsonPropertyName("assetType")]
        public string AssetType { get; set; }


        public static IEnumerable<JsonEntry> CreateEntries(IEnumerable<IDataSetEntry> entries)
        {
            if (!entries.IsNullOrEmpty())
            {
                var jsonEntries = new List<JsonEntry>();
                foreach (var entry in entries)
                {
                    jsonEntries.Add(new JsonEntry(entry));
                }
                return jsonEntries;
            }

            return null;
        }

        public static IEnumerable<JsonEntry> CreateEntries(IEnumerable<ITableEntry> entries)
        {
            if (!entries.IsNullOrEmpty())
            {
                var jsonEntries = new List<JsonEntry>();
                foreach (var entry in entries)
                {
                    jsonEntries.Add(new JsonEntry(entry));
                }
                return jsonEntries;
            }

            return null;
        }

        public static IEnumerable<string> CreateTimeSeriesSamples(IEnumerable<double> samples)
        {
            if (!samples.IsNullOrEmpty())
            {
                var jsonResults = new List<string>();
                foreach (var sample in samples)
                {
                    jsonResults.Add(sample.ToString());
                }
                return jsonResults;
            }

            return null;
        }
    }
}
