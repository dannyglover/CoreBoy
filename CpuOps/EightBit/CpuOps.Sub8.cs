﻿//  Author:
//       Revolt64 <revolt64@outlook.com>
//
//  Copyright (c) 2020 Revolt64
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//

using System;

namespace CoreBoy.CpuOps
{
	using u8 = Byte;
	using u16 = UInt16;

	public partial class CpuOps
	{
		public u8 Sub8(u8 inVal, u8 val, int cycles)
		{
			u8 result = (u8)(inVal - val);

			_gameboy.Flags.Clear(Flags.Z | Flags.H | Flags.C);
			_gameboy.Flags.Set(Flags.N);

			if (result == 0) _gameboy.Flags.Set(Flags.Z);
			if ((inVal & 0xF) < (val & 0xF)) _gameboy.Flags.Set(Flags.H);
			if (inVal < val) _gameboy.Flags.Set(Flags.C);

			_gameboy.Cpu.Cycles += cycles;
			return result;
		}

		public u8 Sbc8(u8 inVal, u8 sub, int cycles)
		{
			u8 carry = _gameboy.Flags.Get(Flags.C);
			u8 result = (u8)(inVal - carry - sub);

			_gameboy.Flags.Clear(Flags.Z | Flags.H | Flags.C);
			_gameboy.Flags.Set(Flags.N);

			if (result == 0) _gameboy.Flags.Set(Flags.Z);
			if ((inVal & 0xF) < (carry & 0xF)) _gameboy.Flags.Set(Flags.H);
			if (((inVal - carry) & 0xF) < (sub & 0xF)) _gameboy.Flags.Set(Flags.H);
			if (inVal < carry) _gameboy.Flags.Set(Flags.C);
			if ((inVal - carry) < sub) _gameboy.Flags.Set(Flags.C);

			_gameboy.Cpu.Cycles += cycles;
			return result;
		}
	}
}
