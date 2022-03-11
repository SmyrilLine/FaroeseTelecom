/*
Copyright 2022 Smyrilline

Use of this source code is governed by an MIT-style
license that can be found in the LICENSE file or at
https://opensource.org/licenses/MIT.

Author: Tummas Andreasen
*/
using System;

namespace FaroeseTelecom
{
    [Flags]
	public enum NumberType
	{
		Emergency = 0,
		Service = 1,
		IP = 2,
		SharedCost = 4,
		Free = 8,
		Fixed = 16,
		GSM = 32,
		_3G = 64
	}
}
