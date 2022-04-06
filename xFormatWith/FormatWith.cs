using System;
using System.Collections.Generic;

/*
 * http://james.newtonking.com/archive/2008/03/29/formatwith-2-0-string-formatting-with-named-variables
 * http://james.newtonking.com/archive/2008/03/27/formatwith-string-format-extension-method
*/
namespace xFormatWith {
	public interface IFormatter {
		/// <summary>
		///		This will use properties to read the source <paramref name="replacementObject"/>
		/// </summary>
		/// <example>
		/// FormatWith("Person name: {Name}", new {Name: "Robert"});
		/// </example>
		/// <param name="rawStringFormat">A composite format string, containing names.</param>
		/// <param name="replacementObject">A source object</param>
		/// <returns>
		/// A copy of format in which the format items have been replaced by the string representation 
		/// of the corresponding objects in args.
		/// </returns>
		string FormatWith(string rawStringFormat, object replacementObject);
		string FormatWith(string rawStringFormat, KeyValuePair<string, string> replacementLookupTable);
		string FormatWith(string rawStringFormat, ReplacementHandler<string, ReplacementResult<object>> replacementHandler);
		string FormatWith(string rawStringFormat, ReplacementHandler<string, string, ReplacementResult<object>> replacementHandler);
		string FormatWith(string rawStringFormat, ValueType replacementValue);
		string FormatWith(string rawStringFormat, IReadOnlyDictionary<string, object> replacementDictionary);
		/// <summary>
		/// Replaces the format items in a string with the string representations of corresponding
		/// objects in a specified array.A parameter supplies culture-specific formatting
		/// information.
		/// </summary>
		/// <param name="rawStringFormat">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>
		/// A copy of format in which the format items have been replaced by the string representation 
		/// of the corresponding objects in args.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">format or args is null.</exception>
		/// <exception cref="System.FormatException">
		/// format is invalid. -or- The index of a format item is less than zero, 
		/// or greater than or equal to the length of the args array.
		/// </exception>
		string FormatIndexed(string rawStringFormat, params object[] args);
		/// <summary>
		/// Replaces the format items in a string with the string representations of corresponding
		/// objects in a specified array.A parameter supplies culture-specific formatting
		/// information.
		/// </summary>
		/// <param name="rawStringFormat">A composite format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>
		/// A copy of format in which the format items have been replaced by the string representation 
		/// of the corresponding objects in args.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">format or args is null.</exception>
		/// <exception cref="System.FormatException">
		/// format is invalid. -or- The index of a format item is less than zero, 
		/// or greater than or equal to the length of the args array.
		/// </exception>
		string FormatIndexed(string rawStringFormat, IFormatProvider? provider, params object[] args);
	}
	public abstract class AbstractFormatter : IFormatter {
		public abstract string FormatWith(string rawStringFormat, object replacementObject);
		public abstract string FormatWith(string rawStringFormat, KeyValuePair<string, string> replacementLookupTable);
		public abstract string FormatWith(string rawStringFormat, ReplacementHandler<string, ReplacementResult<object>> replacementHandler);
		public abstract string FormatWith(string rawStringFormat, ReplacementHandler<string, string, ReplacementResult<object>> replacementHandler);
		public abstract string FormatWith(string rawStringFormat, ValueType replacementValue);
		public abstract string FormatWith(string rawStringFormat, IReadOnlyDictionary<string, object> replacementDictionary);
		public string FormatIndexed(string rawStringFormat, params object[] args) {
			if(rawStringFormat == null) throw new ArgumentNullException(nameof(rawStringFormat));
			if(args == null) throw new ArgumentNullException(nameof(args));
			return string.Format(rawStringFormat, args);
		}
		public string FormatIndexed(string rawStringFormat, IFormatProvider? provider, params object[] args) {
			if(rawStringFormat == null) throw new ArgumentNullException(nameof(rawStringFormat));
			if(args == null) throw new ArgumentNullException(nameof(args));
			return string.Format(provider, rawStringFormat, args);
		}
	}

	public sealed class Formatter : AbstractFormatter {
		private Formatter() { }

		public static IFormatter CreateFormatter() {
			return new Formatter();
		}

		public override string FormatWith(string rawStringFormat, object replacementObject) {
			if(rawStringFormat == null) throw new ArgumentNullException(nameof(rawStringFormat));
			if(replacementObject == null) throw new ArgumentNullException(nameof(replacementObject));

			throw new NotImplementedException();
		}

		public override string FormatWith(string rawStringFormat, KeyValuePair<string, string> replacementLookupTable) {
			throw new NotImplementedException();
		}

		public override string FormatWith(string rawStringFormat, ReplacementHandler<string, ReplacementResult<object>> replacementHandler) {
			throw new NotImplementedException();
		}

		public override string FormatWith(string rawStringFormat, ReplacementHandler<string, string, ReplacementResult<object>> replacementHandler) {
			throw new NotImplementedException();
		}

		public override string FormatWith(string rawStringFormat, ValueType replacementValue) {
			throw new NotImplementedException();
		}

		public override string FormatWith(string rawStringFormat, IReadOnlyDictionary<string, object> replacementDictionary) {
			throw new NotImplementedException();
		}
	}

	class FormatterOption {
		public static char OpenBraceValue => '{';
		public static char CloseBraceChar => '}';
		public static MissingKeyBehaviour MissingKeyBehaviour => MissingKeyBehaviour.Ignore;
		public static object FallbackReplacementValue => "asdf";
	}

	class NamesStringTokenizer {
		public NamesStringTokenizer() {
		}

		public static string Parse(string rawFormatString) {
			NamesStringTokenizer namesStringTokenizer = new NamesStringTokenizer();
			return nameStringTokenizer.Parse(rawFormatString.AsSpan());
		}

		string Parse(ReadOnlySpan<char> rawFormatSpan) {
			char openTokenChar = FormatterOption.OpenBraceValue;
			char closeTokenChar = FormatterOption.CloseBraceChar;
			MissingKeyBehaviour missingKeyBehaviour = FormatterOption.MissingKeyBehaviour;
			object fallbackReplacementValue = FormatterOption.FallbackReplacementValue;

			Tokenize(rawFormatSpan, replacementopenTokenChar, closeTokenChar, missingKeyBehaviour, fallbackReplacementValue);

			throw new NotImplementedException();
		}

		private void Tokenize(ReadOnlySpan<char> rawFormatSpan, Object replacementObject, IFormatProvider formatProvider, char openTokenChar, char closeTokenChar, MissingKeyBehaviour missingKeyBehaviour, object fallbackReplacementValue) {
			if(rawFormatSpan == null) throw new ArgumentNullException(nameof(rawFormatSpan));

			int pos = 0;
			int len = rawFormatSpan.Length;
			char ch = '\x0';

			ICustomFormatter? customFormatter = null;
			if(formatProvider != null) {
				customFormatter = (ICustomFormatter?)formatProvider.GetFormat(typeof(ICustomFormatter));
			}

			while(pos < len) {
				ch = rawFormatSpan[pos];
				pos++;

				if(ch == openTokenChar) {
				}
				if(ch == closeTokenChar) {
					
				}
			}
		}
		public static string Parse(string rawFormatString, char delimiter) {
			throw new NotImplementedException();
		}

		public static string Parse(char delimiter) {
			throw new NotImplementedException();
		}

		private string[] Tokenize() {
			throw new NotImplementedException();
		}

		private string ProcessToken() {
			throw new NotImplementedException();
		}
	}
}
