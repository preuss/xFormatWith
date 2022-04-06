using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFormatWith;
using System.Globalization;
using System.Threading;

namespace xFormatWithTests {
	public class FormatWithTests {
		[Fact]
		public void FormatWithSourceNull() {
			const string expectedOutput = "Output";
			const string expectedError = "";
			const string rawFormat = "Output";
			IFormatter formatter = Formatter.CreateFormatter();

			object? replacementObject = null;
#pragma warning disable CS8604 // Possible null reference argument.
			_ = Assert.Throws<ArgumentNullException>(() => formatter.FormatWith(rawFormat, replacementObject));
#pragma warning restore CS8604 // Possible null reference argument.
		}
		[Fact]
		[UseCulture("en-US")]
		public void TestFormatIndexed() {
			String actualOutput;

			CultureInfo originalCulureInfo = CultureInfo.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
				const string rawFormat = "First: {0}st, FiveAndAHalf: {1}";
				IFormatter formatter = Formatter.CreateFormatter();

				actualOutput = formatter.FormatIndexed(rawFormat, 1, 5.5);
			} finally {
				Thread.CurrentThread.CurrentUICulture = originalCulureInfo;
			}

			const string expectedOutput = "First: 1st, FiveAndAHalf: 5.5";
			Assert.Equal(expectedOutput, actualOutput);
		}
		[Fact]
		[UseCulture("en-US")]
		public void TestFormatIndexedWithProvider() {
			string format = "First: {0}st, FiveAndAHalf: {1}";
			IFormatter formatter = Formatter.CreateFormatter();

			IFormatProvider provider = NumberFormatInfo.GetInstance(CultureInfo.GetCultureInfo("da-DK"));
			String actualOutput = formatter.FormatIndexed(format, provider, 1, 5.5);

			const string expectedOutput = "First: 1st, FiveAndAHalf: 5,5";
			Assert.Equal(expectedOutput, actualOutput);
		}
	}
}
