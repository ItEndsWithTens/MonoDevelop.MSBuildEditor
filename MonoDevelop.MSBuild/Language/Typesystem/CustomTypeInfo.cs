// Copyright (c) 2016 Xamarin Inc.
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace MonoDevelop.MSBuild.Language.Typesystem
{
	[DebuggerDisplay("CustomTypeInfo({Name},nq)")]
	public sealed class CustomTypeInfo
	{
		public CustomTypeInfo (
			IReadOnlyList<CustomTypeValue> values, string? name = null, DisplayText description = default, bool allowUnknownValues = false,
			MSBuildValueKind baseKind = MSBuildValueKind.Unknown, bool caseSensitive = false,
			ImmutableDictionary<string, object>? analyzerHints = null, string? helpUrl = null)
        {
			Values = values ?? throw new ArgumentNullException (nameof (values));
			Name = name;
			Description = description;
			AllowUnknownValues = allowUnknownValues;
			BaseKind = baseKind;
			CaseSensitive = caseSensitive;
			AnalyzerHints = analyzerHints ?? ImmutableDictionary<string, object>.Empty;
			HelpUrl = helpUrl;

			foreach (var v in values) {
				v.SetParent (this);
			}

			// note: SchemaLoadState is even more restrictive on base types right now, this is just to make absolutely sure we don't get stack overflows
			if (baseKind == MSBuildValueKind.CustomType && baseKind.HasModifiers ()) {
				throw new ArgumentException ("Custom types may only derive from intrinsic types without modifiers", nameof (baseKind));
			}
		}

		public bool IsAnonymous => Name is null || Name.Length == 0 || Name[0] == '#';

		public string? Name { get; }
		public DisplayText Description { get; }
		public bool AllowUnknownValues { get; }
		public IReadOnlyList<CustomTypeValue> Values { get; }
		public MSBuildValueKind BaseKind { get;}
		public bool CaseSensitive { get; }

		/// <summary>
		/// Custom annotations that may affect analyzers etc. e.g. GuidFormat="B"
		/// </summary>
		public ImmutableDictionary<string,object> AnalyzerHints { get; }

		/// <summary>
		/// Help URL to be used for all values of this type.
		/// </summary>
		public string? HelpUrl { get; }
	}
}