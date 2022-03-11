/*
Copyright 2022 Smyrilline

Use of this source code is governed by an MIT-style
license that can be found in the LICENSE file or at
https://opensource.org/licenses/MIT.

Author: Tummas Andreasen
*/
namespace FaroeseTelecom
{
    public class PhoneRange
	{
		private int _Start;
		public int Start { get => _Start; set { if (!_Locked) { _Start = value; } } }

		private int _End;
		public int End { get => _End; set { if (!_Locked) { _End = value; } } }

		private bool _Allowed = false;
		public bool Allowed { get => _Allowed; set { if (!_Locked) { _Allowed = value; } } }

		private NumberType _Type;
		public NumberType Type { get => _Type; set { if (!_Locked) { _Type = value; } } }

		private string _Description;
		public string Description { get => _Description; set { if (!_Locked) { _Description = value; } } }


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

		public PhoneRange() { }

		public PhoneRange(int Start, int End, bool Allowed, NumberType Type, string Description, bool Locked = true)
		{
			this._Start = Start;
			this._End = End;
			this._Allowed = Allowed;
			this._Type = Type;
			this._Description = Description;
			this._Locked = Locked;
		}

		public PhoneRange Copy(bool LockCopy = false)
		{
			return new PhoneRange()
			{
				Start = this._Start,
				End = this._End,
				Allowed = this._Allowed,
				Type = this._Type,
				Description = this._Description,
				Locked = LockCopy
			};
		}
	}
}
