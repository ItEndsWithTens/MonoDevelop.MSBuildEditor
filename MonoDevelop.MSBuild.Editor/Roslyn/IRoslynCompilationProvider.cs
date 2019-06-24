// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis;

namespace MonoDevelop.MSBuild.Editor.Roslyn
{
	public interface IRoslynCompilationProvider
	{
		MetadataReference CreateReference (string assemblyPath);
	}
}
