// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.DotNet.Try.Protocol
{
    public class CompletionResult
    {
        public CompletionItem[] Items { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; }

        public IEnumerable<SerializableDiagnostic> Diagnostics { get; }

        public CompletionResult(CompletionItem[] items = null, IEnumerable<SerializableDiagnostic> diagnostics = null, string requestId = null)
        {
            Items = items ?? Array.Empty<CompletionItem>();
            Diagnostics = diagnostics;
            RequestId = requestId;
        }
    }
}
