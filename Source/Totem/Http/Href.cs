﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Totem.IO;

namespace Totem.Http
{
	/// <summary>
	/// An absolute or relative reference to an HTTP resource
	/// </summary>
	[TypeConverter(typeof(Converter))]
	public abstract class Href : LinkPart
	{
		public bool IsRoot
		{
			get
			{
				var resource = this as HttpResource;

				return resource != null && resource == HttpResource.Root;
			}
		}

		public static Href From(string value, bool strict = true)
		{
			var href = HttpLink.From(value, strict: false) ?? HttpResource.From(value, strict: false) as Href;

			ExpectNot(strict && href == null, "Failed to parse href: " + value);

			return href;
		}

		public sealed class Converter : TextConverter
		{
			protected override object ConvertFrom(TextValue value)
			{
				return From(value);
			}
		}
	}
}