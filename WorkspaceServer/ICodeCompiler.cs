// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Clockwise;
using Microsoft.DotNet.Try.Protocol;

namespace WorkspaceServer
{
    public interface ICodeCompiler
    {
        Task<CompileResult> Compile(WorkspaceRequest request, Budget budget = null);
    }
}