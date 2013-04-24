#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using School.Entities;
using School.Data;

#endregion

namespace School.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="StudentsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class StudentsProviderBase : StudentsProviderBaseCore
	{
	} // end class
} // end namespace
