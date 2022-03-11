/*
Copyright 2022 Smyrilline

Use of this source code is governed by an MIT-style
license that can be found in the LICENSE file or at
https://opensource.org/licenses/MIT.

Author: Tummas Andreasen
*/
namespace FaroeseTelecom
{
    public class CountryCode
	{
		private string _Country { get; set; }
		public string Country { get => _Country; set { if (!_Locked) { _Country = value; } } }

		private string _CountryShort;
		public string CountryShort { get => _CountryShort; set { if (!_Locked) { _CountryShort = value; } } }

		private string _Prefix;
		public string Prefix { get => _Prefix; set { if (!_Locked) { _Prefix = value; } } }

		private int _PrefixValue;
		public int PrefixValue { get => _PrefixValue; set { if (!_Locked) { _PrefixValue = value; } } }

		public int _NationalMaxLength;
		public int NationalMaxLength { get => _NationalMaxLength; set { if (!_Locked) { _NationalMaxLength = value; } } }

		public int _NationalMinLength;
		public int NationalMinLength { get => _NationalMinLength; set { if (!_Locked) { _NationalMinLength = value; } } }


		private bool _Locked = false;
		public bool Locked
		{
			get => _Locked;
			set
			{
				if (!_Locked)
					_Locked = value;
			}
		}

		public CountryCode() { }

		public CountryCode(string Country, string CountryShort, string Prefix, int PrefixValue, int NationalLengthMin, int NationalLengthMax, bool Locked = true)
		{
			this._Country = Country;
			this._CountryShort = CountryShort;
			this._Prefix = Prefix;
			this._PrefixValue = PrefixValue;
			this._NationalMinLength = NationalLengthMin;
			this._NationalMaxLength = NationalLengthMax;
			this._Locked = Locked;
		}

		public CountryCode Copy(bool LockCopy = false)
		{
			return new CountryCode()
			{
				Country = this.Country,
				CountryShort = this.CountryShort,
				Prefix = this.Prefix,
				PrefixValue = this.PrefixValue,
				Locked = LockCopy
			};
		}

		public bool LengthValid(int length)
		{
			bool Valid = length >= _NationalMinLength && length <= _NationalMaxLength;
			return Valid;
		}
	}
}
