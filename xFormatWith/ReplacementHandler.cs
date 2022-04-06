using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFormatWith {
	public delegate TResult ReplacementHandler<in TPropertyKey, out TResult>(TPropertyKey propertyKey);
	public delegate TResult ReplacementHandler<in TPropertyKey, in TFormatParameter, out TResult>(TPropertyKey propertyKey, TFormatParameter formatParameter);
}
